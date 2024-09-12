using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GestioneOrdini
{
    public partial class Form1 : Form
    {
        DocTesta doc = new DocTesta();
        public Form1()
        {
            InitializeComponent();
            lblRicerca.Visible = false;
        }

        private async void btnCerca_Click(object sender, EventArgs e)
        {
            lblRicerca.Visible = true;
            string numOrdine = txtNumOrdine.Text;

            doc = await Task.Run(() => Metodo(numOrdine));

            //se doc == null c'è stato un errore
            if (doc.Righe != null)
            {
                var source = new BindingSource();
                source.DataSource = doc.Righe;
                dgvTabella.DataSource = source;
            }

            dgvTabella.Columns[0].HeaderText = "Riga";
            dgvTabella.Columns[0].ReadOnly = true;
            dgvTabella.Columns[1].HeaderText = "Posizione";
            dgvTabella.Columns[1].Visible = false;
            dgvTabella.Columns[2].HeaderText = "Tipo Riga";
            dgvTabella.Columns[3].HeaderText = "Descrizione";
            dgvTabella.Columns[4].HeaderText = "Articolo";
            dgvTabella.Columns[4].ReadOnly = true;
            dgvTabella.Columns[5].HeaderText = "UM";
            dgvTabella.Columns[5].ReadOnly = true;
            dgvTabella.Columns[6].HeaderText = "Qtà";
            dgvTabella.Columns[7].HeaderText = "Valore Unitario";
            dgvTabella.Columns[7].ReadOnly = true;
            dgvTabella.Columns[8].HeaderText = "Sconto";
            dgvTabella.Columns[9].HeaderText = "Quantità scontata";
            dgvTabella.Columns[9].ReadOnly = true;
            dgvTabella.Columns[10].HeaderText = "ID";
            dgvTabella.Columns[10].Visible = false;
            dgvTabella.Columns[11].HeaderText = "Notes";

            lblRicerca.Visible = false;
        }

        private static DocTesta Metodo(string sValore)
        {
            ITMago4WS myWebService = new ITMago4WS("AziendaDemo", "localhost", "80", "mago4", "sa", "itech", "GestioneOrdini");
            int tbPort = myWebService.LoginMago(5);
            GestioneOrdini.DocTesta docTesta = new GestioneOrdini.DocTesta();
            List <Object> listFinale = new List<Object>();

            if (tbPort >= 10000)
            {
                string sXMLDoc = "<?xml version='1.0'?>";
                sXMLDoc += "<maxs:SaleOrd tbNamespace=\"Document.ERP.SaleOrders.Documents.SaleOrd\" xTechProfile=\"DefaultLight\" xmlns:maxs=\"http://www.microarea.it/Schema/2004/Smart/ERP/SaleOrders/SaleOrd/Standard/DefaultLight.xsd\">";
                sXMLDoc += "<maxs:Parameters>";
                sXMLDoc += "<maxs:DefaultDialog title=\"Ricerca campi\">";
                sXMLDoc += "<maxs:DefaultGroup title=\"Gruppo di ricerca\">";
                sXMLDoc += $"<maxs:InternalOrdNo type=\"String\">{sValore}</maxs:InternalOrdNo>";
                sXMLDoc += "</maxs:DefaultGroup>";
                sXMLDoc += "</maxs:DefaultDialog>";
                sXMLDoc += "</maxs:Parameters>";
                sXMLDoc += "</maxs:SaleOrd> ";

                string[] dataOut = myWebService.aTbService.GetData(myWebService.AuthenticationToken, sXMLDoc, DateTime.Now, 0, 0, 0, true);

                //myWebService.aTbService.SetData(myWebService.AuthenticationToken, sXMLDoc, DateTime.Now, 0, true, out string sResult);

                if (dataOut != null && dataOut.Length > 0)
                {
                    XmlDocument xDoc = new XmlDocument
                    {
                        XmlResolver = null
                    };

                    for (int nDoc = 0; nDoc <= dataOut.GetUpperBound(0); nDoc++)
                    {
                        xDoc.LoadXml(dataOut[nDoc]);
                        XmlNamespaceManager nsmSchema = new XmlNamespaceManager(xDoc.NameTable);
                        nsmSchema.AddNamespace(xDoc.DocumentElement.Prefix, xDoc.DocumentElement.NamespaceURI);
                        XmlElement itemNode = (XmlElement)xDoc.DocumentElement;
                        if (itemNode != null)
                        {
                            //XmlNodeList elProgressivoList = (XmlNodeList)itemNode.SelectNodes("//maxs:Progressivo", nsmSchema);
                            XmlNodeList elInternalOrdNo = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:InternalOrdNo", nsmSchema);
                            XmlNodeList elOrderDate = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:OrderDate", nsmSchema);
                            XmlNodeList elExpectedDeliveryDate = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:ExpectedDeliveryDate", nsmSchema);
                            XmlNodeList elConfirmedDeliveryDate = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:ConfirmedDeliveryDate", nsmSchema);
                            XmlNodeList elCustomer = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:Customer", nsmSchema);
                            XmlNodeList elSaleOrdId_1 = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:SaleOrdId", nsmSchema);
                            XmlNodeList elAreaManager = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:AreaManager", nsmSchema);

                            docTesta = new GestioneOrdini.DocTesta
                            {
                                DocNo = elInternalOrdNo[0].InnerText.ToUpper(),
                                DocumentDate = elOrderDate[0].InnerText,
                                ExpectedDeliveryDate = elExpectedDeliveryDate[0].InnerText,
                                ConfirmedDeliveryDate = elConfirmedDeliveryDate[0].InnerText,
                                CustSupp = elCustomer[0].InnerText.ToUpper(),
                                DocId = Convert.ToInt32(elSaleOrdId_1[0].InnerText.ToUpper()),
                                AreaManager = elAreaManager[0].InnerText,
                            };

                            //importo le righe
                            XmlNodeList elLine = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:Line", nsmSchema);
                            XmlNodeList elPosition = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:Position", nsmSchema);
                            XmlNodeList elLineType = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:LineType", nsmSchema);
                            XmlNodeList elDescription = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:Description", nsmSchema);
                            XmlNodeList elItem = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:Item", nsmSchema);
                            XmlNodeList elUoM = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:UoM", nsmSchema);
                            XmlNodeList elQty = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:Qty", nsmSchema);
                            XmlNodeList elUnitValue = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:UnitValue", nsmSchema);
                            XmlNodeList elDiscountFormula = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:DiscountFormula", nsmSchema);
                            XmlNodeList elDiscountAmount = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:DiscountAmount", nsmSchema);
                            XmlNodeList elSaleOrdId_2 = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:SaleOrdId", nsmSchema);
                            XmlNodeList elNotes = (XmlNodeList)itemNode.SelectNodes("//maxs:Detail//maxs:DetailRow//maxs:Notes", nsmSchema);

                            for (int i = 0; i < elLine.Count; i++)
                            {
                                GestioneOrdini.DocRighe docRighe;
                                docRighe = new GestioneOrdini.DocRighe
                                {
                                    RowLine = Int32.Parse(elLine[i].InnerText),
                                    RowPosition = Int32.Parse(elPosition[i].InnerText),
                                    RowLineType = Int32.Parse(elLineType[i].InnerText),
                                    RowDescription = elDescription[i].InnerText,
                                    RowItem = elItem[i].InnerText,
                                    RowUoM = elUoM[i].InnerText,
                                    RowQty = Double.Parse(elQty[i].InnerText, CultureInfo.InvariantCulture),
                                    RowUnitValue = Double.Parse(elUnitValue[i].InnerText, CultureInfo.InvariantCulture),
                                    RowDiscountFormula = elDiscountFormula[i].InnerText,
                                    RowDiscountAmount = Double.Parse(elDiscountAmount[i].InnerText, CultureInfo.InvariantCulture),
                                    RowSaleOrdId = Int32.Parse(elSaleOrdId_2[i].InnerText),
                                    RowNotes = elNotes[i].InnerText,
                                };
                                //inserisco la riga nella lista
                                docTesta.Righe.Insert(i, docRighe);
                            }
                        }
                        else return null;
                    }
                }
                else
                {
                    //stampa messaggio errore
                    Console.WriteLine(myWebService.TbMessage(tbPort));
                    return null;
                }
                myWebService.LogoutMago();
                return docTesta;
            }
            else return null;
        }

        private void btnSalva_Click(object sender, EventArgs e)
        {
            string xmlDoc = "";



        }
    }
}
