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
using MyGame.Entités;

namespace MyGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class PlanCaméra : PlanColoré
    {
        int OldHeatlh;
        LocalPlayer MyPlayer;
        Caméra1stPerson CameraJeu;

        Color Couleur;

        public PlanCaméra(Game game, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale,
            Vector2 étendue, Vector2 charpente, Color couleur, float intervalleMAJ)
            : base(game, homothétieInitiale, rotationInitiale, positionInitiale, étendue, charpente, couleur, intervalleMAJ)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;
            CameraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            DrawOrder = 1000;
            OldHeatlh = 100;
        }

        float TempsÉcoulé = 0;
        float TempsÉcouléCouleur = 0;
        bool IsChangeColor = false;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                MondeÀRecalculer = true;
                TempsÉcoulé = 0;
                Position = CameraJeu.Position + CameraJeu.Direction * 50;
                
                if (OldHeatlh != MyPlayer.Health)
                {
                    TempsÉcoulé = 0;
                    OldHeatlh = MyPlayer.Health;
                    IsChangeColor = true;
                    Couleur = new Color(255, 0, 0, 100);
                }
            }

            if ((TempsÉcouléCouleur += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.1f)
            {
                TempsÉcouléCouleur = 0;
                if (IsChangeColor)
                {
                    if(Couleur.A-- == 0)
                    {
                        IsChangeColor = false;
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
            base.Draw(gameTime);
        }
    }
}
