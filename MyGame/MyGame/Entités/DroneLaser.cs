using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AtelierXNA;

namespace MyGame.Entités
{
    public class DroneLaser : PrimitiveDeBase
    {
        Caméra1stPerson CameraJeu;
        BasicEffect EffetDeBase { get; set; }

        VertexPositionColor[] Sommets { get; set; }
        Color Couleur;

        Vector3 PositionFinale;

        public DroneLaser(Game game, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, Vector3 positionFinale, Color couleur)
            : base(game, homothétieInitiale, rotationInitiale, positionInitiale)
        {
            PositionFinale = positionFinale;
            //PositionInitiale = PositionInitiale - new Vector3(0, 0, 200);
            //PositionFinale = positionFinale - new Vector3(0, 0, 200);
            Couleur = couleur;
        }

        public override void Initialize()
        {
            DrawOrder = 1000;
            Sommets = new VertexPositionColor[3];
            CameraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            EffetDeBase = new BasicEffect(GraphicsDevice);
            EffetDeBase.VertexColorEnabled = true;
            base.LoadContent();
        }

        protected override void InitialiserSommets()
        {
            Sommets[0] = new VertexPositionColor(PositionInitiale, Couleur);
            Sommets[1] = new VertexPositionColor(PositionFinale, Couleur);
            Sommets[2] = new VertexPositionColor(PositionFinale + Vector3.UnitY * 5, Couleur);
        }

        float TempsEcoulé;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if((TempsEcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > 100f)
            {
                Game.Components.Remove(this);
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
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, Sommets, 0, 1);
            }
        }

    }
}
