using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Web.Services;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace GestioneOrdini
{
    public partial class Form1 : Form
    {
        private DocTesta doc = new DocTesta();
        public Form1()
        {
            InitializeComponent();
            lblRicerca.Visible = false;
            lblSalvataggio.Visible = false;
        }
        private async void txtNumOrdine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblRicerca.Visible = true;
                string numOrdine = txtNumOrdine.Text;

                doc = await Task.Run(() => LeggiDocumento(numOrdine));

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
        }
        private async void btnCerca_Click(object sender, EventArgs e)
        {
            lblRicerca.Visible = true;
            string numOrdine = txtNumOrdine.Text;

            doc = await Task.Run(() => LeggiDocumento(numOrdine));

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

        private static DocTesta LeggiDocumento(string sValore)
        {
            ITMago4WS myWebService = new ITMago4WS("AziendaDemo", "localhost", "80", "mago4", "sa", "itech", "GestioneOrdini");
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

            lblSalvataggio.Visible = false;
        }

        private void CreaDocumento_Ordini()
        {
            string xmlDoc = "";
            ITMago4WS myWebService = new ITMago4WS("AziendaDemo", "localhost", "80", "mago4", "sa", "itech", "GestioneOrdini");
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
            ITMago4WS myWebService = new ITMago4WS("AziendaDemo", "localhost", "80", "mago4", "sa", "itech", "GestioneOrdini");
            int tbPort = myWebService.LoginMago(5);

            if (tbPort >= 10000)
            {
                xmlDoc = "<?xml version='1.0'?>";
                xmlDoc += "<maxs:PickingList xmlns:maxs=\"http://www.microarea.it/Schema/2004/Smart/ERP/Sales/PickingList/Users/sa/GestioneOrdini.xsd\" tbNamespace=\"Document.ERP.Sales.Documents.PickingList\" xTechProfile=\"GestioneOrdini\">";
                xmlDoc += "<maxs:Data>";
                xmlDoc += "<maxs:SaleDocument master=\"true\">";
                //xmlDoc += $"<maxs:ExternalOrdNo>{docTesta.ExternalOrdNo}</maxs:ExternalOrdNo>";
                //xmlDoc += $"<maxs:OrderDate>{docTesta.DocumentDate}</maxs:OrderDate>";
                //xmlDoc += $"<maxs:ExpectedDeliveryDate>{docTesta.ExpectedDeliveryDate}</maxs:ExpectedDeliveryDate>";
                //xmlDoc += $"<maxs:ConfirmedDeliveryDate>{docTesta.ConfirmedDeliveryDate}</maxs:ConfirmedDeliveryDate>";
                xmlDoc += $"<maxs:CustSupp>{doc.CustSupp}</maxs:CustSupp>";
                xmlDoc += $"<maxs:OurReference>{doc.OurReference}</maxs:OurReference>";
                xmlDoc += $"<maxs:YourReference>{doc.YourReference}</maxs:YourReference>";
                xmlDoc += $"<maxs:Payment>{doc.Payment}</maxs:Payment>";
                xmlDoc += $"<maxs:Currency>{doc.Currency}</maxs:Currency>";
                xmlDoc += $"<maxs:AreaManager>{doc.AreaManager}</maxs:AreaManager>";
                //xmlDoc += $"<maxs:SaleDocId>{doc.DocId}</maxs:SaleDocId>";
                //xmlDoc += $"<maxs:CompulsoryDeliveryDate>{docTesta.CompulsoryDeliveryDate}</maxs:CompulsoryDeliveryDate>";
                xmlDoc += $"<maxs:ShipToAddress>{doc.ShipToAddress}</maxs:ShipToAddress>";
                xmlDoc += "</maxs:SaleDocument>";
                xmlDoc += "<maxs:Detail>";

                for (int rows = 0; rows < doc.Righe.Count; rows++)
                {
                    var a = doc.Righe[rows];
                    xmlDoc += "<maxs:DetailRow>";
                    xmlDoc += $"<maxs:SaleDocId>{a.RowSaleOrdId}</maxs:SaleDocId>";
                    xmlDoc += $"<maxs:Line>{a.RowLine}</maxs:Line>";
                    //xmlDoc += $"<maxs:Position>{a.RowPosition}</maxs:Position>";
                    xmlDoc += $"<maxs:LineType>{a.RowLineType}</maxs:LineType>";
                    xmlDoc += $"<maxs:Description>{a.RowDescription}</maxs:Description>";
                    xmlDoc += $"<maxs:Item>{a.RowItem}</maxs:Item>";
                    xmlDoc += $"<maxs:UoM>{a.RowUoM}</maxs:UoM>";
                    xmlDoc += $"<maxs:Qty>{a.RowQty}</maxs:Qty>";
                    xmlDoc += $"<maxs:UnitValue>{a.RowUnitValue}</maxs:UnitValue>";
                    xmlDoc += $"<maxs:DiscountFormula>{a.RowDiscountFormula}</maxs:DiscountFormula>";
                    xmlDoc += $"<maxs:DiscountAmount>{a.RowDiscountAmount}</maxs:DiscountAmount>";
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
                case 3538944:
                    return "Nota";
                    break;
                case 3538945:
                    return "Riferimento";
                    break;
                case 3538946:
                    return "Servizio";
                    break;
                case 3538947:
                    return "Merce";
                    break;
                case 3538948:
                    return "Descrittiva";
                    break;
                default:
                    return "error";
                    break;
            }
        }
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            await Task.Run(() => CreaDocumento_PickingPage());

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
            dgvPickingPage.Columns.Add("Lotto", "Lotto");;

            //Nascondo le righe descrittive
            foreach (DocRighe dr in doc.Righe)
            {
                if (dr.RowLineType.Equals("Descrittiva"))
                {
                    dgvPickingPage.Rows[dr.RowLine - 1].Visible = false;
                }
            }
            dgvPickingPage.Refresh();
        }

        private void btnAddLotto_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
