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
        public int seconde { get; set; }

        // variabele voor playermovment
        private bool moveUp2, moveLeft2, moveRight2;
        private bool moveUp1, moveLeft1, moveRight1;

        private bool spaceLeft1, spaceRight1, spaceUp1;
        private bool spaceLeft2, spaceRight2, spaceUp2;

        private bool Gravity1, Gravity2;
        private DispatcherTimer gameTimer = new DispatcherTimer();

        const int playerSpeed = 2;
        const int GravitySpeed = 1;

        private bool knopdown = false;

        // variabele die frames telt 
        private int teller;

        private int jumptime1, jumptime2;

        private int scoreplayer1, scoreplayer2;

        bool player1door = false;
        bool player2door = false;

        public Game(int highscore, string teamname, string player1, string player2)
        {
            InitializeComponent();
            gameTimer.Interval = TimeSpan.FromMilliseconds(5);
            gameTimer.Tick += GameEngine;
            gameTimer.Start();
            game.Focus();

            seconde = 30;

            this.highscore = highscore;
            this.teamname = teamname;
            this.player1 = player1;
            this.player2 = player2;
        }


        private void game_KeyDown(object sender, KeyEventArgs e)
        {
            //player1 movment
            if (e.Key == Key.A)
                moveLeft1 = true;
            if (e.Key == Key.D)
                moveRight1 = true;
            if (e.Key == Key.W)
                moveUp1 = true;

            //player2 movment
            if (e.Key == Key.Left)
                moveLeft2 = true;
            if (e.Key == Key.Right)
                moveRight2 = true;
            if (e.Key == Key.Up)
                moveUp2 = true;

            if (e.Key == Key.K)
                Win();
            if (e.Key == Key.L)
                Lose();
        }
        private void game_KeyUp(object sender, KeyEventArgs e)
        {
            //player1 movment
            if (e.Key == Key.A)
                moveLeft1 = false;
            if (e.Key == Key.D)
                moveRight1 = false;
            if (e.Key == Key.W)
                moveUp1 = false;

            //player2 movment
            if (e.Key == Key.Left)
                moveLeft2 = false;
            if (e.Key == Key.Right)
                moveRight2 = false;
            if (e.Key == Key.Up)
                moveUp2 = false;
        }


        private void GameEngine(object sender, EventArgs e)
        {
            highscore = scoreplayer1 + scoreplayer2 + seconde / 4;
            teller++;

            if (teller >= 200)
            {
                seconde = seconde - 1;
                teller = 0;
            }

            if (seconde == 0)
            {
                Lose();
            }

            if (jumptime1 >= 40)
            {
                moveUp1 = false;
            }
            if (jumptime2 >= 40)
            {
                moveUp2 = false;
            }

            score.Content = "Score: " + highscore;
            klok.Content = "Tijd over: " + seconde;

            //player1 movment
            if (moveUp1 && Canvas.GetTop(Player1) > 0 && spaceUp1)
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) - playerSpeed);
                jumptime1++;
            if (moveRight1 && Canvas.GetLeft(Player1) + (Player1.Width * 1.5) < 815 && spaceRight1)
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + playerSpeed);
            if (moveLeft1 && Canvas.GetLeft(Player1) > 0 && spaceLeft1)
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) - playerSpeed);
            if (Gravity1)
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) + GravitySpeed);

            //player2 movment
            if (moveUp2&& Canvas.GetTop(Player2) > 0 && spaceUp2)
                Canvas.SetTop(Player2, Canvas.GetTop(Player2) - playerSpeed);
                jumptime2++;
            if (moveRight2 && Canvas.GetLeft(Player2) + (Player2.Width * 1.5) < 815 && spaceRight2)
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

            knopdown = false;

            Rect player1HitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);
            Rect player2HitBox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height);

            //telst frames en zorgd voor timer
            foreach (var x in game.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "platform")
                {
                    x.Stroke = Brushes.Black;

                    Rect platformHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (player1HitBox.IntersectsWith(platformHitBox))
                    {
                        if (Canvas.GetTop(Player1) < (Canvas.GetTop(x) - (Player1.Height - 2)))
                        {
                            Gravity1 = false;
                            jumptime1 = 0;
                        }
                        if (Canvas.GetLeft(Player1) == (Canvas.GetLeft(x) - Player1.Width + 1) && Canvas.GetTop(Player1) > (Canvas.GetTop(x) - Player1.Height))
                        {
                            spaceRight1 = false;
                            jumptime1 = 0;
                        }
                        if (Canvas.GetLeft(Player1) == (Canvas.GetLeft(x) + x.Width - 1) && Canvas.GetTop(Player1) > (Canvas.GetTop(x) - Player1.Height))
                        {
                            spaceLeft1 = false;
                            jumptime1 = 0;
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
                            jumptime2 = 0;
                        }
                        if (Canvas.GetLeft(Player2) == (Canvas.GetLeft(x) - Player2.Width + 2) && Canvas.GetTop(Player2) > (Canvas.GetTop(x) - Player2.Height))
                        {
                            spaceRight2 = false;
                            jumptime2 = 0;
                        }
                        if (Canvas.GetLeft(Player2) == (Canvas.GetLeft(x) + x.Width - 2) && Canvas.GetTop(Player2) > (Canvas.GetTop(x) - Player2.Height))
                        {
                            spaceLeft2 = false;
                            jumptime2 = 0;
                        }
                        if (Canvas.GetTop(Player2) == Canvas.GetTop(x) + x.Height - 1)
                        {
                            spaceUp2 = false;
                        }
                    }
                }
                if ((string)x.Tag == "opstakel")
                {
                    x.Stroke = Brushes.Black;

                    x.Visibility = Visibility.Visible;

                    if (knopdown == true)
                    {
                        x.Visibility = Visibility.Hidden;
                    }
                    if (x.IsVisible)
                    {
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
                if ((string)x.Tag == "knop")
                {
                    Rect knopHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (player1HitBox.IntersectsWith(knopHitBox))
                    {
                        knopdown = true;
                    }
                    if (player2HitBox.IntersectsWith(knopHitBox))
                    {
                        knopdown = true;
                    }
                }

            }
            foreach (var x in game.Children.OfType<Image>())
            {
                if ((string)x.Tag == "spike")
                {
                    Rect spikeHitBox = new Rect((Canvas.GetLeft(x) + 2), (Canvas.GetTop(x) + 13), (x.Width - 4), x.Height);
                    if (player1HitBox.IntersectsWith(spikeHitBox))
                    {
                        Lose();
                    }
                    if (player2HitBox.IntersectsWith(spikeHitBox))
                    {
                        Lose();
                    }
                }
                if ((string)x.Tag == "coin")
                {
                    Rect coinHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (player1HitBox.IntersectsWith(coinHitBox))
                    {
                        if (x.IsVisible)
                        {

                            x.Visibility = Visibility.Hidden;
                            scoreplayer1 += 1;
                        }
                    }
                    if (player2HitBox.IntersectsWith(coinHitBox))
                    {
                        if (x.IsVisible)
                        {

                            x.Visibility = Visibility.Hidden;
                            scoreplayer2 += 1;
                            highscore += 1;
                        }
                    }
                }
            }

            Rect doorbleuHitBox = new Rect(Canvas.GetLeft(doorbleu), Canvas.GetTop(doorbleu), doorbleu.Width, doorbleu.Height);
            Rect doorredHitBox = new Rect(Canvas.GetLeft(doorred), Canvas.GetTop(doorred), doorred.Width, doorred.Height);

            if (player1HitBox.IntersectsWith(doorbleuHitBox))
            {
                player1door = true;
            }
            if (player2HitBox.IntersectsWith(doorredHitBox))
            {
                player2door = true;
            }
            if (player1door && (player2door))
            {
                Win();
            }
        }

        /// Zorgt er voor dat je naar het win scherm gaat.Stuurt spelerdata (highscore en namen) mee
        /// </summary>
        private void Win()
        {
            Game2 game2 = new Game2(highscore, teamname, player1, player2, seconde, scoreplayer1, scoreplayer2);
            game2.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
            gameTimer.Stop();
        }

        /// <summary>
        /// Zorgt er voor dat je naar het verlies scherm gaat. Stuurt spelerdata mee.
        /// </summary>
        private void Lose()
        {
            Lose lost = new Lose(highscore, teamname, player1, player2, scoreplayer1, scoreplayer2);
            lost.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
            gameTimer.Stop();
        }

        private void QuitGame(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            StartGame startgame = new StartGame();
            startgame.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
        private void MainWindow(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
    }
}