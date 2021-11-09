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

namespace arcade_game
{
    /// <summary>
    /// Interaction logic for game2.xaml
    /// </summary>
    public partial class Game2 : Window
    {
        // variabele die al een waarde hebben en mee moeten worden gegeven naar andere windows
        public string teamname { get; set; }
        public string player1 { get; set; }
        public string player2 { get; set; }
        public int highscore { get; set; }
        public int seconde { get; set; }
        public int scoreplayer1 { get; set; }
        public int scoreplayer2 { get; set; }

        // variabele voor playermovement
        private bool moveUp2, moveLeft2, moveRight2;
        private bool moveUp1, moveLeft1, moveRight1;

        private bool spaceLeft1, spaceRight1, spaceUp1;
        private bool spaceLeft2, spaceRight2, spaceUp2;

        private bool Gravity1, Gravity2;

        // variabele die frames telt 
        private int teller;

        private DispatcherTimer gameTimer = new DispatcherTimer();

        const int playerSpeed = 2;
        const int GravitySpeed = 1;


        private bool knopdown = false;

        private int jumptime1, jumptime2;

        // variabele voor de deuren 
        bool player1door = false;
        bool player2door = false;

        /// <summary>
        /// in Game word er een gametimer aangemaakt 
        /// de methode word aan geroepen waneer de window word geopend
        /// </summary>
        public Game2(int highscore, string teamname, string player1, string player2, int seconde, int scoreplayer1, int scoreplayer2)
        {
            // gametimer zorgt er voor da gameangine idere 5 ms word aan geroepen
            InitializeComponent();
            gameTimer.Interval = TimeSpan.FromMilliseconds(5);
            gameTimer.Tick += GameEngine;
            gameTimer.Start();
            game2.Focus();

            this.seconde = seconde;
            this.highscore = highscore;
            this.teamname = teamname;
            this.player1 = player1;
            this.player2 = player2;
            this.scoreplayer1 = scoreplayer1;
            this.scoreplayer2 = scoreplayer2;
        }

        /// <summary>
        /// de methode game_KeyDown word aan geroepen als er een keydown is 
        /// </summary>
        private void game2_KeyDown(object sender, KeyEventArgs e)
        {
            //dit zorgt er voor dat de W A S keys werken voor de playermovment van player1 als de key down is 
            if (e.Key == Key.A)
                moveLeft1 = true;
            if (e.Key == Key.D)
                moveRight1 = true;
            if (e.Key == Key.W)
                moveUp1 = true;

            //dit zorgt er voor dat de Up Left Right keys werken voor de playermovment van player2 als de key down is 
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

        /// <summary>
        /// de methode game_KeyUp wordt aangeroepen waneer er een keyup is
        /// </summary>
        private void game2_KeyUp(object sender, KeyEventArgs e)
        {
            // dit zorgt er voor dat als je stopt met de key indrukken dat de player ook stopt met die kan op bewegen 
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

        /// <summary>
        /// de methode GameEngine word iedere 5 ms aangeroepen door de gametimer, hier word alles wat beweegt geregeld
        /// </summary>
        private void GameEngine(object sender, EventArgs e)
        {
            // berekend de highscore die in beeld staat en die je krijgt aan het einde van het spel
            highscore = scoreplayer1 + scoreplayer2 + seconde / 4;
            teller++;

            // heir word de timer die in beeld staat berekend, iedere 200 frames gaat er 1 van seconde af
            if (teller >= 200)
            {
                seconde = seconde - 1;
                teller = 0;
            }


            if (seconde == 0)
            {
                Lose();
            }

            // hier worden moveup1 en moveup2 op false gezet al de players 40 frames of meer omhoog aan het bewegen waren zonder een platform van de zijkant aan te raken of onderkant zodat je niet oneindig kan jumpen
            if (jumptime1 >= 40)
            {
                moveUp1 = false;
            }
            if (jumptime2 >= 40)
            {
                moveUp2 = false;
            }

            // hier worden de labels die rechtboven in het gamewindow staan gevuld zodat je de timer en de score kan zien
            score.Content = "Score: " + highscore;
            klok.Content = "Tijd over: " + seconde;

            // in deze if statemens worden de player movement en word hier gecontroleerd of het keys voor movement zijn ingedrukt en of de player niet links of recht uit het scherm beweegt of dat de players niet tegen een opject aan zitten waar ze niet door heen mogen
            if (moveUp1 && Canvas.GetTop(Player1) > 0 && spaceUp1)
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) - playerSpeed);
                jumptime1++;
            if (moveRight1 && Canvas.GetLeft(Player1) + (Player1.Width * 1.5) < 815 && spaceRight1)
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + playerSpeed);
            if (moveLeft1 && Canvas.GetLeft(Player1) > 0 && spaceLeft1)
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) - playerSpeed);
            if (Gravity1)
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) + GravitySpeed);

            if (moveUp2 && Canvas.GetTop(Player2) > 0 && spaceUp2)
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

            // hier krijgt player1hitbox en player2hitbox de waardenm van de zij en boven en onderkant van de player zodat we kunnen kontroleren of ze eregens tegen aan bewegen
            Rect player1HitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);
            Rect player2HitBox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height);

            // hitboxen voor alle opjecten die ze nodig hebben
            foreach (var x in game2.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "platform")
                {
                    x.Stroke = Brushes.Black;

                    // hier krijgt platformhitbox de waardenm van de zij en boven en onderkant van de platformen zodat we kunnen kontroleren 1 van de players er tegen aan bost
                    Rect platformHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    // deze if statement zorgen er voor dat de players stoppen met bewegen als tegen een platform aan komen
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
                        if (Canvas.GetLeft(Player2) == (Canvas.GetLeft(x) - Player2.Width + 1) && Canvas.GetTop(Player2) > (Canvas.GetTop(x) - Player2.Height))
                        {
                            spaceRight2 = false;
                            jumptime2 = 0;
                        }
                        if (Canvas.GetLeft(Player2) == (Canvas.GetLeft(x) + x.Width - 1) && Canvas.GetTop(Player2) > (Canvas.GetTop(x) - Player2.Height))
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
                // deze if statement regeld de hetboxen voor de opstakels en zorgd er voor dat als je op een knop gaat staan het opstakel verschijnt 
                if ((string)x.Tag == "opstakel")
                {
                    x.Stroke = Brushes.Black;

                    x.Visibility = Visibility.Hidden;

                    if (knopdown == true)
                    {
                        x.Visibility = Visibility.Visible;
                    }
                    if (x.IsVisible)
                    {
                        Rect platformHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                        if (player1HitBox.IntersectsWith(platformHitBox))
                        {
                            if (Canvas.GetTop(Player1) < (Canvas.GetTop(x) - (Player1.Height - 1)))
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
                            if (Canvas.GetLeft(Player2) == (Canvas.GetLeft(x) - Player2.Width + 1) && Canvas.GetTop(Player2) > (Canvas.GetTop(x) - Player2.Height))
                            {
                                spaceRight2 = false;
                                jumptime2 = 0;
                            }
                            if (Canvas.GetLeft(Player2) == (Canvas.GetLeft(x) + x.Width - 1) && Canvas.GetTop(Player2) > (Canvas.GetTop(x) - Player2.Height))
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
                }
                // in deze if statement word er geregistreerd of er een player op een knop staat
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
            foreach (var x in game2.Children.OfType<Image>())
            {
                //deze if statement zorgd er voor dat het spel beeindigd als 1 van de players op een spike staan
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
                //deze if statement zorgd er voor dat de player die op een coin staat een punt krijgt
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
                        }
                    }
                }
            }

            // hier krijgen de deuren ook een hitbox zodat waneer de players voor de goede deuren staan ze naar het vlgende level gaan
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

        /// <summary>
        /// dede methode word aangeroen waneer de player de levels heeft uitgespeeld
        /// Zorgt er voor dat je naar het volgende level gaat
        /// </summary>
        private void Win()
        {
            Win won = new Win(highscore, teamname, player1, player2, scoreplayer1, scoreplayer2);
            won.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
            gameTimer.Stop();
        }

        /// <summary>
        /// dede methode word aangeroen waneer de player op een spike staat of als de timer om is
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

        /// <summary>
        /// methode om de game te restarten 
        /// </summary>
        private void Restart(object sender, RoutedEventArgs e)
        {
            StartGame startgame = new StartGame();
            startgame.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }

        private void Main(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Hidden;
        }
    }

}
