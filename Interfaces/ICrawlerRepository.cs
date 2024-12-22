using WebCrawlerProxyList.Models;

namespace WebCrawlerProxyList.Interfaces
{
    public interface ICrawlerRepository
    {
        Task SaveExecution(CrawlerExecution execution);  // Método para salvar execução
    }
}
