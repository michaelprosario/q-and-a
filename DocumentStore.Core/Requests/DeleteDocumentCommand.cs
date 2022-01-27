using System.Runtime.Serialization;

namespace DocumentStore.Core.Requests
{
    [DataContract]
    public class DeleteDocumentCommand : Request
    {
        [DataMember] public string Id { get; set; } = "";
    }
}