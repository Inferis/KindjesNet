using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inferis.API.Vimeo
{
    public class FailedVimeoCallException : Exception
    {
        public FailedVimeoCallException(string method)
            : base(string.Format("The call to '{0}' failed.", method))
        {
            Method = method;
        }

        public FailedVimeoCallException(string method, string code, string message) 
            : base(string.Format("The call to '{0}' failed with code '{1}': {2}", method, code, message))
        {
            Method = method;
            Code = code;
            Message = message;
        }

        public string Method { get; private set; }
        public string Code { get; private set; }
        public string Message { get; private set; }
    }
}
