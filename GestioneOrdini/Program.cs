using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Services;
using System.Windows.Forms;
using System.Xml;

namespace GestioneOrdini
{
    internal static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //DaSistemare("21/00029");
            Application.Run(new Form1());
        }

        /*public static void DaSistemare(string sValore)
        {
            ITMago4WS myWebService = new ITMago4WS("AziendaDemo", "localhost", "80", "mago4", "sa", "itech", "GestioneOrdini");
            int tbPort = myWebService.LoginMago(5);
            string str1 = "";
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
                            //XmlNodeList elProgressivoList = (XmlNodeList)itemNode.SelectNodes("//maxs:Progressivo", nsmSchema);
                            XmlNodeList elInternalOrdNo = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:InternalOrdNo", nsmSchema);
                            XmlNodeList elOrderDate = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:OrderDate", nsmSchema);
                            XmlNodeList elExpectedDeliveryDate = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:ExpectedDeliveryDate", nsmSchema);
                            XmlNodeList elConfirmedDeliveryDate = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:ConfirmedDeliveryDate", nsmSchema);
                            XmlNodeList elCustomer = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:Customer", nsmSchema);
                            XmlNodeList elSaleOrdId_1 = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:SaleOrdId", nsmSchema);
                            XmlNodeList elAreaManager = (XmlNodeList)itemNode.SelectNodes("//maxs:SaleOrder//maxs:AreaManager", nsmSchema);

                            GestioneOrdini.DocTesta docTesta;
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

                            for(int i = 0; i < elLine.Count; i++)
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
                                    RowQty = Double.Parse(elQty[i].InnerText),                        
                                    RowUnitValue = Double.Parse(elUnitValue[i].InnerText),            
                                    RowDiscountFormula = elDiscountFormula[i].InnerText,
                                    RowDiscountAmount = Double.Parse(elDiscountAmount[i].InnerText),  
                                    RowSaleOrdId = Int32.Parse(elSaleOrdId_2[i].InnerText),          
                                    RowNotes = elNotes[i].InnerText,
                                };
                                //inserisco la riga nella lista
                                docTesta.Righe.Insert(i, docRighe);
                            }
                            str1 = docTesta.ToString();
                            for(int i = 0; i < docTesta.Righe.Count; i++)
                                str1 += docTesta.Righe[i].ToString();
                        }
                    }
                    Console.WriteLine(str1);
                }
                else
                {
                    //stampa messaggio errore
                    Console.WriteLine(myWebService.TbMessage(tbPort));
                }
                myWebService.LogoutMago();
            }
        }*/
    }
}
