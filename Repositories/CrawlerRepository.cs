using WebCrawlerProxyList.Data;
using WebCrawlerProxyList.Interfaces;
using WebCrawlerProxyList.Models;

namespace WebCrawlerProxyList.Repositories
{


    public class CrawlerRepository : ICrawlerRepository
    {
        private readonly CrawlerContext _context;

        public CrawlerRepository(CrawlerContext context)
        {
            _context = context;
        }

        public async Task SaveExecution(CrawlerExecution execution)
        {
            try
            {
                _context.CrawlerExecutions.Add(execution);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar a execução: {ex.Message}");
            }
        }
    }
}
