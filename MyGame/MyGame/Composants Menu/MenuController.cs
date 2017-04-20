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
using AtelierXNA.Composant_Menu;
using AtelierXNA;

namespace MyGame.Composants_Menu
{
    public class MenuController : Microsoft.Xna.Framework.DrawableGameComponent, IController
    {
        public const MainGame.GameState GAME_STATE = MainGame.GameState.Menu;
        public MainGame.GameState CONTROLLER_STATE;

        List<DrawableGameComponent> ListeComposants;

        public enum MenuState
        {
            Jeu,
            Main,
            Map,
            Option,
            Credits,
            ScoreBoard
        }
        ArrièrePlan ArrierePlan;

        RessourcesManager<Song> GestionnaireDeSongs;
        Song ChansonBackground;

        public MainMenu mainMenu;
        public OptionMenu optionMenu;
        public CreditMenu creditMenu;
        public MapMenu mapMenu;
        public MortPlayerMenu mortPlayerMenu;

        public static MenuState State;
        public static MenuState OldState;

        public List<IMenu> ListeMenu;

        public static bool bChoseMap = true;

        public MenuController(Game game)
            : base(game)
        {
            State = MenuState.Main;
            OldState = MenuState.Option;

            mainMenu = new MainMenu(game);
            optionMenu = new OptionMenu(game);
            creditMenu = new CreditMenu(game);
            mapMenu = new MapMenu(game);
            mortPlayerMenu = new MortPlayerMenu(game);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            ListeComposants = new List<DrawableGameComponent>();
            ArrierePlan = new ArrièrePlan(Game, "BackgroundMenu");
            Game.Components.Add(ArrierePlan);
            ListeComposants.Add(ArrierePlan);

            base.Initialize();

            InitialiserMusique();

            InitialiserMenu();

            Game.Components.Add(mainMenu);
            Game.Components.Add(optionMenu);
            Game.Components.Add(creditMenu);
            Game.Components.Add(mapMenu);
            Game.Components.Add(mortPlayerMenu);
        }

        void InitialiserMusique()
        {
            GestionnaireDeSongs = Game.Services.GetService(typeof(RessourcesManager<Song>)) as RessourcesManager<Song>;
            ChansonBackground = GestionnaireDeSongs.Find("MenuBackgroundSound");
            MediaPlayer.Play(ChansonBackground);
        }

        

        void InitialiserMenu()
        {
            ListeMenu = new List<IMenu>();
            ListeMenu.Add(optionMenu);
            ListeMenu.Add(mainMenu);
            ListeMenu.Add(creditMenu);
            ListeMenu.Add(mapMenu);
            ListeMenu.Add(mortPlayerMenu);
        }

        double TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            if ((TempsÉcoulé += gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                base.Update(gameTime);

                if (OldState != State)
                {
                    GererTransition();
                    OldState = State;
                    if(State == MenuState.Jeu)
                    {
                        MediaPlayer.Pause();
                        ArrierePlan.ToggleFondEcran(false);
                    }
                }
                TempsÉcoulé = 0;
            }
        }

        void GererTransition()
        {
            foreach(IMenu menu in ListeMenu)
            {
                menu.ToggleMenu(State);
            }
        }

        public void IsVisibleController(MainGame.GameState gameState)
        {
            bool state = false;

            if (CONTROLLER_STATE == MainGame.MainGameState) //MODIFIER
                state = true;

            foreach (DrawableGameComponent c in ListeComposants)
            {
                c.Enabled = state;
                c.Visible = state;
            }

            GererTransition();

            if (!state)
                MediaPlayer.Pause();
        }
    }
}
