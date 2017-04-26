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
using AtelierXNA;
using MyGame.Entités;

namespace MyGame.Weapons
{
    public class GunTextureManager : DrawableGameComponent, Overlay
    {
        Screen Ecran;
        protected SpriteBatch GestionSprites { get; private set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }

        public Texture2D TextureAfficher;

        Rectangle ZoneAffichage;
        Rectangle ZoneAffichageGun;
        Rectangle ZoneAffichageScoped;

        Gun MyGun;

        public GunTextureManager(Game game)
            :base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            Ecran = Game.Services.GetService(typeof(Screen)) as Screen;
            GestionSprites = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            MyGun = GameController.MyPlayer.MyGun;

            DrawOrder = 300;
        }

        public override void Update(GameTime gameTime)
        {
            if(MainGame.MainGameState == MainGame.GameState.Jeu)
            {
                if (MyGun != GameController.MyPlayer.MyGun)
                {
                    MyGun = GameController.MyPlayer.MyGun;

                    ZoneAffichageGun = GameController.MyPlayer.MyGun.ZoneAffichageGun;

                    ZoneAffichageScoped = GameController.MyPlayer.MyGun.ZoneAffichageScoped;
                }

                if (MyGun != null)
                {
                    if (LocalPlayer.EstScoped)
                    {
                        ZoneAffichage = ZoneAffichageScoped;
                        TextureAfficher = GameController.MyPlayer.MyGun.TextureScoped;
                    }
                    else
                    {
                        ZoneAffichage = ZoneAffichageGun;
                        TextureAfficher = GameController.MyPlayer.MyGun.TextureArme;
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (MainGame.MainGameState == MainGame.GameState.Jeu && MyGun != null && !MyGun.EstReload && GameController.PlayState != GameController.InGameState.Dead)
            {
                GestionSprites.Begin();
                GestionSprites.Draw(TextureAfficher, ZoneAffichage, Color.White);
                GestionSprites.End();
            }
        }
    }
}
