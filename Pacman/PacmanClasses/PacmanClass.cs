using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pacman
{
    class PacmanClass
    {
        public Image ImgPacman = new Image();

        public void BuildPacman(Grid gr)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri("pack://application:,,,/Resources/Pacman.png");
            bmp.EndInit();
            ImgPacman.Source = bmp;
            ImgPacman.Margin = new Thickness(300, 300, 300, 300);
            ImgPacman.RenderTransformOrigin = new Point(0.5, 0.5);
            gr.Children.Add(ImgPacman);
        }

        public void MovePacman(Grid gr, KeyEventArgs e, object _game)
        {
            Game game = (Game)_game;
            switch (e.Key)
            {
                case Key.Up:
                    ImgPacman.RenderTransform = new RotateTransform(90);
                    if (ImgPacman.Margin.Top - 10 >= 0)
                    {
                        int x1 = (int)(ImgPacman.Margin.Left / 30);
                        int y = (int)((ImgPacman.Margin.Top - 10) / 30);
                        int x2 = (int)((ImgPacman.Margin.Left + 25) / 30);

                        if ((game.map[y, x1] == '0' || game.map[y, x1] == '2' || game.map[y, x1] == '3') && (game.map[y, x2] == '0' || game.map[y, x2] == '2' || game.map[y, x2] == '3'))
                        {
                            if ((game.map[y + 1, x1] == '0' || game.map[y + 1, x1] == '3') && game.coins[y + 1, x1].Visibility == Visibility.Visible)
                            {
                                if (game.map[y + 1, x1] == '3' || game.map[y + 1, x2] == '3')
                                    game.GhostsIsSleep = true;
                                game.coins[y + 1, x1].Visibility = Visibility.Hidden;
                                game.count_coins--;
                                game._Score.Content = String.Format("Score: {0}", (game.count_coins_all - game.count_coins) * 10);
                                game.IsWin();
                            }
                            ImgPacman.Margin = new Thickness(ImgPacman.Margin.Left, ImgPacman.Margin.Top - 10, ImgPacman.Margin.Right, ImgPacman.Margin.Bottom + 10);
                        }
                    }
                    break;
                case Key.Down:
                    ImgPacman.RenderTransform = new RotateTransform(-90);
                    if (ImgPacman.Margin.Bottom - 10 >= 0)
                    {
                        int x1 = (int)(ImgPacman.Margin.Left / 30);
                        int y = (int)((ImgPacman.Margin.Top + 30) / 30);
                        int x2 = (int)((ImgPacman.Margin.Left + 25) / 30);

                        if ((game.map[y, x1] == '0' || game.map[y, x1] == '2' || game.map[y, x1] == '3') && (game.map[y, x2] == '0' || game.map[y, x2] == '2' || game.map[y, x2] == '3'))
                        {
                            if ((game.map[y - 1, x1] == '0' || game.map[y - 1, x1] == '3') && game.coins[y - 1, x1].Visibility == Visibility.Visible)
                            {
                                if (game.map[y - 1, x1] == '3' || game.map[y - 1, x2] == '3')
                                    game.GhostsIsSleep = true;
                                game.coins[y - 1, x1].Visibility = Visibility.Hidden;
                                game.count_coins--;
                                game._Score.Content = String.Format("Score: {0}", (game.count_coins_all - game.count_coins) * 10);
                                game.IsWin();
                            }
                            ImgPacman.Margin = new Thickness(ImgPacman.Margin.Left, ImgPacman.Margin.Top + 10, ImgPacman.Margin.Right, ImgPacman.Margin.Bottom - 10);
                        }
                    }
                    break;
                case Key.Right:
                    ImgPacman.RenderTransform = new RotateTransform(180);
                    if (ImgPacman.Margin.Right - 10 >= 0)
                    {
                        int x = (int)((ImgPacman.Margin.Left + 30) / 30);
                        int y1 = (int)(ImgPacman.Margin.Top / 30);
                        int y2 = (int)((ImgPacman.Margin.Top + 25) / 30);

                        if ((game.map[y1, x] == '0' || game.map[y1, x] == '2' || game.map[y1, x] == '3') && (game.map[y2, x] == '0' || game.map[y2, x] == '2' || game.map[y2, x] == '3'))
                        {
                            if ((game.map[y1, x - 1] == '0' || game.map[y1, x - 1] == '3') && game.coins[y1, x - 1].Visibility == Visibility.Visible)
                            {
                                if (game.map[y1, x - 1] == '3' || game.map[y2, x - 1] == '3')
                                    game.GhostsIsSleep = true;
                                game.coins[y1, x - 1].Visibility = Visibility.Hidden;
                                game.count_coins--;
                                game._Score.Content = String.Format("Score: {0}", (game.count_coins_all - game.count_coins) * 10);
                                game.IsWin();
                            }
                            ImgPacman.Margin = new Thickness(ImgPacman.Margin.Left + 10, ImgPacman.Margin.Top, ImgPacman.Margin.Right - 10, ImgPacman.Margin.Bottom);
                        }
                    }
                    break;
                case Key.Left:
                    ImgPacman.RenderTransform = new RotateTransform(0);
                    if (ImgPacman.Margin.Left - 10 >= 0)
                    {
                        int x = (int)((ImgPacman.Margin.Left - 10) / 30);
                        int y1 = (int)(ImgPacman.Margin.Top / 30);
                        int y2 = (int)((ImgPacman.Margin.Top + 25) / 30);

                        if ((game.map[y1, x] == '0' || game.map[y1, x] == '2' || game.map[y1, x] == '3') && (game.map[y2, x] == '0' || game.map[y2, x] == '2' || game.map[y2, x] == '3'))
                        {
                            if ((game.map[y1, x + 1] == '0' || game.map[y1, x + 1] == '3') && game.coins[y1, x + 1].Visibility == Visibility.Visible)
                            {
                                if (game.map[y1, x + 1] == '3' || game.map[y2, x + 1] == '3')
                                    game.GhostsIsSleep = true;
                                game.coins[y1, x + 1].Visibility = Visibility.Hidden;
                                game.count_coins--;
                                game._Score.Content = String.Format("Score: {0}", (game.count_coins_all - game.count_coins) * 10);
                                game.IsWin();
                            }
                            ImgPacman.Margin = new Thickness(ImgPacman.Margin.Left - 10, ImgPacman.Margin.Top, ImgPacman.Margin.Right + 10, ImgPacman.Margin.Bottom);
                        }
                    }
                    break;
            }
        }
    }
}
