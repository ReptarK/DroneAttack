using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
    public class TerrainAvecBase : PrimitiveDeBaseAnimée
    {
        const int NB_TRIANGLES_PAR_TUILE = 2;
        const int NB_SOMMETS_PAR_TRIANGLE = 3;
        const float MAX_COULEUR = 255f;
        Color COLORBLACK = Color.Black;

        Vector3 Étendue { get; set; }
        string NomCarteTerrain { get; set; }
        string[] NomTexturesTerrain { get; set; }
        string NomTextureBase { get; set; }

        BasicEffect EffetDeBase { get; set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
        Texture2D CarteTerrain { get; set; }
        Texture2D TextureTerrain { get; set; }
        Vector3 Origine { get; set; }
        Vector2[,] PtsTexture { get; set; }
        Vector3[,] PtsSommets { get; set; }
        VertexPositionColor[] Sommets { get; set; }
        VertexPositionColor[] SommetsBase1 { get; set; }
        VertexPositionColor[] SommetsBase2 { get; set; }
        VertexPositionColor[] SommetsBase3 { get; set; }
        VertexPositionColor[] SommetsBase4 { get; set; }
        Color Couleur { get; set; }

        Color[] HeightData { get; set; }
        public int NbRangées { get; set; }
        public int NbColonnes { get; set; }
        float DeltaTextY { get; set; }
        float NbNiveauxTextureFloat { get; set; }

        Vector3 Delta { get; set; }
        Color[] Texture1Data { get; set; }
        Color[] Texture2Data { get; set; }
        Color Texture1 { get; set; }
        Color Texture2 { get; set; }

        public TerrainAvecBase(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector3 étendue, string nomTextureCarte, string[] nomsTexturesTerrain, string nomTextureBase, float intervalleMAJ)
           : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, intervalleMAJ)
        {
            Étendue = étendue;
            NomCarteTerrain = nomTextureCarte;
            NomTexturesTerrain = nomsTexturesTerrain;
            NomTextureBase = nomTextureBase;
        }

        public override void Initialize()
        {
            GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            InitialiserDonnéesCarte();
            InitialiserDonnéesTexture();
            NbSommets = (NbColonnes) * (NbRangées) * NB_SOMMETS_PAR_TRIANGLE * NB_TRIANGLES_PAR_TUILE;
            DeltaTextY = 1 / NbNiveauxTextureFloat;
            Origine = new Vector3(-Étendue.X / 2, 0, -Étendue.Z / 2); //pour centrer la primitive au point (0,0,0)
            AllouerTableaux();
            CréerTableauPoints();
            base.Initialize();
        }

        void InitialiserDonnéesCarte()
        {
            CarteTerrain = GestionnaireDeTextures.Find(NomCarteTerrain);
            HeightData = new Color[CarteTerrain.Width * CarteTerrain.Height];
            CarteTerrain.GetData<Color>(HeightData);
        }

        void InitialiserDonnéesTexture()
        {
            NbColonnes = CarteTerrain.Width - 1;
            NbRangées = CarteTerrain.Height - 1;
            Delta = new Vector3(Étendue.X / NbColonnes, Étendue.Y / MAX_COULEUR, Étendue.Z / NbRangées);
        }

        void AllouerTableaux()
        {
            Sommets = new VertexPositionColor[NbSommets];
            PtsSommets = new Vector3[CarteTerrain.Width, CarteTerrain.Height];
            SommetsBase1 = new VertexPositionColor[(int)NbColonnes * 3 * 2];
            SommetsBase2 = new VertexPositionColor[(int)NbColonnes * 3 * 2];
            SommetsBase3 = new VertexPositionColor[(int)NbRangées * 3 * 2];
            SommetsBase4 = new VertexPositionColor[(int)NbRangées * 3 * 2];
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            EffetDeBase = new BasicEffect(GraphicsDevice);
            InitialiserParamètresEffetDeBase();

        }
        private void LoadTextures()
        {
            Texture2D texture1 = GestionnaireDeTextures.Find(NomTexturesTerrain[0]);
            Texture2D texture2 = GestionnaireDeTextures.Find(NomTexturesTerrain[1]);

            Texture1Data = new Color[texture1.Width * texture1.Height];
            texture1.GetData<Color>(Texture1Data);

            Texture2Data = new Color[texture2.Width * texture2.Height];
            texture2.GetData<Color>(Texture2Data);
            MoyenneCouleurTexels();
        }

        void InitialiserParamètresEffetDeBase()
        {
            //EffetDeBase.TextureEnabled = true;
            //EffetDeBase.Texture = TextureTerrain;
            EffetDeBase.VertexColorEnabled = true;
        }

        private void CréerTableauPoints()
        {
            Vector2 v = new Vector2(0, 0);
            for (int i = 0; i < PtsSommets.GetLength(0); ++i)
            {
                for (int j = 0; j < PtsSommets.GetLength(1); ++j)
                {
                    PtsSommets[i, j] = new Vector3(Origine.X + (i * Delta.X), Origine.Y + (HeightData[i * PtsSommets.GetLength(0) + j].B * Delta.Y),
                                                   Origine.Z + (j * Delta.Z));

                }
            }
        }
        //private void InitialiserSommetBase()
        //{

        //    int cpt = -1;

        //    for (int i = 0; i < PtsSommets.GetLength(0) - 1; ++i)
        //    {
        //        SommetsBase1[++cpt].Position = new Vector3(PtsSommets[i, NbRangées].X, 0, PtsSommets[i, NbRangées].Z);
        //        SommetsBase1[cpt].Color = COLORBLACK;

        //        SommetsBase1[++cpt].Position = new Vector3(PtsSommets[i, NbRangées].X, PtsSommets[i, NbRangées].Y, PtsSommets[i, NbRangées].Z);
        //        SommetsBase1[cpt].Color = COLORBLACK;

        //        SommetsBase1[++cpt].Position = new Vector3(PtsSommets[i, NbRangées].X + Delta.X, 0, PtsSommets[i, NbRangées].Z);
        //        SommetsBase1[cpt].Color = COLORBLACK;

        //        SommetsBase1[++cpt].Position = new Vector3(PtsSommets[i, NbRangées].X + Delta.X, 0, PtsSommets[i, NbRangées].Z);
        //        SommetsBase1[cpt].Color = COLORBLACK;

        //        SommetsBase1[++cpt].Position = new Vector3(PtsSommets[i, NbRangées].X, PtsSommets[i, NbRangées].Y, PtsSommets[i, NbRangées].Z);
        //        SommetsBase1[cpt].Color = COLORBLACK;

        //        SommetsBase1[++cpt].Position = new Vector3(PtsSommets[i, NbRangées].X + Delta.X, PtsSommets[i + 1, NbRangées].Y, PtsSommets[i, NbRangées].Z);
        //        SommetsBase1[cpt].Color = COLORBLACK;
        //    }
        //    cpt = -1;

        //    for (int i = 0; i < PtsSommets.GetLength(0) - 1; ++i)
        //    {
        //        SommetsBase2[++cpt].Position = new Vector3(PtsSommets[i, 0].X + Delta.X, 0, PtsSommets[i, 0].Z);
        //        SommetsBase2[cpt].Color = COLORBLACK;

        //        SommetsBase2[++cpt].Position = new Vector3(PtsSommets[i, 0].X, PtsSommets[i, 0].Y, PtsSommets[i, 0].Z);
        //        SommetsBase2[cpt].Color = COLORBLACK;

        //        SommetsBase2[++cpt].Position = new Vector3(PtsSommets[i, 0].X, 0, PtsSommets[i, 0].Z);
        //        SommetsBase2[cpt].Color = COLORBLACK;

        //        SommetsBase2[++cpt].Position = new Vector3(PtsSommets[i, 0].X + Delta.X, PtsSommets[i + 1, 0].Y, PtsSommets[i, 0].Z);
        //        SommetsBase2[cpt].Color = COLORBLACK;

        //        SommetsBase2[++cpt].Position = new Vector3(PtsSommets[i, 0].X, PtsSommets[i, 0].Y, PtsSommets[i, 0].Z);
        //        SommetsBase2[cpt].Color = COLORBLACK;

        //        SommetsBase2[++cpt].Position = new Vector3(PtsSommets[i, 0].X + Delta.X, 0, PtsSommets[i, 0].Z);
        //        SommetsBase2[cpt].Color = COLORBLACK;

        //    }
        //    cpt = -1;
        //    for (int j = 0; j < PtsSommets.GetLength(1) - 1; ++j)
        //    {
        //        SommetsBase3[++cpt].Position = new Vector3(PtsSommets[0, j].X, 0, PtsSommets[0, j].Z);
        //        SommetsBase3[cpt].Color = COLORBLACK;

        //        SommetsBase3[++cpt].Position = new Vector3(PtsSommets[0, j].X, PtsSommets[0, j].Y, PtsSommets[0, j].Z);
        //        SommetsBase3[cpt].Color = COLORBLACK;

        //        SommetsBase3[++cpt].Position = new Vector3(PtsSommets[0, j].X, 0, PtsSommets[0, j].Z + Delta.Z);
        //        SommetsBase3[cpt].Color = COLORBLACK;

        //        SommetsBase3[++cpt].Position = new Vector3(PtsSommets[0, j].X, PtsSommets[0, j].Y, PtsSommets[0, j].Z);
        //        SommetsBase3[cpt].Color = COLORBLACK;

        //        SommetsBase3[++cpt].Position = new Vector3(PtsSommets[0, j].X, PtsSommets[0, j + 1].Y, PtsSommets[0, j].Z + Delta.Z);
        //        SommetsBase3[cpt].Color = COLORBLACK;

        //        SommetsBase3[++cpt].Position = new Vector3(PtsSommets[0, j].X, 0, PtsSommets[0, j].Z + Delta.Z);
        //        SommetsBase3[cpt].Color = COLORBLACK;
        //    }
        //    cpt = -1;

        //    for (int j = 0; j < PtsSommets.GetLength(1) - 1; ++j)
        //    {
        //        SommetsBase4[++cpt].Position = new Vector3(PtsSommets[NbColonnes, j].X, 0, PtsSommets[NbColonnes, j].Z + Delta.Z);
        //        SommetsBase4[cpt].Color = COLORBLACK;

        //        SommetsBase4[++cpt].Position = new Vector3(PtsSommets[NbColonnes, j].X, PtsSommets[NbColonnes, j].Y, PtsSommets[NbColonnes, j].Z);
        //        SommetsBase4[cpt].Color = COLORBLACK;

        //        SommetsBase4[++cpt].Position = new Vector3(PtsSommets[NbColonnes, j].X, 0, PtsSommets[NbColonnes, j].Z);
        //        SommetsBase4[cpt].Color = COLORBLACK;

        //        SommetsBase4[++cpt].Position = new Vector3(PtsSommets[NbColonnes, j].X, PtsSommets[NbColonnes, j + 1].Y, PtsSommets[NbColonnes, j].Z + Delta.Z);
        //        SommetsBase4[cpt].Color = COLORBLACK;

        //        SommetsBase4[++cpt].Position = new Vector3(PtsSommets[NbColonnes, j].X, PtsSommets[NbColonnes, j].Y, PtsSommets[NbColonnes, j].Z);
        //        SommetsBase4[cpt].Color = COLORBLACK;

        //        SommetsBase4[++cpt].Position = new Vector3(PtsSommets[NbColonnes, j].X, 0, PtsSommets[NbColonnes, j].Z + Delta.Z);
        //        SommetsBase4[cpt].Color = COLORBLACK;
        //    }

        //}
        protected override void InitialiserSommets()
        {
            //InitialiserSommetBase();
            LoadTextures();
            int cpt = -1;
            float hauteur;
            for (int j = 0; j < NbRangées; j++)
            {
                for (int i = 0; i < NbColonnes; i++)
                {

                    hauteur = HauteurMoyenneVector3(new Vector3[] { PtsSommets[i, j], PtsSommets[i, j + 1], PtsSommets[i + 1, j] });
                    Couleur = ChoisirCouleur(hauteur, i, j);
                    Sommets[++cpt] = new VertexPositionColor(PtsSommets[i, j], Couleur);
                    Sommets[++cpt] = new VertexPositionColor(PtsSommets[i + 1, j], Couleur);
                    Sommets[++cpt] = new VertexPositionColor(PtsSommets[i, j + 1], Couleur);

                    hauteur = HauteurMoyenneVector3(new Vector3[] { PtsSommets[i + 1, j], PtsSommets[i + 1, j + 1], PtsSommets[i, j + 1] });
                    Couleur = ChoisirCouleur(hauteur, i, j);
                    Sommets[++cpt] = new VertexPositionColor(PtsSommets[i + 1, j], Couleur);
                    Sommets[++cpt] = new VertexPositionColor(PtsSommets[i + 1, j + 1], Couleur);
                    Sommets[++cpt] = new VertexPositionColor(PtsSommets[i, j + 1], Couleur);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (MainGame.MainGameState == MainGame.GameState.Jeu)
            {
                EffetDeBase.World = GetMonde();
                EffetDeBase.View = CaméraJeu.Vue;
                EffetDeBase.Projection = CaméraJeu.Projection;
                foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
                {
                    passeEffet.Apply();
                    GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, Sommets, 0, Sommets.Length / NB_SOMMETS_PAR_TRIANGLE);
                }

                //int nbTrianglesÀAfficher = Sommets.Length / NB_SOMMETS_PAR_TRIANGLE;
                //int NbTrianglesAffichés = 0;
                //int indiceSommet = 0;
                //while (NbTrianglesAffichés < nbTrianglesÀAfficher)
                //{
                //    int nbTriangles = (int)MathHelper.Min(65535, nbTrianglesÀAfficher - NbTrianglesAffichés);
                //    GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, Sommets, indiceSommet, nbTriangles);
                //    indiceSommet += nbTriangles * NB_SOMMETS_PAR_TRIANGLE;
                //    NbTrianglesAffichés += nbTriangles;
                //}
                // GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, SommetsBase1, 0, SommetsBase1.Length / NB_SOMMETS_PAR_TRIANGLE);
                //GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, SommetsBase2, 0, SommetsBase2.Length / NB_SOMMETS_PAR_TRIANGLE);
                //GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, SommetsBase3, 0, SommetsBase3.Length / NB_SOMMETS_PAR_TRIANGLE);
                //DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, SommetsBase4, 0, SommetsBase4.Length / NB_SOMMETS_PAR_TRIANGLE);
            }
        }
        public Vector3 GetPointSpatial(int x, int y)
        {
            return new Vector3(PtsSommets[x, y].X, PtsSommets[x, y].Y, PtsSommets[x, y].Z);
        }
        public Vector3 GetNormale(int x, int y)
        {
            Vector3 A = GetPointSpatial(x, y);
            Vector3 AB = Vector3.Subtract(GetPointSpatial(x + 1, y), A);
            Vector3 AC = Vector3.Subtract(GetPointSpatial(x, y + 1), A);
            Vector3 normal = Vector3.Cross(AB, AC);
            Vector3.Normalize(normal);
            return normal;
        }
        private Color ChoisirCouleur(float h, int x, int y)
        {
            Color couleur;
            float pourcentage1 = h / Étendue.Y;
            float pourcentage2 = 1 - pourcentage1;

            couleur = new Color(Convert.ToByte(Texture2Data[x * y].R * pourcentage1 + Texture1Data[x * y].R * pourcentage2),
                Convert.ToByte(Texture2Data[x * y].G * pourcentage1 + Texture1Data[x * y].G * pourcentage2),
                Convert.ToByte(Texture2Data[x * y].B * pourcentage1 + Texture1Data[x * y].B * pourcentage2));

            // POUR METTRE TERRAIN LISSE 

            //couleur = new Color(Convert.ToByte(Texture1.R * pourcentage2 + Texture2.R * pourcentage1),
            //    Convert.ToByte(Texture1.G * pourcentage2 + Texture2.G * pourcentage1),
            //    Convert.ToByte(Texture1.B * pourcentage2 + Texture2.B * pourcentage1));
            return couleur;
        }
        float HauteurMoyenneVector3(Vector3[] tableauVecteurs)
        {
            float hauteurTotale = 0;
            for (int i = 0; i < tableauVecteurs.Length; ++i)
            {
                hauteurTotale += tableauVecteurs[i].Y;
            }

            return hauteurTotale / tableauVecteurs.Length;
        }
        private void MoyenneCouleurTexels()
        {
            // POUR METTRE TERRAIN LISSE 

            //int r = 0;
            //int g = 0;
            //int b = 0;

            //for(int x=0;x<Texture1Data.Length;x++)
            //{
            //    r = r + (Texture1Data[x].R);
            //    g = g + (Texture1Data[x].G);
            //    b = b + (Texture1Data[x].B);
            //}
            //r = r / Texture1Data.Length;
            //g = g / Texture1Data.Length;
            //b = b / Texture1Data.Length;
            //Texture1 = new Color(r, g, b);

            //int r2 = 0;
            //int g2 = 0;
            //int b2= 0;
            //for (int x = 0; x < Texture2Data.Length; x++)
            //{
            //    r2 = r2 + (Texture2Data[x].R);
            //    g2 = g2 + (Texture2Data[x].G);
            //    b2 = b2 + (Texture2Data[x].B);
            //}
            //r2 = r2 / Texture2Data.Length;
            //g2 = g2 / Texture2Data.Length;
            //b2 = b2 / Texture2Data.Length;
            //Texture2 = new Color(r2, g2, b2);

        }
    }
}
