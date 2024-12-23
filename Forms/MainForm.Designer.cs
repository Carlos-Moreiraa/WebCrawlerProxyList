namespace WebCrawlerProxyList
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStartCrawler = new Button();
            dgvVizu = new DataGridView();
            IP = new DataGridViewTextBoxColumn();
            Port = new DataGridViewTextBoxColumn();
            Country = new DataGridViewTextBoxColumn();
            Protocol = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvVizu).BeginInit();
            SuspendLayout();
            // 
            // btnStartCrawler
            // 
            btnStartCrawler.Location = new Point(644, 436);
            btnStartCrawler.Name = "btnStartCrawler";
            btnStartCrawler.Size = new Size(121, 73);
            btnStartCrawler.TabIndex = 0;
            btnStartCrawler.Text = "Iniciar";
            btnStartCrawler.UseVisualStyleBackColor = true;
            btnStartCrawler.Click += btnStartCrawler_Click;
            // 
            // dgvVizu
            // 
            dgvVizu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVizu.Columns.AddRange(new DataGridViewColumn[] { IP, Port, Country, Protocol });
            dgvVizu.Location = new Point(28, 24);
            dgvVizu.Name = "dgvVizu";
            dgvVizu.Size = new Size(576, 485);
            dgvVizu.TabIndex = 2;
            // 
            // IP
            // 
            IP.HeaderText = "Ip";
            IP.Name = "IP";
            // 
            // Port
            // 
            Port.HeaderText = "Port";
            Port.Name = "Port";
            // 
            // Country
            // 
            Country.HeaderText = "Country";
            Country.Name = "Country";
            // 
            // Protocol
            // 
            Protocol.HeaderText = "Protocol";
            Protocol.Name = "Protocol";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe Print", 17F, FontStyle.Bold);
            label1.ForeColor = Color.Teal;
            label1.Location = new Point(610, 280);
            label1.Name = "label1";
            label1.Size = new Size(227, 40);
            label1.TabIndex = 3;
            label1.Text = "Crawler ProxyList";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(610, 352);
            label2.Name = "label2";
            label2.Size = new Size(214, 45);
            label2.TabIndex = 4;
            label2.Text = "Para executar o web Crawler, clique em\r\n iniciar, e selecione o diretorio onde\r\n será salvo os arquivos JSON e HTML.";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 557);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvVizu);
            Controls.Add(btnStartCrawler);
            Name = "MainForm";
            Text = "WebCrawler";
            ((System.ComponentModel.ISupportInitialize)dgvVizu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStartCrawler;
        private DataGridView dgvVizu;
        private DataGridViewTextBoxColumn IP;
        private DataGridViewTextBoxColumn Port;
        private DataGridViewTextBoxColumn Country;
        private DataGridViewTextBoxColumn Protocol;
        private Label label1;
        private Label label2;
    }
}
