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
 
        public int highscore = 0;

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
        private void Starten(object sender, RoutedEventArgs e)
        {
            teamname = teamnaam.Text;
            player1 = speler1.Text;
            player2 = speler2.Text;

            if (string.IsNullOrEmpty(teamname) || string.IsNullOrEmpty(player1) || string.IsNullOrEmpty(player2))
            {
                MessageBox.Show("Vul alle velden in!");
            } else
            {
                WpfApp1.Game game = new WpfApp1.Game(highscore, teamname, player1, player2);
                game.Visibility = Visibility.Visible;
                this.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Sluit het spel helemaal af
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuitGame(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// gaat terug naar het startscherm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Startscherm(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Gaat naar de options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opties(object sender, RoutedEventArgs e)
        {
            Window1 options = new Window1();
            options.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
    }
}
