using System;
using System.Net;

namespace Microservices.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public HttpStatusCode? Status { get; set; }

        public DomainException()
        {
        }
        public DomainException(HttpStatusCode status, string message)
            : base(message)
        {
            Status = status;
        }
        public DomainException(HttpStatusCode status) => Status = status;
    }
}
