using arcade_game;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Game : Window
    {
        public string teamname { get; set; }
        public string player1 { get; set; }
        public string player2 { get; set; }
        public int highscore { get; set; }

        private bool moveUp2, moveLeft2, moveRight2;
        private bool moveUp1, moveLeft1, moveRight1;

        private bool spaceLeft1, spaceRight1, spaceUp1;
        private bool spaceLeft2, spaceRight2, spaceUp2;

        private bool Gravity1, Gravity2;
        private DispatcherTimer gameTimer = new DispatcherTimer();

        const int playerSpeed = 2;
        const int GravitySpeed = 1;

        public Game(int highscore, string teamname, string player1, string player2)
        {
            InitializeComponent();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameEngine;
            gameTimer.Start();
            game.Focus();

            this.highscore = highscore;
            this.teamname = teamname;
            this.player1 = player1;
            this.player2 = player2;
        }


        private void game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
                moveLeft1 = true;
            if (e.Key == Key.D)
                moveRight1 = true;
            if (e.Key == Key.W)
                moveUp1 = true;

            if (e.Key == Key.Left)
                moveLeft2 = true;
            if (e.Key == Key.Right)
                moveRight2 = true;
            if (e.Key == Key.Up)
                moveUp2 = true;

            // Dit moet nog aangepast worden
            if (e.Key == Key.K)
                Win();
            if (e.Key == Key.L)
                Lose();
        }
        private void game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
                moveLeft1 = false;
            if (e.Key == Key.D)
                moveRight1 = false;
            if (e.Key == Key.W)
                moveUp1 = false;

            if (e.Key == Key.Left)
                moveLeft2 = false;
            if (e.Key == Key.Right)
                moveRight2 = false;
            if (e.Key == Key.Up)
                moveUp2 = false;
        }


        private void GameEngine(object sender, EventArgs e)
        {
            if (moveUp1 && Canvas.GetTop(Player1) > 0 && spaceUp1)
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) - playerSpeed);
            if (moveRight1 && Canvas.GetLeft(Player1) + (Player1.Width * 1.5) < 510 && spaceRight1)
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + playerSpeed);
            if (moveLeft1 && Canvas.GetLeft(Player1) > 0 && spaceLeft1)
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) - playerSpeed);
            if (Gravity1)
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) + GravitySpeed);

            if (moveUp2&& Canvas.GetTop(Player2) > 0 && spaceUp2)
                Canvas.SetTop(Player2, Canvas.GetTop(Player2) - playerSpeed);
            if (moveRight2  && Canvas.GetLeft(Player2) + (Player2.Width * 1.5) < 510 && spaceRight2)
                Canvas.SetLeft(Player2, Canvas.GetLeft(Player2) + playerSpeed);
            if (moveLeft2 && Canvas.GetLeft(Player2) > 0 && spaceLeft2)
                Canvas.SetLeft(Player2, Canvas.GetLeft(Player2) - playerSpeed);
            if (Gravity2)
                Canvas.SetTop(Player2, Canvas.GetTop(Player2) + GravitySpeed);


            Gravity1 = true;
            spaceRight1 = true;
            spaceLeft1 = true;
            spaceUp1 = true;

            Gravity2 = true;
            spaceRight2 = true;
            spaceLeft2 = true;
            spaceUp2 = true;

            foreach (var x in game.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "platform")
                {
                    x.Stroke = Brushes.Black;

                    Rect player1HitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);
                    Rect player2HitBox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height);
                    Rect platformHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (player1HitBox.IntersectsWith(platformHitBox))
                    {
                        if (Canvas.GetTop(Player1) < (Canvas.GetTop(x) - (Player1.Height - 1)))
                        {
                            Gravity1 = false;
                        }
                        if (Canvas.GetLeft(Player1) == (Canvas.GetLeft(x) - Player1.Width + 1) && Canvas.GetTop(Player1) > (Canvas.GetTop(x) - Player1.Height))
                        {
                            spaceRight1 = false;
                        }
                        if (Canvas.GetLeft(Player1) == (Canvas.GetLeft(x) + x.Width - 1) && Canvas.GetTop(Player1) > (Canvas.GetTop(x) - Player1.Height))
                        {
                            spaceLeft1 = false;
                        }
                        if (Canvas.GetTop(Player1) == Canvas.GetTop(x) + x.Height - 1)
                        {
                            spaceUp1 = false;
                        }
                    }

                    if (player2HitBox.IntersectsWith(platformHitBox))
                    {
                        if (Canvas.GetTop(Player2) < (Canvas.GetTop(x) - (Player2.Height - 1)))
                        {
                            Gravity2 = false;
                        }
                        if (Canvas.GetLeft(Player2) == (Canvas.GetLeft(x) - Player2.Width + 1) && Canvas.GetTop(Player2) > (Canvas.GetTop(x) - Player2.Height))
                        {
                            spaceRight2 = false;
                        }
                        if (Canvas.GetLeft(Player2) == (Canvas.GetLeft(x) + x.Width - 1) && Canvas.GetTop(Player2) > (Canvas.GetTop(x) - Player2.Height))
                        {
                            spaceLeft2 = false;
                        }
                        if (Canvas.GetTop(Player2) == Canvas.GetTop(x) + x.Height - 1)
                        {
                            spaceUp2 = false;
                        }
                    }
                }
            }
        }

            /// Zorgt er voor dat je naar het win scherm gaat.Stuurt spelerdata (highscore en namen) mee
            /// </summary>
            private void Win()
        {
            Win won = new Win(highscore, teamname, player1, player2);
            won.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Zorgt er voor dat je naar het verlies scherm gaat. Stuurt spelerdata mee.
        /// </summary>
        private void Lose()
        {
            Lose lost = new Lose(highscore, teamname, player1, player2);
            lost.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void QuitGame(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

