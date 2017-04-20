using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame;
using MyGame.Entités;

namespace AtelierXNA
{
    public class CubeCaméra : PrimitiveDeBaseAnimée
    {
        int OldHeatlh;
        LocalPlayer MyPlayer;
        Caméra1stPerson CameraJeu;

        const int NB_SOMMETS = 16;
        const int NB_TRIANGLES =6;
        Color Couleur;
        VertexPositionColor[] Sommets { get; set; }
        VertexPositionColor[] SommetsDeux { get; set; }
        Vector3 Origine { get; set; }
        Vector3 Delta { get; set; }
        BasicEffect EffetDeBase { get; set; }
        Vector3[] TableauDePoint { get; set; }

        public CubeCaméra(Game game, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Color couleur, Vector3 dimension, float intervalleMAJ)
         : base(game, homothétieInitiale, rotationInitiale, positionInitiale, intervalleMAJ)
        {
            Couleur = couleur;
            Delta = dimension;
            Origine = new Vector3(-Delta.X / 2, -Delta.Y / 2,+Delta.Z/2 );
            SetTableauDePoints();

        }

        public override void Initialize()
        {
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;
            CameraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            DrawOrder = 1000;
            OldHeatlh = 100;
            Sommets = new VertexPositionColor[NB_SOMMETS];
            base.Initialize();

        }
        private void SetTableauDePoints()
        {
            TableauDePoint = new Vector3[8];

            TableauDePoint[0] = Origine; //A
            TableauDePoint[1] = TableauDePoint[0] + new Vector3(0, Delta.Y, 0); //B
            TableauDePoint[2] = TableauDePoint[0] + new Vector3(Delta.X, 0, 0); //C
            TableauDePoint[3] = TableauDePoint[1] + new Vector3(Delta.X, 0, 0); //D
            TableauDePoint[4] = TableauDePoint[2] - new Vector3(0, 0, Delta.Z); //E
            TableauDePoint[5] = TableauDePoint[3] - new Vector3(0, 0, Delta.Z); //F
            TableauDePoint[6] = TableauDePoint[0] - new Vector3(0, 0, Delta.Z); //H
            TableauDePoint[7] = TableauDePoint[1] - new Vector3(0, 0, Delta.Z); //I
            //TableauDePoint = new Vector3[8];
            //TableauDePoint[0] = Origine; // A
            //TableauDePoint[1] = new Vector3(Origine.X, Origine.Y+Delta.Y,Origine.Z); // B
            //TableauDePoint[2] = new Vector3(Origine.X + Delta.X, Origine.Y, Origine.Z); // C
            //TableauDePoint[3] = new Vector3(Origine.X + Delta.X, Origine.Y + Delta.Y, Origine.Z); // D
            //TableauDePoint[4] = new Vector3(Origine.X + Delta.X, Origine.Y , Origine.Z-Delta.Z); // E
            //TableauDePoint[5] = new Vector3(Origine.X + Delta.X, Origine.Y+Delta.Y, Origine.Z - Delta.Z); // F
            //TableauDePoint[6] = new Vector3(Origine.X, Origine.Y, Origine.Z - Delta.Z); // G
            //TableauDePoint[7] = new Vector3(Origine.X, Origine.Y + Delta.Y, Origine.Z-Delta.Z); // H

        }
        protected override void LoadContent()
        {
            EffetDeBase = new BasicEffect(GraphicsDevice);
            EffetDeBase.VertexColorEnabled = true;
            base.LoadContent();

        }
        protected override void InitialiserSommets()
        {
            Sommets[0] = new VertexPositionColor(TableauDePoint[0], Couleur);
            Sommets[1] = new VertexPositionColor(TableauDePoint[1], Couleur);
            Sommets[2] = new VertexPositionColor(TableauDePoint[2], Couleur);
            Sommets[3] = new VertexPositionColor(TableauDePoint[3], Couleur);
            Sommets[4] = new VertexPositionColor(TableauDePoint[4], Couleur);
            Sommets[5] = new VertexPositionColor(TableauDePoint[5], Couleur);
            Sommets[6] = new VertexPositionColor(TableauDePoint[6], Couleur);
            Sommets[7] = new VertexPositionColor(TableauDePoint[7], Couleur);

            Sommets[8] = new VertexPositionColor(TableauDePoint[2], Couleur);
            Sommets[9] = new VertexPositionColor(TableauDePoint[4], Couleur);
            Sommets[10] = new VertexPositionColor(TableauDePoint[0], Couleur);
            Sommets[11] = new VertexPositionColor(TableauDePoint[6], Couleur);
            Sommets[12] = new VertexPositionColor(TableauDePoint[1], Couleur);
            Sommets[13] = new VertexPositionColor(TableauDePoint[7], Couleur);
            Sommets[14] = new VertexPositionColor(TableauDePoint[3], Couleur);
            Sommets[15] = new VertexPositionColor(TableauDePoint[5], Couleur);

            //Sommets[8] = new VertexPositionColor(TableauDePoint[2], Couleur);
            //Sommets[9] = new VertexPositionColor(TableauDePoint[4], Color.Magenta);
            //Sommets[10] = new VertexPositionColor(TableauDePoint[0], Color.Maroon);
            //Sommets[11] = new VertexPositionColor(TableauDePoint[6], Color.Chartreuse);
            //Sommets[12] = new VertexPositionColor(TableauDePoint[1], Color.Pink);
            //Sommets[13] = new VertexPositionColor(TableauDePoint[7], Color.Chartreuse);
            //Sommets[14] = new VertexPositionColor(TableauDePoint[3], Color.Chartreuse);
            //Sommets[15] = new VertexPositionColor(TableauDePoint[5], Color.Yellow);

        }
        void InitialiserCouleur()
        {
            for (int i = 0; i < Sommets.Length; ++i)
                Sommets[i].Color = Couleur;
        }

        float TempsÉcoulé = 0;
        float TempsÉcouléCouleur = 0;
        bool IsChangeColorRed = false;
        bool ResetColorBlack = true;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE &&
                GameController.PlayState != GameController.InGameState.Dead)
            {
                MondeÀRecalculer = true;
                TempsÉcoulé = 0;
                Position = CameraJeu.Position;
                InitialiserCouleur();

                if (OldHeatlh > MyPlayer.Health)
                {
                    TempsÉcoulé = 0;
                    OldHeatlh = MyPlayer.Health;
                    IsChangeColorRed = true;
                    Couleur = new Color(50, 0, 0, 50);
                    InitialiserCouleur();
                }

            }
            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE &&
            GameController.PlayState == GameController.InGameState.Dead)
            {
                if(ResetColorBlack)
                {
                    Couleur = new Color(0, 0, 0, 100);
                    ResetColorBlack = false;
                }

                Position = CameraJeu.Position;
                MondeÀRecalculer = true;

                CameraJeu.Position -= Vector3.UnitY * 0.07f;
                CameraJeu.Direction += Vector3.UnitY * 0.01f;

                TempsÉcoulé = 0;
                InitialiserCouleur();
                if (Couleur.A != 255)
                    Couleur.A++;
                else
                    MortPlayerMenu.ShowDeathRecap = true;
            }

            if ((TempsÉcouléCouleur += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.05f && 
                GameController.PlayState != GameController.InGameState.Dead)
            {
                TempsÉcouléCouleur = 0;
                if (IsChangeColorRed)
                {
                    InitialiserCouleur();
                    Couleur.A--;
                    Couleur.R--;
                    if (Couleur.A == 0)
                    {
                        InitialiserCouleur();
                        IsChangeColorRed = false;
                    }
                }
            }
        }

        RasterizerState JeuRasterizerState;
        public override void Draw(GameTime gameTime)
        {
            JeuRasterizerState = new RasterizerState();
            JeuRasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = JeuRasterizerState;
            EffetDeBase.World = GetMonde();
            EffetDeBase.View = CameraJeu.Vue;
            EffetDeBase.Projection = CameraJeu.Projection;
            foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
            {
                passeEffet.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, Sommets, 0, NB_TRIANGLES);
               GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, Sommets, 8, NB_TRIANGLES);
            }
            //foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
            //{
            //    passeEffet.Apply();
            //    GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, Sommets, 7, NB_TRIANGLES);
            //}
        }
    }
}