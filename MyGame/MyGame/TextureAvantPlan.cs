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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TextureAvantPlan : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteBatch GestionSprites { get; private set; }
        protected RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }

        public Rectangle ZoneAffichage;
        public string NomTexture;
        public Texture2D TextureAfficher;
        public Color Couleur;
        private Game game;

        public TextureAvantPlan(Game game, Rectangle zoneAffichage, string nomTexture, Color couleur)
            : base(game)
        {
            ZoneAffichage = zoneAffichage;
            NomTexture = nomTexture;
            Couleur = couleur;
        }

        public override void Initialize()
        {
            base.Initialize();
            DrawOrder = 1000;
        }

        public void ChangeTexture(string nomTexture, Rectangle rectangleAffichage)
        {
            NomTexture = nomTexture;
            ZoneAffichage = rectangleAffichage;

            TextureAfficher = GestionnaireDeTextures.Find(NomTexture);
        }

        protected override void LoadContent()
        {
            GestionSprites = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            TextureAfficher = GestionnaireDeTextures.Find(NomTexture);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GestionSprites.Begin();
            GestionSprites.Draw(TextureAfficher, ZoneAffichage, Couleur);
            GestionSprites.End();
        }
    }
}
