using WebCrawlerProxyList.Models;
using WebCrawlerProxyList.Services;



namespace WebCrawlerProxyList
{
    public partial class MainForm : Form
    {
        private readonly WebCrawlerService _webCrawlerService;

        public MainForm(WebCrawlerService webCrawlerService)
        {
            InitializeComponent();

            _webCrawlerService = webCrawlerService;
            _webCrawlerService.OnProxiesExtracted += UpdateGridView;

        }

        private async void btnStartCrawler_Click(object sender, EventArgs e)
        {

            var outputDirectory = WebCrawlerProxyList.Helper.FolderSelectorHelper.GetUserSelectedDirectory();

            if (string.IsNullOrEmpty(outputDirectory))
            {
                MessageBox.Show("Sele��o de pasta cancelada. O processo n�o ser� iniciado.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                UseWaitCursor = true;
                Enabled = false;

                // Chama o servi�o.
                await _webCrawlerService.StartCrawlWithConcurrency(outputDirectory);

                MessageBox.Show("Processo conclu�do com sucesso!", "Sucesso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao executar o processo: {ex.Message}",
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                UseWaitCursor = false;
                Enabled = true;
            }

        }

        private void UpdateGridView(List<Proxy> proxies)
        {
            if (dgvVizu.InvokeRequired)
            {
                dgvVizu.Invoke(new Action(() =>
                {
                    dgvVizu.Rows.Clear();

                    foreach (var proxy in proxies)
                    {
                        dgvVizu.Rows.Add(proxy.Ip, proxy.Port, proxy.Country, proxy.Protocol);
                    }
                }));
            }
            else
            {
                dgvVizu.Rows.Clear();

                foreach (var proxy in proxies)
                {
                    dgvVizu.Rows.Add(proxy.Ip, proxy.Port, proxy.Country, proxy.Protocol);
                }
            }


        }


    }




}
