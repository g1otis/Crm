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
        public DateTime? CreationUtc { get; set; } = DateTime.UtcNow;

        public DateTime? LastUpdateUtc { get; set; }
    }
}
