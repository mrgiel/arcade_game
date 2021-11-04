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
    /// Interaction logic for Highscore.xaml
    /// </summary>
    public partial class Highscore : Window
    {
        //Dictionary en list zijn hier nog leeg maar owrden later aangevuld met data uit de database (de GetHighscore() functie)
        //Dictionary teams = Id + Teamnaam
        //List player1 = ingevulde naam voor speler 1
        //List player2 = ingevulde naam voor speler 2
        //List highscore = behaalde highscore van het team
        Dictionary<int, string> teams = new Dictionary<int, string>();
        List<string> player1 = new List<string>();
        List<string> player2 = new List<string>();
        List<int> highscore = new List<int>();

        public object HighScorePannel { get; private set; }

        public Highscore()
        {
            InitializeComponent();
            GetHighScore();
            CreateLabels();
            
        }

        private void QuitGame(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void Opties(object sender, RoutedEventArgs e)
        {
            Window1 options = new Window1();
            options.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Maakt database verbinding en zorgt ervoor dat de list en dictinary worden gevuld met de juiste data
        /// </summary>
        private void GetHighScore()
        {
            teams.Clear();
            string connectionString = ConfigurationManager.ConnectionStrings
                ["MyConnectionString"].ConnectionString; ;
            string query = "SELECT Id, Teamnaam, Speler1, Speler2, Highscore FROM Game ORDER BY Highscore DESC";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand();
            try
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                connection.Open();
                //command.ExecuteNonQuery();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //reader[0] is het eerste tabel waar hij door heen komt in de database. (dus Id)
                    //reader[1] is de tweede tabel, dus Teamnaam
                    // etc. Zie regel 72
                    teams.Add((int)reader[0], (string)reader[1]);
                    player1.Add((string)reader[2]);
                    player2.Add((string)reader[3]);
                    highscore.Add((int)reader[4]);
                }

                connection.Close();
            }
            catch (Exception e)
            {
                connection.Close();
            }

        }

        private void CreateLabels()
        {
            int i = 0;
            HighScorePanel.Children.Clear();
            foreach (var team in teams)
            {
                Label label = new Label();
                label.Content = team.Value + player1[i] + player2[i] + highscore[i];
                label.Foreground = Brushes.White;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                label.FontSize = 24;
                label.FontWeight = FontWeights.Bold;
                HighScorePanel.Children.Add(label);
                i++;

            }
        }

    }
}
