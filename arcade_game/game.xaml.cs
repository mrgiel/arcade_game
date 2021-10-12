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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool moveUp2 = false, moveLeft2 = false, moveRight2 = false;
        private bool moveUp1 = false, moveLeft1 = false, moveRight1 = false;

        private bool Gravity1 = true, Gravity2 = true;
        private DispatcherTimer gameTimer = new DispatcherTimer();

        const int playerSpeed = 10;
        const int GravitySpeed = 4;

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

        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameEngine;
            gameTimer.Start();

            game.Focus();
        }

        private void GameEngine(object sender, EventArgs e)
        {
            if (moveUp1 == true && Canvas.GetTop(Player1) > 0)
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) - playerSpeed);
            if (moveRight1 == true && Canvas.GetLeft(Player1) + (Player1.Width * 1.5) < Application.Current.MainWindow.Width)
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) + playerSpeed);
            if (moveLeft1 == true && Canvas.GetLeft(Player1) > 0)
                Canvas.SetLeft(Player1, Canvas.GetLeft(Player1) - playerSpeed);
            if (Gravity1)
                Canvas.SetTop(Player1, Canvas.GetTop(Player1) + GravitySpeed);

            if (moveUp2 == true && Canvas.GetTop(Player2) > 0)
                Canvas.SetTop(Player2, Canvas.GetTop(Player2) - playerSpeed);
            if (moveRight2 == true && Canvas.GetLeft(Player2) + (Player2.Width * 1.5) < Application.Current.MainWindow.Width)
                Canvas.SetLeft(Player2, Canvas.GetLeft(Player2) + playerSpeed);
            if (moveLeft2 == true && Canvas.GetLeft(Player2) > 0)
                Canvas.SetLeft(Player2, Canvas.GetLeft(Player2) - playerSpeed);
            if (Gravity2)
                Canvas.SetTop(Player2, Canvas.GetTop(Player2) + GravitySpeed);


            foreach (var x in game.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "platform")
                {
                    Rect player1HitBox = new Rect(Canvas.GetLeft(Player1), Canvas.GetTop(Player1), Player1.Width, Player1.Height);
                    Rect player2HitBox = new Rect(Canvas.GetLeft(Player2), Canvas.GetTop(Player2), Player2.Width, Player2.Height);
                    Rect platformHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (player1HitBox.IntersectsWith(platformHitBox))
                    {
                        Gravity1 = false;
                        Canvas.SetTop(Player1, Canvas.GetTop(x) - Player1.Height);
                    }
                    else
                    {
                        Gravity1 = true;
                    }
                    if (player2HitBox.IntersectsWith(platformHitBox))
                    {
                        Gravity2 = false;
                        Canvas.SetTop(Player2, Canvas.GetTop(x) - Player2.Height);
                    }
                    else
                    {
                        Gravity2 = true;
                    }
                }
            }
        }
    }
}

