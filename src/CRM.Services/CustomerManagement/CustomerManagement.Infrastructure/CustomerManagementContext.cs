using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using CustomerManagement.Domain.SeedWork;
using CustomerManagement.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;

namespace CustomerManagement.Infrastructure
{
    public class CustomerManagementContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "CustomerManagement";

        private readonly IMediator? _mediator;
        private IDbContextTransaction? _currentTransaction;

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Gender> Genders { get; set; } = null!;
        public DbSet<AddressType> AddressTypes { get; set; } = null!;
        public DbSet<TelephoneType> TelephoneTypes { get; set; } = null!;

        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public CustomerManagementContext(DbContextOptions<CustomerManagementContext> options) : base(options)
        {
        }

        public CustomerManagementContext(DbContextOptions<CustomerManagementContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine("CustomerManagementContext::ctor ->" + this.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GenderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AddressTypeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TelephoneTypeEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await (_mediator?.DispatchDomainEventsAsync(this) ?? Task.CompletedTask);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction?> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return default;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }

    public class CustomerManagementContextDesignFactory : IDesignTimeDbContextFactory<CustomerManagementContext>
    {
        public CustomerManagementContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerManagementContext>()
                .UseSqlServer("Server=.;Initial Catalog=CRM.Services.CustomerManagementDb;Integrated Security=true");

            return new CustomerManagementContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult<TResponse>(default);
            }

            public Task<object?> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}
