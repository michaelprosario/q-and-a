using DocumentStore.Core.Interfaces;
using DocumentStore.Entities;
using System;
using System.Runtime.Serialization;

namespace QA.Core.Entities
{
    [DataContract]
    public class Post : BaseEntity,IEntity
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public string Content { get; set; }
        [DataMember] public string HtmlContent { get; set; }
        [DataMember] public string Tags { get; set; }
        [DataMember] public string PermaLink { get; set; }
        [DataMember] public string Status { get; set; }
        [DataMember] public DateTime CreatedAt { get; set; }
        [DataMember] public string CreatedBy { get; set; }
        [DataMember] public DateTime? DeletedAt { get; set; }
        [DataMember] public string DeletedBy { get; set; }
        [DataMember] public string Id { get; set; }
        [DataMember] public bool IsDeleted { get; set; }
        [DataMember] public DateTime? UpdatedAt { get; set; }
        [DataMember] public string UpdatedBy { get; set; }
        [DataMember] public string Abstract { get; set; }        
        [DataMember] public string FeaturedImage { get; set; }

    }
}