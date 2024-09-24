using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GestioneOrdini
{
    public partial class Form2 : Form
    {
        private Form1 form1 = new Form1();
        private double nElementiLotto = 0;
        bool mostra = false;
        public Form2(Form1 f, double nElementiLotto)
        {
            InitializeComponent();
            form1 = f;
            if (nElementiLotto > 0)
                this.nElementiLotto = nElementiLotto;
            else
                mostra = true;

            if(!mostra)
            {
                label3.Visible = false;
                txtElementiLotto.Visible = false;
            }
        }

        private void caricaTabellaLotto()
        {
            if (form1.doc.Righe != null)
            {
                var source = new BindingSource();
                source.DataSource = form1.doc.Righe;
                form1.dgvPickingPage.DataSource = source;
            }

            form1.dgvPickingPage.Columns[0].HeaderText = "Riga";
            form1.dgvPickingPage.Columns[0].ReadOnly = true;
            form1.dgvPickingPage.Columns[0].Visible = true;
            form1.dgvPickingPage.Columns[1].HeaderText = "Posizione";
            form1.dgvPickingPage.Columns[1].Visible = false;
            form1.dgvPickingPage.Columns[2].HeaderText = "Tipo Riga";
            form1.dgvPickingPage.Columns[2].Visible = true;
            form1.dgvPickingPage.Columns[3].HeaderText = "Descrizione";
            form1.dgvPickingPage.Columns[3].Visible = true;
            form1.dgvPickingPage.Columns[4].HeaderText = "Articolo";
            form1.dgvPickingPage.Columns[4].ReadOnly = true;
            form1.dgvPickingPage.Columns[4].Visible = true;
            form1.dgvPickingPage.Columns[5].HeaderText = "UM";
            form1.dgvPickingPage.Columns[5].ReadOnly = true;
            form1.dgvPickingPage.Columns[5].Visible = true;
            form1.dgvPickingPage.Columns[6].HeaderText = "Qtà";
            form1.dgvPickingPage.Columns[6].Visible = true;
            form1.dgvPickingPage.Columns[7].HeaderText = "Valore Unitario";
            form1.dgvPickingPage.Columns[7].Visible = false;
            form1.dgvPickingPage.Columns[8].HeaderText = "Sconto";
            form1.dgvPickingPage.Columns[8].Visible = false;
            form1.dgvPickingPage.Columns[9].HeaderText = "Quantità scontata";
            form1.dgvPickingPage.Columns[9].Visible = false;
            form1.dgvPickingPage.Columns[10].HeaderText = "ID";
            form1.dgvPickingPage.Columns[10].Visible = false;
            form1.dgvPickingPage.Columns[11].HeaderText = "Note";
            form1.dgvPickingPage.Columns[11].Visible = true;
            form1.dgvPickingPage.Columns[12].HeaderText = "Lotto";
            form1.dgvPickingPage.Columns[12].Visible = true;
            form1.dgvPickingPage.Columns[13].HeaderText = "Elementi Lotto";
            form1.dgvPickingPage.Columns[13].Visible = true;
            form1.dgvPickingPage.Columns[14].Visible = false;

            //Nascondo le righe descrittive
            foreach (DocRighe dr in form1.doc.Righe)
            {
                if (dr.RowLineType.Equals("Descrittiva"))
                {
                    form1.dgvPickingPage.Rows[dr.RowLine - 1].Visible = false;
                }
            }
            form1.dgvPickingPage.Update();
            form1.dgvPickingPage.Refresh();

            form1.coloraTabella();
        }

        private void btnAddLotto_Click(object sender, EventArgs e)
        {
            String nLotto = txtScriviLotto.Text;

            if (form1.dgvPickingPage.SelectedRows.Count > 0)
            {
                if(mostra) nElementiLotto = Double.Parse(txtElementiLotto.Text);
                int nRiga = form1.dgvPickingPage.SelectedRows[0].Index;

                int n = nRiga;
                n++;

                if (Int32.Parse(form1.dgvPickingPage.Rows[nRiga].Cells["RowQty"].Value.ToString()) <= nElementiLotto)
                {
                    DocRighe dr = form1.doc.Righe.Find(a => a.RowLine == n);
                    dr.RowLotto = nLotto;
                    dr.RowElementiLotto = nElementiLotto;
                    dr.Stato = 1;
                }
                else
                {
                    DocRighe dr = form1.doc.Righe.Find(a => a.RowLine == n);
                    DocRighe dr2 = dr.ShallowCopy();
                    
                    dr.RowLotto = nLotto;
                    dr.RowElementiLotto = nElementiLotto;
                    double qtyOriginale = Int32.Parse(form1.dgvPickingPage.Rows[nRiga].Cells["RowQty"].Value.ToString());
                    dr.RowQty = nElementiLotto;
                    dr.Stato = 1;

                    double qtyModificata = qtyOriginale - nElementiLotto;
                    n++;
                    dr2.RowLine = n;
                    dr2.RowQty = qtyModificata;
                    dr2.Stato = 2;
                    form1.doc.Righe.Add(dr2);

                    //scalare tutti i RowLine della lista tranne l'ultimo e poi ordinarla in base al RowLine
                    for(int i = 0; i < form1.doc.Righe.Count - 1; i++)
                    {
                        if (form1.doc.Righe[i].RowLine >= n)
                        {
                            n++;
                            form1.doc.Righe[i].RowLine = n;
                        }
                    }
                    form1.doc.Righe.Sort();
                }
                caricaTabellaLotto();
            }
            Hide();
            form1.txtBarcode.Text = "";
        }
    }
}
