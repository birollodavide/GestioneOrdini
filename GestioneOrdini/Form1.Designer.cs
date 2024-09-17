namespace GestioneOrdini
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTabella = new System.Windows.Forms.DataGridView();
            this.txtNumOrdine = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCerca = new System.Windows.Forms.Button();
            this.lblRicerca = new System.Windows.Forms.Label();
            this.btnSalva = new System.Windows.Forms.Button();
            this.lblSalvataggio = new System.Windows.Forms.Label();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvPickingPage = new System.Windows.Forms.DataGridView();
            this.btnAddLotto = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnElimina = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabella)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickingPage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTabella
            // 
            this.dgvTabella.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTabella.Location = new System.Drawing.Point(0, 0);
            this.dgvTabella.Name = "dgvTabella";
            this.dgvTabella.Size = new System.Drawing.Size(1086, 450);
            this.dgvTabella.TabIndex = 0;
            // 
            // txtNumOrdine
            // 
            this.txtNumOrdine.Location = new System.Drawing.Point(1244, 76);
            this.txtNumOrdine.Name = "txtNumOrdine";
            this.txtNumOrdine.Size = new System.Drawing.Size(128, 20);
            this.txtNumOrdine.TabIndex = 1;
            this.txtNumOrdine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumOrdine_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1140, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Inserisci numero:";
            // 
            // btnCerca
            // 
            this.btnCerca.Location = new System.Drawing.Point(1143, 134);
            this.btnCerca.Name = "btnCerca";
            this.btnCerca.Size = new System.Drawing.Size(75, 23);
            this.btnCerca.TabIndex = 3;
            this.btnCerca.Text = "Cerca";
            this.btnCerca.UseVisualStyleBackColor = true;
            this.btnCerca.Click += new System.EventHandler(this.btnCerca_Click);
            // 
            // lblRicerca
            // 
            this.lblRicerca.AutoSize = true;
            this.lblRicerca.Location = new System.Drawing.Point(1241, 139);
            this.lblRicerca.Name = "lblRicerca";
            this.lblRicerca.Size = new System.Drawing.Size(80, 13);
            this.lblRicerca.TabIndex = 4;
            this.lblRicerca.Text = "Sto cercando...";
            // 
            // btnSalva
            // 
            this.btnSalva.Location = new System.Drawing.Point(1190, 183);
            this.btnSalva.Name = "btnSalva";
            this.btnSalva.Size = new System.Drawing.Size(112, 23);
            this.btnSalva.TabIndex = 5;
            this.btnSalva.Text = "Salva modifiche";
            this.btnSalva.UseVisualStyleBackColor = true;
            this.btnSalva.Click += new System.EventHandler(this.btnSalva_Click);
            // 
            // lblSalvataggio
            // 
            this.lblSalvataggio.AutoSize = true;
            this.lblSalvataggio.Location = new System.Drawing.Point(1159, 231);
            this.lblSalvataggio.Name = "lblSalvataggio";
            this.lblSalvataggio.Size = new System.Drawing.Size(160, 13);
            this.lblSalvataggio.TabIndex = 6;
            this.lblSalvataggio.Text = "Salvataggio modifiche in corso...";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(1190, 281);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(108, 23);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.Text = "Picking page";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1094, 476);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvTabella);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1086, 450);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvPickingPage);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1086, 450);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvPickingPage
            // 
            this.dgvPickingPage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPickingPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPickingPage.Location = new System.Drawing.Point(3, 3);
            this.dgvPickingPage.Name = "dgvPickingPage";
            this.dgvPickingPage.Size = new System.Drawing.Size(1080, 444);
            this.dgvPickingPage.TabIndex = 0;
            // 
            // btnAddLotto
            // 
            this.btnAddLotto.Location = new System.Drawing.Point(1190, 369);
            this.btnAddLotto.Name = "btnAddLotto";
            this.btnAddLotto.Size = new System.Drawing.Size(108, 23);
            this.btnAddLotto.TabIndex = 9;
            this.btnAddLotto.Text = "Aggiungi lotto";
            this.btnAddLotto.UseVisualStyleBackColor = true;
            this.btnAddLotto.Click += new System.EventHandler(this.btnAddLotto_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 513);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Elimina riga selezionata:";
            // 
            // btnElimina
            // 
            this.btnElimina.Location = new System.Drawing.Point(159, 508);
            this.btnElimina.Name = "btnElimina";
            this.btnElimina.Size = new System.Drawing.Size(75, 23);
            this.btnElimina.TabIndex = 11;
            this.btnElimina.Text = "Elimina";
            this.btnElimina.UseVisualStyleBackColor = true;
            this.btnElimina.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 556);
            this.Controls.Add(this.btnElimina);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddLotto);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lblSalvataggio);
            this.Controls.Add(this.btnSalva);
            this.Controls.Add(this.lblRicerca);
            this.Controls.Add(this.btnCerca);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumOrdine);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabella)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickingPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTabella;
        public System.Windows.Forms.TextBox txtNumOrdine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCerca;
        private System.Windows.Forms.Label lblRicerca;
        private System.Windows.Forms.Button btnSalva;
        private System.Windows.Forms.Label lblSalvataggio;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.DataGridView dgvPickingPage;
        private System.Windows.Forms.Button btnAddLotto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnElimina;
    }
}