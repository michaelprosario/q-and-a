using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DocumentStore.Core.Responses
{
    [DataContract]
    public class GetDocumentsResponse<T> : AppResponse
    {
        public List<T> Documents { get; set; }
        public long TotalItemCount { get; set; }
        public long PageCount { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public long FirstItemOnPage { get; set; }
        public long LastItemOnPage { get; set; }
    }
}