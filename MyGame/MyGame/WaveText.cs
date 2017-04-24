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
using MyGame.Composants_de_base;

namespace MyGame
{
    public class WaveText : TexteAvantPlan, Overlay
    {
        GameController GameController;

        public WaveText(Game game, string texte, string nomFont, Vector2 position, Color couleur)
            : base(game, texte, nomFont, position, couleur)
        { }

        public override void Initialize()
        {
            base.Initialize();

            DrawOrder = 1000;

            GameController = Game.Services.GetService(typeof(GameController)) as GameController;
        }

        float TempsÉcoulé = 0;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (GameController.PlayState == GameController.InGameState.NewWave && (TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.2f)
            {
                Texte = "Numero Vague : " + GameController.WaveNo.ToString();
                if(GameController.EstWaveSpéciale)
                {
                    Texte = "Vague Speciale";
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
