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
using MyGame;

namespace AtelierXNA
{
    public class ObjetDeDémo : ObjetDeBase
    {
        const float INCRÉMENTATION_ANGLE = (float)(Math.PI / 120);
        const float INCRÉMENTATION_ZOOM = 0.001f;
        float IntervalleMAJ { get; set; }
        float TempsÉcouléDepuisMAJ { get; set; }
        InputManager GestionInput { get; set; }
        float IntervalMAJ { get; set; }
        bool EstEnRotationY { get; set; }
        bool EstEnRotationX { get; set; }
        bool EstEnRotationZ { get; set; }
        bool EstEnAgrandissement { get; set; }
        bool EstEnRetrecissement { get; set; }
        bool EstEnChangement { get; set; }
        float ÉchelleCopie { get; set; }
        Vector3 RotationCopie { get; set; }
        Vector3 PositionCopie { get; set; }

        protected Color Couleur;

        public ObjetDeDémo(Game jeu, string nomModèle, float échelleInitiale, Vector3 rotationInitiale,
            Vector3 positionInitiale, float intervalleMAJ, Color couleur)
         : base(jeu, nomModèle, échelleInitiale, rotationInitiale, positionInitiale)
        {
            IntervalleMAJ = intervalleMAJ;
            ÉchelleCopie = Échelle;
            RotationCopie = Rotation;
            PositionCopie = Position;
            Couleur = couleur;
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
        }
        public override void Update(GameTime gameTime)
        {
            EstEnChangement = false;
            GérerRotation();
            float TempsÉcoulé = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TempsÉcouléDepuisMAJ += TempsÉcoulé;
            if (TempsÉcouléDepuisMAJ >= IntervalleMAJ)
            {
                EffectuerMiseÀJour();
                TempsÉcouléDepuisMAJ = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (ModelMesh maille in Modèle.Meshes)
            {
                Matrix mondeLocal = TransformationsModèle[maille.ParentBone.Index] * GetMonde();
                foreach (ModelMeshPart portionDeMaillage in maille.MeshParts)
                {
                    BasicEffect effet = (BasicEffect)portionDeMaillage.Effect;
                    effet.EnableDefaultLighting();
                    effet.DiffuseColor = Couleur.ToVector3(); //COLOR
                    effet.Projection = CaméraJeu.Projection;
                    effet.View = CaméraJeu.Vue;
                    effet.World = mondeLocal;
                }
                maille.Draw();
            }
        }


        protected virtual void EffectuerMiseÀJour()
        {
            GérerHomothétie();
            GérerTransformationObjet();
        }
        protected void GérerRotation()
        {
            if (GestionInput.EstClavierActivé)
            {
                EstEnChangement = true;
                if (GestionInput.EstNouvelleTouche(Keys.D1))
                    EstEnRotationY = !EstEnRotationY;
                if (GestionInput.EstNouvelleTouche(Keys.D2))
                    EstEnRotationX = !EstEnRotationX;
                if (GestionInput.EstNouvelleTouche(Keys.D3))
                    EstEnRotationZ = !EstEnRotationZ;

                if (GestionInput.EstNouvelleTouche(Keys.Space))
                    ResetMonde();
            }
        }

        void GérerHomothétie()
        {
            if (GestionInput.EstEnfoncée(Keys.OemMinus))
                EstEnRetrecissement = true;
            else
                EstEnRetrecissement = false;
            if (GestionInput.EstEnfoncée(Keys.OemPlus))
                EstEnAgrandissement = true;
            else
                EstEnAgrandissement = false;
        }

        void GérerTransformationObjet()
        {
            EstEnChangement = false;
            if (EstEnAgrandissement && ÉchelleCopie < 2)
            {
                ÉchelleCopie += INCRÉMENTATION_ZOOM;
                EstEnChangement = true;
            }

            if (EstEnRetrecissement && ÉchelleCopie > Échelle / 2)
            {
                ÉchelleCopie -= INCRÉMENTATION_ZOOM;
                EstEnChangement = true;
            }

            if(EstEnRotationX)
            {
                RotationCopie += Vector3.UnitX * INCRÉMENTATION_ANGLE;
                EstEnChangement = true;
            }
            if (EstEnRotationY)
            {
                RotationCopie += Vector3.UnitY * INCRÉMENTATION_ANGLE;
                EstEnChangement = true;
            }
            if (EstEnRotationZ)
            {
                RotationCopie += Vector3.UnitZ * INCRÉMENTATION_ANGLE;
                EstEnChangement = true;
            }

            //if (EstEnChangement)
                CalculerMonde();
        }
        protected void CalculerMonde()
        {
            if(!EstEnChangement)
            {
                RotationCopie = Rotation;
                PositionCopie = Position;
                EstEnRotationX = false;
                EstEnRotationY = false;
                EstEnRotationZ = false;
            }
            Monde = Matrix.Identity;
            Monde *= Matrix.CreateScale(ÉchelleCopie);
            Monde *= Matrix.CreateFromYawPitchRoll(RotationCopie.Y, RotationCopie.X, RotationCopie.Z);
            Monde *= Matrix.CreateTranslation(PositionCopie);
        }
        private void ResetMonde()
        {
            RotationCopie = Rotation;
            PositionCopie = Position;
            Monde = Matrix.Identity;
            Monde *= Matrix.CreateScale(Échelle);
            Monde *= Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);
            Monde *= Matrix.CreateTranslation(Position);
        }
    }
}