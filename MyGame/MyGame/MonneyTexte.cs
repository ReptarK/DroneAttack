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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MonneyTexte : TexteAvantPlan, Overlay
    {
        int Monney;
        public MonneyTexte(Game game, string texte, string nomFont, Vector2 position, Color couleur)
            : base(game, texte, nomFont, position, couleur)
        {
            Monney = GameController.MyPlayer.Monney;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            TempsÉcoulé = 0;
        }

        float TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            if((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.5f)
            {
                if(Monney != GameController.MyPlayer.Monney)
                {
                    Monney = GameController.MyPlayer.Monney;

                    Texte = "Argent : " + Monney.ToString();
                }
            }
        }
    }
}
