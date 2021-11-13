using System;
using System.Reflection;
using CustomerManagement.API.PipelineBehaviors;
using CustomerManagement.Application.Commands;
using CustomerManagement.Application.IntegrationEvents;
using CustomerManagement.Application.Queries;
using CustomerManagement.Domain.Aggregates.CustomerAggregate;
using CustomerManagement.Infrastructure;
using CustomerManagement.Infrastructure.Repositories;
using EventBus.Abstractions;
using EventBusStub;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace CustomerManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var databaseName = Guid.NewGuid().ToString();

            //services.AddDbContext<CustomerManagementContext>(options =>
            //{
            //    options.UseInMemoryDatabase(databaseName);
            //});
            services.AddDbContext<CustomerManagementContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CustomerManagementService"),
                    sqlOptions =>
                    {
                        //sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    });
            });

            services.AddScoped<IEventBus, EventBusStubImpl>();

            services.AddScoped<ICustomerQueries, CustomerQueries>();

            services.AddScoped<ICustomerIntegrationEventService, CustomerIntegrationEventService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            var applicationAssembly = typeof(CreateCustomerCommand).Assembly;
            services.AddMediatR(applicationAssembly);
            services.AddValidatorsFromAssembly(applicationAssembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddLogging(services => services.AddJsonConsole());

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CustomerManagement.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CustomerManagement.API v1"));
            }
            //app.UseAuthentication();
            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
