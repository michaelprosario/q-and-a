namespace DocumentStore.Core.Requests
{
    public class GetDocumentsQuery : Request
    {
        public int First { get; set; }
        public int Rows { get; set; } = 40;
        public int Page { get; set; } = 1;
        public string SortField { get; set; }
        public int? SortOrder { get; set; }
        public string Keyword { get; set; } = "";
        public string Tag { get; set; } = "";
    }
}