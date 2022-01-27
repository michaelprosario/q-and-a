using System.Runtime.Serialization;

namespace DocumentStore.Core.Responses
{
    [DataContract]
    public class NewRecordResponse : AppResponse
    {
        [DataMember] public string RecordId { get; set; }
    }
}