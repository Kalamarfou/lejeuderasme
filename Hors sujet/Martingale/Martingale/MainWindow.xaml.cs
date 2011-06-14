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
        

        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int chiffre_sorti = Roulette.Randomiser_Chiffre();
            foreach (Mise m in Data.Mises)
            {
                int gain = m.CalculerGain(chiffre_sorti);
                
                if (gain > 0)
                {
                    Data.Pognon += gain;
                    m.FailCount = 0;
                }
                else
                {
                    m.FailCount++;
                }
            }

            Data.HistoriqueChiffres.Add(chiffre_sorti);
            Data.HistoriquePognon.Add(Data.Pognon);
            dernierPognon.Text = Data.Pognon.ToString();

            AfficherHistoriques();

            foreach (Mise m in Data.Mises)
            {
                if (m.FailCount > 0)
                {
                    m.MiseActuelle = m.MiseActuelle * 2;
                    //la mise actuelle es tdéja multipliée par deux
                    Data.Pognon -= m.MiseActuelle;
                    m.FailCount++;
                }
                else
                {
                    m.MiseActuelle = m.MiseDeDepart;
                    Data.Pognon -= m.MiseDeDepart;
                }
            }
        }

        private void AfficherHistoriques()
        {
            textBoxChiffres.Text =  Data.AfficherHistoriqueChiffres();
            textBoxPognon.Text = Data.AfficherHistoriquePognon();
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Data.init(50);
            Miser();
        }

        

        private void Miser()
        {
            MisesAuto.Pairs();
        }
    }
}
