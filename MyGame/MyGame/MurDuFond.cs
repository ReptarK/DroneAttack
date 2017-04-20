using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
    public class MurDuFond : PrimitiveDeBase
    {
        const int NB_TRIANGLES_PAR_TUILE = 2;
        const int NB_SOMMETS_PAR_TRIANGLE = 3;
        const float MAX_COULEUR = 255f;

        Vector3 Étendue { get; set; }

        BasicEffect EffetDeBase { get; set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
        Texture2D CarteTerrain { get; set; }
        Texture2D TextureTerrain { get; set; }
        Vector3 Origine { get; set; }
        Vector2[,] PtsTexture { get; set; }
        Vector3[,] PtsSommets1 { get; set; }
        Vector3[,] PtsSommets2 { get; set; }
        Vector3[,] PtsSommets3 { get; set; }
        Vector3[,] PtsSommets4 { get; set; }
        VertexPositionTexture[] Sommets1 { get; set; }
        VertexPositionTexture[] Sommets2 { get; set; }
        VertexPositionTexture[] Sommets3 { get; set; }
        VertexPositionTexture[] Sommets4 { get; set; }

        Color Couleur { get; set; }

        Color[] HeightData { get; set; }
        public int NbRangées { get; set; }
        public int NbColonnes { get; set; }
        float DeltaTextY { get; set; }
        float NbNiveauxTextureFloat { get; set; }

        Vector3 Delta { get; set; }



        public MurDuFond(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector3 étendue)
           : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale)
        {
            Étendue = étendue;
        }

        public override void Initialize()
        {
            GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            InitialiserDonnéesTexture();
            NbSommets = (NbColonnes) * (NbRangées) * NB_SOMMETS_PAR_TRIANGLE * NB_TRIANGLES_PAR_TUILE;
            Origine = new Vector3(0, 0, 0); //pour centrer la primitive au point (0,0,0)
            AllouerTableaux();
            CréerTableauPoints();
            base.Initialize();
        }


        void InitialiserDonnéesTexture()
        {
            NbColonnes = 30;
            NbRangées = 30;
            Delta = new Vector3(Étendue.X / (NbRangées - 1), Étendue.Y / NbRangées, Étendue.Z / (NbColonnes - 1));
        }

        void AllouerTableaux()
        {
            NbSommets = (NbColonnes) * (NbRangées) * NB_SOMMETS_PAR_TRIANGLE * NB_TRIANGLES_PAR_TUILE;
            Sommets1 = new VertexPositionTexture[NbSommets];
            Sommets2 = new VertexPositionTexture[NbSommets];
            Sommets3 = new VertexPositionTexture[NbSommets];
            Sommets4 = new VertexPositionTexture[NbSommets];
            PtsSommets1 = new Vector3[NbRangées, NbColonnes];
            PtsSommets2 = new Vector3[NbRangées, NbColonnes];
            PtsSommets3 = new Vector3[NbRangées, NbColonnes];
            PtsSommets4 = new Vector3[NbRangées, NbColonnes];

        }

        protected override void LoadContent()
        {
            base.LoadContent();
            EffetDeBase = new BasicEffect(GraphicsDevice);
            InitialiserParamètresEffetDeBase();

        }
        private void LoadTextures()
        {
            TextureTerrain = GestionnaireDeTextures.Find("MurExterneTexture");
        }

        void InitialiserParamètresEffetDeBase()
        {
            EffetDeBase.TextureEnabled = true;
            EffetDeBase.Texture = TextureTerrain;
        }

        private void CréerTableauPoints()
        {
            Vector2 v = new Vector2(0, 0);
            for (int i = 0; i < PtsSommets1.GetLength(0); ++i)

            {
                for (int j = 0; j < PtsSommets1.GetLength(1); ++j)
                {
                    PtsSommets1[i, j] = new Vector3(Origine.X + Étendue.X - 2, Origine.Y + (j * Delta.Y),
                                                   Origine.Z + (i * Delta.Z));
                    PtsSommets2[i, j] = new Vector3(Origine.X, Origine.Y + (j * Delta.Y),
                                                   Origine.Z + (i * Delta.Z));
                    PtsSommets3[i, j] = new Vector3(Origine.X + (i * Delta.X), Origine.Y + (j * Delta.Y),
                                                   Origine.Z);
                    PtsSommets4[i, j] = new Vector3(Origine.X + (i * Delta.X), Origine.Y + (j * Delta.Y),
                                                   Origine.Z + Étendue.Z - 2);

                }
            }
        }

        protected override void InitialiserSommets()
        {
            LoadTextures();
            CréerTableauPointsTexture();
            int cpt = -1;
            float DeltaTexture = (0.05f);

            int compteuri = 0;
            int compteurj = 0;

            for (int j = 0; j < NbRangées - 1; j++)
            {
                if (j % 10 == 0)
                    compteurj = 0;

                for (int i = 0; i < NbColonnes - 1; i++)
                {
                    if (i % 10 == 0)
                        compteuri = 0;

                    //Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i + 1, j], new Vector2(0 + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    //Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i, j], new Vector2(DeltaTexture + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    //Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i + 1, j + 1], new Vector2(0 + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));


                    //Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i, j], new Vector2(DeltaTexture + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    //Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i, j + 1], new Vector2(DeltaTexture + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));
                    //Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i + 1, j + 1], new Vector2(0 + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));

                   

                    Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i + 1, j], new Vector2(0 , 0 ));
                    Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i, j], new Vector2(1 , 0 ));
                    Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i + 1, j + 1], new Vector2(0, 1));


                    Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i, j], new Vector2(1, 0 ));
                    Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i, j + 1], new Vector2(1, 1));
                    Sommets1[++cpt] = new VertexPositionTexture(PtsSommets1[i + 1, j + 1], new Vector2(0, 1));
                    compteuri++;
                }
                compteurj++;
            }
            cpt = -1;
            compteuri = 0;
            compteurj = 0;

            for (int j = 0; j < NbRangées - 1; j++)
            {
                if (j % 10 == 0)
                    compteurj = 0;

                for (int i = 0; i < NbColonnes - 1; i++)
                {
                    if (i % 10 == 0)
                        compteuri = 0;

                    Sommets2[++cpt] = new VertexPositionTexture(PtsSommets2[i, j], new Vector2(0 + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets2[++cpt] = new VertexPositionTexture(PtsSommets2[i + 1, j], new Vector2(DeltaTexture + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets2[++cpt] = new VertexPositionTexture(PtsSommets2[i, j + 1], new Vector2(0 + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));


                    Sommets2[++cpt] = new VertexPositionTexture(PtsSommets2[i + 1, j], new Vector2(DeltaTexture + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets2[++cpt] = new VertexPositionTexture(PtsSommets2[i + 1, j + 1], new Vector2(DeltaTexture + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));
                    Sommets2[++cpt] = new VertexPositionTexture(PtsSommets2[i, j + 1], new Vector2(0 + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));



                    compteuri++;
                }
                compteurj++;
            }

            cpt = -1;

            compteuri = 0;
            compteurj = 0;

            for (int j = 0; j < NbRangées - 1; j++)
            {
                if (j % 10 == 0)
                    compteurj = 0;

                for (int i = 0; i < NbColonnes - 1; i++)
                {
                    if (i % 10 == 0)
                        compteuri = 0;

                    Sommets3[++cpt] = new VertexPositionTexture(PtsSommets3[i + 1, j], new Vector2(0 + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets3[++cpt] = new VertexPositionTexture(PtsSommets3[i, j], new Vector2(DeltaTexture + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets3[++cpt] = new VertexPositionTexture(PtsSommets3[i + 1, j + 1], new Vector2(0 + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));


                    Sommets3[++cpt] = new VertexPositionTexture(PtsSommets3[i, j], new Vector2(DeltaTexture + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets3[++cpt] = new VertexPositionTexture(PtsSommets3[i, j + 1], new Vector2(DeltaTexture + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));
                    Sommets3[++cpt] = new VertexPositionTexture(PtsSommets3[i + 1, j + 1], new Vector2(0 + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));



                    compteuri++;
                }
                compteurj++;
            }
            cpt = -1;

            compteuri = 0;
            compteurj = 0;

            for (int j = 0; j < NbRangées - 1; j++)
            {
                if (j % 10 == 0)
                    compteurj = 0;

                for (int i = 0; i < NbColonnes - 1; i++)
                {
                    if (i % 10 == 0)
                        compteuri = 0;

                    Sommets4[++cpt] = new VertexPositionTexture(PtsSommets4[i, j], new Vector2(0 + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets4[++cpt] = new VertexPositionTexture(PtsSommets4[i + 1, j], new Vector2(DeltaTexture + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets4[++cpt] = new VertexPositionTexture(PtsSommets4[i, j + 1], new Vector2(0 + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));


                    Sommets4[++cpt] = new VertexPositionTexture(PtsSommets4[i + 1, j], new Vector2(DeltaTexture + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets4[++cpt] = new VertexPositionTexture(PtsSommets4[i + 1, j + 1], new Vector2(DeltaTexture + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));
                    Sommets4[++cpt] = new VertexPositionTexture(PtsSommets4[i, j + 1], new Vector2(0 + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));



                    compteuri++;
                }
                compteurj++;
            }
        }
        private void CréerTableauPointsTexture()
        {


            PtsTexture = new Vector2[2, 2];


            PtsTexture[0, 0] = new Vector2(0, 0);
            PtsTexture[1, 0] = new Vector2(1, 0);
            PtsTexture[0, 1] = new Vector2(0, 1);
            PtsTexture[1, 1] = new Vector2(1, 1);

        }

        public override void Draw(GameTime gameTime)
        {
            EffetDeBase.World = GetMonde();
            EffetDeBase.View = MainGame.CaméraJeu.Vue;
            EffetDeBase.Projection = MainGame.CaméraJeu.Projection;
            foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
            {
                passeEffet.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, Sommets1, 0, Sommets1.Length / NB_SOMMETS_PAR_TRIANGLE);
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, Sommets2, 0, Sommets2.Length / NB_SOMMETS_PAR_TRIANGLE);
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, Sommets3, 0, Sommets3.Length / NB_SOMMETS_PAR_TRIANGLE);
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, Sommets4, 0, Sommets4.Length / NB_SOMMETS_PAR_TRIANGLE);

            }
        }
        public Vector3 GetPointSpatial(int x, int y)
        {
            return new Vector3(PtsSommets1[x, y].X, PtsSommets1[x, y].Y, PtsSommets1[x, y].Z);
        }
    }
}
