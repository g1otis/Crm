using System;
namespace CRM.UI.Services
{
    public class ServiceResult<TData>
    {
        public bool IsSucceeded { get; set; }
        public TData? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
