using AtelierXNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using MyGame.Entit�s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.Weapons
{
    public class Famas : Gun
    {
        Screen Ecran;
        InputManager GestionInput;

        Cam�ra1stPerson Cam�raJeu;

        const string NOM_ARME = "FAMAS";
        const string NOM_TEXTURE_ARME = "Famas";
        const string NOM_TEXTURE_SCOPED = "FamasScoped";

        const string NOM_GUN_SHOT = "FamasGunShot"; //A CHANGER
        const string NOM_RELOAD = "FamasReload";

        const int DOMMAGE = 5;
        const int MUNITIONS = 30;
        const int TOTAL_MUNITIONS = 180;
        const float TEMPS_RELOAD = 2.76f;

        public const int PRIX = 4000;

        SoundEffect GunShot;
        SoundEffect ReloadSound;


        public Famas(Game game)
            : base(game)
        {
            Prix = PRIX;
            Dommage = DOMMAGE;
            Munitions = MUNITIONS;
            TotalMunitions = TOTAL_MUNITIONS;
            MunitionsParLoad = MUNITIONS;
        }

        public override void Initialize()
        {
            Ecran = new Screen(Game);
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
            Cam�raJeu = Game.Services.GetService(typeof(Cam�ra1stPerson)) as Cam�ra1stPerson;

            base.Initialize();

            TextureArme = GestionnaireDeTextures.Find(NOM_TEXTURE_ARME);
            TextureScoped = GestionnaireDeTextures.Find(NOM_TEXTURE_SCOPED);

            GunShot = GestionnaireDeSons.Find(NOM_GUN_SHOT);
            ReloadSound = GestionnaireDeSons.Find(NOM_RELOAD);

            ZoneAffichageGun = new Rectangle(Ecran.CenterScreen.X, Ecran.CenterScreen.Y + 50, 400, 200); //A CHANGER
            ZoneAffichageScoped = new Rectangle(Ecran.CenterScreen.X - 160, Ecran.CenterScreen.Y - 18, 300, 250);
        }

        float Temps�coul�Shoot = 0;
        float Temps�coul�Reload = 0;
        public override void GererTirs(GameTime gameTime)
        {
            Temps�coul�Shoot += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            GererReload(gameTime);

            if (GestionInput.EstClicGauche() && CanShoot())
            {
                if (!LocalPlayer.EstScoped)
                {
                    Cam�raJeu.Direction += Vector3.UnitY * 0.005f;
                    Game.Components.Add(new FadeTexture(Game, new Rectangle(Ecran.CenterScreen.X + 56, Ecran.CenterScreen.Y + 90, 45, 45), "blast", Color.White, 0.01f));
                }

                GunShot.Play(0.3f, 0, 0);
                LocalPlayer.EstShoot = true;
                Munitions--;
                Temps�coul�Shoot = 0;
            }

        }

        bool CanShoot()
        {
            if ((Temps�coul�Shoot) < 80)
                return false;

            if (EstReload)
                return false;

            if (Munitions < 1)
                return false;

            return true;
        }

        void GererReload(GameTime gameTime)
        {
            if (GestionInput.EstNouvelleTouche(Keys.R) && !EstReload && TotalMunitions > MUNITIONS && Munitions != MUNITIONS)
            {
                ReloadSound.Play();
                EstReload = true;
            }

            if (EstReload)
            {
                Temps�coul�Reload += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Temps�coul�Reload > TEMPS_RELOAD)
                {
                    TotalMunitions -= MUNITIONS - Munitions;

                    if (TotalMunitions < MUNITIONS)
                        Munitions = TotalMunitions;
                    else
                        Munitions = MUNITIONS;

                    if (TotalMunitions < 0)
                        TotalMunitions = 0;

                    Temps�coul�Reload = 0;
                    EstReload = false;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
