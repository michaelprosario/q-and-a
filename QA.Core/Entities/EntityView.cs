using DocumentStore.Core.Interfaces;
using System;
using System.Runtime.Serialization;

namespace QA.Core.Entities
{
    [DataContract]
    public class EntityView : IEntity
    {                        
        [DataMember] public DateTime CreatedAt { get; set; }
        [DataMember] public string CreatedBy { get; set; }
        [DataMember] public DateTime? DeletedAt { get; set; }
        [DataMember] public string DeletedBy { get; set; }
        [DataMember] public string Id { get; set; }
        [DataMember] public string ParentEntityType { get; set; }
        [DataMember] public string ParentEntityId { get; set; }
        [DataMember] public bool IsDeleted { get; set; }
        [DataMember] public DateTime? UpdatedAt { get; set; }
        [DataMember] public string UpdatedBy { get; set; }
    }
}