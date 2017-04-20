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
    public class MainMenu : Microsoft.Xna.Framework.DrawableGameComponent, IMenu
    {
        RessourcesManager<SpriteFont> GestionnaireDeFonts;
        SpriteBatch GestionnaireDesSprites;
        string nomDeCarte = "";
        Texte TexteCarte;
        SpriteFont TexteFont;

        Screen Ecran;

        public List<DrawableGameComponent> ListeBoxEventMenu;

        public static BoxEventMenu PlayGameBox;
        public static BoxEventMenu ChoixMapBox;
        public static BoxEventMenu OptionBox;
        public static BoxEventMenu QuitBox;
        public static BoxEventMenu CreditsBox;

        public MainMenu(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
            Ecran = Game.Services.GetService(typeof(Screen)) as Screen;

            InitialiserTexte();
            InitialiserBoites();
        }

        void InitialiserTexte()
        {
            GestionnaireDesSprites = new SpriteBatch(GraphicsDevice);
            GestionnaireDeFonts = Game.Services.GetService(typeof(RessourcesManager<SpriteFont>)) as RessourcesManager<SpriteFont>;
            TexteFont = GestionnaireDeFonts.Find("Arial_Box");
        }

        void InitialiserBoites()
        {
            ListeBoxEventMenu = new List<DrawableGameComponent>();

            PlayGameBox = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 14, Ecran.CenterScreen.Y / 8), "DÉMARRER LA PARTIE", Color.White, Actions.StartGame);
            ListeBoxEventMenu.Add(PlayGameBox);

            ChoixMapBox = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 14, Ecran.CenterScreen.Y / 8 + 40), "CHOIX DE LA CARTE", Color.White, Actions.OpenMapMenu);
            ListeBoxEventMenu.Add(ChoixMapBox);

            TexteCarte = new Texte(Game, GestionnaireDesSprites, TexteFont, " : " + Data.NomCarteChoisi,
                                           new Vector2(Ecran.CenterScreen.X / 14 + TexteFont.MeasureString(ChoixMapBox.Texte).X, Ecran.CenterScreen.Y / 8 + 40), Color.LightGoldenrodYellow, Actions.Null);
            ListeBoxEventMenu.Add(TexteCarte);

            OptionBox = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 14, Ecran.CenterScreen.Y / 8 + 40 + 40), "OPTIONS", Color.White, Actions.OpenOptionMenu);
            ListeBoxEventMenu.Add(OptionBox);

            CreditsBox = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 14, Ecran.CenterScreen.Y / 8 + 40 + 40 + 40), "CRÉDITS", Color.White, Actions.OpenCreditsMenu);
            ListeBoxEventMenu.Add(CreditsBox);

            QuitBox = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 14, Ecran.CenterScreen.Y + 200), "QUITTER", Color.LightGoldenrodYellow, Actions.QuitGame);
            ListeBoxEventMenu.Add(QuitBox);

            Game.Components.Add(PlayGameBox); 
            Game.Components.Add(ChoixMapBox);
            Game.Components.Add(TexteCarte);
            Game.Components.Add(OptionBox);
            Game.Components.Add(CreditsBox);
            Game.Components.Add(QuitBox); 
        }

        //UPDATER

        double TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            if ((TempsÉcoulé += gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                foreach (IBoxMenu box in ListeBoxEventMenu)
                {
                    if (box.bEstClické)
                    {
                        box.OnClick.Invoke(Game);
                        box.bEstClické = false;
                    }
                }

                if(nomDeCarte != Data.NomCarteChoisi)
                {
                    TexteCarte.Message = " : " + Data.NomCarteChoisi;
                }

                base.Update(gameTime);
            }
        }

        public void ToggleMenu(MenuController.MenuState menuState)
        {
            bool state = false;

            if (menuState == MenuController.MenuState.Main)
                state = true;

            foreach(DrawableGameComponent c in ListeBoxEventMenu)
            {
                c.Enabled = state;
                c.Visible = state;
            }
        }
    }
}
