using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame;

namespace AtelierXNA
{
    class CubeTexturéAnimé : PrimitiveDeBaseAnimée
    {
        const int NB_SOMMETS = 16;
        const int NB_TRIANGLES = 6;

        RasterizerState JeuRasterizerState;

        VertexPositionTexture[] Sommets { get; set; }
        Vector3 Origine { get; set; }
        public float DeltaX { get; set; }
        public float DeltaY { get; set; }
        public float DeltaZ { get; set; }
        BasicEffect EffetDeBase { get; set; }
        Vector3[] TableauDePoint { get; set; }
        protected Vector2 CharpenteTexture { get; private set; }
        protected Vector2[,] PtsTexture { get; private set; }
        string NomTextureCube { get; set; }
        float Rayon { get; set; }
        protected float IntervalMAJ { get; set; }

        public float Force;

        Caméra1stPerson CaméraJeu;

        //COLLISION : PARAMETRES
        public List<BoundingBox> ListeBoundingBox { get; set; }
        public List<Vector3> ListeNormales { get; set; }

        public bool ÀDétruire { get; set; }

        RessourcesManager<Texture2D> gestionnaireDeTextures;
        Texture2D texture;
        public CubeTexturéAnimé(Game jeu, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale,string nomTextureCube, Vector3 dimension, float intervalMAJ) 
            : base(jeu, homothétieInitiale, rotationInitiale, positionInitiale, intervalMAJ)
        {
            CaméraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            NomTextureCube = nomTextureCube;
            DeltaX = dimension.X;
            DeltaY = dimension.Y;
            DeltaZ = dimension.Z;
            Origine = new Vector3(-DeltaX/2, 0, -DeltaZ/2);
            
            SetTableauDePoints();
            Rayon = TrouverPlusGrosDelta();
            IntervalMAJ = intervalMAJ;
        }

        public override void Initialize()
        {
            ListeBoundingBox = new List<BoundingBox>();
            ListeNormales = new List<Vector3>();
            Sommets = new VertexPositionTexture[NB_SOMMETS];

            CréerTableauPointsTexture();
            GenererBoundingBox();
            base.Initialize();
        }

        public void GenererBoundingBox()
        {
            ListeBoundingBox.Add(new BoundingBox(PositionInitiale, PositionInitiale + new Vector3(DeltaX, DeltaY, 0)));
            ListeNormales.Add(-Vector3.UnitZ * 3);

            ListeBoundingBox.Add(new BoundingBox(PositionInitiale + new Vector3(DeltaX, 0, 0), PositionInitiale + new Vector3(DeltaX, DeltaY, DeltaZ)));
            ListeNormales.Add(Vector3.UnitX * 3);

            ListeBoundingBox.Add(new BoundingBox(PositionInitiale, PositionInitiale + new Vector3(0, DeltaY, DeltaZ)));
            ListeNormales.Add(-Vector3.UnitX * 3);

            ListeBoundingBox.Add(new BoundingBox(PositionInitiale + new Vector3(0, 0, DeltaZ), PositionInitiale + new Vector3(DeltaX, DeltaY, DeltaZ)));
            ListeNormales.Add(Vector3.UnitZ * 3);

            ListeBoundingBox.Add(new BoundingBox(PositionInitiale + new Vector3(0, DeltaY, 0), PositionInitiale + new Vector3(DeltaX, DeltaY, DeltaZ)));
            ListeNormales.Add(Vector3.UnitY * Data.GRAVITÉ);

            //ListeBoundingBox.Add(new BoundingBox(PositionInitiale + new Vector3(1, 0, 1), PositionInitiale + new Vector3(DeltaX - 1, 1, DeltaZ - 1)));
            //ListeNormales.Add(Vector3.UnitY * 50);
        }

        private void SetTableauDePoints()
        {
            TableauDePoint = new Vector3[8];
            TableauDePoint[0] = Origine; // A
            TableauDePoint[1] = new Vector3(Origine.X, Origine.Y + DeltaY, Origine.Z); // B
            TableauDePoint[2] = new Vector3(Origine.X + DeltaX, Origine.Y, Origine.Z); // C
            TableauDePoint[3] = new Vector3(Origine.X + DeltaX, Origine.Y + DeltaY, Origine.Z); // D
            TableauDePoint[4] = new Vector3(Origine.X + DeltaX, Origine.Y, Origine.Z + DeltaZ); // E
            TableauDePoint[5] = new Vector3(Origine.X + DeltaX, Origine.Y + DeltaY, Origine.Z + DeltaZ); // F
            TableauDePoint[6] = new Vector3(Origine.X, Origine.Y, Origine.Z + DeltaZ); // G
            TableauDePoint[7] = new Vector3(Origine.X, Origine.Y + DeltaY, Origine.Z + DeltaZ); // H

            //CHANGER DELTA - a + du DeltaZ

        }
        protected override void LoadContent()
        {
            gestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            texture = gestionnaireDeTextures.Find(NomTextureCube);
            CharpenteTexture = new Vector2(texture.Width, texture.Height);

            EffetDeBase = new BasicEffect(GraphicsDevice);
            EffetDeBase.TextureEnabled = true;
            EffetDeBase.Texture = texture;
            base.LoadContent();

        }
        private void CréerTableauPointsTexture()
        {
            PtsTexture = new Vector2[2, 2];
            PtsTexture[0, 0] = new Vector2(0, 1);
            PtsTexture[0, 1] = new Vector2(0, 0);
            PtsTexture[1, 0] = new Vector2(1, 1);
            PtsTexture[1, 1] = new Vector2(1, 0);
        }
        protected override void InitialiserSommets()
        {
            Sommets[0] = new VertexPositionTexture(TableauDePoint[0], PtsTexture[0, 0]);
            Sommets[1] = new VertexPositionTexture(TableauDePoint[1], PtsTexture[0, 1]);
            Sommets[2] = new VertexPositionTexture(TableauDePoint[2], PtsTexture[1, 0]);
            Sommets[3] = new VertexPositionTexture(TableauDePoint[3], PtsTexture[1, 1]);
            Sommets[4] = new VertexPositionTexture(TableauDePoint[4], PtsTexture[0, 0]);
            Sommets[5] = new VertexPositionTexture(TableauDePoint[5], PtsTexture[0, 1]);
            Sommets[6] = new VertexPositionTexture(TableauDePoint[6], PtsTexture[1, 0]);
            Sommets[7] = new VertexPositionTexture(TableauDePoint[7], PtsTexture[1, 1]);

            Sommets[8] = new VertexPositionTexture(TableauDePoint[2], PtsTexture[1, 1]);
            Sommets[9] = new VertexPositionTexture(TableauDePoint[4], PtsTexture[0, 1]);
            Sommets[10] = new VertexPositionTexture(TableauDePoint[0], PtsTexture[1, 0]);
            Sommets[11] = new VertexPositionTexture(TableauDePoint[6], PtsTexture[0, 0]);
            Sommets[12] = new VertexPositionTexture(TableauDePoint[1], PtsTexture[1, 1]);
            Sommets[13] = new VertexPositionTexture(TableauDePoint[7], PtsTexture[0, 1]);
            Sommets[14] = new VertexPositionTexture(TableauDePoint[3], PtsTexture[1, 0]);
            Sommets[15] = new VertexPositionTexture(TableauDePoint[5], PtsTexture[0, 0]);

        }

        public override void Draw(GameTime gameTime)
        {
            JeuRasterizerState = new RasterizerState();
            JeuRasterizerState.CullMode = CullMode.CullClockwiseFace;
            GraphicsDevice.RasterizerState = JeuRasterizerState;
            EffetDeBase.World = GetMonde();
            EffetDeBase.View = CaméraJeu.Vue;
            EffetDeBase.Projection = CaméraJeu.Projection;
            foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
            {
                passeEffet.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, Sommets, 0, NB_TRIANGLES);
                GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, Sommets, 8, NB_TRIANGLES);
            }
        }

        float TrouverPlusGrosDelta()
        {
            float temp = DeltaX;
            if (temp < DeltaY)
                temp = DeltaY;
            if (temp < DeltaZ)
                temp = DeltaZ;

            return temp;
        }

        public bool EstEnCollision(object autreObject)
        {
            throw new NotImplementedException();
        }
    }
}
