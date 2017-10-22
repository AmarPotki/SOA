using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using FluentValidation.Results;

namespace RahyabServices.Common.Exceptions
{
    [DataContract]
    public class ValidationFault
    {
        public ValidationFault(IEnumerable<ValidationFailure> errors)
        {
            Message = BuildErrorMesage(errors);
        }

        [DataMember]
        public string Message { get; set; }

        private static string BuildErrorMesage(IEnumerable<ValidationFailure> errors)
        {
            return "خطا : " + string.Join("", errors.Select(x => "\r\n -- " + x.ErrorMessage).ToArray());
        }
    }
}
