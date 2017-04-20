using AtelierXNA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    public interface IController
    {
        void IsVisibleController(MainGame.GameState gameState);
    }
}
