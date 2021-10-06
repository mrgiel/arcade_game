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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class StartGame : Window
    {
        public string teamname;
        public string player1;
        public string player2;

        public StartGame()
        {
            InitializeComponent();
        }
///
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            teamname = teamnaam.Text;
            player1 = speler1.Text;
            player2 = speler2.Text;
            System.Diagnostics.Debug.WriteLine("teamnaam is: " + teamname);
            System.Diagnostics.Debug.WriteLine("speler 1 is: " + player1);
            System.Diagnostics.Debug.WriteLine("speler 2 is: " + player2);
            //todo Database koppeling
            //todo check of alles is ingevuld
            //todo doorgaan naar de game
        }
    }
}
