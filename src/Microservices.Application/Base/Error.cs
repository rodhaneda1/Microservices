using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Application.Base
{
    public class Error
    {
        public string Status { get; set; }
        public string CodError { get; set; }
        public string DescriptionError { get; set; }
        public bool Ok { get; set; }

    }
}
