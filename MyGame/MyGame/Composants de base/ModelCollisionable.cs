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

namespace MyGame.Composants_de_base
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ModelCollisionable : ObjetDeBase, ILadder
    {
        public BoundingBox BoiteDeCollision { get; set; }

        public ModelCollisionable(Game jeu, String nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, BoundingBox boiteDeCollision)
            : base(jeu, nomModèle, échelleInitiale, rotationInitiale, positionInitiale)
        {
            BoiteDeCollision = boiteDeCollision;
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
