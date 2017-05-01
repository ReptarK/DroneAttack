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

namespace MyGame.Composants_de_base
{
    public class TexteAvantPlan : DrawableGameComponent
    {
        protected SpriteBatch GestionSprites;
        protected RessourcesManager<SpriteFont> GestionnaireDeFonts;

        protected SpriteFont Font;
        public string Texte;
        protected string NomFont;
        protected Vector2 Position;
        protected Color Couleur;

        public TexteAvantPlan(Game game, string texte, string nomFont, Vector2 position, Color couleur)
            : base(game)
        {
            Texte = texte;
            NomFont = nomFont;
            Position = position;
            Couleur = couleur;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            GestionSprites = new SpriteBatch(Game.GraphicsDevice);
            GestionnaireDeFonts = Game.Services.GetService(typeof(RessourcesManager<SpriteFont>)) as RessourcesManager<SpriteFont>;
            Font = GestionnaireDeFonts.Find(NomFont);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GestionSprites.Begin();
            GestionSprites.DrawString(Font, Texte, Position, Couleur);
            GestionSprites.End();
        }
    }
}
