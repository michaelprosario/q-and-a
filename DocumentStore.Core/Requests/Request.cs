using System.Runtime.Serialization;

namespace DocumentStore.Core.Requests
{
    [DataContract]
    public class Request
    {
        [DataMember] public string UserId { get; set; }
    }
}