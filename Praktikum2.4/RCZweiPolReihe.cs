using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praktikum2._4
{
    internal class RCZweiPolReihe
    {
        private Kondensator ko;
        private double f;
        private double r;
        private string bauform;
        private double c;

     


        /// <summary>
        /// Konstuktor
        /// </summary>
        /// <param name="fC">frequency</param>
        /// <param name="rC">resistance</param>
        /// <param name="bau">build style</param>
        /// <param name="c">capacity</param>
        public RCZweiPolReihe(double fC, double rC, string bau, double c)
        {

            c = c / 1000000000;

            Ko = new Kondensator(bau, c);

            if (fC < 0)
            {
                f = (fC * (-1));
            }
            else if (fC == 0)
            {
                f = 1;
            }
            else
            {
                f = fC;
            }

            if (rC < 0)
            {
                r = (rC * (-1));
            }
            else if (rC == 0)
            {
                r = 1;
            }
            else
            {
                r = rC;
            }

        }


        //Getter/Setter

        public double F
        {
            get => f; set
            {
                if (value < 0)
                {
                    f = (value * (-1));
                }
                else if (value == 0)
                {
                    f = 1;
                }
                else
                {
                    f = value;
                }
            }
        }
        public double R
        {
            get => r; set
            {
                if (value < 0)
                {
                    r = (value * (-1));
                }
                else if (value == 0)
                {
                    r = 1;
                }
                else
                {
                    r = value;
                }
            }
        }
        internal Kondensator Ko { get => ko; set => ko = value; }

        //methods


        /// <summary>
        /// berechnet den realen Widerstandsanteil des Objektes
        /// </summary>
        /// <returns>der berechente reale Wiederstandsanteil</returns>
        public double GetZReal()
        {
            double ZReal;

            ZReal = r;

            return ZReal;
        }


        /// <summary>
        /// berechnet den imaginären Widerstandsanteil des Objektes
        /// </summary>
        /// <returns>der berechente imaginäre Wiederstandsanteil</returns>
        public double GetZImag()
        {
            double ZImag;

            double tmp = (2 * (Math.PI) * f * ko.Kapazitaet);

            if (tmp == 0)
            {
                ZImag = 1;//placeholder div durch 0 not defined
            }

            ZImag = 1 / tmp;

            return ZImag;
        }


        /// <summary>
        /// Berechent den Betrag der beiden Widerstandsanteile
        /// </summary>
        /// <returns>den berechenten Betrag</returns>
        public double GetZBetrag()
        {
            double tmp, ZBetrag;

            tmp = ((GetZImag() * GetZImag()) + (GetZReal() * GetZReal()));

            ZBetrag = Math.Sqrt(tmp);

            return ZBetrag;
        }


        /// <summary>
        /// berechent die Kreisfrequenz aus der Frequenz
        /// </summary>
        /// <returns>die berechente Kreisfrequenz</returns>
        public double GetKreisFrequenz()
        {
            double omega;

            omega = (2 * (Math.PI) * f);

            return omega;
        }
    }
}
