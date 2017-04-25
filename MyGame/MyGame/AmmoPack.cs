using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyGame.Entit�s;
using MyGame;

namespace AtelierXNA
{
    class AmmoPack : CubeTextur�Anim�, IPack
    {
        float IntervalleMAJ { get; set; }
        public float Compteur { get; private set; }
        public float Temps�coul�DepuisMAJ { get; private set; }

        public bool EstD�truit { get; set; }

        BoundingBox boiteDeCollision;
        public BoundingBox BoiteDeCollision
        {
            get { return boiteDeCollision; }
        }

        public AmmoPack(Game game, float homoth�tieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, string nomTextureCube, Vector3 dimension, float intervalMAJ)
            : base(game, homoth�tieInitiale, rotationInitiale, positionInitiale, nomTextureCube, dimension, intervalMAJ)
        {
            boiteDeCollision = new BoundingBox(PositionInitiale - dimension / 2, PositionInitiale + dimension / 2);
        }
        public override void Initialize()
        {
            EstD�truit = false;
            Lacet = true;
            base.Initialize();
        }
        protected override void EffectuerMise�Jour()
        {
            this.Visible = !EstD�truit;

            Compteur += 0.05f;
            Vector3 hauteur = new Vector3(0, (float)Math.Sin(Compteur), 0);
            Position += hauteur * 0.12f; //CHANGER AU BESOIN
            base.CalculerMatriceMonde();
        }
    }
}
