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
    /// Interaction logic for Highscore.xaml
    /// </summary>
    public partial class Highscore : Window
    {
        Dictionary<string, int> highscores = new Dictionary<string, int>();

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 options = new Window1();
            options.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void GetHighScore()
        {
            highscores.Clear();
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\youss\\Source\\Repos\\mrgiel\\arcade_game\\arcade_game\\Data\\Database1.mdf\";Integrated Security=True";
            string query = "SELECT Teamnaam, Highscore FROM Game";
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
                    highscores.Add((string)reader[0], (int)reader[1]);
                }

                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                connection.Close();
            }

        }

        private void CreateLabels()
        {
            HighScorePanel.Children.Clear();
            var sortedhighscore = from Highscore in highscores orderby Highscore.Value descending select Highscore;
            foreach (KeyValuePair<string, int>highscore in sortedhighscore)
            {
                Label label = new Label();
                label.Content = highscore.Key + highscore.Value;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                HighScorePanel.Children.Add(label);

            }
        }

    }
}
