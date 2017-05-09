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
        //public const MainGame.GameState GAME_STATE = MainGame.GameState.Menu;
        public MainGame.GameState CONTROLLER_STATE;

        //Liste des composants du menu
        List<DrawableGameComponent> ListeComposants;

        //Les états des menus
        public enum MenuState
        {
            Jeu,
            Main,
            Map,
            Option,
            Credits,
            ScoreBoard
        }

        ArrièrePlan ArrierePlan { get; set; }

        RessourcesManager<Song> GestionnaireDeChansons { get; set; }
        Song TrameDeFond { get; set; }

        //Les menus que cette classe contrôle
        MainMenu mainMenu { get; set; }
        OptionMenu optionMenu { get; set; }
        CreditMenu creditMenu { get; set; }
        MapMenu mapMenu { get; set; }
        MortPlayerMenu mortPlayerMenu { get; set; }

        //Permet de savoir si le menu state à changé
        public static MenuState State { get; set; }
        static MenuState OldState { get; set; }

        //Liste de tous les menus gérables par cette classe
        public List<IMenu> ListeMenu { get; set; }

        public static bool bChoseMap { get; set; }

        public MenuController(Game game)
            : base(game)
        {
            bChoseMap = true;

            State = MenuState.Main;
            OldState = MenuState.Option;

            mainMenu = new MainMenu(game);
            optionMenu = new OptionMenu(game);
            creditMenu = new CreditMenu(game);
            mapMenu = new MapMenu(game);
            mortPlayerMenu = new MortPlayerMenu(game);
        }

        public override void Initialize()
        {
            ListeComposants = new List<DrawableGameComponent>();
            base.Initialize();

            InitialiserArrierePlanMenu();
            InitialiserMusique();
            InitialiserMenu();
        }

        void InitialiserArrierePlanMenu()
        {
            ArrierePlan = new ArrièrePlan(Game, "BackgroundMenu");
            Game.Components.Add(ArrierePlan);
            ListeComposants.Add(ArrierePlan);
        }

        void InitialiserMusique()
        {
            GestionnaireDeChansons = Game.Services.GetService(typeof(RessourcesManager<Song>)) as RessourcesManager<Song>;
            TrameDeFond = GestionnaireDeChansons.Find("MenuBackgroundSound");
            MediaPlayer.Play(TrameDeFond);
        }

        void InitialiserMenu()
        {
            ListeMenu = new List<IMenu>();
            ListeMenu.Add(optionMenu);
            ListeMenu.Add(mainMenu);
            ListeMenu.Add(creditMenu);
            ListeMenu.Add(mapMenu);
            ListeMenu.Add(mortPlayerMenu);

            Game.Components.Add(mainMenu);
            Game.Components.Add(optionMenu);
            Game.Components.Add(creditMenu);
            Game.Components.Add(mapMenu);
            Game.Components.Add(mortPlayerMenu);
        }

        float TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                base.Update(gameTime);

                if (OldState != State)
                {
                    GererTransition();
                    OldState = State;
                    if (State == MenuState.Jeu)
                    {
                        MediaPlayer.Pause();
                        ArrierePlan.ToggleFondEcran(false);
                    }
                }
                TempsÉcoulé = 0;
            }
        }

        //Défini quel menu doit afficher (interface IMenu --> ToggleMenu)
        void GererTransition()
        {
            foreach (IMenu menu in ListeMenu)
            {
                menu.ToggleMenu(State);
            }
        }

        //Défini si le jeu doit afficher le menu
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

            if (!state)
                MediaPlayer.Pause();
        }
    }
}
