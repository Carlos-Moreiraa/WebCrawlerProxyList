namespace WebCrawlerProxyList.Helper
{
    public static class FolderSelectorHelper
    {
        public static string GetUserSelectedDirectory()
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Selecione a pasta onde deseja salvar os arquivos JSON e HTML";
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    return folderDialog.SelectedPath; // Retorna a pasta selecionada pelo usuário
                }
            }

            // Retorna null se o usuário cancelar
            return null;
        }
    }
}
