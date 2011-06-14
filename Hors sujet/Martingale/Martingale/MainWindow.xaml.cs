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
            try
            {
                LancerSession();
            }
            catch (Exception)
            {
                dernierPognon.Text = "rentre des vrais trucs dans les cases";
            }
            
        }

        private void LancerSession()
        {
            Data.init(int.Parse(nbPognonDeDepart.Text));
            Miser();

            for (int i = 0; i < int.Parse(nbParties.Text); i++)
            {
                if (!Data.Doom)
                {
                    Roulette.JouerEtEncaisser();
                    MisesAuto.ReMiser();
                }
            }

            AfficherData();
        }

        private void AfficherData()
        {
            
            if (Data.Doom)
            {
                dernierPognon.Text = "DOOMED: " + Data.Pognon.ToString();
            }
            else
            {
                dernierPognon.Text = Data.Pognon.ToString();
            }
            textBoxChiffres.Text =  Data.AfficherHistoriqueChiffres();
            textBoxPognon.Text = Data.AfficherHistoriquePognon();
        }

        //les mises qui vont etre utilisées
        private void Miser()
        {
            MisesAuto.Pairs();
        }
    }
}
