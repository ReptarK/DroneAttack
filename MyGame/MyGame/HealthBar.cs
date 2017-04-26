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
using AtelierXNA;

namespace MyGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class HealthBar : TextureAvantPlan
    {
        Screen Ecran;
        Texture2D TextureBack;
        LocalPlayer MyPlayer;

        Rectangle ZoneBase;
        Rectangle ZoneHealth;

        int health;

        public HealthBar(Game game, Rectangle zoneAffichage, string nomTexture, Color couleur)
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

            Ecran = new Screen(Game);
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;

            health = MyPlayer.Health;

            ZoneBase = new Rectangle(17, (int)(Ecran.CenterScreen.Y * 1.9f) - 3, 104, 16);
            ZoneHealth = new Rectangle(20, (int)(Ecran.CenterScreen.Y * 1.9f), (int)(100 * (health / 100f)), 10);
            ZoneBase.Width *= (int)Ecran.ScreenScale.X;
            ZoneBase.Height *= (int)Ecran.ScreenScale.Y;
            ZoneHealth.Width *= (int)Ecran.ScreenScale.X;
            ZoneHealth.Height *= (int)Ecran.ScreenScale.Y;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            TextureBack = GestionnaireDeTextures.Find(NomTexture);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(health != MyPlayer.Health)
            {
                health = MyPlayer.Health;

                ZoneHealth = new Rectangle(20, (int)(Ecran.CenterScreen.Y * 1.9f), (int)(100 * (health / 100f)), 10);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GestionSprites.Begin();
            GestionSprites.Draw(TextureBack, ZoneBase, Color.Black);
            GestionSprites.Draw(TextureAfficher, ZoneHealth, Couleur);
            GestionSprites.End();
        }
    }
}
