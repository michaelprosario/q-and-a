using System;
using System.Collections.Generic;
using DocumentStore.Core.Responses;
using QA.Core.Entities;
using System.Runtime.Serialization;

namespace QA.Core
{
    [DataContract]
    public class LoginResponse : AppResponse
    {
        [DataMember]
        public bool IsAuthSuccessful { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public UserInformation UserInformation { get; set; }
    }
}