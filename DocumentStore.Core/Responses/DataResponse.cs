using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DocumentStore.Core.Responses
{
    [DataContract]
    public class DataResponse<T> : AppResponse
    {
        [DataMember] public T Data { get; set; }
    }
}