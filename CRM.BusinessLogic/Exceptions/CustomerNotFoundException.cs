using System;
using System.Runtime.Serialization;

namespace CRM.BusinessLogic.Exceptions
{
    [Serializable]
    internal class CustomerNotFoundException : Exception
    {
        private Guid customerId;

        public CustomerNotFoundException()
        {
        }

        public CustomerNotFoundException(Guid customerId)
        {
            this.customerId = customerId;
        }

        public CustomerNotFoundException(string message) : base(message)
        {
        }

        public CustomerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}