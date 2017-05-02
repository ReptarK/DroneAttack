using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierXNA
{
    public class Sphere : PrimitiveDeBaseAnimée
    {
        RasterizerState JeuRasterizerState;
        Caméra1stPerson CaméraJeu;
        BasicEffect EffetDeBase { get; set; }
        VertexPositionColor[] vertices; //later, I will provide another example with VertexPositionNormalTexture
        VertexBuffer vbuffer;
        short[] indices; //my laptop can only afford Reach, no HiDef :(
        IndexBuffer ibuffer;
        float radius;
        int nvertices, nindices;
        BasicEffect effect;
        GraphicsDevice graphicd;
        public Color Couleur;
        public Sphere(Game game, float Radius, GraphicsDevice graphics, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ, Color couleur)
            :base(game, homothétieInitiale, rotationInitiale, positionInitiale, intervalleMAJ)
        {
            radius = Radius;
            graphicd = graphics;
            effect = new BasicEffect(graphicd);
            nvertices = 90 * 90; // 90 vertices in a circle, 90 circles in a sphere
            nindices = 90 * 90 * 6;
            vbuffer = new VertexBuffer(graphics, typeof(VertexPositionNormalTexture), nvertices, BufferUsage.WriteOnly);
            ibuffer = new IndexBuffer(graphics, IndexElementSize.SixteenBits, nindices, BufferUsage.WriteOnly);
            createindices();
            vertices = new VertexPositionColor[nvertices];
            vbuffer.SetData<VertexPositionColor>(vertices);
            ibuffer.SetData<short>(indices);
            effect.VertexColorEnabled = true;
            Couleur = couleur;
            base.Initialize();
        }

        public override void Initialize()
        {
            base.Initialize();
            JeuRasterizerState = new RasterizerState();
            JeuRasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
            JeuRasterizerState.FillMode = FillMode.WireFrame;
        }
        protected override void LoadContent()
        {
            CaméraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
        }
        protected override void InitialiserSommets()
        {
            Vector3 center = new Vector3(0, 0, 0);
            Vector3 rad = new Vector3((float)Math.Abs(radius), 0, 0);
            for (int x = 0; x < 90; x++) //90 circles, difference between each is 4 degrees
            {
                float difx = 360.0f / 90.0f;
                for (int y = 0; y < 90; y++) //90 veritces, difference between each is 4 degrees 
                {
                    float dify = 360.0f / 90.0f;
                    Matrix zrot = Matrix.CreateRotationZ(MathHelper.ToRadians(y * dify));
                    Matrix yrot = Matrix.CreateRotationY(MathHelper.ToRadians(x * difx));
                    Vector3 point = Vector3.Transform(Vector3.Transform(rad, zrot), yrot);

                    vertices[x + y * 90] = new VertexPositionColor(point, Couleur);
                }
            }
        }
        void createindices()
        {
            indices = new short[nindices];
            int i = 0;
            for (int x = 0; x < 90; x++)
            {
                for (int y = 0; y < 90; y++)
                {
                    int s1 = x == 89 ? 0 : x + 1;
                    int s2 = y == 89 ? 0 : y + 1;
                    short upperLeft = (short)(x * 90 + y);
                    short upperRight = (short)(s1 * 90 + y);
                    short lowerLeft = (short)(x * 90 + s2);
                    short lowerRight = (short)(s1 * 90 + s2);
                    indices[i++] = upperLeft;
                    indices[i++] = upperRight;
                    indices[i++] = lowerLeft;
                    indices[i++] = lowerLeft;
                    indices[i++] = upperRight;
                    indices[i++] = lowerRight;
                }
            }
        }

        public void ChangeCouleur(Color couleur)
        {
            Couleur = couleur;

            InitialiserSommets();
        }

        public override void Draw(GameTime gameTime)
        {
            RasterizerState jeuRasterizerStateTampon = GraphicsDevice.RasterizerState;
            GraphicsDevice.RasterizerState = JeuRasterizerState;
            EffetDeBase = new BasicEffect(GraphicsDevice);
            EffetDeBase.VertexColorEnabled = true;

            EffetDeBase.World = GetMonde();
            EffetDeBase.View = CaméraJeu.Vue;
            EffetDeBase.Projection = CaméraJeu.Projection;
            foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
            {
                passeEffet.Apply();
                graphicd.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices, 0, nvertices, indices, 0, indices.Length / 3);
            }
            GraphicsDevice.RasterizerState = jeuRasterizerStateTampon;
        }
    }
}