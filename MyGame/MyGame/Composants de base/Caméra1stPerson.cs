using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MyGame;
using MyGame.Entités;

namespace AtelierXNA
{
    public class Caméra1stPerson : Caméra
    {
        Screen Ecran;
        InputManager GestionInput { get; set; }

        const float DELTA_LACET = MathHelper.Pi / 180; // 1 degré à la fois
        const float DELTA_TANGAGE = MathHelper.Pi / 180; // 1 degré à la fois

        public const float HAUTEUR_PLAYER = 15;
        public const float PLAYER_WIDTH = 2.4f;


        const float VITESSE_UPDATE_DEPLACEMENT = 0.45f;
        public static float FORCE_JOUEUR = 1.8f;

        LocalPlayer MyPlayer;

        Vector3 direction;
        public Vector3 Direction
        {
            get { return direction; }
            set
            {
                if (value.Y > 0.9f)
                    value.Y = 0.9f;
                if (value.Y < -0.9f)
                    value.Y = -0.9f;

                direction = value;
            }
        }
        public Vector3 Latéral { get; set; }
        float VitesseTranslation { get; set; }
        float VitesseRotation { get; set; }

        MouseState Souris { get; set; }
        Vector2 AncienncePositionSouris { get; set; }
        Vector2 NouvellePositionSouris { get; set; }
        Vector2 DéplacementSouris { get; set; }

        float TempsÉcouléDepuisMAJ { get; set; }
        float IntervalleMAJ { get; set; }

        public Vector3 SommeDesForces;
        public float Masse { get; set; }

        public static bool EstEnSaut = false;
        public static bool EstCrouch = false;
        public static bool EstSol = false;
        public static bool EstLadder = false;

        bool estEnZoom;
        bool EstEnZoom
        {
            get { return estEnZoom; }
            set
            {
                float ratioAffichage = Game.GraphicsDevice.Viewport.AspectRatio;
                estEnZoom = value;
                if (estEnZoom)
                {
                    CréerVolumeDeVisualisation(OUVERTURE_OBJECTIF / 1.5f, ratioAffichage, DISTANCE_PLAN_RAPPROCHÉ, DISTANCE_PLAN_ÉLOIGNÉ);
                }
                else
                {
                    CréerVolumeDeVisualisation(OUVERTURE_OBJECTIF, ratioAffichage, DISTANCE_PLAN_RAPPROCHÉ, DISTANCE_PLAN_ÉLOIGNÉ);
                }
            }
        }

        public static bool EstEnCollision = false;
        public static List<ICollisionableList> ListeObjetEnCollision;
        public static List<Vector3> ListNormaleObjetCollision;
        public Vector3 FacteurForces;

        PlancherMap Terrain;
        public Caméra1stPerson(Game jeu, Vector3 positionCaméra, Vector3 cible, Vector3 orientation, float intervalleMAJ)
            : base(jeu)
        {
            IntervalleMAJ = intervalleMAJ;
            CréerVolumeDeVisualisation(OUVERTURE_OBJECTIF, DISTANCE_PLAN_RAPPROCHÉ, DISTANCE_PLAN_ÉLOIGNÉ);
            CréerPointDeVue(positionCaméra, cible, orientation);
            EstEnZoom = false;
        }
        public override void Initialize()
        {
            ListeObjetEnCollision = new List<ICollisionableList>();
            ListNormaleObjetCollision = new List<Vector3>();
            Masse = 1;

            Ecran = new Screen(Game);
            VitesseTranslation = VITESSE_UPDATE_DEPLACEMENT;  // vitesse du joueur
            VitesseRotation = 0.1f;       // SENSIBILITÉ
            TempsÉcouléDepuisMAJ = 0;
            Mouse.SetPosition(Ecran.CenterScreen.X, Ecran.CenterScreen.Y);
            base.Initialize();
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
            Terrain = Game.Services.GetService(typeof(PlancherMap)) as PlancherMap;
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;
        }
        public override void CréerPointDeVue()
        {
            Direction = Vector3.Normalize(Direction);
            Latéral = Vector3.Cross(OrientationVerticale, Direction);
            Latéral = Vector3.Normalize(Latéral);

            if (GameController.PlayState == GameController.InGameState.Dead)
                Vue = Matrix.CreateLookAt(Position, Position + Direction, OrientationVerticale + new Vector3(0, 0, 3));
            Vue = Matrix.CreateLookAt(Position, Position + Direction, OrientationVerticale);
            GénérerFrustum();
        }
        protected override void CréerPointDeVue(Vector3 position, Vector3 cible, Vector3 orientation)
        {

            Cible = cible;
            Position = position;
            OrientationVerticale = orientation;


            Direction = Cible - Position;
            Direction = Vector3.Normalize(Direction);


            CréerPointDeVue();
        }

        bool DoOnce = false;
        float TempsÉcouléMort = 0;
        public override void Update(GameTime gameTime)
        {
            float TempsÉcoulé = (float)gameTime.ElapsedGameTime.TotalSeconds;
            TempsÉcouléDepuisMAJ += TempsÉcoulé;

            if (GestionInput.EstNouvelleTouche(Keys.Space) && EstEnCollision && EstSol)
                EstEnSaut = true;

            if (GestionInput.EstEnfoncée(Keys.LeftControl))
                EstCrouch = true;

            CréerPointDeVue();
            if (TempsÉcouléDepuisMAJ >= Data.INTERVALLE_MAJ_BASE / 1F && MainGame.MainGameState == MainGame.GameState.Jeu && GameController.PlayState != GameController.InGameState.Dead)
            {
                SommeDesForces = Vector3.Zero;

                GérerSouris();
                GérerRotation();

                GererCollision();
                GérerDéplacement();

                if (GestionInput.EstClicDroit() && MyPlayer.MyGun != null && !MyPlayer.MyGun.EstReload)
                {
                    EstEnZoom = true;
                    DoOnce = true;
                }
                else
                {
                    if (DoOnce)
                    {
                        DoOnce = false;
                        EstEnZoom = false;
                    }
                }

                if (EstEnSaut)
                    GererJump();

                else { EstCrouch = false; }

                GererForces(VITESSE_UPDATE_DEPLACEMENT);

                if (GestionInput.EstSourisActive)
                    Mouse.SetPosition(Ecran.CenterScreen.X, Ecran.CenterScreen.Y);

                TempsÉcouléDepuisMAJ = 0;
            }

            base.Update(gameTime);
        }


        Ray UpdatePlayerRay()
        {
            if (rayPlayer == null)
                rayPlayer = new Ray(Position, Direction);
            else
            {
                rayPlayer.Position = Position;
                rayPlayer.Direction = Direction;
            }

            return rayPlayer;
        }

        Ray rayPlayer;
        Ray GetRayPlayer
        {
            get { return UpdatePlayerRay(); }
        }

        //MOUVEMENT
        Vector3 Accélération;
        Vector3 Vitesse;
        Vector3 Deplacement;

        Vector3 OldVitesse;
        Vector3 OldPosition;

        void GererForces(float deltaTemps)
        {

            OldPosition = Position;
            OldVitesse = Vitesse;

            if (!EstLadder)
                SommeDesForces.Y -= Data.GRAVITÉ * Masse;

            Accélération.X = SommeDesForces.X / Masse;
            Accélération.Y = SommeDesForces.Y / Masse;
            Accélération.Z = SommeDesForces.Z / Masse;

            Vitesse.X = Accélération.X * deltaTemps;
            Vitesse.Y = Accélération.Y * deltaTemps;
            Vitesse.Z = Accélération.Z * deltaTemps;

            Position = ((Accélération * deltaTemps) / 2) + (Vitesse * deltaTemps) + OldPosition;

            EstEnCollision = false;
            ListNormaleObjetCollision.Clear();
            Caméra1stPerson.ListeObjetEnCollision.Clear();
        }

        private void GérerDéplacement()
        {
            if (EstLadder)
            {
                SommeDesForces += Vector3.UnitY * (GérerTouche(Keys.W) - GérerTouche(Keys.S));
                SommeDesForces += new Vector3(Latéral.X, 0, Latéral.Z) * (GérerTouche(Keys.A) - GérerTouche(Keys.D));
                return;
            }

            if (GestionInput.EstNouvelleTouche(Keys.B))
            { }
            Vector3 vecteurTotal;
            Vector3 vecteurD = new Vector3(Direction.X, 0, Direction.Z) * (GérerTouche(Keys.W) - GérerTouche(Keys.S));
            Vector3 vecteurL = new Vector3(Latéral.X, 0, Latéral.Z) * (GérerTouche(Keys.A) - GérerTouche(Keys.D));

            if (vecteurD != Vector3.Zero && vecteurL != Vector3.Zero)
            {
                vecteurTotal = Vector3.Normalize(vecteurD + vecteurL);
                SommeDesForces += vecteurTotal * FORCE_JOUEUR;
            }
            else
            {
                SommeDesForces += vecteurD * FORCE_JOUEUR;

                SommeDesForces += vecteurL * FORCE_JOUEUR;
            }
        }

        float compteurSaut = 0;
        float ForceSaut = 8;
        void GererJump()
        {
            if (EstEnSaut)
                if (compteurSaut < 40)
                {
                    if (compteurSaut % 5 == 0)
                        ForceSaut--;

                    ++compteurSaut;
                    SommeDesForces += Vector3.UnitY * ForceSaut;
                }
                else
                {
                    LocalPlayer.DoOnce = false;
                    compteurSaut = 0;
                    ForceSaut = 8;
                    EstEnSaut = false;
                }
        }

        void GererCollision()
        {
            if (GestionInput.EstNouvelleTouche(Keys.B))
            { }

            foreach (Vector3 v in ListNormaleObjetCollision)
            {
                SommeDesForces += v;
            }
        }

        private int GérerTouche(Keys touche)
        {
            return GestionInput.EstEnfoncée(touche) ? 1 : 0;
        }
        public void GérerSouris()
        {
            AncienncePositionSouris = new Vector2(Ecran.CenterScreen.X, Ecran.CenterScreen.Y);
            Souris = Mouse.GetState();
            NouvellePositionSouris = new Vector2(Souris.X, Souris.Y);
            DéplacementSouris = NouvellePositionSouris - AncienncePositionSouris;
        }
        public void GérerRotation()
        {
            GérerLacet();
            GérerTangage();
        }
        private void GérerLacet()
        {
            float i = 0;
            i = -DéplacementSouris.X * VitesseRotation;
            Direction = Vector3.Transform(Direction, Matrix.CreateFromAxisAngle(Vector3.Up, DELTA_LACET * i * Data.Sensivity));
        }

        private void GérerTangage()
        {
            float i = 0;
            i = DéplacementSouris.Y * VitesseRotation;

            Direction = Vector3.Transform(Direction, Matrix.CreateFromAxisAngle(Latéral, DELTA_TANGAGE * i * Data.Sensivity));
        }
    }
}
