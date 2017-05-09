using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.Composants_Menu
{
    public interface IMenu
    {
        void ToggleMenu(MenuController.MenuState menuState);
    }
}
