
using System.Runtime.Serialization;
using System.ComponentModel;

namespace QA.Core.Requests
{
    [DataContract]
    public class LoginCommand
    {
        [DataMember]
        public string Email { get; set; }
        
        [DataMember]
        public string Password { get; set; }
    }
}
