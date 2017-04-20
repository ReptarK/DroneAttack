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
using MyGame.Composants_Menu;
using MyGame;

namespace AtelierXNA.Composant_Menu
{

    public class BoxTextureMenu : Microsoft.Xna.Framework.DrawableGameComponent, IBoxMenu
    {
        SpriteBatch GestionSprites;

        RessourcesManager<SpriteFont> GestionnaireDeFonts;
        RessourcesManager<Texture2D> GestionnaireDeTextures;

        InputManager GestionnaireDeTouches;

        SpriteFont TexteFont;

        Point Position;
        Point Grandeur;
        Rectangle Size;

        Vector2 TexteSize;
        public string Texte;

        Color Couleur;

        Texture2D Texture;
        string NomTexture;

        bool bDrawBox;
        public bool bEstClické { get; set; }

        public Action<Game> OnClick { get; set; }

        public BoxTextureMenu(Game game, Point position, Point grandeur, string texte, Color color, string nomTexture, Action<Game> onClick)
            : base(game)
        {
            Position = position;
            Grandeur = grandeur;
            Texte = texte;
            Couleur = color;
            NomTexture = nomTexture;
            OnClick = onClick;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            GestionSprites = new SpriteBatch(Game.GraphicsDevice);

            GestionnaireDeFonts = Game.Services.GetService(typeof(RessourcesManager<SpriteFont>)) as RessourcesManager<SpriteFont>;
            GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            GestionnaireDeTouches = Game.Services.GetService(typeof(InputManager)) as InputManager;

            TexteFont = GestionnaireDeFonts.Find("Arial_Box");
            Texture = GestionnaireDeTextures.Find(NomTexture);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            bDrawBox = false;
            bEstClické = false;

            TexteSize = TexteFont.MeasureString(Texte);

            Size.Location = Position;
            Size.Height = Grandeur.Y;
            Size.Width = Grandeur.X;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        public override void Update(GameTime gameTime)
        {
            if (Size.Intersects(GestionnaireDeTouches.GetRectangleSouris()))
            {
                bDrawBox = true;
                if (GestionnaireDeTouches.EstNouveauClicGauche())
                    bEstClické = true;
            }
            else
                bDrawBox = false;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GestionSprites.Begin();

            GestionSprites.Draw(Texture, Size, Color.White);
            if (bDrawBox)
                GestionSprites.Draw(Texture, Size, Color.Gray);
            GestionSprites.DrawString(TexteFont, Texte, new Vector2(Position.X, Position.Y), Couleur);

            GestionSprites.End();
        }
    }
}
