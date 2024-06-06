namespace DataAccess.Pagination
{
    public class PagedResult<T> where T : class
    {
        public PagedResult()
        {
            Results = new List<T>();
        }
        public int PageSelected { get; set; }
        public List<T> Results { get; set; }
        public int NumberPage { get; set; }
        public string PRE_URL { get; set; } = null!;
        public string NEXT_URL { get; set; } = null!;

    }
}
