using System.Runtime.Serialization;

namespace DocumentStore.Core.Requests
{
    [DataContract]
    public class GetDocumentsByCollection : Request
    {
        [DataMember] public string Collection { get; set; } = "";
    }
}