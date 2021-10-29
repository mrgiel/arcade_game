using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    /// <summary>
    /// Interaction logic for Win.xaml
    /// </summary>
    public partial class Win : Window
    {
        public string teamname { get; set; }
        public string player1 { get; set; }
        public string player2 { get; set; }
        public int highscore { get; set; }

        private int scoreplayer1 { get; set; }
        private int scoreplayer2 { get; set; }


        public Win(int highscore, string teamname, string player1, string player2, int scoreplayer1, int scoreplayer2)
        {
            //Zorgt er voor dat de variabel die in dit script wordt aangeroepen dezelfde waarde heeft als in het vorige script.
            //this.highscore is regel 28 en de blauwe 'highscore' komt uit game2.xaml.cs (daar krijgt hij de waarde)
            this.highscore = highscore;
            this.scoreplayer1 = scoreplayer1;
            this.scoreplayer2 = scoreplayer2;
            this.teamname = teamname;
            this.player1 = player1;
            this.player2 = player2;

            InitializeComponent();
            setName();
            AddHighscoreToDatabase(highscore, teamname, player1, player2);
        }
        /// <summary>
        /// Zorgt er voor dat spelernamen en scores zichtbaar zijn in de game
        /// </summary>
        private void setName()
        {
            p1.Content = player1;
            p2.Content = player2;
            hs.Content = highscore;
            sp1.Content = scoreplayer1;
            sp2.Content = scoreplayer2;
        }

        /// <summary>
        /// Stuurt de playerdata (namen en score) door naar de database.
        /// </summary>
        /// <param name="highscore"></param>
        /// <param name="teamname"></param>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        private void AddHighscoreToDatabase(int highscore, string teamname, string player1, string player2)
        {

            //connectionString haalt hij op bij App.config. Op deze manier kan het gedeelt worden via git en hoef je niet elke keer de string aan te passen.
            string connectionString = ConfigurationManager.ConnectionStrings
                ["MyConnectionString"].ConnectionString; ;

            //standaard insert query. Stuurt de waardes van teamname, player1, player2 en highscore in de tables Teamnaam, Speler1, Speler2 en Highscore.
            string query = "INSERT INTO [Game] ([Teamnaam],[Speler1],[Speler2],[Highscore]) VALUES ('" + teamname + "', '" + player1 + "','" + player2 + "','" + highscore + "')";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                connection.Close();
            }
        }

        private void QuitGame(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HomeScreen(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void Opnieuw(object sender, RoutedEventArgs e)
        {
            StartGame startgame = new StartGame();
            startgame.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void Highscore(object sender, RoutedEventArgs e)
        {
            Highscore highscore = new Highscore();
            highscore.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
    }
}
