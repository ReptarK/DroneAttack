using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
    public class PlanTexturé : Plan
    {
        RessourcesManager<Texture2D> gestionnaireDeTextures;
        Texture2D texture;
        VertexPositionTexture[] Sommets { get; set; }
        Vector2[,] PtsTexture { get; set; }
        Vector2 DeltaTexture { get; set; }
        BlendState GestionAlpha { get; set; }
        string NomTexture { get; set; }
        

        public PlanTexturé(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector2 étendue, Vector2 charpente,string nomTexture, float intervalleMAJ)
              : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, étendue, charpente, intervalleMAJ)
        {
            NomTexture = nomTexture;
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {

            gestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            texture = gestionnaireDeTextures.Find(NomTexture);
            base.LoadContent();
        }

        protected override void CréerTableauSommets()
        {
            Sommets = new VertexPositionTexture[(NbColonnes * 2 + 2) * NbRangées];
        }

        protected override void InitialiserParamètresEffetDeBase()
        {
            EffetDeBase.TextureEnabled = true;
            EffetDeBase.Texture = texture;
            GestionAlpha = BlendState.AlphaBlend;
        }


        protected override void DessinerTriangleStrip(int noStrip)
        {
            GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, Sommets, noStrip * (NbTrianglesParStrip + 2), NbTrianglesParStrip);

        }
        private void CréerTableauPointsTexture()
        {
            DeltaTexture = new Vector2(1 / (float)(NbColonnes), 1 / (float)(NbRangées));

            PtsTexture = new Vector2[NbColonnes + 1, NbRangées + 1];

            for (int j = 0; j <= NbRangées; ++j)
            {
                for (int i = 0; i <= NbColonnes; ++i)
                {
                   PtsTexture[i,j] = new Vector2(DeltaTexture.X*i, (1-DeltaTexture.Y*j));
                }
            }
        }

        protected override void InitialiserSommets()
        {
            CréerTableauPointsTexture();

            int NoSommet = -1;

            for (int j = 0; j < NbRangées; ++j)
            {
                for (int i = 0; i <= NbColonnes ; ++i)
                {
                    Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, j], PtsTexture[i, j]);
                    Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, j+1], PtsTexture[i, j+1]);
                }
            }
        }

        RasterizerState JeuRasterizerState;
        public override void Draw(GameTime gameTime)
        {
            JeuRasterizerState = new RasterizerState();
            JeuRasterizerState.CullMode = CullMode.CullClockwiseFace;
            GraphicsDevice.RasterizerState = JeuRasterizerState;

            base.Draw(gameTime);
        }

    }
}
