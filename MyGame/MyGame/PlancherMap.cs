using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyGame;
using MyGame.Entités;

namespace AtelierXNA
{
    public class PlancherMap : PrimitiveDeBaseAnimée, ICollisionableList
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
        VertexPositionTexture[] Sommets { get; set; }
        
        Color Couleur { get; set; }

        Color[] HeightData { get; set; }
        public int NbRangées { get; set; }
        public int NbColonnes { get; set; }
        float DeltaTextY { get; set; }
        float NbNiveauxTextureFloat { get; set; }

        Vector3 Delta { get; set; }

        public List<BoundingBox> ListeBoundingBox { get; set; }

        public List<Vector3> ListeNormales { get; set; }

        Caméra1stPerson CaméraJeu;

        public PlancherMap(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector3 étendue, float intervalleMAJ)
           : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, intervalleMAJ)
        {
            Étendue = étendue;
        }

        public override void Initialize()
        {
            ListeBoundingBox = new List<BoundingBox>();
            ListeNormales = new List<Vector3>();

            GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            CaméraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            InitialiserDonnéesTexture();
            NbSommets = (NbColonnes) * (NbRangées) * NB_SOMMETS_PAR_TRIANGLE * NB_TRIANGLES_PAR_TUILE;
            Origine = new Vector3(0, 0, 0); //pour centrer la primitive au point (0,0,0)

            GenererBoundingBox();

            AllouerTableaux();
            CréerTableauPoints();
            base.Initialize();
        }


        void InitialiserDonnéesTexture()
        {
            NbColonnes = 240;
            NbRangées = 240;
            Delta = new Vector3(Étendue.X / NbColonnes, Étendue.Y / MAX_COULEUR, Étendue.Z / NbRangées);
        }

        void AllouerTableaux()
        {
            Sommets = new VertexPositionTexture[NbSommets];
            PtsSommets = new Vector3[240, 240];
          
        }

        public void GenererBoundingBox()
        {
            ListeBoundingBox.Add(new BoundingBox(Origine, Étendue));
            ListeNormales.Add(Vector3.UnitY * Data.GRAVITÉ);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            EffetDeBase = new BasicEffect(GraphicsDevice);
            InitialiserParamètresEffetDeBase();
            
        }
        private void LoadTextures()
        {
            TextureTerrain = GestionnaireDeTextures.Find("SolTexture");
        }

        void InitialiserParamètresEffetDeBase()
        {
            EffetDeBase.TextureEnabled = true;
            EffetDeBase.Texture = TextureTerrain;
        }

        private void CréerTableauPoints()
        {
            Vector2 v = new Vector2(0, 0);
            for (int i = 0; i < PtsSommets.GetLength(0); ++i)
            {
                for (int j = 0; j < PtsSommets.GetLength(1); ++j)
                {
                    PtsSommets[i, j] = new Vector3(Origine.X + (i * Delta.X), Origine.Y,
                                                   Origine.Z + (j * Delta.Z));
                   
                }
            }
        }
        
        protected override void InitialiserSommets()
        {
            LoadTextures();
            CréerTableauPointsTexture();
            float DeltaTexture=(0.10f);
            int cpt = -1;
            int compteuri = 0;
            int compteurj = 0;
            
            for (int j = 0; j < NbRangées-1; j++)
            {
                if (j % 9 == 0)
                    compteurj = 0;

                for (int i = 0; i < NbColonnes-1; i++)
                {
                    if (i % 9 == 0)
                        compteuri = 0;

                    Sommets[++cpt] = new VertexPositionTexture(PtsSommets[i, j], new Vector2(0 + DeltaTexture*compteuri, 0  + DeltaTexture * compteurj));
                    Sommets[++cpt] = new VertexPositionTexture(PtsSommets[i + 1, j], new Vector2(DeltaTexture + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets[++cpt] = new VertexPositionTexture(PtsSommets[i, j + 1], new Vector2(0 + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));


                    Sommets[++cpt] = new VertexPositionTexture(PtsSommets[i + 1, j], new Vector2(DeltaTexture + DeltaTexture * compteuri, 0 + DeltaTexture * compteurj));
                    Sommets[++cpt] = new VertexPositionTexture(PtsSommets[i + 1, j + 1], new Vector2(DeltaTexture + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));
                    Sommets[++cpt] = new VertexPositionTexture(PtsSommets[i, j + 1], new Vector2(0 + DeltaTexture * compteuri, DeltaTexture + DeltaTexture * compteurj));
                    
                    //

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
            if(MainGame.MainGameState == MainGame.GameState.Jeu)
            {
                EffetDeBase.World = GetMonde();
                EffetDeBase.View = CaméraJeu.Vue;
                EffetDeBase.Projection = CaméraJeu.Projection;
                foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
                {
                    passeEffet.Apply();
                    GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, Sommets, 0, Sommets.Length / NB_SOMMETS_PAR_TRIANGLE);

                }
            }
        }
        public Vector3 GetPointSpatial(int x, int y)
        {
            return new Vector3(PtsSommets[x, y].X, PtsSommets[x, y].Y, PtsSommets[x, y].Z);
        }
    }
}
