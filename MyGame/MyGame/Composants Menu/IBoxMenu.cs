using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.Composants_Menu
{
    interface IBoxMenu
    {
        Action<Game> OnClick { get; set; }

        bool bEstClické { get; set; }
    }
}
