using System;

namespace RahyabServices.Common.Exceptions
{
    public class FaultException : ApplicationException
    {
        public FaultException(string message)
            : base(message)
        {

        }
    }
}
