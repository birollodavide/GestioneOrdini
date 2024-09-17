using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdini
{
    public class DocTesta
    {
        private string docNo;
        private string externalOrdNo;
        private string documentDate;
        private string expectedDeliveryDate;
        private string confirmedDeliveryDate;
        private string custSupp;
        private string ourReference;
        private string yourReference;
        private string payment;
        private string currency;
        private int docId;
        private string areaManager;
        private string compulsoryDeliveryDate;
        private string shipToAddress;
        private string tbGuid;

        public List<DocRighe> Righe = new List<DocRighe> ();

        public string DocNo
        {
            get
            {
                return docNo;
            }
            set
            {
                if(value != null)
                    docNo = value;
            }
        }
        public string ExternalOrdNo
        {
            get
            {
                return externalOrdNo;
            }
            set
            {
                externalOrdNo = value;
            }
        }
        public string DocumentDate
        {
            get
            {
                return documentDate;
            }
            set
            {
                if (value != null)
                    documentDate = value;
            }
        }
        public string ExpectedDeliveryDate
        {
            get
            {
                return expectedDeliveryDate;
            }
            set
            {
                if (value != null)
                    expectedDeliveryDate = value;
            }
        }
        public string ConfirmedDeliveryDate
        {
            get
            {
                return confirmedDeliveryDate;
            }
            set
            {
                if (value != null)
                    confirmedDeliveryDate = value;
            }
        }
        public string CustSupp
        {
            get
            {
                return custSupp;
            }
            set
            {
                if (value != null)
                    custSupp = value;
            }
        }
        public string OurReference
        {
            get
            {
                return ourReference;
            }
            set
            {
                ourReference = value;
            }
        }
        public string YourReference
        {
            get
            {
                return ourReference;
            }
            set
            {
                ourReference = value;
            }
        }
        public string Payment
        {
            get
            {
                return payment;
            }
            set
            {
                payment = value;
            }
        }
        public string Currency
        {
            get
            {
                return currency;
            }
            set
            {
                currency = value;
            }
        }
        public int DocId
        {
            get
            {
                return docId;
            }
            set
            {
                if (value >= 0)
                    docId = value;
            }
        }
        public string AreaManager
        {
            get
            {
                return areaManager;
            }
            set
            {
                if (value != null)
                    areaManager = value;
            }
        }
        public string CompulsoryDeliveryDate
        {
            get
            {
                return compulsoryDeliveryDate;
            }
            set
            {
                compulsoryDeliveryDate = value;
            }
        }
        public string ShipToAddress
        {
            get
            {
                return shipToAddress;
            }
            set
            {
                shipToAddress = value;
            }
        }
        public string TBGuid
        {
            get
            {
                return tbGuid;
            }
            set
            {
                tbGuid = value;
            }
        }

        public DocTesta()
        {
            Righe.Clear();
        }
        public DocTesta(string docNo, string documentDate, string expectedDeliveryDate, string confirmedDeliveryDate, string custSupp, int docId, string salesPerson)
        {
            DocNo = docNo;
            DocumentDate = documentDate;
            ExpectedDeliveryDate = expectedDeliveryDate;
            ConfirmedDeliveryDate = confirmedDeliveryDate;
            CustSupp = custSupp;
            DocId = docId;
            AreaManager = areaManager;
            Righe.Clear();
        }

        public override string ToString()
        {
            string sToRet = "";
            sToRet += $"DocNo: {docNo}\t";
            sToRet += $"ExternalOrdNo: {externalOrdNo}\t";
            sToRet += $"DocumentDate: {documentDate}\t";
            sToRet += $"ExpectedDeliveryDate: {expectedDeliveryDate}\t";
            sToRet += $"ConfirmedDeliveryDate: {confirmedDeliveryDate}\t";
            sToRet += $"CustSupp: {custSupp}\t";
            sToRet += $"OurReference: {ourReference}\t";
            sToRet += $"YourReference: {yourReference}\t";
            sToRet += $"Payment: {payment}\t";
            sToRet += $"Currency: {currency}\t";
            sToRet += $"DocId: {docId}\t";
            sToRet += $"AreaManager: {areaManager}\n";
            sToRet += $"CompulsoryDeliverydate: {compulsoryDeliveryDate}\t";
            sToRet += $"ShipToAddress: {shipToAddress}\t";
            sToRet += $"TBGuid: {tbGuid}\t";

            return sToRet;
        }
    }
}
