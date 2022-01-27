using System.Runtime.Serialization;
using DocumentStore.Core.Interfaces;

namespace DocumentStore.Core.Requests
{
    [DataContract]
    public class UpdateDocumentCommand<T> : Request where T : IEntity
    {
        [DataMember] public T Document { get; set; }
    }
}