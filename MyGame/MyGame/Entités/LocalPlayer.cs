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
using MyGame.Weapons;
using MyGame.Composants_de_base;

namespace MyGame.Entités
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class LocalPlayer : Entity
    {
        RessourcesManager<SoundEffect> GestionnaireDeSons;
        List<SoundEffect> ListeWalking;
        SoundEffect JumpSound;

        Screen Ecran;
        InputManager GestionInput;
        Caméra1stPerson CaméraJeu;
        Random GenerateurRandom;

        public static bool EstShoot;
        public static bool EstScoped;

        public int Monney;
        public Gun MyGun;
        public Gun AncienGun;

        public int NbKillDrones;

        MonneyTexte TexteArgent;
        HealthBar BarreDeVie;
        GestionnaireAmmo GestionnaireDeMunitions;

        int health;
        public int Health
        {
            get { return health; }
            set
            {
                if (value > 100)
                    value = 100;
                if(value <= 0)
                {
                    value = 0;
                    GameController.PlayState = GameController.InGameState.Dead;
                }

                health = value;
            }
        }

        Ray playerRay;
        public Ray GetPlayerRay
        {
            get { return UpdatePlayerRay(); }
        }

        public LocalPlayer(Game game, string nom, Vector3 position, int health)
            : base(game, nom, position, health)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Ecran = new Screen(Game);
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
            CaméraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            GestionnaireDeSons = Game.Services.GetService(typeof(RessourcesManager<SoundEffect>)) as RessourcesManager<SoundEffect>;
            GenerateurRandom = Game.Services.GetService(typeof(Random)) as Random;
            CreerWalkingSounds();
            JumpSound = GestionnaireDeSons.Find("Jump");

            InitialiserLocalPlayer();

            InitialiserOverlay();
        }

        void InitialiserLocalPlayer()
        {
            EstShoot = false;
            playerRay = new Ray(CaméraJeu.Position, CaméraJeu.Direction);

            AncienGun = MyGun;

            Monney = 10000;
            Health = 100;
        }

        void CreerWalkingSounds()
        {
            ListeWalking = new List<SoundEffect>();

            for(int i = 1; i <= 4; ++i)
            {
                ListeWalking.Add(GestionnaireDeSons.Find("Walking" + i.ToString()));
            }
        }

        void InitialiserOverlay()
        {
            TexteArgent = new MonneyTexte(Game, "Argent : " + Monney.ToString(), "Pescadero", new Vector2(20, Ecran.CenterScreen.Y / 2), Color.White);
            BarreDeVie = new HealthBar(Game, new Rectangle(20, (int)(Ecran.CenterScreen.Y * 1.5f), 100 * (int)(health / 100), 10), "HealthBar", Color.Red);
            GestionnaireDeMunitions = new GestionnaireAmmo(Game, Rectangle.Empty, "ammoIcon", Color.White);

            Game.Components.Add(TexteArgent);
            GameController.ListeDrawableComponents.Add(TexteArgent);

            Game.Components.Add(BarreDeVie);
            GameController.ListeDrawableComponents.Add(BarreDeVie);

            Game.Components.Add(GestionnaireDeMunitions);
            GameController.ListeDrawableComponents.Add(GestionnaireDeMunitions);
        }

        Ray UpdatePlayerRay()
        {
            playerRay.Position = CaméraJeu.Position;
            playerRay.Direction = CaméraJeu.Direction;
            return playerRay;
        }

        float TempsÉcouléLent = 0;
        float TempsÉcouléSound = 0;
        float TempsÉcoulé = 0;

        public override void Update(GameTime gameTime)
        {
            if ((TempsÉcouléLent += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE && MyGun != null &&
                 GameController.PlayState != GameController.InGameState.Dead)
            {
                MyGun.GererTirs(gameTime);
                GererPlayer();
            }

            //MARDE TEST
            if (GestionInput.EstNouvelleTouche(Keys.Up))
                Health -= 10;

            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE / 10)
            {
                UpdatePosition();
                GererJump();
                TempsÉcoulé = 0;
            }

            if ((TempsÉcouléSound += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.5f)
            {
                GererWalk();
            }
            base.Update(gameTime);
        }

        void GererPlayer()
        {
            if (GestionInput.EstClicDroit())
                EstScoped = true;
            else
                EstScoped = false;
        }

        void GererWalk()
        {
            if(Caméra1stPerson.EstSol && EstToucheMarche())
            {
                TempsÉcouléSound = 0;
                ListeWalking[GenerateurRandom.Next(0, ListeWalking.Count - 1)].Play();
            }
        }

        public static bool DoOnce = false;
        void GererJump()
        {
            if (Caméra1stPerson.EstEnSaut && !DoOnce)
            {
                DoOnce = true;
                JumpSound.Play(0.5f,0,0);
            }
        }

        bool EstToucheMarche()
        {
            if (GestionInput.EstEnfoncée(Keys.W) || GestionInput.EstEnfoncée(Keys.S) ||
                GestionInput.EstEnfoncée(Keys.A) || GestionInput.EstEnfoncée(Keys.D))
                return true;
            else
                return false;
        }

        void UpdatePosition()
        {
            if(Caméra1stPerson.EstSol)
            {
                Position.Y = 0;
            }
            float hauteur = Caméra1stPerson.HAUTEUR_PLAYER;

            if (Caméra1stPerson.EstCrouch)
            {
                hauteur = hauteur / 2;
                Caméra1stPerson.EstCrouch = false;
            }

            Position = CaméraJeu.Position - (Vector3.UnitY * Caméra1stPerson.HAUTEUR_PLAYER);
        }




        float DroneDistance;
        float WallDistance;
        public bool EstEnCollision(Drone drone)
        {
            Ray myRay = GetPlayerRay;
            List<BoundingBox> ListeBoundingBox = new List<BoundingBox>();
            if (myRay.Intersects(drone.BoiteDeCollision) != null)
            {
                foreach (ICollisionableList c in GameController.ListMurs)
                {
                    foreach (BoundingBox b in c.ListeBoundingBox)
                    {
                        if (myRay.Intersects(b) != null)
                            ListeBoundingBox.Add(b);
                    }
                }

                DroneDistance = Vector3.Distance(drone.Position, CaméraJeu.Position);
                foreach (BoundingBox b in ListeBoundingBox)
                {
                    WallDistance = Vector3.Distance(GetRayBoundingBoxIntersectionPoint(myRay, b), CaméraJeu.Position);

                    if (WallDistance < DroneDistance)
                        return false;
                }
                return true;
            }
            return false;
        }

        Vector3 GetRayBoundingBoxIntersectionPoint(Ray ray, BoundingBox box)
        {
            float? distance = ray.Intersects(box);
            return ray.Position + ray.Direction * distance.Value;
        }

    }
}
