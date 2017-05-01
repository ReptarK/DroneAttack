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
    public class CadreTourelle : PlanTextur�
    {
        const int PRIX_TOURELLE = 4000;

        InputManager GestionInput;
        Cam�ra1stPerson Cam�raJeu;

        public BoundingBox BoiteDeCollision;

        Texture2D texture;

        public float DeltaX { get; set; }
        public float DeltaY { get; set; }

        public CadreTourelle(Game jeu, float homoth�tieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector2 �tendue, Vector2 charpente, string nomTexture, float intervalleMAJ) 
            : base(jeu, homoth�tieInitiale, rotationInitiale, positionInitiale,�tendue,charpente,nomTexture,intervalleMAJ)
        {
            Cam�raJeu = Game.Services.GetService(typeof(Cam�ra1stPerson)) as Cam�ra1stPerson;
            DeltaX = �tendue.X;
            DeltaY = �tendue.Y;
            BoiteDeCollision = new BoundingBox(PositionInitiale + new Vector3(DeltaX / 2, DeltaY / 2, 0),
                                               PositionInitiale + new Vector3(-DeltaX / 2, -DeltaY / 2, 0));
        }

        public override void Initialize()
        {
            base.Initialize();
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
        }

        float Temps�coul� = 0;
        bool CanCheck;
        public override void Update(GameTime gameTime)
        {
            CanCheck = false;
            base.Update(gameTime);
            if (GestionInput.EstNouvelleTouche(Keys.E))
                CanCheck = true;

            if ((Temps�coul� += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                if (CanCheck && GameController.MyPlayer.GetPlayerRay.Intersects(BoiteDeCollision) != null)
                {
                    Temps�coul� = 0;

                    float CadreDistance = Vector3.Distance(MainGame.Cam�raJeu.Position,
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
