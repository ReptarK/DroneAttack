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
using MyGame.Composants_Menu;
using MyGame.Entités;
using MyGame.Weapons;

namespace MyGame
{
    public class GameController : Microsoft.Xna.Framework.GameComponent, IController
    {
        Random GenerateurRandom;

        Screen Ecran;

        RessourcesManager<Song> GestionnaireDeSongs;
        RessourcesManager<Model> GestionnaireDeModèles { get; set; }
        RessourcesManager<SoundEffect> GestionnaireDeSons { get; set; }
        InputManager GestionInput { get; set; }

        Caméra1stPerson CaméraJeu;

        FadeTexture HitMark;

        public static List<CubeTexturé> ListCrate;
        public static List<CubeTexturé> ListMurs;
        public static List<Drone> ListDrones;
        public static List<CadreGun> ListCadresGun;
        public static List<Gun> ListeGuns;

        LoadBridgeMap LoadBridgeMap;
        LoadOuaiMap LoadOuaiMap;

        public static PlancherMap Terrain;
        ArrièrePlan FondEcran;
        public static TextureAvantPlan CrossHair;

        SoundEffect HitMarkerSound;

        public static LocalPlayer MyPlayer;

        public static bool DoInitialize;

        public static MiniMap MiniCarte;

        public static InGameState PlayState;
        public enum InGameState
        {
            Play,
            Dead,
            NewWave
        }

        public const MainGame.GameState CONTROLLER_STATE = MainGame.GameState.Jeu;
        public MainGame.GameState ActualGameState;

        public static List<DrawableGameComponent> ListeDrawableComponents;
        public static List<GameComponent> ListeComponent;


        public GameController(Game game)
            : base(game)
        {
            LoadOuaiMap = new LoadOuaiMap(Game);
            LoadBridgeMap = new LoadBridgeMap(Game);
        }


        public override void Initialize()
        {
            base.Initialize();

            ListeDrawableComponents = new List<DrawableGameComponent>();
            ListeComponent = new List<GameComponent>();
            ListCrate = new List<CubeTexturé>();
            ListMurs = new List<CubeTexturé>();
            ListDrones = new List<Drone>();
            ListCadresGun = new List<CadreGun>();
            ListeGuns = new List<Gun>();
            PlayState = InGameState.NewWave;

            GenerateurRandom = Game.Services.GetService(typeof(Random)) as Random;
            Game.Services.AddService(typeof(GameController), this);
            MyPlayer = new LocalPlayer(Game, "me", new Vector3(200, 100, 10) - (Vector3.UnitY * Caméra1stPerson.HAUTEUR_PLAYER), 100);
            Game.Services.AddService(typeof(LocalPlayer), MyPlayer);

            Ecran = Game.Services.GetService(typeof(Screen)) as Screen;
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;
            GestionnaireDeModèles = Game.Services.GetService(typeof(RessourcesManager<Model>)) as RessourcesManager<Model>;
            GestionnaireDeSons = Game.Services.GetService(typeof(RessourcesManager<SoundEffect>)) as RessourcesManager<SoundEffect>;
            GestionnaireDeSongs = Game.Services.GetService(typeof(RessourcesManager<Song>)) as RessourcesManager<Song>;
            CaméraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            ListeComponent.Add(CaméraJeu);

            InitialiserFondEcran();

            Terrain = new PlancherMap(Game, 1f, Vector3.Zero, Vector3.Zero, Data.DimentionCarte, Data.INTERVALLE_MAJ_BASE);
            Terrain.Initialize();
            ListeDrawableComponents.Add(Terrain);

            InitialiserCarte();
            CubeCaméra CubeCaméra = new CubeCaméra(Game, 1, Vector3.Zero, CaméraJeu.Position, Color.Transparent, new Vector3(20, 20, 20), Data.INTERVALLE_MAJ_BASE);
            ListeDrawableComponents.Add(CubeCaméra);
            Game.Components.Add(CubeCaméra);

            Game.Components.Add(new GererPack(Game));

            CrossHair = new CrossHair(Game, new Rectangle(Ecran.CenterScreen.X - 20, Ecran.CenterScreen.Y - 20, 40, 40), "CrossHair", Data.TableauCouleur[Data.CouleurIndex]);
            ListeDrawableComponents.Add(CrossHair);
            Game.Components.Add(CrossHair);

            Game.Components.Add(new GunTextureManager(Game));

            HitMark = new FadeTexture(Game, new Rectangle(Ecran.CenterScreen.X - 50, Ecran.CenterScreen.Y - 50, 100, 100), "HitMark", Color.Red, 0.2f);

            MiniCarte = new MiniMap(Game, new Rectangle(20, 20, 100, 100), Data.NomCarte.ToString(), Color.White);
            Game.Components.Add(MiniCarte);
            ListeDrawableComponents.Add(MiniCarte);

            DroneAliveText DroneEnVieTexte = new DroneAliveText(Game, "", "Pescadero", new Vector2(15, Ecran.CenterScreen.Y * 1.74f), Color.Blue);
            ListeDrawableComponents.Add(DroneEnVieTexte);
            WaveText TexteVague = new WaveText(Game, "", "Pescadero", new Vector2(15, Ecran.CenterScreen.Y * 1.65f), new Color(255, 178, 102));
            ListeDrawableComponents.Add(TexteVague);

            ChangeWaveSong = GestionnaireDeSons.Find("ChangeWaveSong");
            HitMarkerSound = GestionnaireDeSons.Find("HitMarker");

            TexteBienvenue texteBienvenue = new TexteBienvenue(Game, "Ramasse une arme, les drones arrivent !", "Pescadero", new Rectangle(0, 0, Game.Window.ClientBounds.Width, (int)(Game.Window.ClientBounds.Height / 1.5f)), Color.Red, 0.2f);
            //ListeDrawableComponents.Add(texteBienvenue);
            //Game.Components.Add(texteBienvenue);
            Game.Components.Add(TexteVague);
            Game.Components.Add(DroneEnVieTexte);

            //Pour initialiser la BoiteDeCollision statique
            Drone d = new Drone(Game, "drone", 0.4f, Vector3.Zero, Vector3.Zero, Data.INTERVALLE_MAJ_BASE, new Color(GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255)), PointsDePatrouille.ListeSpawns[GenerateurRandom.Next(0, PointsDePatrouille.ListeSpawns.Count)], GenerateurRandom.Next(80, 150), (int)Math.Ceiling(WaveNo * WaveNo / 2f));
            d.Initialize();
            d = null;

            //ENLEVER
            //ObjetDeDémo drone = new ObjetDeDémo(Game, "drone", 0.4f, Vector3.Zero, new Vector3(42, 10, 137), Data.INTERVALLE_MAJ_BASE, Color.Yellow);
            //ListeDrawableComponents.Add(drone);
            //Game.Components.Add(new Afficheur3D(Game));
            //Game.Components.Add(drone);
        }

        public void InitialiserCarte()
        {
            switch (Data.NomCarte)
            {
                case (Data.CarteName.BridgeMap): LoadOuaiMap.UnloadCarte(); LoadBridgeMap.LoadCarte(); break;
                case (Data.CarteName.OuaiMap): LoadBridgeMap.UnloadCarte(); LoadOuaiMap.LoadCarte(); break;
            }
        }

        void InitialiserFondEcran()
        {
            ListeDrawableComponents.Add(new Skybox(Game, 1f, Vector3.Zero, new Vector3(-100, -10, -100), "Starscape", new Vector3(800, 400, 800)));
        }



        float TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            if (MainGame.MainGameState == MainGame.GameState.Jeu)
            {
                if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
                {
                    Game.Window.Title = CaméraJeu.Position.ToString();
                    GererCollision();
                    NettoyerListeComponents();
                    if (MyPlayer.MyGun != null)
                        GererDrones(gameTime);
                    TempsÉcoulé = 0;

                    if (MortPlayerMenu.ShowDeathRecap)
                    {
                        foreach (DrawableGameComponent o in Game.Components.Where(c => c is DrawableGameComponent && c is Overlay))
                        {
                            o.Enabled = false;
                            o.Visible = false;
                        }
                        MainGame.MainGameState = MainGame.GameState.Menu;
                        MenuController.State = MenuController.MenuState.ScoreBoard;
                    }
                }

            }

            base.Update(gameTime);
        }

        private void GererCollision()
        {
            Caméra1stPerson.EstSol = false;
            foreach (ICollisionableList c in Game.Components.Where(composant => composant is ICollisionableList))
            {
                if (MyPlayer.EstEnCollision(c))
                {
                    if (c is PlancherMap)
                        Caméra1stPerson.EstSol = true;

                    Caméra1stPerson.EstEnCollision = true;
                    if (!Caméra1stPerson.ListeObjetEnCollision.Contains(c))
                        Caméra1stPerson.ListeObjetEnCollision.Add(c);
                }
            }

            Caméra1stPerson.EstLadder = false;
            foreach (ILadder l in Game.Components.Where(c => c is ILadder))
            {
                if (MyPlayer.BoiteDeCollision.Intersects(l.BoiteDeCollision))
                {
                    Caméra1stPerson.EstLadder = true;
                }
            }

            foreach (IPack c in Game.Components.Where(composant => composant is IPack))
            {
                if (MyPlayer.BoiteDeCollision.Intersects(c.BoiteDeCollision) && !c.EstDétruit)
                {
                    if (MyPlayer.Health < 100 && c is HealthPack)
                        MyPlayer.Health += 25;
                    if (c is AmmoPack && MyPlayer.MyGun != null)
                        MyPlayer.MyGun.TotalMunitions += MyPlayer.MyGun.MunitionsParLoad * 3;

                    c.EstDétruit = true;
                }
            }

            if (LocalPlayer.EstShoot)
            {
                Vector3 positionTir = new Vector3();
                Vector3 rotationBulletHole = new Vector3();

                foreach (Drone drone in ListDrones)
                {
                    if (MyPlayer.EstEnCollision(drone, out positionTir, out rotationBulletHole))
                    {
                        if (Game.Components.Contains(HitMark))
                            HitMark.Alpha = 1;
                        else
                        {
                            HitMark.Initialize();
                            Game.Components.Add(HitMark);
                        }
                        drone.Health -= MyPlayer.MyGun.Dommage;
                        HitMarkerSound.Play(0.4f, 0, 0);
                        MyPlayer.Monney += 50;
                        LocalPlayer.EstShoot = false;
                        return;
                    }
                    else
                    {
                        if(positionTir != Vector3.Zero)
                        {
                            TrouDeBalle bulletHole = new TrouDeBalle(Game, 0.1f, rotationBulletHole, positionTir, new Vector2(10, 10), new Vector2(10, 10), "BulletHole", Data.INTERVALLE_MAJ_BASE, 3.5f);
                            Game.Components.Add(bulletHole);
                            ListeDrawableComponents.Add(bulletHole);
                        }
                    }
                }
                LocalPlayer.EstShoot = false;
            }
        }


        void NettoyerListeComponents()
        {
            for (int i = Game.Components.Count - 1; i >= 0; --i)
            {
                if (Game.Components[i] is IDestructible && ((IDestructible)Game.Components[i]).ADétruire)
                {
                    if (Game.Components[i] is Drone)
                    {
                        ListDrones.Remove((Drone)Game.Components[i]);
                        ++MyPlayer.NbKillDrones;
                    }
                    Game.Components.RemoveAt(i);
                }
            }
        }

        public int NbDronesRestants { get { return ListDrones.Count; } }
        public int NbDronesParWave { get { return WaveNo; } }
        public bool EstWaveSpéciale { get { return WaveNo % 5 == 0; } }
        public int compteurDroneSpawn = 0;
        public static int WaveNo = 1;
        SoundEffect ChangeWaveSong;
        bool bCheck = true;
        float TempsÉcouléSpawn;
        void GererDrones(GameTime gameTime)
        {
            if((TempsÉcouléSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.6f)
            {
                TempsÉcouléSpawn = 0;
                if (PlayState == InGameState.NewWave)
                {
                    if (compteurDroneSpawn < NbDronesParWave)
                    {
                        bCheck = true;
                        ++compteurDroneSpawn;
                        SpawnNewRandomDrone();
                    }
                    else
                        PlayState = InGameState.Play;
                }

                if (bCheck && NbDronesRestants == 0)
                {
                    ChangeWaveSong.Play(0.3f, 0, 0);
                    bCheck = false;
                    WaveNo++;
                    PlayState = InGameState.NewWave;
                    compteurDroneSpawn = 0;
                }
            }
        }

        void SpawnNewRandomDrone()
        {
            Drone d;
            int indexDrone = GenerateurRandom.Next(0, 3);
            if(!EstWaveSpéciale)
            {
                switch (indexDrone)
                {
                    case (RedDrone.IndexDrone): d = new RedDrone(Game, "drone", 0.4f, Vector3.Zero, Vector3.Zero, Data.INTERVALLE_MAJ_BASE, new Color(GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255)), PointsDePatrouille.ListeSpawns[GenerateurRandom.Next(0, PointsDePatrouille.ListeSpawns.Count)], GenerateurRandom.Next(80, 150), (int)Math.Ceiling(WaveNo * WaveNo / 2f)); break;
                    case (BlueDrone.IndexDrone): d = new BlueDrone(Game, "drone", 0.4f, Vector3.Zero, Vector3.Zero, Data.INTERVALLE_MAJ_BASE, new Color(GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255)), PointsDePatrouille.ListeSpawns[GenerateurRandom.Next(0, PointsDePatrouille.ListeSpawns.Count)], GenerateurRandom.Next(80, 150), (int)Math.Ceiling(WaveNo * WaveNo / 2f)); break;
                    case (YellowDrone.IndexDrone): d = new YellowDrone(Game, "drone", 0.4f, Vector3.Zero, Vector3.Zero, Data.INTERVALLE_MAJ_BASE, new Color(GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255)), PointsDePatrouille.ListeSpawns[GenerateurRandom.Next(0, PointsDePatrouille.ListeSpawns.Count)], GenerateurRandom.Next(80, 150), (int)Math.Min(Math.Ceiling(WaveNo * WaveNo / 2f), 162)); break;
                    default: d = new Drone(Game, "drone", 0.4f, Vector3.Zero, Vector3.Zero, Data.INTERVALLE_MAJ_BASE, new Color(GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255)), PointsDePatrouille.ListeSpawns[GenerateurRandom.Next(0, PointsDePatrouille.ListeSpawns.Count)], GenerateurRandom.Next(80, 150), (int)Math.Ceiling(WaveNo * WaveNo / 2f)); break;
                }
            }
            else { d = new InvisibleDrone(Game, "drone", 0.4f, Vector3.Zero, Vector3.Zero, Data.INTERVALLE_MAJ_BASE, new Color(GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255), GenerateurRandom.Next(0, 255)), PointsDePatrouille.ListeSpawns[GenerateurRandom.Next(0, PointsDePatrouille.ListeSpawns.Count)], GenerateurRandom.Next(80, 150), (int)Math.Ceiling(WaveNo * WaveNo / 2f)); }

            ListDrones.Add(d);
            ListeDrawableComponents.Add(d);
            Game.Components.Add(new Afficheur3D(Game));
            Game.Components.Add(d);
        }

        public void IsVisibleController(MainGame.GameState gameState)
        {
            bool state = false;

            if (CONTROLLER_STATE == MainGame.MainGameState) //MODIFIER
                state = true;

            foreach (DrawableGameComponent c in ListeDrawableComponents)
            {
                c.Enabled = state;
                c.Visible = state;
            }

            foreach (GameComponent c in ListeComponent)
            {
                c.Enabled = state;
            }
        }
    }
}
