using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace Pacman
{
    class Game : IGame
    {
        PacmanClass pmc = new PacmanClass();
        GhostClass ghc = new GhostClass();
        Game game;

        public bool GhostsIsSleep = false;
        public char[,] map = new char[21, 21];
        public Image[,] coins = new Image[21, 21];
        public int count_coins = 0;
        public int count_coins_all = 0;
        public bool is_lose = false;
        public Label _Score = null;
        private Grid _Grid = null;

        public void StartGame(Grid gr, Label scr, object _game)
        {
            game = (Game)_game;
            _Grid = gr;
            _Score = scr;
            BuildMap();
            pmc.BuildPacman(gr);
            ghc.LoadGhosts(gr);
            Task.Run(() => { ghc.Ghosts(gr, game, pmc); });
        }

        private void BuildMap()
        {
            StreamResourceInfo fileInfo = Application.GetResourceStream(new Uri("pack://application:,,,/Resources/1.txt"));
            StreamReader sr = new StreamReader(fileInfo.Stream);
            BitmapImage bmp1 = new BitmapImage(), bmp0 = new BitmapImage(), bmp3 = new BitmapImage();
            bmp1.BeginInit();
            bmp1.UriSource = new Uri("pack://application:,,,/Resources/Block.bmp");
            bmp1.EndInit();
            bmp0.BeginInit();
            bmp0.UriSource = new Uri("pack://application:,,,/Resources/Coin.png");
            bmp0.EndInit();
            bmp3.BeginInit();
            bmp3.UriSource = new Uri("pack://application:,,,/Resources/MagicCoin.png");
            bmp3.EndInit();
            for (int i = 0; i < 21; i++)
                for (int j = 0; j < 21; j++)
                {
                    map[i, j] = (char)sr.Read();
                    switch (map[i, j])
                    {
                        case '1':
                            Image imgBlock = new Image();
                            imgBlock.Source = bmp1;
                            imgBlock.Margin = new Thickness(j * 30, i * 30, _Grid.Width - (j * 30) - 30, _Grid.Height - (i * 30) - 30);
                            _Grid.Children.Add(imgBlock);
                            break;
                        case '3':
                            Image imgCoin2 = new Image();
                            imgCoin2.Source = bmp3;
                            imgCoin2.Margin = new Thickness((j * 30), (i * 30), _Grid.Width - (j * 30) - 30, _Grid.Height - (i * 30) - 30);
                            _Grid.Children.Add(imgCoin2);
                            coins[i, j] = imgCoin2;
                            count_coins++;
                            break;
                        case '0':
                            Image imgCoin1 = new Image();
                            imgCoin1.Source = bmp0;
                            imgCoin1.Margin = new Thickness((j * 30) + 10, (i * 30) + 10, _Grid.Width - (j * 30) - 20, _Grid.Height - (i * 30) - 20);
                            _Grid.Children.Add(imgCoin1);
                            coins[i, j] = imgCoin1;
                            count_coins++;
                            break;
                    }
                }
            sr.Close();
            count_coins_all = count_coins;
        }

        private void RestartGame()
        {
            count_coins = 0;
            for (int i = 0; i < 21; i++)
                for (int j = 0; j < 21; j++)
                    if (map[i, j] == '0' || map[i, j] == '3')
                    {
                        count_coins++;
                        coins[i, j].Visibility = Visibility.Visible;
                    }

            ghc.Blinky.Margin = new Thickness(180, 180, 420, 420);
            ghc.Pinky.Margin = new Thickness(420, 180, 180, 420);
            ghc.Inky.Margin = new Thickness(180, 420, 420, 180);
            ghc.Clyde.Margin = new Thickness(420, 420, 180, 180);

            pmc.ImgPacman.Margin = new Thickness(300, 300, 300, 300);
            pmc.ImgPacman.RenderTransform = new RotateTransform(0);
            Task.Run(() => { ghc.Ghosts(_Grid, game, pmc); });
            is_lose = false;
            _Score.Content = "Score: 0";
        }

        public void IsWin()
        {
            if (count_coins == 0 && !is_lose)
                if (MessageBox.Show("Вы выиграли!\nПоиграем ещё?", "Pac-Man \u263a", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    RestartGame();
                else
                    Application.Current.Shutdown();
            else if (is_lose)
                if (MessageBox.Show("Вы проиграли!\nНачать с начала?", "Pac-Man \u2639", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    RestartGame();
                else
                    Application.Current.Shutdown();
        }

        public void MovePacman(KeyEventArgs e)
        {
            pmc.MovePacman(_Grid, e, game);
        }
    }
}
