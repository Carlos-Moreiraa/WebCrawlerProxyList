namespace WebCrawlerProxyList.Models
{
    public class CrawlerExecution
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PageCount { get; set; }
        public int RowCount { get; set; }
        public string JsonData { get; set; }
    }
}
