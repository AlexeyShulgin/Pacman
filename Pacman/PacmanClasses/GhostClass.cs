using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Pacman
{
    class GhostClass
    {
        public Image Blinky = new Image(), Pinky = new Image(), Inky = new Image(), Clyde = new Image(), Sleep = new Image();
        private BitmapImage bmp1 = new BitmapImage(), bmp2 = new BitmapImage(), bmp3 = new BitmapImage(), bmp4 = new BitmapImage(), bmp5 = new BitmapImage();

        public void LoadGhosts(Grid gr)
        {
            bmp1.BeginInit();
            bmp1.UriSource = new Uri("pack://application:,,,/Resources/Blinky.png");
            bmp1.EndInit();
            bmp2.BeginInit();
            bmp2.UriSource = new Uri("pack://application:,,,/Resources/Pinky.png");
            bmp2.EndInit();
            bmp3.BeginInit();
            bmp3.UriSource = new Uri("pack://application:,,,/Resources/Inky.png");
            bmp3.EndInit();
            bmp4.BeginInit();
            bmp4.UriSource = new Uri("pack://application:,,,/Resources/Clyde.png");
            bmp4.EndInit();
            bmp5.BeginInit();
            bmp5.UriSource = new Uri("pack://application:,,,/Resources/Sleep.png");
            bmp5.EndInit();
            Blinky.Source = bmp1;
            Blinky.Margin = new Thickness(180, 180, 420, 420);
            gr.Children.Add(Blinky);
            Pinky.Source = bmp2;
            Pinky.Margin = new Thickness(420, 180, 180, 420);
            gr.Children.Add(Pinky);
            Inky.Source = bmp3;
            Inky.Margin = new Thickness(180, 420, 420, 180);
            gr.Children.Add(Inky);
            Clyde.Source = bmp4;
            Clyde.Margin = new Thickness(420, 420, 180, 180);
            gr.Children.Add(Clyde);
            Sleep.Source = bmp5;
        }

        public void Ghosts(Grid gr, object _game, object _pmc)
        {
            Game game = (Game)_game;
            Thread.Sleep(500);

            Random rnd = new Random();
            while (!game.is_lose && game.count_coins > 0)
            {
                if(!game.GhostsIsSleep)
                {
                    Thread.Sleep(50);
                    #region Перемещение Ghosts
                    GhostMove(Blinky, game, _pmc);
                    GhostMove(Pinky, game, _pmc);
                    GhostMove(Inky, game, _pmc);
                    GhostMove(Clyde, game, _pmc);

                    if (game.is_lose)
                    {
                        return;
                    }
                    #endregion;
                }
                else
                {
                    Blinky.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        Blinky.Source = bmp5;
                        Pinky.Source = bmp5;
                        Inky.Source = bmp5;
                        Clyde.Source = bmp5;
                    }));
                    Thread.Sleep(8000);
                    Blinky.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        Blinky.Source = bmp1;
                        Pinky.Source = bmp2;
                        Inky.Source = bmp3;
                        Clyde.Source = bmp4;
                    }));
                    game.GhostsIsSleep = false;
                }
            }
        }

        private void GhostMove(Image Ghost, object _game, object _pmc)
        {
            Game game = (Game)_game;
            PacmanClass pmc = (PacmanClass)_pmc;

            Thread.Sleep(8);
            Random rnd = new Random();
            switch (rnd.Next(1, 5))
            {
                case 1:
                    Ghost.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        if (Ghost.Margin.Top - 10 >= 0)
                        {
                            int x1 = (int)(Ghost.Margin.Left / 30);
                            int y = (int)((Ghost.Margin.Top - 10) / 30);
                            int x2 = (int)((Ghost.Margin.Left + 29) / 30);

                            if ((game.map[y, x1] == '0' || game.map[y, x1] == '2' || game.map[y, x1] == '3') && (game.map[y, x2] == '0' || game.map[y, x2] == '2' || game.map[y, x2] == '3'))
                            {
                                if (Ghost.Margin.Top + 10 <= (630 - pmc.ImgPacman.Margin.Bottom) && Ghost.Margin.Top >= pmc.ImgPacman.Margin.Top && (x1 == (int)(pmc.ImgPacman.Margin.Left / 30) || x2 == (int)((pmc.ImgPacman.Margin.Left + 25) / 30)))
                                {
                                    game.is_lose = true;
                                    game.IsWin();
                                    return;
                                }

                                Ghost.Margin = new Thickness(Ghost.Margin.Left, Ghost.Margin.Top - 10, Ghost.Margin.Right, Ghost.Margin.Bottom + 10);
                            }
                        }
                    }));
                    break;
                case 2:
                    Ghost.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        if (Ghost.Margin.Bottom - 10 >= 0)
                        {
                            int x1 = (int)(Ghost.Margin.Left / 30);
                            int y = (int)((Ghost.Margin.Top + 30) / 30);
                            int x2 = (int)((Ghost.Margin.Left + 29) / 30);

                            if ((game.map[y, x1] == '0' || game.map[y, x1] == '2' || game.map[y, x1] == '3') && (game.map[y, x2] == '0' || game.map[y, x2] == '2' || game.map[y, x2] == '3'))
                            {
                                if (Ghost.Margin.Bottom + 10 <= (630 - pmc.ImgPacman.Margin.Top) && Ghost.Margin.Bottom >= pmc.ImgPacman.Margin.Bottom && (x1 == (int)(pmc.ImgPacman.Margin.Left / 30) || x2 == (int)((pmc.ImgPacman.Margin.Left + 29) / 30)))
                                {
                                    game.is_lose = true;
                                    game.IsWin();
                                    return;
                                }

                                Ghost.Margin = new Thickness(Ghost.Margin.Left, Ghost.Margin.Top + 10, Ghost.Margin.Right, Ghost.Margin.Bottom - 10);
                            }
                        }
                    }));
                    break;
                case 3:
                    Ghost.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        if (Ghost.Margin.Right - 10 >= 0)
                        {
                            int x = (int)((Ghost.Margin.Left + 30) / 30);
                            int y1 = (int)(Ghost.Margin.Top / 30);
                            int y2 = (int)((Ghost.Margin.Top + 29) / 30);

                            if ((game.map[y1, x] == '0' || game.map[y1, x] == '2' || game.map[y1, x] == '3') && (game.map[y2, x] == '0' || game.map[y2, x] == '2' || game.map[y2, x] == '3'))
                            {
                                if (Ghost.Margin.Right + 10 <= (630 - pmc.ImgPacman.Margin.Left) && Ghost.Margin.Right >= pmc.ImgPacman.Margin.Right && (y1 == (int)(pmc.ImgPacman.Margin.Top / 30) || y2 == (int)((pmc.ImgPacman.Margin.Top + 25) / 30)))
                                {
                                    game.is_lose = true;
                                    game.IsWin();
                                    return;
                                }

                                Ghost.Margin = new Thickness(Ghost.Margin.Left + 10, Ghost.Margin.Top, Ghost.Margin.Right - 10, Ghost.Margin.Bottom);
                            }
                        }
                    }));
                    break;
                case 4:
                    Ghost.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        if (Ghost.Margin.Left - 10 >= 0)
                        {
                            int x = (int)((Ghost.Margin.Left - 10) / 30);
                            int y1 = (int)(Ghost.Margin.Top / 30);
                            int y2 = (int)((Ghost.Margin.Top + 29) / 30);

                            if ((game.map[y1, x] == '0' || game.map[y1, x] == '2' || game.map[y1, x] == '3') && (game.map[y2, x] == '0' || game.map[y2, x] == '2' || game.map[y2, x] == '3'))
                            {
                                if (Ghost.Margin.Left + 10 <= (630 - pmc.ImgPacman.Margin.Right) && Ghost.Margin.Left >= pmc.ImgPacman.Margin.Left && (y1 == (int)(pmc.ImgPacman.Margin.Top / 30) || y2 == (int)((pmc.ImgPacman.Margin.Top + 25) / 30)))
                                {
                                    game.is_lose = true;
                                    game.IsWin();
                                    return;
                                }

                                Ghost.Margin = new Thickness(Ghost.Margin.Left - 10, Ghost.Margin.Top, Ghost.Margin.Right + 10, Ghost.Margin.Bottom);
                            }
                        }
                    }));
                    break;
            }
        }
    }
}
