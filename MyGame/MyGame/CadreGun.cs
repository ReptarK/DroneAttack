using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MyGame.Entités;
using System.Collections.Generic;
using MyGame;
using MyGame.Weapons;

namespace AtelierXNA
{
    public class CadreGun : PlanTexturé
    {
        InputManager GestionInput;

        public float DeltaX { get; set; }
        public float DeltaY { get; set; }


        Caméra1stPerson CaméraJeu;

        //COLLISION : PARAMETRES
        public BoundingBox BoiteDeCollision;

        RessourcesManager<Texture2D> gestionnaireDeTextures;
        Texture2D texture;

        string NomGun;
        Gun Weapon;

        public CadreGun(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector2 étendue, Vector2 charpente, string nomTexture, float intervalleMAJ, string nomGun) 
            : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale,étendue,charpente,nomTexture,intervalleMAJ)
        {
            CaméraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            DeltaX = étendue.X;
            DeltaY = étendue.Y;
            BoiteDeCollision = new BoundingBox(PositionInitiale + new Vector3(DeltaX / 2, DeltaY / 2, 0),
                                               PositionInitiale + new Vector3(-DeltaX / 2, -DeltaY / 2, 0));
            NomGun = nomGun;
        }

        public override void Initialize()
        {
            base.Initialize();

            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;

            Weapon = GetWeapon();
        }

        float TempsÉcoulé = 0;
        bool CanCheck = false;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if((TempsÉcoulé += (float) gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                CanCheck = true;
            }

            if (CanCheck && GestionInput.EstNouvelleTouche(Keys.E) && GameController.MyPlayer.GetPlayerRay.Intersects(BoiteDeCollision) != null)
            {
                CanCheck = false;
                float CadreDistance = Vector3.Distance(MainGame.CaméraJeu.Position,
                                                        GetRayBoundingBoxIntersectionPoint(GameController.MyPlayer.GetPlayerRay, BoiteDeCollision));

                if (CadreDistance < 30 && (GameController.MyPlayer.Monney - Weapon.Prix) >= 0)
                {
                    //CHANGE GUN
                    GameController.MyPlayer.MyGun = GetWeapon();
                    GameController.MyPlayer.MyGun.Initialize();
                    GameController.MyPlayer.Monney -= Weapon.Prix;
                }
            }
        }

        Gun GetWeapon()
        {
            switch(NomGun)
            {
                case (Gun.M9_NAME): return new M9(Game);
                case (Gun.SPECTRE_NAME): return new Spectre(Game);
                case (Gun.UMP45_NAME): return new UMP45(Game);
                case (Gun.FAMAS_NAME): return new Famas(Game);
                case (Gun.M16_NAME): return new M16(Game);
                case (Gun.M60_NAME): return new M60(Game);

            }
            return null;
        }

        Vector3 GetRayBoundingBoxIntersectionPoint(Ray ray, BoundingBox box)
        {
            float? distance = ray.Intersects(box);
            return ray.Position + ray.Direction * distance.Value;
        }
    }
}
