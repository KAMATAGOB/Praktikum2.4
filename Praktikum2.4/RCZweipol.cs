using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikum2._4
{
    internal class RCZweipol
    {
        private Kondensator ko;
        private double r;


        public double R
        {
            get => r;
            set
            {
                if (value == null || value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Fehler! Widerstand muss positiv sein!");
                }
                r = value;
            }
        }

        //Konstruktor
        internal Kondensator Ko
        {
            get => ko;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Fehler! Kondensatorobjekt muss existieren!");
                }
                ko = value;
            }
        }

        public RCZweipol(double rC, double cC, string bauForm)
        {
            ko = new Kondensator(bauForm, cC);

            if (rC <= 0)
            {
                throw new ArgumentOutOfRangeException("Fehler! Der Widerstand muss positiv sein;");
            }
            else
            {
                r = rC;
            }




        }
    }
}
