using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GestioneOrdini
{
    public class UoM
    {
        private String item;
        private String baseUoM;
        private String comparableUoM;
        private double baseUoMQty;
        private int compUoMQty;

        public String Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
            }
        }
        public String BaseUoM
        {
            get
            {
                return baseUoM;
            }
            set
            {
                baseUoM = value;
            }
        }
        public String ComparableUoM
        {
            get
            {
                return comparableUoM;
            }
            set
            {
                comparableUoM = value;
            }
        }
        public double BaseUoMQty
        {
            get
            {
                return baseUoMQty;
            }
            set
            {
                baseUoMQty = value;
            }
        }
        public int CompUoMQty
        {
            get
            {
                return compUoMQty;
            }
            set
            {
                compUoMQty = value;
            }
        }

        public UoM()
        {
        }
        public UoM(String Item, String BaseUoM, String ComparableUoM, double BaseUoMQty, int CompUoMQty)
        {
            this.Item = Item;
            this.BaseUoM = BaseUoM;
            this.ComparableUoM = ComparableUoM;
            this.BaseUoMQty = BaseUoMQty;
            this.CompUoMQty = CompUoMQty;
        }

        public override string ToString()
        {
            String S = "Item: " + Item + "; ";
            S += "BaseUoM: " + BaseUoM + "; ";
            S += "ComparableUoM: " + ComparableUoM + "; ";
            S += "BaseUoMQty: " + BaseUoMQty + "; ";
            S += "CompUoMQty: " + CompUoMQty + "\n";
            return S;
        }
    }
}
