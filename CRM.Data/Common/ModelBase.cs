using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM.Data.Common
{
    public abstract class ModelBase<TId> where TId : notnull
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TId Id { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreationUtc { get => CreationUtc; set => CreationUtc = value ?? DateTime.UtcNow; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastUpdateUtc { get => LastUpdateUtc; set => LastUpdateUtc = value; }
    }
}
