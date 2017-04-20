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
using MyGame.Composants_de_base;
using AtelierXNA;

namespace MyGame.Composants_Menu
{
    public class TexteCarte : TexteAvantPlan
    {
        Screen Ecran;

        Data.CarteName OldCarteName;

        public TexteCarte(Game game, string texte, string nomFont, Vector2 position, Color couleur)
            : base(game, texte, nomFont, position, couleur)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Ecran = new Screen(Game);

            OldCarteName = Data.NomCarte;
            Texte = "Carte Chargée : " + Data.NomCarte.ToString();

            Vector2 TexteSize = Font.MeasureString(Texte);

            Position = new Vector2(Ecran.Size.Width - TexteSize.X, Ecran.Size.Height - TexteSize.Y);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            if (MainGame.MainGameState == MainGame.GameState.Menu && MenuController.State != MenuController.MenuState.ScoreBoard)
                base.Draw(gameTime);
        }
    }
}
