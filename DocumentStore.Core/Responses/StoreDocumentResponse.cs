using System.Runtime.Serialization;

namespace DocumentStore.Core.Responses
{
    [DataContract]
    public class StoreDocumentResponse<T> : AppResponse
    {
        [DataMember] public T Document { get; set; }
    }
}