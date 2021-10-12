using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace arcade_game
{
    public partial class StartGame : Window
    {
        public string teamname;
        public string player1;
        public string player2;

        public StartGame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Zorgt er voor dat als je op de knop drukt, de ingevulde team/speler namen worden opgeslagen in een string.
        /// Checkt of er iets is ingevuld, anders geeft het een foutmelding.
        /// Als alles goed is gaat het spel verder
        /// </summary>
        /// <param name="sender">De knop die je moet klikken</param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            teamname = teamnaam.Text;
            player1 = speler1.Text;
            player2 = speler2.Text;
            System.Diagnostics.Debug.WriteLine("teamnaam is: " + teamname);
            System.Diagnostics.Debug.WriteLine("speler 1 is: " + player1);
            System.Diagnostics.Debug.WriteLine("speler 2 is: " + player2);
            //todo check of alles is ingevuld
            //todo doorgaan naar de game
        }
    }
}
