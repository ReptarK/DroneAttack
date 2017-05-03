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
using System.IO;

//Mes Composants
using AtelierXNA.Composant_Menu;
using MyGame.Composants_Menu;
using MyGame;

namespace AtelierXNA
{
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        public static StreamReader LecteurFichier;
        public static StreamWriter EcritureFichier;


        RessourcesManager<Model> GestionnaireDeMod�les { get; set; }

        public enum GameState
        {
            Menu,
            Jeu
        }

        public static GameState MainGameState;

        public const float INTERVALLE_CALCUL_FPS = 1f;
        public const float INTERVALLE_MAJ_STANDARD = 1f / 600f;

        public static GraphicsDeviceManager P�riph�riqueGraphique { get; set; }

        public static Cam�ra Cam�raJeu { get; set; }
        InputManager GestionInput { get; set; }

        Screen Ecran;

        List<IController> ListeController;
        MenuController Menu;
        public static GameController Jeu;

        public static bool EstSourisActive;

        public static bool DoGameInitialize;

        public MainGame()
        {
            InitialiserEcran();
            GetMap();
            P�riph�riqueGraphique = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            P�riph�riqueGraphique.SynchronizeWithVerticalRetrace = true;
            IsFixedTimeStep = true;
            IsMouseVisible = true;
            //P�riph�riqueGraphique.PreferredBackBufferWidth = (int)Ecran.Resolution.X;
            //P�riph�riqueGraphique.PreferredBackBufferHeight = (int)Ecran.Resolution.Y;

            P�riph�riqueGraphique.IsFullScreen = false;
            
            MainGameState = GameState.Menu;

            EstSourisActive = true;

        }

        protected override void Initialize()
        {

            GestionInput = new InputManager(this);
            Components.Add(GestionInput);
            Cam�raJeu = new Cam�ra1stPerson(this, new Vector3(100, 80, 26), Vector3.Zero, Vector3.Up, INTERVALLE_MAJ_STANDARD);
            Services.AddService(typeof(Cam�ra1stPerson), Cam�raJeu);
            GestionnaireDeMod�les = new RessourcesManager<Model>(this, "Models");
            Services.AddService(typeof(RessourcesManager<Model>), GestionnaireDeMod�les);

            Components.Add(new Afficheur3D(this));
            Components.Add(new AfficheurFPS(this, "Arial_FPS", Color.LightYellow, INTERVALLE_CALCUL_FPS / 3));

            InitialiserServices();

            ListeController = new List<IController>();
            InitialiserMenu();
            InitialiserGame();

            base.Initialize();
        }

        void InitialiserServices()
        {
            //InitialiserEcran();
            Services.AddService(typeof(Random), new Random());
            Services.AddService(typeof(RessourcesManager<SpriteFont>), new RessourcesManager<SpriteFont>(this, "Fonts"));
            Services.AddService(typeof(RessourcesManager<SoundEffect>), new RessourcesManager<SoundEffect>(this, "Sounds"));
            Services.AddService(typeof(RessourcesManager<Song>), new RessourcesManager<Song>(this, "Songs"));
            Services.AddService(typeof(RessourcesManager<Texture2D>), new RessourcesManager<Texture2D>(this, "Textures"));
            Services.AddService(typeof(InputManager), GestionInput);
            Services.AddService(typeof(SpriteBatch), new SpriteBatch(GraphicsDevice));
        }

        public static void GetMap()
        {
            LecteurFichier = new StreamReader("MapName.txt");

            string line = LecteurFichier.ReadLine();

            switch(line)
            {
                case ("BridgeMap"): Data.NomCarte = Data.CarteName.BridgeMap; break;
                case ("OuaiMap"): Data.NomCarte = Data.CarteName.OuaiMap; break;
            }
            LecteurFichier.Dispose();
            LecteurFichier.Close();
        }

        void InitialiserMenu()
        {
            Menu = new MenuController(this);
            Components.Add(Menu);

            ListeController.Add(Menu);
        }

        void InitialiserGame()
        {
            Jeu = new GameController(this);
            Components.Add(Jeu);

            ListeController.Add(Jeu);
        }

        void InitialiserEcran()
        {
            //IntPtr hWnd = this.Window.Handle;
            //var control = System.Windows.Forms.Control.FromHandle(hWnd);
            //var form = control.FindForm();
            //form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //form.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            Ecran = new Screen(this);
            Services.AddService(typeof(Screen), Ecran);

            //P�riph�riqueGraphique.PreferredBackBufferWidth = (int)Ecran.Size.X;
            //P�riph�riqueGraphique.PreferredBackBufferHeight = (int)Ecran.Size.Y;
            //P�riph�riqueGraphique.ToggleFullScreen();

            //P�riph�riqueGraphique.ApplyChanges();
        }

        protected override void LoadContent()
        {

        }

        protected override void UnloadContent()
        {
        }

        float TempsEcoul�;
        protected override void Update(GameTime gameTime)
        {
            if((TempsEcoul� += (float)gameTime.ElapsedGameTime.TotalSeconds) >= Data.INTERVALLE_MAJ_BASE)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();
                if (GestionInput.EstNouvelleTouche(Keys.Escape))
                {
                    MainGameState = GameState.Menu;
                    MenuController.State = MenuController.MenuState.Main;
                }

                GererTransition();

                if (MainGameState == GameState.Jeu)
                    IsMouseVisible = false;
                else
                    IsMouseVisible = true;

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        void GererTransition()
        {
            foreach (IController controller in ListeController)
            {
                controller.IsVisibleController(MainGameState);
            }
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            base.OnDeactivated(sender, args);

            EstSourisActive = false;
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);

            EstSourisActive = true;
        }
    }
}
