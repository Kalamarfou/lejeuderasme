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
                for (int i = 0; i < int.Parse(nbSession.Text); i++)
                {
                    LancerSession();
                }
                AfficherData();
            }
            catch (Exception x)
            {
                pourcentageChance.Text = x.Message;
            }
            
        }

        private void LancerSession()
        {
            Data.initSession(int.Parse(nbPognonDeDepart.Text));
            Miser();

            for (int i = 0; i < int.Parse(nbParties.Text); i++)
            {
                if (!Data.Doom)
                {
                    MisesAuto.ReMiser();
                    Roulette.JouerEtEncaisser();
                }
            }

            Data.HistoriquePognonFinal.Add(Data.Pognon);
        }

        private void AfficherData()
        {
            textBoxPognon.Text = Data.AfficherHistoriquePognon();
            textBoxStatsChiffres.Text = Data.AfficherHistoriqueChiffresGlobal();
            textBoxStatsPognon.Text = Data.AfficherHistoriquePognonFinal();
            string s = Data.AfficherHistoriquePognonFinalResume();
            textBoxStatsPognonResumees.Text = s;
            pourcentageChance.Text = s.Substring(s.IndexOf("ratio"), s.IndexOf("%") - s.IndexOf("ratio") +1);
        }

        //les mises qui vont etre utilisées
        private void Miser()
        {
            MisesAuto.Pairs();
            //MisesAuto.Passe();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Data.initGlobal();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Data.initGlobal();
            AfficherData();
        }
    }
}
