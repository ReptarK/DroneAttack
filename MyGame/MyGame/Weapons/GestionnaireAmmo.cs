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
    public class GestionnaireAmmo : TextureAvantPlan, Overlay
    {
        Screen Ecran;
        LocalPlayer MyPlayer;

        RessourcesManager<SpriteFont> GestionnaireDeFont;
        SpriteFont FontAmmo;

        int ammo;
        int totalAmmo;
        Vector2 TextureSize;

        Vector2 FontSize;

        Point AmmoFirstPoint;

        public GestionnaireAmmo(Game game, Rectangle zoneAffichage, string nomTexture, Color couleur)
            : base(game, zoneAffichage, nomTexture, couleur)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            Ecran = new Screen(Game);
            TempsÉcoulé = 0;
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;
            GestionnaireDeFont = Game.Services.GetService(typeof(RessourcesManager<SpriteFont>)) as RessourcesManager<SpriteFont>;

            TextureSize = new Vector2(4, 22);

            AmmoFirstPoint = new Point((int)(Ecran.CenterScreen.X * 1.9f), (int)(Ecran.CenterScreen.Y * 1.9f));
            FontAmmo = GestionnaireDeFont.Find("Lindsey");
            FontSize = FontAmmo.MeasureString(totalAmmo.ToString());
        }

        float TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.1f && MyPlayer.MyGun != null)
            {
                ammo = MyPlayer.MyGun.Munitions;
                totalAmmo = (MyPlayer.MyGun.TotalMunitions- MyPlayer.MyGun.MunitionsParLoad) + MyPlayer.MyGun.Munitions;
                if (totalAmmo <= 0)
                {
                    totalAmmo = 0;
                }
                    FontSize = FontAmmo.MeasureString(totalAmmo.ToString());
                
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GestionSprites.Begin();

            for (int i = 0; i < ammo; i++)
            {
                GestionSprites.Draw(TextureAfficher, new Rectangle((int)(AmmoFirstPoint.X - (i * 2 * TextureSize.X)), AmmoFirstPoint.Y,
                                                                   (int)TextureSize.X, (int)TextureSize.Y), Couleur);
            }
            GestionSprites.DrawString(FontAmmo, totalAmmo.ToString(), new Vector2(AmmoFirstPoint.X - FontSize.X, AmmoFirstPoint.Y - FontSize.Y), Color.Yellow);
            GestionSprites.End();
        }
    }
}
