using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebCrawlerProxyList.Interfaces;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;




namespace WebCrawlerProxyList.Services
{
    public class WebCrawlerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICrawlerRepository _crawlerRepository;
        public event Action<List<Models.Proxy>> OnProxiesExtracted;
        private ChromeDriver _driver;
        int _totalPages = 0;

        public WebCrawlerService(IHttpClientFactory httpClientFactory, ICrawlerRepository crawlerRepository)
        {
            _httpClientFactory = httpClientFactory;
            _crawlerRepository = crawlerRepository;
        }

        //Método para iniciar as tarefas.
        public async Task StartCrawlWithConcurrency(string outputDirectory)
        {

            var startDate = DateTime.Now;
            var semaphore = new SemaphoreSlim(3);
            var tasks = new List<Task>();
            var baseUrl = "https://proxyservers.pro/proxy/list/order/updated/order_dir/desc/page/";
            _totalPages = await GetTotalPages(baseUrl);





            // Configuração do driver.
            new DriverManager().SetUpDriver(new ChromeConfig());
            var path = AppDomain.CurrentDomain.BaseDirectory.ToString();
            ChromeOptions options = new ChromeOptions();
            options.AddArgument($"user-data-dir={Path.Combine(path, "ChromeDriver\\Cache")}");
            options.AddArgument("--disable-gpu");
            _driver = new ChromeDriver(options);


            for (int page = 1; page <= _totalPages; page++)
            {
                var pageUrl = baseUrl + page;


                tasks.Add(Task.Run(async () =>
                {
                    await semaphore.WaitAsync();
                    try
                    {

                        await ProcessPageAsync(pageUrl, semaphore, startDate, outputDirectory, _driver);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao processar a página {page}. Detalhes: {ex.Message}",
                                        "Erro de Processamento",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            // Aguarda todas as tarefas terminarem.
            await Task.WhenAll(tasks);

            // Fecha o driver após todas as tarefas terminarem.
            _driver.Quit();
        }

        //Método para salvar no banco, criar JSON e salvar o HTML.
        private async Task ProcessPageAsync(string url, SemaphoreSlim semaphore, DateTime startDate, string outputDirectory, ChromeDriver driver)
        {
            
            await semaphore.WaitAsync();

            try
            {
                var client = _httpClientFactory.CreateClient();
                var html = await client.GetStringAsync(url);


                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                var htmlFilePath = Path.Combine(outputDirectory, $"{startDate:yyyyMMddHHmmss}.html");
                File.WriteAllText(htmlFilePath, html);

                var proxyData = await ExtractProxyData(html);

                var jsonData = JsonConvert.SerializeObject(proxyData, Formatting.Indented);

                var jsonFilePath = Path.Combine(outputDirectory, $"{startDate:yyyyMMddHHmmss}.json");
                File.WriteAllText(jsonFilePath, jsonData);

                var execution = new Models.CrawlerExecution
                {
                    StartDate = startDate,
                    EndDate = DateTime.Now,
                    PageCount = _totalPages,
                    RowCount = proxyData.Count,
                    JsonData = jsonData
                };
                //Salva dados em banco.
                await _crawlerRepository.SaveExecution(execution);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao processar a página {url}: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                semaphore.Release();
            }
        }

        //Método para recuperar dados das colunas e linhas do site.
        public async Task<List<Models.Proxy>> ExtractProxyData(string html)
        {
           

            var proxyList = new List<Models.Proxy>();

            try
            {

                _driver.Navigate().GoToUrl("https://proxyservers.pro/proxy/list/order/updated/order_dir/desc/page/");


                List<IWebElement> rows = _driver.FindElements(By.XPath("//table/tbody/tr")).ToList();


                List<List<IWebElement>> allColumns = rows.Select(row => row.FindElements(By.TagName("td")).ToList()).ToList();

                foreach (var columns in allColumns)
                {

                    if (columns.Count >= 4)
                    {
                        proxyList.Add(new Models.Proxy
                        {
                            Ip = columns[1].Text.Trim(),
                            Port = columns[2].Text.Trim(),
                            Country = columns[3].Text.Trim(),
                            Protocol = columns[6].Text.Trim()
                        });
                    }
                }

                OnProxiesExtracted?.Invoke(proxyList);
            }
            catch (Exception ex)
            {
               MessageBox.Show($"Erro ao extrair dados do HTML: {ex.Message}", 
                   "Erro",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                throw;
            }

            return proxyList;
        }

        //Método para recuperar o total de páginas.
        private async Task<int> GetTotalPages(string baseUrl)
        {
            
            try
            {

                var client = _httpClientFactory.CreateClient();
                var html = await client.GetStringAsync(baseUrl + "1");


                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);


                var paginationNode = doc.DocumentNode.SelectSingleNode("//ul[contains(@class, 'pagination')]");
                if (paginationNode != null)
                {
                    // Identifica última página.
                    var pageLinks = paginationNode.SelectNodes(".//a");
                    if (pageLinks != null && pageLinks.Count > 0)
                    {
                        var lastPageNode = pageLinks.LastOrDefault();
                        if (lastPageNode != null && int.TryParse(lastPageNode.InnerText.Trim(), out int totalPages))
                        {
                            return totalPages;
                        }
                    }
                }

                // Se não encontrar a paginação, assume 1 página.
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao determinar o número de páginas: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                
            }
            return 1; // Retorna 1 como padrão.
            }
        }




    
}
