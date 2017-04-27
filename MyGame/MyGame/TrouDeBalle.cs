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

namespace MyGame
{
    public class TrouDeBalle : PlanTexturé
    {

        float TempsDisparaitre;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tempsDisparaitre">En secondes</param>
        public TrouDeBalle(Game game, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale,
                           Vector2 étendue, Vector2 charpente, string nomTexture, float intervalleMAJ, float tempsDisparaitre)
            : base(game, homothétieInitiale, rotationInitiale, positionInitiale, étendue, charpente, nomTexture, intervalleMAJ)
        {
            TempsDisparaitre = tempsDisparaitre;
        }

        DepthStencilState tamponDepth;
        DepthStencilState jeuDepthStencilState;
        RasterizerState tamponRasterizer;
        RasterizerState JeuRasterizerState;
        public override void Initialize()
        {
            JeuRasterizerState = new RasterizerState();
            jeuDepthStencilState = new DepthStencilState();
            jeuDepthStencilState.DepthBufferEnable = false;
            JeuRasterizerState.CullMode = CullMode.CullClockwiseFace;
            base.Initialize();
        }

        float TempsEcoulé = 0;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if((TempsEcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > TempsDisparaitre)
            {
                GameController.ListeDrawableComponents.Remove(this);
                Game.Components.Remove(this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            tamponDepth = GraphicsDevice.DepthStencilState;
            tamponRasterizer = GraphicsDevice.RasterizerState;
            GraphicsDevice.DepthStencilState = jeuDepthStencilState;
            GraphicsDevice.RasterizerState = JeuRasterizerState;
            base.Draw(gameTime);
            GraphicsDevice.DepthStencilState = tamponDepth;
            GraphicsDevice.RasterizerState = tamponRasterizer;
        }
    }
}
