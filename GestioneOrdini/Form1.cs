using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Services;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using static GestioneOrdini.Barcode;

namespace GestioneOrdini
{
    public partial class Form1 : Form
    {
        public DocTesta doc = new DocTesta();
        private List<UoM> listUoM = new List<UoM>();
        private String oldValue = "";

        public Form1()
        {
            InitializeComponent();
            lblRicerca.Visible = false;
            lblSalvataggio.Visible = false;
            listUoM.Clear();
        }

        private void caricaTabellaOrdini()
        {
            try
            {
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
                dgvTabella.Columns[5].ReadOnly = false;
                dgvTabella.Columns[6].HeaderText = "Qtà";
                dgvTabella.Columns[7].HeaderText = "Valore Unitario";
                dgvTabella.Columns[7].ReadOnly = true;
                dgvTabella.Columns[8].HeaderText = "Sconto";
                dgvTabella.Columns[9].HeaderText = "Quantità scontata";
                dgvTabella.Columns[9].ReadOnly = true;
                dgvTabella.Columns[10].HeaderText = "ID";
                dgvTabella.Columns[10].Visible = false;
                dgvTabella.Columns[11].HeaderText = "Notes";
                dgvTabella.Columns[12].Visible = false;
                dgvTabella.Columns[13].Visible = false;
                dgvTabella.Columns[14].Visible = false;
            }
            catch (NullReferenceException ex)
            {
                Form1 form1 = new Form1();
                form1.lblRicerca.Visible = true;
                form1.lblRicerca.Text = "Errore nella ricerca!";
                Wait(2000);
                form1.lblRicerca.Visible = false;
                form1.lblRicerca.Text = "Sto cercando...";
            }
        }
        private void caricaTabellaPickingPage()
        {
            try
            {
                if (doc.Righe != null)
                {
                    var source = new BindingSource();
                    source.DataSource = doc.Righe;
                    dgvPickingPage.DataSource = source;
                }

                dgvPickingPage.Columns[0].HeaderText = "Riga";
                dgvPickingPage.Columns[0].ReadOnly = true;
                dgvPickingPage.Columns[1].HeaderText = "Posizione";
                dgvPickingPage.Columns[1].Visible = false;
                dgvPickingPage.Columns[2].HeaderText = "Tipo Riga";
                dgvPickingPage.Columns[3].HeaderText = "Descrizione";
                dgvPickingPage.Columns[4].HeaderText = "Articolo";
                dgvPickingPage.Columns[4].ReadOnly = true;
                dgvPickingPage.Columns[5].HeaderText = "UM";
                dgvPickingPage.Columns[5].ReadOnly = true;
                dgvPickingPage.Columns[6].HeaderText = "Qtà";
                dgvPickingPage.Columns[7].HeaderText = "Valore Unitario";
                dgvPickingPage.Columns[7].Visible = false;
                dgvPickingPage.Columns[8].HeaderText = "Sconto";
                dgvPickingPage.Columns[8].Visible = false;
                dgvPickingPage.Columns[9].HeaderText = "Quantità scontata";
                dgvPickingPage.Columns[9].Visible = false;
                dgvPickingPage.Columns[10].HeaderText = "ID";
                dgvPickingPage.Columns[10].Visible = false;
                dgvPickingPage.Columns[11].HeaderText = "Note";
                dgvPickingPage.Columns[12].HeaderText = "Lotto";
                dgvPickingPage.Columns[13].HeaderText = "Elementi Lotto";
                dgvPickingPage.Columns[14].Visible = false;

                //Nascondo le righe descrittive
                foreach (DocRighe dr in doc.Righe)
                {
                    if (dr.RowLineType.Equals("Descrittiva"))
                    {
                        dgvPickingPage.Rows[dr.RowLine - 1].Visible = false;
                    }
                }
                dgvPickingPage.Refresh();

                coloraTabella();
            }
            catch (NullReferenceException ex)
            {
                Form1 form1 = new Form1();
                form1.lblRicerca.Visible = true;
                form1.lblRicerca.Text = "Errore nella ricerca!";
                Wait(2000);
                form1.lblRicerca.Visible = false;
                form1.lblRicerca.Text = "Sto cercando...";
            }
        }

        private async void txtNumOrdine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblRicerca.Visible = true;
                string numOrdine = txtNumOrdine.Text;

                doc = await Task.Run(() => LeggiDocumento(numOrdine));

                caricaTabellaOrdini();
                lblRicerca.Visible = false;
            }
        }
        private async void btnCerca_Click(object sender, EventArgs e)
        {
            lblRicerca.Visible = true;
            string numOrdine = txtNumOrdine.Text;

            doc = await Task.Run(() => LeggiDocumento(numOrdine));

            caricaTabellaOrdini();
            lblRicerca.Visible = false;
        }

        private static DocTesta LeggiDocumento(string sValore)
        {
            ITMago4WS myWebService = new ITMago4WS("TestBarcode", "localhost", "80", "mago4", "sa", "itech", "GestioneOrdini");
            int tbPort = myWebService.LoginMago(5);
            GestioneOrdini.DocTesta docTesta = new GestioneOrdini.DocTesta();
            List<Object> listFinale = new List<Object>();

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
                            XmlNodeList elInternalOrdNo = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:InternalOrdNo", nsmSchema);
                            XmlNodeList elExternalOrdNo = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:ExternalOrdNo", nsmSchema);
                            XmlNodeList elOrderDate = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:OrderDate", nsmSchema);
                            XmlNodeList elExpectedDeliveryDate = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:ExpectedDeliveryDate", nsmSchema);
                            XmlNodeList elConfirmedDeliveryDate = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:ConfirmedDeliveryDate", nsmSchema);
                            XmlNodeList elCustomer = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:Customer", nsmSchema);
                            XmlNodeList elOurReference = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:OurReference", nsmSchema);
                            XmlNodeList elYourReference = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:YourReference", nsmSchema);
                            XmlNodeList elPayment = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:Payment", nsmSchema);
                            XmlNodeList elCurrency = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:Currency", nsmSchema);
                            XmlNodeList elSaleOrdId_1 = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:SaleOrdId", nsmSchema);
                            XmlNodeList elAreaManager = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:AreaManager", nsmSchema);
                            XmlNodeList elCompulsoryDeliveryDate = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:CompulsoryDeliveryDate", nsmSchema);
                            XmlNodeList elShipToAddress = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:ShipToAddress", nsmSchema);
                            XmlNodeList elTBGuid = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:TBGuid", nsmSchema);

                            try
                            {
                                docTesta = new GestioneOrdini.DocTesta
                                {
                                    DocNo = elInternalOrdNo[0].InnerText.ToUpper(),
                                    ExternalOrdNo = elExternalOrdNo[0].InnerText,
                                    DocumentDate = elOrderDate[0].InnerText,
                                    ExpectedDeliveryDate = elExpectedDeliveryDate[0].InnerText,
                                    ConfirmedDeliveryDate = elConfirmedDeliveryDate[0].InnerText,
                                    CustSupp = elCustomer[0].InnerText.ToUpper(),
                                    OurReference = elOurReference[0].InnerText,
                                    YourReference = elYourReference[0].InnerText,
                                    Payment = elPayment[0].InnerText,
                                    Currency = elCurrency[0].InnerText,
                                    DocId = Convert.ToInt32(elSaleOrdId_1[0].InnerText.ToUpper()),
                                    AreaManager = elAreaManager[0].InnerText,
                                    CompulsoryDeliveryDate = elCompulsoryDeliveryDate[0].InnerText,
                                    ShipToAddress = elShipToAddress[0].InnerText,
                                    TBGuid = elTBGuid[0].InnerText,
                                };
                            }
                            catch (NullReferenceException ex)
                            {
                                Form1 form1 = new Form1();
                                form1.lblRicerca.Visible = true;
                                form1.lblRicerca.Text = "Errore nella ricerca!";
                                Wait(2000);
                                form1.lblRicerca.Visible = false;
                                form1.lblRicerca.Text = "Sto cercando...";
                            }

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
                                    RowLineType = ConvertToString(Int32.Parse(elLineType[i].InnerText)),
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

        private async void btnSalva_Click(object sender, EventArgs e)
        {
            lblSalvataggio.Visible = true;
            
            //Creazione documento
            await Task.Run(() => CreaDocumento_Ordini());
            dgvPickingPage.Refresh(); 

            lblSalvataggio.Visible = false;
        }

        private void CreaDocumento_Ordini()
        {
            string xmlDoc = "";
            ITMago4WS myWebService = new ITMago4WS("TestBarcode", "localhost", "80", "mago4", "sa", "itech", "GestioneOrdini");
            int tbPort = myWebService.LoginMago(5);
            
            if(tbPort >= 10000)
            {
                xmlDoc = "<?xml version='1.0'?>";
                xmlDoc += "<maxs:SaleOrd tbNamespace=\"Document.ERP.SaleOrders.Documents.SaleOrd\" xTechProfile=\"DefaultLight\" xmlns:maxs=\"http://www.microarea.it/Schema/2004/Smart/ERP/SaleOrders/SaleOrd/Standard/DefaultLight.xsd\">";
                xmlDoc += "<maxs:Data>";
                xmlDoc += "<maxs:SaleOrder master=\"true\">";
                xmlDoc += $"<maxs:InternalOrdNo>{doc.DocNo}</maxs:InternalOrdNo>";
                xmlDoc += $"<maxs:ExternalOrdNo>{doc.ExternalOrdNo}</maxs:ExternalOrdNo>";
                xmlDoc += $"<maxs:OrderDate>{doc.DocumentDate}</maxs:OrderDate>";
                xmlDoc += $"<maxs:ExpectedDeliveryDate>{doc.ExpectedDeliveryDate}</maxs:ExpectedDeliveryDate>";
                xmlDoc += $"<maxs:ConfirmedDeliveryDate>{doc.ConfirmedDeliveryDate}</maxs:ConfirmedDeliveryDate>";
                xmlDoc += $"<maxs:Customer>{doc.CustSupp}</maxs:Customer>";
                xmlDoc += $"<maxs:OurReference>{doc.OurReference}</maxs:OurReference>";
                xmlDoc += $"<maxs:YourReference>{doc.YourReference}</maxs:YourReference>";
                xmlDoc += $"<maxs:Payment>{doc.Payment}</maxs:Payment>"; 
                xmlDoc += $"<maxs:Currency>{doc.Currency}</maxs:Currency>";
                xmlDoc += $"<maxs:AreaManager>{doc.AreaManager}</maxs:AreaManager>";
                xmlDoc += $"<maxs:SaleOrdId>{doc.DocId}</maxs:SaleOrdId>";
                xmlDoc += $"<maxs:CompulsoryDeliveryDate>{doc.CompulsoryDeliveryDate}</maxs:CompulsoryDeliveryDate>";
                xmlDoc += $"<maxs:ShipToAddress>{doc.ShipToAddress}</maxs:ShipToAddress>";
                xmlDoc += $"<maxs:TBGuid>{doc.TBGuid}</maxs:TBGuid>";
                xmlDoc += "</maxs:SaleOrder>";
                xmlDoc += "<maxs:Detail>";

                for (int rows = 0; rows < doc.Righe.Count; rows++)
                {
                    var a = doc.Righe[rows];
                    xmlDoc += "<maxs:DetailRow>";
                    xmlDoc += $"<maxs:Line>{a.RowLine}</maxs:Line>";
                    xmlDoc += $"<maxs:Position>{a.RowPosition}</maxs:Position>";
                    xmlDoc += $"<maxs:LineType>{a.RowLineType}</maxs:LineType>";
                    xmlDoc += $"<maxs:Description>{a.RowDescription}</maxs:Description>";
                    xmlDoc += $"<maxs:Item>{a.RowItem}</maxs:Item>";
                    xmlDoc += $"<maxs:UoM>{a.RowUoM}</maxs:UoM>";
                    xmlDoc += $"<maxs:Qty>{a.RowQty}</maxs:Qty>";
                    xmlDoc += $"<maxs:UnitValue>{a.RowUnitValue}</maxs:UnitValue>";
                    xmlDoc += $"<maxs:DiscountFormula>{a.RowDiscountFormula}</maxs:DiscountFormula>";
                    xmlDoc += $"<maxs:DiscountAmount>{a.RowDiscountAmount}</maxs:DiscountAmount>";
                    xmlDoc += $"<maxs:SaleOrdId>{a.RowSaleOrdId}</maxs:SaleOrdId>";
                    xmlDoc += $"<maxs:Notes>{a.RowNotes}</maxs:Notes>";
                    xmlDoc += "</maxs:DetailRow>";
                }
                xmlDoc += "</maxs:Detail>";
                xmlDoc += "</maxs:Data>";
                xmlDoc += "</maxs:SaleOrd>";
            }
            myWebService.aTbService.SetData(myWebService.AuthenticationToken, xmlDoc, DateTime.Now, 0, true, out string sResult);
            myWebService.LogoutMago();
        }

        private void CreaDocumento_PickingPage()
        {
            string xmlDoc = "";
            ITMago4WS myWebService = new ITMago4WS("TestBarcode", "localhost", "80", "mago4", "sa", "itech", "GestioneOrdini");
            int tbPort = myWebService.LoginMago(5);

            if (tbPort >= 10000)
            {
                xmlDoc = "<?xml version='1.0'?>";
                xmlDoc += "<maxs:PickingList xmlns:maxs=\"http://www.microarea.it/Schema/2004/Smart/ERP/Sales/PickingList/Users/sa/GestioneOrdini.xsd\" tbNamespace=\"Document.ERP.Sales.Documents.PickingList\" xTechProfile=\"GestioneOrdini\">";
                xmlDoc += "<maxs:Data>";
                xmlDoc += "<maxs:SaleDocument master=\"true\">";
                xmlDoc += $"<maxs:CustSupp>{doc.CustSupp}</maxs:CustSupp>";
                xmlDoc += $"<maxs:OurReference>{doc.OurReference}</maxs:OurReference>";
                xmlDoc += $"<maxs:YourReference>{doc.YourReference}</maxs:YourReference>";
                xmlDoc += $"<maxs:Payment>{doc.Payment}</maxs:Payment>";
                xmlDoc += $"<maxs:Currency>{doc.Currency}</maxs:Currency>";
                xmlDoc += $"<maxs:AreaManager>{doc.AreaManager}</maxs:AreaManager>";
                xmlDoc += $"<maxs:ShipToAddress>{doc.ShipToAddress}</maxs:ShipToAddress>";
                xmlDoc += "</maxs:SaleDocument>";
                xmlDoc += "<maxs:Detail>";

                for (int rows = 0; rows < doc.Righe.Count; rows++)
                {
                    var a = doc.Righe[rows];
                    xmlDoc += "<maxs:DetailRow>";
                    xmlDoc += $"<maxs:SaleDocId>{a.RowSaleOrdId}</maxs:SaleDocId>";
                    xmlDoc += $"<maxs:Line>{a.RowLine}</maxs:Line>";
                    xmlDoc += $"<maxs:LineType>{a.RowLineType}</maxs:LineType>";
                    xmlDoc += $"<maxs:Description>{a.RowDescription}</maxs:Description>";
                    xmlDoc += $"<maxs:Item>{a.RowItem}</maxs:Item>";
                    xmlDoc += $"<maxs:UoM>{a.RowUoM}</maxs:UoM>";
                    xmlDoc += $"<maxs:Qty>{a.RowQty}</maxs:Qty>";
                    xmlDoc += $"<maxs:UnitValue>{a.RowUnitValue}</maxs:UnitValue>";
                    xmlDoc += $"<maxs:DiscountFormula>{a.RowDiscountFormula}</maxs:DiscountFormula>";
                    xmlDoc += $"<maxs:DiscountAmount>{a.RowDiscountAmount}</maxs:DiscountAmount>";
                    xmlDoc += $"<maxs:Lot>{a.RowLotto}</maxs:Lot>";
                    xmlDoc += $"<maxs:Notes>{a.RowNotes}</maxs:Notes>";
                    xmlDoc += "</maxs:DetailRow>";
                }
                xmlDoc += "</maxs:Detail>";
                xmlDoc += "</maxs:Data>";
                xmlDoc += "</maxs:PickingList>";
            }
            myWebService.aTbService.SetData(myWebService.AuthenticationToken, xmlDoc, DateTime.Now, 0, true, out string sResult);
            myWebService.LogoutMago();
        }

        private static string ConvertToString(int n)
        {
            switch (n)
            {
                case 03538944:
                    return "Nota";
                    break;
                case 03538945:
                    return "Riferimento";
                    break;
                case 03538946:
                    return "Servizio";
                    break;
                case 03538947:
                    return "Merce";
                    break;
                case 03538948:
                    return "Descrittiva";
                    break;
                default:
                    return "error";
                    break;
            }
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = (tabControl1.SelectedIndex + 1) % tabControl1.TabCount;
            await Task.Run(() => CreaDocumento_PickingPage());

            caricaTabellaPickingPage();
        }

        private void btnAddLotto_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this, 0, "");
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = dgvPickingPage.SelectedRows[0].Index;
            doc.Righe.RemoveAt(i);

            caricaTabellaPickingPage();
        }

        public void coloraTabella()
        {
            checkStato();
            for (int i = 0; i < dgvPickingPage.Rows.Count - 1; i++)
            {
                if (doc.Righe[i].Stato == 1)
                {
                    //Colore verde
                    dgvPickingPage.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
                else if (doc.Righe[i].Stato == 2)
                {
                    //Colore giallo
                    dgvPickingPage.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (doc.Righe[i].Stato == 3)
                {
                    //Colore rosso
                    dgvPickingPage.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        //Il metodo controlla che non siano stati cambiati valori riguardo alla quantità
        public void checkStato()
        {
            for(int i = 0; i < dgvPickingPage.Rows.Count - 1; i++)
            {
                if (Double.Parse(dgvPickingPage.Rows[i].Cells["RowQty"].Value.ToString()) > Double.Parse(dgvPickingPage.Rows[i].Cells["RowElementiLotto"].Value.ToString()) && Double.Parse(dgvPickingPage.Rows[i].Cells["RowElementiLotto"].Value.ToString()) != 0)
                {
                    doc.Righe[i].Stato = 3;
                }
            }
        }

        private static async void Wait(int n)
        {
            await Task.Delay(n);
        }


        private void textBox1_EnabledChanged(object sender, EventArgs e)
        {
            if (txtNumOrdine.Enabled == false)
                txtBarcode.Enabled = true;
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                String barcodeString = txtBarcode.Text, numOrdine = "", Item = "", numLotto = "";
                double Peso = 0;
                bool errore = false;

                if (barcodeString.Length == 13)
                {
                    //Barcode corto
                    numOrdine = barcodeString.Substring(0, 7);
                    String p = barcodeString.Substring(7, 5);
                    Peso = Double.Parse(p);

                    //Aggiungo la virgola a n
                    Peso /= 1000;

                    //Gestione SQL server
                    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestioneOrdiniConnectionString"].ConnectionString);
                    String query = $"select * from MA_ItemsPurchaseBarCode where barcode = '{numOrdine}'";
                    SqlCommand command = new SqlCommand(query, sqlConn);

                    sqlConn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        //Caso in cui esiste il numero ordine
                        Item = reader.GetValue(0).ToString();
                        sqlConn.Close();
                    }
                    else
                    {
                        //Caso in cui non esiste il numero ordine e devo usare l'intero barcode
                        sqlConn.Close();

                        SqlConnection newSqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestioneOrdiniConnectionString"].ConnectionString);
                        String newQuery = $"select * from MA_ItemsPurchaseBarCode where barcode = '{barcodeString}'";
                        SqlCommand newCommand = new SqlCommand(newQuery, newSqlConn);

                        newSqlConn.Open();
                        SqlDataReader newReader = newCommand.ExecuteReader();

                        if (newReader.Read())
                        {
                            Item = newReader.GetValue(0).ToString();
                            Peso = 0;                           //In questo modo viene richiesto il peso all'utente
                        }
                        else
                        {
                            //Errore
                            errore = true;
                        }

                        newSqlConn.Close();
                    }

                    if (!errore)
                    {
                        Form2 form2 = new Form2(this, Peso, "");

                        for (int i = 0; i < dgvPickingPage.RowCount - 1; i++)
                        {
                            if (dgvPickingPage.Rows[i].Cells["RowItem"].Value.ToString() == Item)
                                dgvPickingPage.Rows[i].Selected = true;
                        }

                        form2.Show();
                    }
                    else
                    {
                        //Mostro a video la box di errore
                        MessageBox.Show("Errore nella lettura del barcode!");
                    }
                }
                else if (barcodeString.Length > 13)
                {
                    //Barcode lungo
                    //Controllo che non ci siano le partentesi altrimenti le tolgo
                    if (barcodeString.Contains("("))
                    {
                        barcodeString = RemoveParentheses(barcodeString);
                    }

                    Barcode.HasCheckSum = false;
                    Dictionary<AII, string> result = Parse(barcodeString, false);

                    String p = "";

                    foreach (var sottoStringa in result)
                    {
                        if (sottoStringa.Key.AI == "01")
                            numOrdine = sottoStringa.Value;
                        else if (sottoStringa.Key.AI == "10")
                            numLotto = sottoStringa.Value;
                        else if (sottoStringa.Key.AI == "310d")
                            p = sottoStringa.Value;
                    }

                    if (p != "")
                    {
                        Peso = Double.Parse(p);
                        Peso /= 1000;
                    }
                    else Peso = 0;

                    //Gestione SQL server
                    SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestioneOrdiniConnectionString"].ConnectionString);
                    String query = $"select * from MA_ItemsPurchaseBarCode where barcode = '{numOrdine}'";
                    SqlCommand command = new SqlCommand(query, sqlConn);

                    sqlConn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Item = reader.GetValue(0).ToString();
                        sqlConn.Close();
                    }

                    Form2 form2 = new Form2(this, Peso, numLotto);

                    for (int i = 0; i < dgvPickingPage.RowCount - 1; i++)
                    {
                        if (dgvPickingPage.Rows[i].Cells["RowItem"].Value.ToString() == Item)
                            dgvPickingPage.Rows[i].Selected = true;
                    }

                    if (Peso == 0)
                        form2.Show();
                    else
                        form2.inserisciLotto();
                }
            }
        }

        private String RemoveParentheses(String S)
        {
            return Regex.Replace(S, "[()]", "");
        }

        private void prendiDatiUoM(String item)
        {
            String baseUoM = "";
            listUoM.Clear();

            //Connessione al server per ottenere Item da numOrdine
            SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["GestioneOrdiniConnectionString"].ConnectionString);
            
            String query = $"select * from MA_Items where item = '{item}'";
            SqlCommand command = new SqlCommand(query, sqlConn);

            sqlConn.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                baseUoM = reader["BaseUoM"].ToString();
            }

            reader.Close();

            query = $"select * from MA_ItemsComparableUoM where item = '{item}'";
            SqlCommand command2 = new SqlCommand(query, sqlConn);
            SqlDataReader reader2 = command2.ExecuteReader();

            while (reader2.Read())
            {
                UoM um = new UoM();
                um.ComparableUoM = reader2["ComparableUoM"].ToString();
                um.BaseUoMQty = Double.Parse(reader2["BaseUoMQty"].ToString());
                um.CompUoMQty = Int32.Parse(reader2["CompUoMQty"].ToString());
                um.Item = item;
                um.BaseUoM = baseUoM;
                listUoM.Add(um);
            }

            reader2.Close();
            sqlConn.Close();
        }

        private double convertToBase(double Qty)        //La stringa UoM conterrà l'unità di misura dalla quale bisogna convertire
        {
            double baseUoMQty = 0;

            foreach (UoM um in listUoM)
            {
                if (um.ComparableUoM == oldValue)
                    baseUoMQty = um.BaseUoMQty;
            }

            //Quantità ordine (NON unità base) * BaseUoMQty = Quantità ordine (unità base)
            return Math.Round(Qty * baseUoMQty, 2);
        }

        private double convertFromBase(String UoM, double Qty)      //La stringa UoM conterrà l'unità di misura in cui bisognerà convertire
        {
            if(oldValue != listUoM[0].BaseUoM)
                Qty = convertToBase(Qty);

            double baseUoMQty = 0;

            foreach (UoM um in listUoM)
            {
                if(um.ComparableUoM == UoM)
                    baseUoMQty = um.BaseUoMQty;
            }

            //Quantità ordine (unità base) / BaseUoMQty (unità da convertire) = Quantità ordine (unità convertita)
            return Math.Round(Qty / baseUoMQty, 2);
        }

        private void dgvTabella_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    String cellValue = dgvTabella.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    cellValue = cellValue.ToUpper();
                    prendiDatiUoM(dgvTabella.Rows[e.RowIndex].Cells["RowItem"].Value.ToString());       //Riempio la lista per l'item in cui è stato cambiato il valore della cella

                    if(cellValue != listUoM[0].BaseUoM)
                    {
                        double n = convertFromBase(cellValue, Double.Parse(dgvTabella.Rows[e.RowIndex].Cells["RowQty"].Value.ToString()));
                        doc.Righe[Int32.Parse(dgvTabella.Rows[e.RowIndex].Cells["RowLine"].Value.ToString()) - 1].RowUoM = cellValue;
                        doc.Righe[Int32.Parse(dgvTabella.Rows[e.RowIndex].Cells["RowLine"].Value.ToString()) - 1].RowQty = n;
                    }
                    else
                    {
                        double n = convertToBase(Double.Parse(dgvTabella.Rows[e.RowIndex].Cells["RowQty"].Value.ToString()));

                        doc.Righe[Int32.Parse(dgvTabella.Rows[e.RowIndex].Cells["RowLine"].Value.ToString()) - 1].RowUoM = cellValue;
                        doc.Righe[Int32.Parse(dgvTabella.Rows[e.RowIndex].Cells["RowLine"].Value.ToString()) - 1].RowQty = n;
                    }
                }
            }
            caricaTabellaOrdini();
        }

        private void dgvTabella_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    oldValue = dgvTabella[e.ColumnIndex, e.RowIndex].Value.ToString();
                    oldValue = oldValue.ToUpper();
                }
            }
        }
    }
}
