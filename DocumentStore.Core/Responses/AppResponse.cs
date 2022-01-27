using System.Collections.Generic;
using System.Runtime.Serialization;
using DocumentStore.Core.Enums;
using FluentValidation.Results;

namespace DocumentStore.Core.Responses
{
    [DataContract]
    public class AppResponse
    {
        public AppResponse()
        {
            Code = ResponseCode.Success;
            Message = "";
            ValidationErrors = new List<ValidationFailure>();
        }

        [DataMember] public ResponseCode Code { get; set; }

        [DataMember] public string Message { get; set; }

        [DataMember] public IList<ValidationFailure> ValidationErrors { get; set; }
        [DataMember] public object AdditionalData { get; set; }

        public bool Ok()
        {
            return Code == ResponseCode.Success;
        }
    }
}