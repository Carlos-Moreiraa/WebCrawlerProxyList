using WebCrawlerProxyList.Models;

namespace WebCrawlerProxyList.Interfaces
{
    public interface ICrawlerRepository
    {
        Task SaveExecution(CrawlerExecution execution);  // Chama método para salvar execução
    }
}
