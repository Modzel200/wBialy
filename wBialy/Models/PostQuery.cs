namespace wBialy.Models
{
    public class PostQuery
    {
#pragma warning disable CS8618 
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
#pragma warning restore CS8618
    }
}
