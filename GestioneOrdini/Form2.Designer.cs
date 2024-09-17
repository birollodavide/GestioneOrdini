namespace GestioneOrdini
{
    partial class Form2
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
            this.txtScriviLotto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddLotto = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtElementiLotto = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtScriviLotto
            // 
            this.txtScriviLotto.Location = new System.Drawing.Point(133, 19);
            this.txtScriviLotto.Name = "txtScriviLotto";
            this.txtScriviLotto.Size = new System.Drawing.Size(100, 20);
            this.txtScriviLotto.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Inserisci numero lotto:";
            // 
            // btnAddLotto
            // 
            this.btnAddLotto.Location = new System.Drawing.Point(82, 117);
            this.btnAddLotto.Name = "btnAddLotto";
            this.btnAddLotto.Size = new System.Drawing.Size(75, 23);
            this.btnAddLotto.TabIndex = 4;
            this.btnAddLotto.Text = "Aggiungi";
            this.btnAddLotto.UseVisualStyleBackColor = true;
            this.btnAddLotto.Click += new System.EventHandler(this.btnAddLotto_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Inserisci elementi lotto:";
            // 
            // txtElementiLotto
            // 
            this.txtElementiLotto.Location = new System.Drawing.Point(131, 67);
            this.txtElementiLotto.Name = "txtElementiLotto";
            this.txtElementiLotto.Size = new System.Drawing.Size(100, 20);
            this.txtElementiLotto.TabIndex = 6;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 451);
            this.Controls.Add(this.txtElementiLotto);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAddLotto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtScriviLotto);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtScriviLotto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddLotto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtElementiLotto;
    }
}