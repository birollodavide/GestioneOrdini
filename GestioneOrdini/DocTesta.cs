using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdini
{
    public class DocTesta
    {
        private string docNo;
        private string documentDate;
        private string expectedDeliveryDate;
        private string confirmedDeliveryDate;
        private string custSupp;
        private int docId;
        private string areaManager;

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
        public int DocId
        {
            get
            {
                return docId;
            }
            set
            {
                if (value != null && value >= 0)
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
            sToRet += $"DocumentDate: {documentDate}\t";
            sToRet += $"ExpectedDeliveryDate: {expectedDeliveryDate}\t";
            sToRet += $"ConfirmedDeliveryDate: {confirmedDeliveryDate}\t";
            sToRet += $"CustSupp: {custSupp}\t";
            sToRet += $"DocId: {docId}\t";
            sToRet += $"AreaManager: {areaManager}\n";

            return sToRet;
        }
    }
}
