using AtelierXNA;
using Microsoft.Xna.Framework;
using MyGame.Entités;
using MyGame.GererPath;
using MyGame.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    public class LoadBridgeMap : LoadMap, ILoadMap
    {
        Caméra1stPerson CameraJeu;
        LocalPlayer MyPlayer;

        public List<GameComponent> ListeMapComponents;

        public LoadBridgeMap(Game game)
            :base(game)
        {
            ListeDePack = new List<IPack>();
        }

        public override void Initialize()
        {
            base.Initialize();
            LoadCarte();
        }

        public override void LoadCarte()
        {
            InitialiserPlayers();
            InitialiserCrates();
            InitialiserMurs();
            InitialiserCadresGuns();
            InitialiserComposants();

            Game.Services.AddService(typeof(LoadBridgeMap), this);
        }

        public override void UnloadCarte()
        {
            foreach(GameComponent c in GameController.ListCadresGun)
            {
                Game.Components.Remove(c);
            }
            foreach (GameComponent c in GameController.ListCrate)
            {
                Game.Components.Remove(c);
            }
            foreach (GameComponent c in GameController.ListDrones)
            {
                Game.Components.Remove(c);
            }
            foreach (GameComponent c in GameController.ListMurs)
            {
                Game.Components.Remove(c);
            }

            Game.Components.Remove(MyPlayer);
        }

        public override void InitialiserMurs()
        {
            // MURS BORNE EXTERNE
            Vector3 positionMur;
            positionMur = new Vector3(0, 0, -2);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(500, 40, 5)));

            positionMur = new Vector3(500 - 2.1f, 0, 0);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(5, 40, 498)));

            positionMur = new Vector3(-2, 0, 0);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(5, 40, 498)));

            positionMur = new Vector3(0, 0, 500 - 2.1f);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(498, 40, 5)));

            //MUR INTERNE
            int largeurMur = 12;
            positionMur = new Vector3(300, 0, 400);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurMur, 35, 100 - 2.1f)));

            positionMur = new Vector3(100 + largeurMur, 0, 300 - largeurMur);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(200, 35, largeurMur)));

            positionMur = new Vector3(50 + largeurMur, 0, 400);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(150, 35, largeurMur)));

            positionMur = new Vector3(50, 0, 200 + largeurMur);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurMur, 35, 200)));


            positionMur = new Vector3(0, 0, 85);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(50, 35, largeurMur / 2)));
            positionMur = new Vector3(50, 0, 65 + largeurMur / 2);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurMur / 2, 35, 20)));
            positionMur = new Vector3(50, 0, 63 + largeurMur / 2);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurMur / 2, 35, 22)));


            //MARCHES
            int largeurEscalier = 8;
            //Escalier en haut
            positionMur = new Vector3(80, 0, 0);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 2, 40)));
            positionMur = new Vector3(80 + largeurEscalier, 0, 0);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 4, 40)));
            positionMur = new Vector3(80 + largeurEscalier * 2, 0, 0);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 6, 40)));
            positionMur = new Vector3(80 + largeurEscalier * 3, 0, 0);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 8, 40)));
            positionMur = new Vector3(80 + largeurEscalier * 4, 0, 0);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 10, 40)));
            positionMur = new Vector3(80 + largeurEscalier * 5, 0, 0);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 12, 40)));

            // Escalier Bas
            positionMur = new Vector3(420 - largeurEscalier, 0, 0);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 2, 40)));
            positionMur = new Vector3(420 - largeurEscalier * 2, 0, 0);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 4, 40)));
            positionMur = new Vector3(420 - largeurEscalier * 3, 0, 0);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 6, 40)));
            positionMur = new Vector3(420 - largeurEscalier * 4, 0, 0);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 8, 40)));
            positionMur = new Vector3(420 - largeurEscalier * 5, 0, 0);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 10, 40)));
            positionMur = new Vector3(420 - largeurEscalier * 6, 0, 0);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 12, 40)));

            // Escalier pont droite
            positionMur = new Vector3(230, 12, 40 - largeurEscalier * 3);
            GameController.ListMurs.Add(new StairsZPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 2, largeurEscalier)));
            positionMur = new Vector3(230, 12, 40 - largeurEscalier * 2);
            GameController.ListMurs.Add(new StairsZPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 4, largeurEscalier)));
            positionMur = new Vector3(230, 12, 40 - largeurEscalier);
            GameController.ListMurs.Add(new StairsZPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 6, largeurEscalier)));
            positionMur = new Vector3(230, 0, 40);
            GameController.ListMurs.Add(new StairsZPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 20, largeurEscalier)));
            // Pont
            int longueurPont = 130;
            positionMur = new Vector3(230, 18f, 40 + largeurEscalier);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 2, longueurPont - largeurEscalier)));
            //EscalierPontGauche
            positionMur = new Vector3(230, 0, 40 + longueurPont);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 20, largeurEscalier)));
            positionMur = new Vector3(230, 0, 40 + longueurPont + largeurEscalier);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 16, largeurEscalier)));
            positionMur = new Vector3(230, 0, 40 + longueurPont + largeurEscalier * 2);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 12, largeurEscalier)));
            positionMur = new Vector3(230, 0, 40 + longueurPont + largeurEscalier * 3);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 8, largeurEscalier)));
            positionMur = new Vector3(230, 0, 40 + longueurPont + largeurEscalier * 4);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 4, largeurEscalier)));

            largeurMur = largeurEscalier;
            //Grosse plateforme entre escaliers
            positionMur = new Vector3(80 + 6 * largeurEscalier, -5, 0);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(260 - largeurEscalier * 2  + 0.1f, 17, 40)));
            // Bordure de Mur en haut droite
            positionMur = new Vector3(80 + 5 * largeurEscalier, 0, 40);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(150 - +5 * largeurEscalier, 25, largeurMur)));
            // Bordure de mur milieu (en dessous du pont)

            //Bordure de Mur en bas droite
            positionMur = new Vector3(270, 0, 40);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(150 - 5 * largeurEscalier, 25, largeurMur)));
            // Bordure de Mur en haut gauche
            positionMur = new Vector3(80 + 5 * largeurEscalier, 0, 40 + longueurPont);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(150 - +5 * largeurEscalier, 25, largeurMur)));
            //Bordure de Mur en bas gauche
            positionMur = new Vector3(270, 0, 40 + longueurPont);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(150 - 5 * largeurEscalier, 25, largeurMur)));

            foreach (CubeTexturé c in GameController.ListMurs)
            {
                GameController.ListeDrawableComponents.Add(c);
            }
        }

        public override void InitialiserCrates()
        {
            Vector3 taille = new Vector3(14, 14, 14);
            Vector3 positionCrate = new Vector3(290, 0, 412);

            positionCrate = new Vector3(360, 0, 370);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 2 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 3 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 4 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, 2 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X * 2, 0, 2 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X * 3, 0, 2 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, taille.Y, 0), "MetalBox2", taille));
            //GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, taille.Y, 3 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, taille.Y, 4 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, taille.Y, 2 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X * 3, taille.Y, 2 * taille.Z), "MetalBox2", taille));


            positionCrate = new Vector3(410, 0, 200);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 2 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 3 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 4 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X * 2, 0, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X * 3, 0, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, taille.Y, 0), "MetalBox2", taille));
            //GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, taille.Y, 3 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, taille.Y, 4 * taille.Z), "MetalBox2", taille));



            positionCrate = new Vector3(300, 0, 226);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, -taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(-taille.X, 0, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, taille.Y, 0), "MetalBox2", taille));


            positionCrate = new Vector3(300 - taille.X, 0, 500 - taille.Z - 2.1f);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, -taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(-taille.Z, 0, 0), "MetalBox2", taille));

            positionCrate = new Vector3(20, 0, 450);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, -taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.Z, 0, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, taille.Y, 0), "MetalBox2", taille));

            //foreach(Case c in PointsDePatrouille.ListePoints)
            //{
            //    GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, c.Position, "MetalBox2", taille));
            //}

            foreach (CubeTexturé c in GameController.ListCrate)
            {
                GameController.ListeDrawableComponents.Add(c);
            }
        }

        public override void InitialiserCadresGuns()
        {
            Vector3 positionCadre;
            int dimensionCadre = 14;

            positionCadre = new Vector3(11 + dimensionCadre * 2, dimensionCadre - 5f, 84.2f);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, Vector3.Zero, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadrePistol", Data.INTERVALLE_MAJ_BASE, M9.M9_NAME));

            positionCadre = new Vector3(11 + dimensionCadre * 2, dimensionCadre * 2 - 5f, 84.2f);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, Vector3.Zero, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadreFamas", Data.INTERVALLE_MAJ_BASE, Famas.FAMAS_NAME));

            positionCadre = new Vector3(11 + dimensionCadre, dimensionCadre - 5f, 84.2f);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, Vector3.Zero, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadreSpectre", Data.INTERVALLE_MAJ_BASE, Spectre.SPECTRE_NAME));

            positionCadre = new Vector3(11, dimensionCadre - 5f, 84.2f);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, Vector3.Zero, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadreUmp45", Data.INTERVALLE_MAJ_BASE, UMP45.UMP45_NAME));

            positionCadre = new Vector3(11, dimensionCadre * 2 - 5f, 84.2f);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, Vector3.Zero, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadreM60", Data.INTERVALLE_MAJ_BASE, M60.M60_NAME));

            positionCadre = new Vector3(11 + dimensionCadre, dimensionCadre * 2 - 5f, 84.2f);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, Vector3.Zero, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadreM16", Data.INTERVALLE_MAJ_BASE, M16.M16_NAME));

            positionCadre = new Vector3(11 + dimensionCadre, 32f, 84.2f);
            GameController.ListeDrawableComponents.Add(new PlanTexturé(Game, 1f, Vector3.Zero, positionCadre, new Vector2(3 * dimensionCadre, 4), new Vector2(1, 1), "TexteParDessusCadresGuns", Data.INTERVALLE_MAJ_BASE));

            positionCadre = new Vector3(49.5f, dimensionCadre, 76.5f);
            CadreTourelle cadreTourelle = new CadreTourelle(Game, 1f, new Vector3((float)Math.PI, 1.5f * (float)Math.PI, (float)Math.PI), positionCadre, new Vector2(dimensionCadre * 0.9f, dimensionCadre * 1.5f), new Vector2(1, 1), "CadreTourelle", Data.INTERVALLE_MAJ_BASE);
            GameController.ListeDrawableComponents.Add(cadreTourelle);
            Game.Components.Add(cadreTourelle);

            foreach (CadreGun c in GameController.ListCadresGun)
            {
                GameController.ListeDrawableComponents.Add(c);
            }
        }

        public override void InitialiserPlayers()
        {
            Random random = Game.Services.GetService(typeof(Random)) as Random;
            List<Drone> ListeDrone = new List<Drone>();
            
            CameraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;
            MainGame.CaméraJeu.Position = new Vector3(26, 40, 36);
            CameraJeu.Direction = new Vector3(0, 0, 1);

            Vector3 positionObjet = new Vector3(0, 10, 100);

            HealthPack healthPack = new HealthPack(Game, 1f, Vector3.Zero, new Vector3(230, 2, 348), "HealthPackTexture", new Vector3(6, 6, 6), Data.INTERVALLE_MAJ_BASE);
            //Health Pack 1 
            GameController.ListeDrawableComponents.Add(healthPack);
            ListeDePack.Add(healthPack);

            // Health Pack 2 Pont
            healthPack = new HealthPack(Game, 1f, Vector3.Zero, new Vector3(247.5f, 20.5f, 105), "HealthPackTexture", new Vector3(6, 6, 6), Data.INTERVALLE_MAJ_BASE);
            GameController.ListeDrawableComponents.Add(healthPack);
            ListeDePack.Add(healthPack);

            AmmoPack ammoPack = new AmmoPack(Game, 1f, Vector3.Zero, new Vector3(460, 2, 460), "AmmoTexture", new Vector3(6, 6, 6), Data.INTERVALLE_MAJ_BASE);
            // Ammo 1
            GameController.ListeDrawableComponents.Add(ammoPack);
            ListeDePack.Add(ammoPack);

            // Ammo 2
            ammoPack = new AmmoPack(Game, 1f, Vector3.Zero, new Vector3(450, 2, 60), "AmmoTexture", new Vector3(6, 6, 6), Data.INTERVALLE_MAJ_BASE);
            GameController.ListeDrawableComponents.Add(ammoPack);
            ListeDePack.Add(ammoPack);

            // Pipes au fond 
            GameController.ListeDrawableComponents.Add(new Afficheur3D(Game));
            GameController.ListeDrawableComponents.Add(new ObjetDeBase(Game, "kulmaputki", 0.10f, Vector3.Zero, new Vector3(484, 2.5f, 490)));
            GameController.ListeDrawableComponents.Add(new ObjetDeBase(Game, "kulmaputki", 0.10f, Vector3.Zero, new Vector3(484, 2.5f, 475)));
            GameController.ListeDrawableComponents.Add(new ObjetDeBase(Game, "kulmaputki", 0.10f, Vector3.Zero, new Vector3(484, 2.5f, 460)));
            GameController.ListeDrawableComponents.Add(new ObjetDeBase(Game, "kulmaputki", 0.10f, new Vector3(0, 4.73f, 0), new Vector3(455, 2.5f, 484)));
            GameController.ListeDrawableComponents.Add(new ObjetDeBase(Game, "kulmaputki", 0.10f, new Vector3(0, 4.73f, 0), new Vector3(440, 2.5f, 484)));
            GameController.ListeDrawableComponents.Add(new ObjetDeBase(Game, "kulmaputki", 0.10f, new Vector3(0, 4.73f, 0), new Vector3(425, 2.5f, 484)));


            GameController.ListeDrawableComponents.Add(new ObjetDeBase(Game, "kulmaputki", 0.10f, Vector3.Zero, new Vector3(484, 2.5f, 10)));
            GameController.ListeDrawableComponents.Add(new ObjetDeBase(Game, "kulmaputki", 0.10f, Vector3.Zero, new Vector3(484, 2.5f, 25)));
            GameController.ListeDrawableComponents.Add(new ObjetDeBase(Game, "kulmaputki", 0.10f, Vector3.Zero, new Vector3(484, 2.5f, 40)));
            GameController.ListeDrawableComponents.Add(new ObjetDeBase(Game, "Earth", 7f, new Vector3((float)Math.PI,(float)Math.PI,0), new Vector3(450, 200f, 450)));

            //GameController.ListeDrawableComponents.Add(new Tourelle(Game, "turret", 1, Vector3.Zero, new Vector3(135, 0, 103), Data.INTERVALLE_MAJ_BASE, Color.White));


            GameController.ListeDrawableComponents.Add(MyPlayer);
        }

        public override void InitialiserComposants()
        {
            foreach (DrawableGameComponent c in GameController.ListeDrawableComponents)
            {
                if (!Game.Components.Contains(c))
                    Game.Components.Add(c);
            }

            foreach (GameComponent c in GameController.ListeComponent)
            {
                if (!Game.Components.Contains(c))
                    Game.Components.Add(c);
            }
        }
    }
}
