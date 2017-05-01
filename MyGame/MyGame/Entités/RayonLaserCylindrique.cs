using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AtelierXNA
{
   class RayonLaserCylindrique : PrimitiveDeBase // Tu n'avais pas besoin de PrimitiveDeBaseAnimé, mais de faire un nouveau descendant de PrimitiveDeBase
    {
        Caméra1stPerson CameraJeu;
        Vector3[,] PtsSommets { get; set; }
        Vector3 Origine { get; set; }
        string NomTexture { get; set; }
        int NbRangées { get; set; }
        int NbColonnes { get; set; }
        int NbTrianglesParStrip { get; set; }
        float Variation { get; set; }
        BasicEffect EffetDeBase { get; set; }
        BlendState GestionAlpha { get; set; }
        Texture2D TexturePlan { get; set; }
        RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
        VertexPositionTexture[] Sommets { get; set; }
        Vector2 Delta { get; set; }        
        Vector2[,] PtsTexture { get; set; }
        Vector2 Étendue { get; set; }
        Vector3 Direction { get; set; }
        Vector3 Position { get; set; }
        Vector3 Déplacement { get; set; }
        float DuréeDeVie { get; set; }
        float IntervalleMAJ { get; set; }
        float TempsÉcouléDepuisMAJ { get; set; }

        

        public RayonLaserCylindrique(Game jeu, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector3 direction, 
                                     Vector2 étendue, Vector2 charpente, string nomTexture, float duréeDeVie, float intervalleMAJ)
            : base(jeu, échelleInitiale, rotationInitiale, positionInitiale)
        {
            IntervalleMAJ = intervalleMAJ;
            NomTexture = nomTexture;
            NbRangées = (int)charpente.X;
            NbColonnes = (int)charpente.Y;
            Étendue = étendue;
            Origine = new Vector3(0, -Étendue.Y, 0);
            Delta = new Vector2(étendue.X/charpente.X, étendue.Y/charpente.Y);
            Variation = MathHelper.Pi * 2 / NbColonnes;
            Étendue = étendue;

            // J'ai introduit les notions de Direction, Déplacement et DuréeDeVie...
            Direction = direction;
            // Je calcule la position en fonction de la Direction et de la position initiale
            Position = PositionInitiale + 15 * Direction - Vector3.Normalize(Vector3.Cross(Vector3.Up, Direction));
            Déplacement = Direction/10;
            DuréeDeVie = duréeDeVie;
        }

        public override void Initialize()
        {
            CameraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            TempsÉcouléDepuisMAJ = 0;
            NbTrianglesParStrip = 2 * NbColonnes;
            NbSommets = (NbTrianglesParStrip + 2) * NbRangées + 2*NbColonnes;
            PtsSommets = new Vector3[(NbColonnes + 1), (NbRangées + 3) ];
            CréerTableauSommet();
            CréerTableauPoints();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            TexturePlan = GestionnaireDeTextures.Find(NomTexture);
            EffetDeBase = new BasicEffect(GraphicsDevice);
            InitialiserParamètresEffetDeBase();
 	        base.LoadContent();
        }
        void InitialiserParamètresEffetDeBase()
        {
            EffetDeBase.TextureEnabled = true;
            EffetDeBase.Texture = TexturePlan;
            GestionAlpha = BlendState.AlphaBlend;
        }

        void CréerTableauSommet()
        {
            PtsTexture = new Vector2[NbColonnes + 1, NbRangées + 1];
            Sommets = new VertexPositionTexture[NbSommets +2*NbColonnes];
            CréerTableauPointsTexture();
        }
        
       //J'ai remplacé ton code de création des points de texture par le mien, car le tien ne fonctionnait pas correctement (il créait des points négatifs dans certains cas...)
        void CréerTableauPointsTexture()
        {
           float posX = 0;
           float deltaTextureHorizontale = 1.0f / NbColonnes;
           float deltaTextureVerticale = 1.0f / NbRangées;
           for (int i = 0; i <= NbColonnes; ++i)
           {
              float posY = 1;
              for (int j = 0; j <= NbRangées; ++j)
              {
                 PtsTexture[i, j] = new Vector2(posX, posY);
                 posY -= deltaTextureVerticale;
              }
              posX += deltaTextureHorizontale;
           }
        }

        void CréerTableauPoints()
        {

           for (int i = 0; i <= NbColonnes; ++i)
           {
              //PtsSommets[i, 0] = new Vector3(Origine.X, Origine.Y + Delta.Y, Origine.Z);

              for (int j = 0; j <= NbRangées; ++j)
              {
                 PtsSommets[i, j] = new Vector3(Origine.X + Étendue.X * (float)Math.Sin(i * Variation), Origine.Y + j * Delta.Y, Origine.Z + Étendue.X * (float)Math.Cos(i * Variation));
              }

              //PtsSommets[i, NbRangées] = new Vector3(Origine.X, Origine.Y + Étendue.Y - Delta.Y, Origine.Z);
           }

           // Le bout de code ci-dessous sert à faire pivoter ton cyclindre pour le coucher. Tu pourrais simplement modifier le bout de code ci-dessus pour qu'il crée le cyclindre dans la bonne position.
           Matrix rotationEnX = Matrix.CreateRotationX(MathHelper.PiOver2);
           for (int i = 0; i <= NbColonnes; ++i)
           {
              for (int j = 0; j < NbRangées; ++j)
              {
                 PtsSommets[i, j] = Vector3.Transform(PtsSommets[i, j], rotationEnX);
              }
           }

        }

        protected override void InitialiserSommets()
        {
            int NoSommet = -1;

            for (int j = 0; j < NbRangées; ++j)
            {
                for (int i = 0; i <= NbColonnes; ++i)
                {
                    Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, j], PtsTexture[i, j]);
                    Sommets[++NoSommet] = new VertexPositionTexture(PtsSommets[i, j + 1], PtsTexture[i, j + 1]);
                }
            }
        }

        protected override void CalculerMatriceMonde()
        {
           Monde = Matrix.Identity;
           Monde *= Matrix.CreateScale(HomothétieInitiale);
            Monde *= Matrix.CreateWorld(Position, Direction, Vector3.Up);
            //Monde *= Matrix.CreateWorld(Position, -CameraJeu.Direction, Vector3.Up);
        }

        public override void Update(GameTime gameTime)
        {
           float TempsÉcoulé = (float)gameTime.ElapsedGameTime.TotalSeconds;
           TempsÉcouléDepuisMAJ += TempsÉcoulé;
           if (TempsÉcouléDepuisMAJ >= IntervalleMAJ)
           {
              EffectuerMiseÀJour();
              // Le bout de code ci-dessous permet de retirer le composant de la liste des composants actifau bout d'un certain temps (DuréeDeVie)
              DuréeDeVie -= TempsÉcouléDepuisMAJ;
              if (DuréeDeVie < 0)
              {
                 Game.Components.Remove(this);
              }
              //
              TempsÉcouléDepuisMAJ =0;
           }
           base.Update(gameTime);
        }

        protected virtual void EffectuerMiseÀJour()
        {
           Position += Déplacement;
           CalculerMatriceMonde();
        }

        public override void Draw(GameTime gameTime)
        {
            CalculerMatriceMonde();
            BlendState oldBlendState = GraphicsDevice.BlendState;
            RasterizerState oldRendu = GraphicsDevice.RasterizerState;
            RasterizerState NewRendu = new RasterizerState();
            NewRendu.CullMode = CullMode.None;
            NewRendu.FillMode = oldRendu.FillMode;
            GraphicsDevice.RasterizerState = NewRendu;
            GraphicsDevice.BlendState = GestionAlpha;
            EffetDeBase.World = GetMonde();
            EffetDeBase.View = CameraJeu.Vue;
            EffetDeBase.Projection = CameraJeu.Projection;

            foreach (EffectPass passeEffet in EffetDeBase.CurrentTechnique.Passes)
            {
                passeEffet.Apply();
                for(int j = 0; j < NbRangées; ++j)
                {
                    DessinerTriangleStrip(j);
                }
            }
            GraphicsDevice.BlendState = oldBlendState;
            GraphicsDevice.RasterizerState = oldRendu;

        }

        void DessinerTriangleStrip(int noStrip)
        {
            GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, Sommets, noStrip * (NbTrianglesParStrip + 2), NbTrianglesParStrip + 2 * NbColonnes);
        }
    }
}
