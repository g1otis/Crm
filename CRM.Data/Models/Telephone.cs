using System;
using System.ComponentModel.DataAnnotations;
using CRM.Data.Common;
using CRM.Data.Enums;
using static CRM.Data.Constants.TelephoneConstants;

namespace CRM.Data.Models
{
    public class Telephone : ModelBase<Guid>
    {
        private string extension;

        public TelephoneType Type { get; set; }

        [Required, MinLength(ExtensionMinSize), MaxLength(ExtensionMaxSize)]
        public string Extension { get => extension; set => extension = value ?? throw new ArgumentNullException(nameof(Extension)); }

        [Required, MinLength(PhoneMinSize), MaxLength(PhoneMaxSize)]
        public int Phone { get; set; }

        #region Relations
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;
        #endregion
    }
}
