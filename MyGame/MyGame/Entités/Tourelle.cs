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
using MyGame.Entit�s;

namespace MyGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Tourelle : ObjetDeD�mo, IDestructible
    {
        const float RAYON_TIR = 100;
        const float TEMPS_TIR = 1f;

        Color CouleurBase = new Color(0, 0, 255, 50);
        Color CouleurTir = new Color(255, 255, 255, 50);

        RessourcesManager<SoundEffect> GestionnaireDeSons;
        SoundEffect LaserSound;

        LocalPlayer MyPlayer;
        Cam�ra1stPerson Cam�raJeu;

        Sphere SphereTirCouleur;

        protected virtual int Degats { get; set; }

        public bool AD�truire { get; set; }

        int RoundActiv�e;

        public BoundingSphere SphereDeTir;


        public Tourelle(Game jeu, string nomMod�le, float �chelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ, Color couleur)
            : base(jeu, nomMod�le, �chelleInitiale, rotationInitiale, positionInitiale, intervalleMAJ, couleur)
        {
            Degats = 10;
        }

        public override void Initialize()
        {
            base.Initialize();
            AD�truire = false;
            LaserSound = GestionnaireDeSons.Find("Laser");

            SphereDeTir = new BoundingSphere(Position, RAYON_TIR);
            RoundActiv�e = GameController.WaveNo;

            SphereTirCouleur = new Sphere(Game, RAYON_TIR, GraphicsDevice, 1, Vector3.Zero, Position, Data.INTERVALLE_MAJ_BASE, CouleurBase);
            this.DrawOrder = SphereTirCouleur.DrawOrder + 1;
            GameController.ListeDrawableComponents.Add(SphereTirCouleur);
            Game.Components.Add(SphereTirCouleur);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;
            Cam�raJeu = Game.Services.GetService(typeof(Cam�ra1stPerson)) as Cam�ra1stPerson;
            GestionnaireDeSons = new RessourcesManager<SoundEffect>(Game, "Sounds");
        }

        public int compteurWave = 0;
        float Temps�coul�;
        float Temps�coul�SonTir = 0;
        float Temps�coul�Destruction;
        float Temps�coul�Couleur = 0;
        const int WAVE_DURATION = 5;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if ((Temps�coul�Destruction += (float)gameTime.ElapsedGameTime.TotalSeconds) > 1)
            {
                if((RoundActiv�e + WAVE_DURATION) == GameController.WaveNo)
                {
                    GameController.ListeDrawableComponents.Remove(SphereTirCouleur);
                    GameController.ListeDrawableComponents.Remove(this);
                    Game.Components.Remove(SphereTirCouleur);
                    Game.Components.Remove(this);
                }
            }

            if ((Temps�coul�SonTir += (float)gameTime.ElapsedGameTime.TotalSeconds) > TEMPS_TIR)
            {
                Temps�coul�SonTir = 0;

                GererTir(gameTime);
            }

            if ((Temps�coul�Couleur += (float)gameTime.ElapsedGameTime.TotalSeconds) > 1f)
            {
                Temps�coul�Couleur = 0;
                SphereTirCouleur.ChangeCouleur(CouleurBase);
            }

        }

        bool CanShoot;
        float Temps�coul�Tir;
        void GererTir(GameTime gameTime)
        {
            CanShoot = true;
            foreach (Drone d in GameController.ListDrones)
            {
                if (EstEnCollision(d) && CanShoot)
                {
                    SphereTirCouleur.Couleur = CouleurTir;
                    Temps�coul�Couleur = 0;
                    CanShoot = false;
                    //Game.Components.Add(new RayonLaserCylindrique(Game, 1, Vector3.Zero, SphereDeTir.Center + new Vector3(0, 10, -22), Vector3.Normalize(d.Position - SphereDeTir.Center),
                    //new Vector2(0.1f, Vector3.Distance(SphereDeTir.Center, d.Position)), new Vector2(10, 10), "GreenScreen", 0.01f, 0.1f));
                    LaserSound.Play(0.3f, 0, 0);
                    d.Health -= d.Health;
                    SphereTirCouleur.ChangeCouleur(CouleurTir);
                }
            }

        }

        float DroneDistance;
        float WallDistance;
        public bool EstEnCollision(Drone drone)
        {
            SphereDeTir.Transform(GetMonde());
            if (SphereDeTir.Intersects(drone.BoiteDeCollision))
            {
                Ray TourelleRay = new Ray(SphereDeTir.Center, Vector3.Normalize(SphereDeTir.Center - drone.Position));
                List<BoundingBox> ListeBoundingBox = new List<BoundingBox>();

                DroneDistance = Vector3.Distance(SphereDeTir.Center, drone.Position);
                TourelleRay = new Ray(SphereDeTir.Center, Vector3.Normalize(drone.Position - SphereDeTir.Center));

                foreach (ICollisionableList m in GameController.ListMurs)
                {
                    foreach (BoundingBox b in m.ListeBoundingBox)
                    {
                        if (TourelleRay.Intersects(b) != null)
                            ListeBoundingBox.Add(b);
                    }
                }


                foreach (BoundingBox b in ListeBoundingBox)
                {
                    WallDistance = Vector3.Distance(GetRayBoundingBoxIntersectionPoint(TourelleRay, b), SphereDeTir.Center);

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
            if (distance != null)
                return ray.Position + ray.Direction * distance.Value;
            else
                return Vector3.One * float.MaxValue;
        }
    }
}
