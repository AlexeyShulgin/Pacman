using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Pacman
{
    interface IGame
    {
        void StartGame(Grid gr, Label scr, object _game);

        void MovePacman(KeyEventArgs e);
    }
}
