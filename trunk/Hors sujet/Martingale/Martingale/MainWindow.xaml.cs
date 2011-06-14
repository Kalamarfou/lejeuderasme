using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using Martingale.Mises;

namespace Martingale
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int Pognon { get; set; }
        public ArrayList Mises { get; set; }
        public ArrayList HistoriqueChiffres { get; set; }
        public ArrayList HistoriquePognon { get; set; }

        Random rndNumbers = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int chiffre_sorti = Randomiser_Chiffre();
            foreach (Mise m in Mises)
            {
                int gain = m.CalculerGain(chiffre_sorti);
                
                if (gain > 0)
                {
                    Pognon += gain - m.MiseDeDepart;
                    m.MiseActuelle = m.MiseDeDepart;
                    m.FailCount = 0;
                }
                else
                {
                    //a améliorer pour l'historique. On doit voir le pognon avant mise
                    m.MiseActuelle = m.MiseActuelle * 2;
                    Pognon -= m.MiseActuelle * 2;
                    m.FailCount++;
                }
            }
            HistoriqueChiffres.Add(chiffre_sorti);
            HistoriquePognon.Add(Pognon);

            AfficherHistoriques();
        }

        private void AfficherHistoriques()
        {
            AfficherHistoriqueChiffres();
            AfficherHistoriquePognon();
        }

        private void AfficherHistoriqueChiffres()
        {
            textBox1.Text = "";
            foreach (int c in HistoriqueChiffres)
            {
                textBox1.Text += c.ToString() + "\r\n";
            }
        }

        private void AfficherHistoriquePognon()
        {
            textBox2.Text = "";
            foreach (int p in HistoriquePognon)
            {
                textBox2.Text += p.ToString() + "\r\n";
            }
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Pognon = 50;
            HistoriquePognon = new ArrayList();
            HistoriqueChiffres = new ArrayList();
            Miser();
        }

        private int Randomiser_Chiffre()
        {
            return rndNumbers.Next(0, 36);
        }

        private void Miser()
        {
            Mises = new ArrayList();
            ArrayList c = new ArrayList();
            for (int i = 1; i < 37; i++)
			{
			    if( (i % 2) == 0 )
	            {
		            c.Add(i);
	            }
			}
            Mises.Add(new Mise(c,1,2));
            Pognon -= 1;
        }
    }
}
