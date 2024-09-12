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
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabella)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTabella
            // 
            this.dgvTabella.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTabella.Location = new System.Drawing.Point(12, 12);
            this.dgvTabella.Name = "dgvTabella";
            this.dgvTabella.Size = new System.Drawing.Size(1125, 476);
            this.dgvTabella.TabIndex = 0;
            // 
            // txtNumOrdine
            // 
            this.txtNumOrdine.Location = new System.Drawing.Point(1244, 76);
            this.txtNumOrdine.Name = "txtNumOrdine";
            this.txtNumOrdine.Size = new System.Drawing.Size(128, 20);
            this.txtNumOrdine.TabIndex = 1;
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
            this.btnSalva.Location = new System.Drawing.Point(1209, 427);
            this.btnSalva.Name = "btnSalva";
            this.btnSalva.Size = new System.Drawing.Size(112, 23);
            this.btnSalva.TabIndex = 5;
            this.btnSalva.Text = "Salva modifiche";
            this.btnSalva.UseVisualStyleBackColor = true;
            this.btnSalva.Click += new System.EventHandler(this.btnSalva_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 500);
            this.Controls.Add(this.btnSalva);
            this.Controls.Add(this.lblRicerca);
            this.Controls.Add(this.btnCerca);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumOrdine);
            this.Controls.Add(this.dgvTabella);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabella)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTabella;
        private System.Windows.Forms.TextBox txtNumOrdine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCerca;
        private System.Windows.Forms.Label lblRicerca;
        private System.Windows.Forms.Button btnSalva;
    }
}