using System;

namespace DocumentStore.Core.Interfaces
{
    public interface IEntity
    {
        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; }
        DateTime? DeletedAt { get; set; }
        string DeletedBy { get; set; }
        string Id { get; set; }
        bool IsDeleted { get; set; }
        DateTime? UpdatedAt { get; set; }
        string UpdatedBy { get; set; }
    }
}