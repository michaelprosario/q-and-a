using System.Runtime.Serialization;

namespace DocumentStore.Core.Requests
{
    [DataContract]
    public class GetDocumentQuery : Request
    {
        [DataMember] public string Id { get; set; } = "";
    }
}