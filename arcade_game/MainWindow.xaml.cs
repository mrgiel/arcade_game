using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace arcade_game
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

        /// <summary>
        /// Opent naam invul scherm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartGame(object sender, RoutedEventArgs e)
        {
            StartGame startGame = new StartGame();
            startGame.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void Highscore(object sender, RoutedEventArgs e)
        {
            Highscore highscore = new Highscore();
            highscore.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void QuitGame(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Opties(object sender, RoutedEventArgs e)
        {
            Window1 options = new Window1();
            options.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
    }
}
