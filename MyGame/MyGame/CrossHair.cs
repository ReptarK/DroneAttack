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
using MyGame.Entités;

namespace MyGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class CrossHair : TextureAvantPlan
    {
        public CrossHair(Game game, Rectangle zoneAffichage, string nomTexture, Color couleur)
            : base(game, zoneAffichage, nomTexture, couleur)
        {

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        float TempsÉcoulé = 0;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.7f)
            {
                Couleur = Data.TableauCouleur[Data.CouleurIndex];
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (!LocalPlayer.EstScoped)
            {
                GestionSprites.Begin();
                GestionSprites.Draw(TextureAfficher, ZoneAffichage, Couleur);
                GestionSprites.End();
            }
        }
    }
}
