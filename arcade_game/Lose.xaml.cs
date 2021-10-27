using System;
using System.Collections.Generic;
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
    /// Interaction logic for Lose.xaml
    /// </summary>
    public partial class Lose : Window
    {
        public string teamname { get; set; }
        public string player1 { get; set; }
        public string player2 { get; set; }
        public int highscore { get; set; }

        public Lose(int highscore, string teamname, string player1, string player2)
        {
            this.highscore = highscore;
            this.teamname = teamname;
            this.player1 = player1;
            this.player2 = player2;
          
            InitializeComponent();
            AddHighscoreToDatabase(highscore, teamname, player1, player2);
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
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\gbuss\\Source\\Repos\\mrgiel\\arcade_game\\arcade_game\\Data\\Database1.mdf\";Integrated Security=True";
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

        private void Opties(object sender, RoutedEventArgs e)
        {
            Window1 options = new Window1();
            options.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
    }
}
