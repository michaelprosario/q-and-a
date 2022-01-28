using System.Runtime.Serialization;

namespace DocumentStore.Entities
{
    public abstract class BaseEntity
    {
        [DataMember] public string Id { get; set; }
    }
}