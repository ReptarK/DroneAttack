using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierXNA
{
    public class Screen
    {
        Game Game;
        public Rectangle Size { get { return Game.Window.ClientBounds; } }

        public Vector2 Resolution { get; private set; }

        public Point CenterScreen { get { return new Point(Size.Width / 2, Size.Height / 2); } }

        public Screen(Game game)
        {
            Game = game;
            Resolution = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                                     GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
        }
    }
}
