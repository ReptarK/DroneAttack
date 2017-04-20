using AtelierXNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using MyGame.Entités;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.Weapons
{
    public class Spectre : Gun
    {
        Screen Ecran;
        InputManager GestionInput;

        Caméra1stPerson CaméraJeu;

        const string NOM_ARME = "SPECTRE";
        const string NOM_TEXTURE_ARME = "Spectre";
        const string NOM_TEXTURE_SCOPED = "SpectreScoped";

        const string NOM_GUN_SHOT = "SpectreGunShot"; //A CHANGER
        const string NOM_RELOAD = "SpectreReload";

        const int DOMMAGE = 4;
        const int MUNITIONS = 30;
        const int TOTAL_MUNITIONS = 90;
        const float TEMPS_RELOAD = 2.03f;

        public const int PRIX = 1000;

        SoundEffect GunShot;
        SoundEffect ReloadSound;


        public Spectre(Game game)
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
            CaméraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;

            base.Initialize();

            TextureArme = GestionnaireDeTextures.Find(NOM_TEXTURE_ARME);
            TextureScoped = GestionnaireDeTextures.Find(NOM_TEXTURE_SCOPED);

            GunShot = GestionnaireDeSons.Find(NOM_GUN_SHOT);
            ReloadSound = GestionnaireDeSons.Find(NOM_RELOAD);

            ZoneAffichageGun = new Rectangle(Ecran.CenterScreen.X, Ecran.CenterScreen.Y + 50, 400, 200); //A CHANGER
            ZoneAffichageScoped = new Rectangle(Ecran.CenterScreen.X - 276, Ecran.CenterScreen.Y - 14, 400, 250);

            EstReload = false;
        }

        float TempsÉcouléShoot = 0;
        float TempsÉcouléReload = 0;
        public override void GererTirs(GameTime gameTime)
        {
            TempsÉcouléShoot += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            GererReload(gameTime);

            if (GestionInput.EstClicGauche() && CanShoot())
            {
                if (!LocalPlayer.EstScoped)
                {
                    CaméraJeu.Direction += Vector3.UnitY * 0.005f;
                    Game.Components.Add(new FadeTexture(Game, new Rectangle(Ecran.CenterScreen.X + 155, Ecran.CenterScreen.Y + 60, 45, 45), "blast", Color.White, 0.01f));
                }

                GunShot.Play(0.3f, 0, 0);
                LocalPlayer.EstShoot = true;
                Munitions--;
                TempsÉcouléShoot = 0;
            }

        }

        bool CanShoot()
        {
            if ((TempsÉcouléShoot) < 80)
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
                TempsÉcouléReload += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (TempsÉcouléReload > TEMPS_RELOAD)
                {
                    TotalMunitions -= MUNITIONS - Munitions;

                    if (TotalMunitions < MUNITIONS)
                        Munitions = TotalMunitions;
                    else
                        Munitions = MUNITIONS;

                    if (TotalMunitions < 0)
                        TotalMunitions = 0;

                    TempsÉcouléReload = 0;
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
