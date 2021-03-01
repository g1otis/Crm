using System;
using System.Runtime.Serialization;

namespace CRM.BusinessLogic.Exceptions
{
    [Serializable]
    internal class CustomerUpdateException : Exception
    {
        private Guid id;

        public CustomerUpdateException()
        {
        }

        public CustomerUpdateException(Guid id)
        {
            this.id = id;
        }

        public CustomerUpdateException(string message) : base(message)
        {
        }

        public CustomerUpdateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerUpdateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}