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

namespace MyGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Tourelle : ObjetDeDémo, IDestructible
    {
        const float RAYON_TIR = 100;
        const float TEMPS_TIR = 2f;

        RessourcesManager<SoundEffect> GestionnaireDeSons;
        SoundEffect LaserSound;

        LocalPlayer MyPlayer;

        protected virtual int Degats { get; set; }

        public bool ADétruire { get; set; }

        public BoundingSphere SphereDeTir;


        public Tourelle(Game jeu, string nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ, Color couleur)
            : base(jeu, nomModèle, échelleInitiale, rotationInitiale, positionInitiale, intervalleMAJ, couleur)
        {
            Degats = 10;
        }

        public override void Initialize()
        {
            base.Initialize();
            ADétruire = false;
            LaserSound = GestionnaireDeSons.Find("Laser");

            SphereDeTir = new BoundingSphere(Position, RAYON_TIR);

        }

        protected override void LoadContent()
        {
            base.LoadContent();
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;
            GestionnaireDeSons = new RessourcesManager<SoundEffect>(Game, "Sounds");
        }

        public int compteurWave = 0;
        float TempsÉcouléTir = 0;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if ((TempsÉcouléTir += (float)gameTime.ElapsedGameTime.TotalSeconds) > TEMPS_TIR)
            {
                TempsÉcouléTir = 0;

                GererTir();
            }
        }

        bool CanShoot;
        void GererTir()
        {
            CanShoot = true;
            foreach (Drone d in GameController.ListDrones)
            {
                if (EstEnCollision(d) && CanShoot)
                {
                    CanShoot = false;
                    Game.Components.Add(new RayonLaserCylindrique(Game, 1, Vector3.Zero, SphereDeTir.Center, Vector3.Normalize(d.Position - SphereDeTir.Center),
                    new Vector2(0.1f, 100), new Vector2(10, 10), "GreenScreen", 0.01f, 0.1f));
                    LaserSound.Play(0.3f, 0, 0);
                    d.Health -= Degats;
                }
            }

        }

        float DroneDistance;
        float WallDistance;
        public bool EstEnCollision(Drone drone)
        {
            //Position += Vector3.UnitY * 3;
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
