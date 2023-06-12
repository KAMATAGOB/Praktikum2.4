using Praktikum_2._3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktikum2._4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try{
                read();
            }
            catch(Exeption ex){
            MessageBox.Show(ex.Message, "Fehler")
            }
            
            //BerechneParallelZweiPol();//dunno
        }

        private void slFrequency_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lbFreq.Content = slFrequency.Value;
            BerechneParallelZweiPol();
        }
        
        private void read()
        {
            

            string zeile;
            string path = @"..\..\..\txt.txt";
            FileStream fs;
            fs = new FileStream(path, FileMode.OpenOrCreate);
            

            using(StreamReader sr = new StreamReader(fs))
            {
                while(!sr.EndOfStream)
                {
                    zeile = sr.ReadLine();
                    cbPreset.Items.Add(zeile);
                    
                }


            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string bau = "Parallel";
            
            bool saved = false;
            string path = @"..\..\..\txt.txt";
            FileStream fs;
            using (new FileStream(path, FileMode.OpenOrCreate));

            fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine("{0},{1},{2},{3}", tbWid.Text, tbCap.Text, slFrequency.Value, bau);   
                sw.Flush();
            }
            return;
            
        }

        private void BerechneZweipol()
        {
            
            switch(cbType.Text)
            {
                case "RC-Reihen-Zweipol":

                    BerechneReihenZweiPol();
                    break;
                case "RC-Parallel-Zweipol":
                    BerechneParallelZweiPol();
                    break;
            }
        }

        private void BerechneParallelZweiPol()
        {
            double r;
            double f;
            double c;
            string tmp;
            bool tmp1;
            RCZweipolParallel zppObjekt;
            string bauform = "placeholder";


            string betrag;
            string im, re, kom;


            if (tbKomplex == null) {
                return;
            }



            try {
                tmp = tbWid.Text;
                tmp1 = double.TryParse(tmp,out r);
                if (r<0) {
                    tbBetrag.Text = "";
                    tbKomplex.Text = "";
                    throw new ArgumentOutOfRangeException("Fehler! Der Widerstand muss eine positive Zahl sein!");
                    
                }
                else if (!tmp1 )
                {
                    tbBetrag.Text = "";
                    tbKomplex.Text = "";
                    throw new ArgumentException("Fehler! Der Widerstand muss eine Zahl sein!");
                    
                }

                tmp = tbCap.Text;
                tmp1 = double.TryParse(tmp, out c);
                if (c < 0)
                {
                    tbBetrag.Text = "";
                    tbKomplex.Text = "";
                    throw new ArgumentOutOfRangeException("Fehler! Die Frequenz muss eine positive Zahl sein!");
                    
                }
                else if (!tmp1)
                {
                    tbBetrag.Text = "";
                    tbKomplex.Text = "";
                    throw new ArgumentException("Fehler! Die Frequenz muss eine Zahl sein!");
                    
                }

                f = slFrequency.Value;
                c = c * 1E-6;


                if (f == 0 || c == 0 || r == 0)
                {
                    //in diesem fall hat nur r einfluss c = 0
                    tbBetrag.Text = r.ToString();
                    tbKomplex.Text = r.ToString() + " +j0";
                    return;
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message,);
                return;
            }

            zppObjekt = new RCZweipolParallel(r, c, bauform, f);

            betrag = zppObjekt.GetZBetrag().ToString();
            re = zppObjekt.GetZReal().ToString();
            im = zppObjekt.GetZImag().ToString();
            
            kom = string.Format("{0} -j{1}",re ,im);
            tbBetrag.Text = betrag;
            tbKomplex.Text = kom;


            return;
        }

        private void BerechneReihenZweiPol()
        {
            double r;
            double f;
            double c;
            string tmp;
            bool tmp1;
            RCZweiPolReihe zppObjekt;
            string bauform = "placeholder";


            string betrag;
            string im, re, kom;


            if (tbKomplex == null)
            {
                return;
            }



            try
            {
                tmp = tbWid.Text;
                tmp1 = double.TryParse(tmp, out r);
                if (r < 0)
                {
                    tbBetrag.Text = "";
                    tbKomplex.Text = "";
                    throw new ArgumentOutOfRangeException("Fehler! Der Widerstand muss eine positive Zahl sein!");
                }
                else if (!tmp1)
                {
                    tbBetrag.Text = "";
                    tbKomplex.Text = "";
                    throw new ArgumentException("Fehler! Der Widerstand muss eine Zahl sein!");
                    
                }

                tmp = tbCap.Text;
                tmp1 = double.TryParse(tmp, out c);
                if (c < 0)
                {
                    tbBetrag.Text = "";
                    tbKomplex.Text = "";
                    throw new ArgumentOutOfRangeException("Fehler! Die Frequenz muss eine positive Zahl sein!");
                    
                }
                else if (!tmp1)
                {
                    tbBetrag.Text = "";
                    tbKomplex.Text = "";
                    throw new ArgumentException("Fehler! Die Frequenz muss eine Zahl sein!");
                    
                }

                f = slFrequency.Value;
                c = c * 1E-6;

                

                if (f == 0 || c==0)
                {
                    //in diesem fall hat nur r einfluss c = 0
                    tbBetrag.Text = "∞";
                    tbKomplex.Text = r.ToString() + " +j∞";
                    return;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            zppObjekt = new RCZweiPolReihe(f, r, bauform, c);



            betrag = zppObjekt.GetZBetrag().ToString();
            re = zppObjekt.GetZReal().ToString();
            im = zppObjekt.GetZImag().ToString();
            
            kom = string.Format("{0} -j{1}", re, im);


            tbBetrag.Text = betrag;
            tbKomplex.Text = kom;


            return;
        }

        private void tbWid_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            BerechneParallelZweiPol();
            
            
        }

        private void tbCap_TextChanged(object sender, TextChangedEventArgs e)
        {
            BerechneParallelZweiPol();
        }

        
    }
}
