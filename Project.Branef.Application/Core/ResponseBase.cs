using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Project.Branef.Application.Core
{
    public class ResponseBase
    {
        public object Data { get; private set; }
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public ResponseBase(object data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }

    }
}
