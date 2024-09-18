using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdini
{
    public class DocRighe : IComparable<DocRighe>
    {
        private int rowLine;
        private int rowPosition;
        private string rowLineType;
        private string rowDescription;
        private string rowItem;
        private string rowUoM;
        private double rowQty;
        private double rowUnitValue;
        private string rowDiscountFormula;
        private double rowDiscountAmount;
        private int rowSaleOrdId;
        private string rowNotes;
        private string rowLotto;
        private int rowElementiLotto;
        private int stato;

        public int RowLine
        {
            get
            {
                return rowLine;
            }
            set
            {
                rowLine = value;
            }
        }
        public int RowPosition
        {
            get
            {
                return rowPosition;
            }
            set
            {
                rowPosition = value;
            }
        }
        public string RowLineType
        {
            get
            {
                return rowLineType;
            }
            set
            {
                rowLineType = value;
            }
        }
        public string RowDescription
        {
            get
            {
                return rowDescription;
            }
            set
            {
                rowDescription = value;
            }
        }
        public string RowItem
        {
            get
            {
                return rowItem;
            }
            set
            {
                rowItem = value;
            }
        }
        public string RowUoM
        {
            get
            {
                return rowUoM;
            }
            set
            {
                rowUoM = value;
            }
        }
        public double RowQty
        {
            get
            {
                return rowQty;
            }
            set
            {
                rowQty = value;
            }
        }
        public double RowUnitValue
        {
            get
            {
                return rowUnitValue;
            }
            set
            {
                rowUnitValue = value;
            }
        }
        public string RowDiscountFormula
        {
            get
            {
                return rowDiscountFormula;
            }
            set
            {
                rowDiscountFormula = value;
            }
        }
        public double RowDiscountAmount
        {
            get
            {
                return rowDiscountAmount;
            }
            set
            {
                rowDiscountAmount = value;
            }
        }
        public int RowSaleOrdId
        {
            get
            {
                return rowSaleOrdId;
            }
            set
            {
                rowSaleOrdId = value;
            }
        }
        public string RowNotes
        {
            get
            {
                return rowNotes;
            }
            set
            {
                rowNotes = value;
            }
        }
        public string RowLotto
        {
            get
            {
                return rowLotto;
            }
            set
            {
                rowLotto = value;
            }
        }
        public int RowElementiLotto
        {
            get
            {
                return rowElementiLotto;
            }
            set
            {
                rowElementiLotto = value;
            }
        }
        public int Stato
        {
            get
            {
                return stato;
            }
            set
            {
                /*
                 * Se stato == 1 è stato ritirato del tutto
                 * Se stato == 2 non è stato ritirato del tutto
                 * Se stato == 3 è stato ritirato più del dovuto
                 */
                stato = value;
            }
        }

        public DocRighe()
        {
        }

        public DocRighe ShallowCopy()
        {
            return (DocRighe)this.MemberwiseClone();
        }

        public int CompareTo(DocRighe altraRiga)
        {
            if (altraRiga == null) return 1;

            // Confronta per rowLine
            return this.rowLine.CompareTo(altraRiga.rowLine);
        }

        public override string ToString()
        {
            string sToRet = "";
            sToRet += $"Line: {rowLine}\t";
            sToRet += $"Position: {rowPosition}\t";
            sToRet += $"LineType: {rowLineType}\t";
            sToRet += $"Description: {rowDescription}\t";
            sToRet += $"Item: {rowItem}\t";
            sToRet += $"UoM: {rowUoM}\t";
            sToRet += $"Qty: {rowQty}\t";
            sToRet += $"UnitValue: {rowUnitValue}\t";
            sToRet += $"DiscountFormula: {rowDiscountFormula}\t";
            sToRet += $"DiscountAmount: {rowDiscountAmount}\t";
            sToRet += $"SaleOrdId: {rowSaleOrdId}\t";
            sToRet += $"Notes: {rowNotes}\t";
            sToRet += $"Lotto: {rowLotto}\t";
            sToRet += $"Elementi Lotto: {rowElementiLotto}\n";

            return sToRet;
        }
    }
}
