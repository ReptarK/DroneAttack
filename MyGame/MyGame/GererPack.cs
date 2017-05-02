using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace MyGame
{
    public class GererPack : Microsoft.Xna.Framework.GameComponent
    {
        public GererPack(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            WaveNoTemp = GameController.WaveNo;
        }

        float TeampEcoulé;
        int WaveNoTemp;
        public override void Update(GameTime gameTime)
        {
            if ((TeampEcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                TeampEcoulé = 0;
                if (WaveNoTemp != GameController.WaveNo)
                {
                    WaveNoTemp = GameController.WaveNo;
                    foreach (IPack c in Game.Components.Where(composant => composant is IPack && composant is DrawableGameComponent))
                    {
                        c.EstDétruit = false;
                    }
                }
            }
        }
    }
}
