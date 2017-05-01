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
    public class CadreTourelle : PlanTexturé
    {
        const int PRIX_TOURELLE = 4000;

        InputManager GestionInput;
        Caméra1stPerson CaméraJeu;

        public BoundingBox BoiteDeCollision;

        Texture2D texture;

        public float DeltaX { get; set; }
        public float DeltaY { get; set; }

        public CadreTourelle(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector2 étendue, Vector2 charpente, string nomTexture, float intervalleMAJ) 
            : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale,étendue,charpente,nomTexture,intervalleMAJ)
        {
            CaméraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            DeltaX = étendue.X;
            DeltaY = étendue.Y;
            BoiteDeCollision = new BoundingBox(PositionInitiale + new Vector3(DeltaX / 2, DeltaY / 2, 0),
                                               PositionInitiale + new Vector3(-DeltaX / 2, -DeltaY / 2, 0));
        }

        public override void Initialize()
        {
            base.Initialize();
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
        }

        float TempsÉcoulé = 0;
        bool CanCheck;
        public override void Update(GameTime gameTime)
        {
            CanCheck = false;
            base.Update(gameTime);
            if (GestionInput.EstNouvelleTouche(Keys.E))
                CanCheck = true;

            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                if (CanCheck && GameController.MyPlayer.GetPlayerRay.Intersects(BoiteDeCollision) != null)
                {
                    TempsÉcoulé = 0;

                    float CadreDistance = Vector3.Distance(MainGame.CaméraJeu.Position,
                                                            GetRayBoundingBoxIntersectionPoint(GameController.MyPlayer.GetPlayerRay, BoiteDeCollision));

                    if (CadreDistance < 30 && (GameController.MyPlayer.Monney - PRIX_TOURELLE) >= 0)
                    {
                        GameController.MyPlayer.Monney -= PRIX_TOURELLE;
                        LocalPlayer.NbTurret += 1;
                    }
                }
                CanCheck = false;
            }
        }

        Vector3 GetRayBoundingBoxIntersectionPoint(Ray ray, BoundingBox box)
        {
            float? distance = ray.Intersects(box);
            return ray.Position + ray.Direction * distance.Value;
        }
    }
}
