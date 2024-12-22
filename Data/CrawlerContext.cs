using Microsoft.EntityFrameworkCore;
using WebCrawlerProxyList.Models;

namespace WebCrawlerProxyList.Data
{
    public class CrawlerContext : DbContext
    {

        public CrawlerContext(DbContextOptions<CrawlerContext> options) : base(options)
        {
        }

        public DbSet<CrawlerExecution> CrawlerExecutions { get; set; }


    }
}
