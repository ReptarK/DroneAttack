using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    public class FadeTexture : TextureAvantPlan
    {
        public float Alpha;

        float TempsÉcoulé;
        float FadeTime;

        public FadeTexture(Game game, Rectangle zoneAffichage, string nomTexture, Color couleur, float fadeTime)
            :base(game, zoneAffichage, nomTexture, couleur)
        {
            FadeTime = fadeTime;
        }

        public override void Initialize()
        {
            base.Initialize();
            TempsÉcoulé = 0;
            Alpha = 1f;
            DrawOrder = 99;
        }

        public override void Update(GameTime gameTime)
        {
            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > FadeTime)
            {
                Alpha = Alpha - 0.25f;
                TempsÉcoulé = 0;
            }

            if (Alpha <= 0)
                Game.Components.Remove(this);
        }

        public override void Draw(GameTime gameTime)
        {
            GestionSprites.Begin();
            GestionSprites.Draw(this.TextureAfficher, ZoneAffichage, new Color(Couleur.R, Couleur.G, Couleur.B, Alpha));
            GestionSprites.End();
        }
    }
}
