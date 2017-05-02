using AtelierXNA;
using Microsoft.Xna.Framework;
using MyGame.Composants_de_base;
using MyGame.Entités;
using MyGame.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    class LoadOuaiMap : LoadMap
    {
        Caméra1stPerson CameraJeu;
        LocalPlayer MyPlayer;

        public LoadOuaiMap(Game game)
            :base(game)
        {
            ListeDePack = new List<IPack>();
        }

        public override void LoadCarte()
        {
            InitialiserPlayers();
            InitialiserCrates();
            InitialiserMurs();
            InitialiserCadresGuns();
            InitialiserComposants();

            Game.Services.AddService(typeof(LoadOuaiMap), this);
        }

        public override void UnloadCarte()
        {
            foreach (CadreGun c in GameController.ListCadresGun)
            {
                Game.Components.Remove(c);
                GameController.ListeDrawableComponents.Remove(c);
            }
            foreach (CubeTexturé c in GameController.ListCrate)
            {
                Game.Components.Remove(c);
                GameController.ListeDrawableComponents.Remove(c);
            }
            foreach (DrawableGameComponent c in GameController.ListDrones)
            {
                Game.Components.Remove(c);
                GameController.ListeDrawableComponents.Remove(c);
            }
            foreach (CubeTexturé c in GameController.ListMurs)
            {
                Game.Components.Remove(c);
                GameController.ListeDrawableComponents.Remove(c);
            }

            foreach (GameComponent c in GameController.ListeComponent)
            {
                Game.Components.Remove(c);
            }

            foreach (GameComponent c in GameController.ListeDrawableComponents)
            {
                Game.Components.Remove(c);
            }

            Game.Components.Remove(MyPlayer);
        }

        public override void InitialiserMurs()
        {
            // 2iem map
            //// MURS BORNE EXTERNE
            Vector3 positionMur;
            positionMur = new Vector3(-2, 0, -2);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(505, 40, 7)));

            positionMur = new Vector3(500 - 2.1f, 0, 0);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(7, 60, 500)));

            positionMur = new Vector3(-2, 0, 0);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(7, 40, 505)));

            positionMur = new Vector3(0, 0, 500 - 2.1f);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(505, 60, 10)));

            //MUR INTERNE
            int largeurMur = 12;
            positionMur = new Vector3(50, 0.4f, 450);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurMur / 2, 35, 50)));
            positionMur = new Vector3(28 + largeurMur / 2, 0.4f, 450 - largeurMur / 2);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(22, 35, largeurMur / 2)));
            positionMur = new Vector3(56, 0.4f, 225);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurMur, 35, 180)));
            positionMur = new Vector3(80, 0.4f, 90);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurMur, 35, 100)));
            positionMur = new Vector3(92.15f, 0.4f, 128);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(270, 35, largeurMur)));
            positionMur = new Vector3(280, 0.4f, 280);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(70 - 0.15f, 35, largeurMur)));
            positionMur = new Vector3(350, 0.4f, 243);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurMur, 35, 180)));

            //Plateformes
            positionMur = new Vector3(427.85f, 0.4f, 0.15f);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(72, 15, 72)));
            positionMur = new Vector3(140, 0.4f, 330);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(75, 15, 75)));
            positionMur = new Vector3(444.85f, 0.4f, 250);
            GameController.ListMurs.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(58, 36, 255 - 2.2f)));

            int largeurEscalier = 4;
            //Escalier en haut
            positionMur = new Vector3(215 + largeurEscalier * 4, 0, 348.75f);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 3, 40)));
            positionMur = new Vector3(215 + largeurEscalier * 4 - largeurEscalier, 0, 348.75f);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 6, 40)));
            positionMur = new Vector3(215 + largeurEscalier * 4 - largeurEscalier * 2, 0, 348.75f);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 9, 40)));
            positionMur = new Vector3(215 + largeurEscalier * 4 - largeurEscalier * 3, 0, 348.75f);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 12, 40)));
            positionMur = new Vector3(215 + largeurEscalier * 4 - largeurEscalier * 4, 0, 348.75f);
            GameController.ListMurs.Add(new StairsXNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 15, 40)));


            // Escalier droite
            positionMur = new Vector3(158.75f, 0, 405 + largeurEscalier * 4);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 3, largeurEscalier)));
            positionMur = new Vector3(158.75f, 0, 405 + largeurEscalier * 4 - largeurEscalier);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 6, largeurEscalier)));
            positionMur = new Vector3(158.75f, 0, 405 + largeurEscalier * 4 - largeurEscalier * 2);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 9, largeurEscalier)));
            positionMur = new Vector3(158.75f, 0, 405 + largeurEscalier * 4 - largeurEscalier * 3);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 12, largeurEscalier)));
            positionMur = new Vector3(158.75f, 0, 405 + largeurEscalier * 4 - largeurEscalier * 4);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 16, largeurEscalier)));

            // Escalier Bas
            positionMur = new Vector3(140 - largeurEscalier * 5, 0, 348.75f);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 3, 40)));
            positionMur = new Vector3(140 - largeurEscalier * 5 + largeurEscalier * 1, 0, 348.75f);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 6, 40)));
            positionMur = new Vector3(140 - largeurEscalier * 5 + largeurEscalier * 2, 0, 348.75f);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 9, 40)));
            positionMur = new Vector3(140 - largeurEscalier * 5 + largeurEscalier * 3, 0, 348.75f);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 12, 40)));
            positionMur = new Vector3(140 - largeurEscalier * 5 + largeurEscalier * 4, 0, 348.75f);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 16, 40)));



            //EscalierGauche
            positionMur = new Vector3(158.75f, 0, 330 - largeurEscalier * 5);
            GameController.ListMurs.Add(new StairsZPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 3, largeurEscalier)));
            positionMur = new Vector3(158.75f, 0, 330 - largeurEscalier * 5 + largeurEscalier);
            GameController.ListMurs.Add(new StairsZPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 6, largeurEscalier)));
            positionMur = new Vector3(158.75f, 0, 330 - largeurEscalier * 5 + largeurEscalier * 2);
            GameController.ListMurs.Add(new StairsZPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 9, largeurEscalier)));
            positionMur = new Vector3(158.75f, 0, 330 - largeurEscalier * 5 + largeurEscalier * 3);
            GameController.ListMurs.Add(new StairsZPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 12, largeurEscalier)));
            positionMur = new Vector3(158.75f, 0, 330 - largeurEscalier * 5 + largeurEscalier * 4);
            GameController.ListMurs.Add(new StairsZPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 16, largeurEscalier)));


            // Escalier droite 2iem plateforme
            positionMur = new Vector3(441f, 0, 72 + largeurEscalier * 4);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 3, largeurEscalier)));
            positionMur = new Vector3(441f, 0, 72 + largeurEscalier * 4 - largeurEscalier);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 6, largeurEscalier)));
            positionMur = new Vector3(441f, 0, 72 + largeurEscalier * 4 - largeurEscalier * 2);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 9, largeurEscalier)));
            positionMur = new Vector3(441f, 0, 72 + largeurEscalier * 4 - largeurEscalier * 3);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 12, largeurEscalier)));
            positionMur = new Vector3(441f, 0, 72 + largeurEscalier * 4 - largeurEscalier * 4);
            GameController.ListMurs.Add(new StairsZNegatif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(40, 16, largeurEscalier)));

            //Escalier bas 2iem plateforme
            positionMur = new Vector3(427.85f - largeurEscalier * 5, 0, 18);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 3, 40)));
            positionMur = new Vector3(427.85f - largeurEscalier * 5 + largeurEscalier * 1, 0, 18);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 6, 40)));
            positionMur = new Vector3(427.85f - largeurEscalier * 5 + largeurEscalier * 2, 0, 18);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 9, 40)));
            positionMur = new Vector3(427.85f - largeurEscalier * 5 + largeurEscalier * 3, 0, 18);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 12, 40)));
            positionMur = new Vector3(427.85f - largeurEscalier * 5 + largeurEscalier * 4, 0, 18);
            GameController.ListMurs.Add(new StairsXPositif(Game, 1f, Vector3.Zero, positionMur, "MurTexture", new Vector3(largeurEscalier, 16, 40)));

            foreach (CubeTexturé c in GameController.ListMurs)
                GameController.ListeDrawableComponents.Add(c);
        }

        public override void InitialiserCrates()
        {
            Vector3 taille = new Vector3(14, 14, 14);
            Vector3 positionCrate = new Vector3(290, 0, 412);

            //2iem map
            positionCrate = new Vector3(20, 0, 30);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, taille.Y, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, 0), "MetalBox2", taille));

            positionCrate = new Vector3(120, 0, 230);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, taille.Y, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, -taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(2 * taille.X, 0, 0), "MetalBox2", taille));

            positionCrate = new Vector3(250, 0, 190);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, -taille.Z), "MetalBox2", taille));

            positionCrate = new Vector3(390, 0, 145);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 2 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 3 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, taille.Y, 2 * taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, 2 * taille.Z), "MetalBox2", taille));

            positionCrate = new Vector3(135, 0, 15);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 2 * taille.Z), "MetalBox2", taille));

            positionCrate = new Vector3(230, 0, 60);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 2 * taille.Z), "MetalBox2", taille));

            positionCrate = new Vector3(312, 0, 15);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, 2 * taille.Z), "MetalBox2", taille));

            positionCrate = new Vector3(250, 0, 350);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X, 0, 0), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(taille.X * 2, 0, taille.Z), "MetalBox2", taille));
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate + new Vector3(0, 0, taille.Z * 2), "MetalBox2", taille));

            positionCrate = new Vector3(320, 0, 445);
            GameController.ListCrate.Add(new CubeTexturé(Game, 1f, Vector3.Zero, positionCrate, "MetalBox2", taille));

            foreach (CubeTexturé c in GameController.ListCrate)
                GameController.ListeDrawableComponents.Add(c);
        }

        public override void InitialiserCadresGuns()
        {
            int dimensionCadre = 14;
            Vector3 rotation = new Vector3(0, (float)Math.PI / 2, 0);
            Vector3 positionCadre = new Vector3(49.3f, dimensionCadre - 5f, 460);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, rotation, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadrePistol", Data.INTERVALLE_MAJ_BASE, M9.M9_NAME));

            positionCadre = new Vector3(49.3f, dimensionCadre * 2 - 5f, 460);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, rotation, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadreFamas", Data.INTERVALLE_MAJ_BASE, Famas.FAMAS_NAME));

            positionCadre = new Vector3(49.3f, dimensionCadre - 5f, 460 + dimensionCadre);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, rotation, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadreSpectre", Data.INTERVALLE_MAJ_BASE, Spectre.SPECTRE_NAME));

            positionCadre = new Vector3(49.3f, dimensionCadre - 5f, 460 + dimensionCadre * 2);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, rotation, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadreUmp45", Data.INTERVALLE_MAJ_BASE, UMP45.UMP45_NAME));

            positionCadre = new Vector3(49.3f, dimensionCadre * 2 - 5f, 460 + dimensionCadre * 2);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, rotation, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadreM60", Data.INTERVALLE_MAJ_BASE, M60.M60_NAME));

            positionCadre = new Vector3(49.3f, dimensionCadre * 2 - 5f, 460 + dimensionCadre);
            GameController.ListCadresGun.Add(new CadreGun(Game, 1f, rotation, positionCadre, new Vector2(dimensionCadre, dimensionCadre), new Vector2(1, 1), "CadreM16", Data.INTERVALLE_MAJ_BASE, M16.M16_NAME));

            positionCadre = new Vector3(49.3f, 32f, 460 + dimensionCadre);
            GameController.ListeDrawableComponents.Add(new PlanTexturé(Game, 1f, rotation, positionCadre, new Vector2(3 * dimensionCadre, 4), new Vector2(1, 1), "TexteParDessusCadresGuns", Data.INTERVALLE_MAJ_BASE));

            positionCadre = new Vector3(42, dimensionCadre, 450.7f);
            CadreTourelle cadreTourelle = new CadreTourelle(Game, 1f, new Vector3((float)Math.PI, 0, (float)Math.PI), positionCadre, new Vector2(dimensionCadre * 0.9f, dimensionCadre * 1.5f), new Vector2(1, 1), "CadreTourelle", Data.INTERVALLE_MAJ_BASE);
            GameController.ListeDrawableComponents.Add(cadreTourelle);
            Game.Components.Add(cadreTourelle);


            foreach (CadreGun c in GameController.ListCadresGun)
                GameController.ListeDrawableComponents.Add(c);
        }

        public override void InitialiserPlayers()
        {
            Vector3 posLadder = new Vector3(447, -9, 444);
            BoundingBox boiteLadder = new BoundingBox(new Vector3(443,0,452), new Vector3(444,40,455));
            GameController.ListeDrawableComponents.Add(new ModelCollisionable(Game, "Ladder", 1f, Vector3.Zero, new Vector3(447, -9, 444), boiteLadder));

            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;
            CameraJeu = Game.Services.GetService(typeof(Caméra1stPerson)) as Caméra1stPerson;

            MainGame.CaméraJeu.Position = new Vector3(26, 30, 475);
            CameraJeu.Direction = new Vector3(1, 0, 0);

            // Ammo 1
            AmmoPack ammoPack = new AmmoPack(Game, 1f, Vector3.Zero, new Vector3(470, 40, 470), "AmmoTexture", new Vector3(6, 6, 6), Data.INTERVALLE_MAJ_BASE);
            GameController.ListeDrawableComponents.Add(ammoPack);
            ListeDePack.Add(ammoPack);

            // Ammo 2
            ammoPack = new AmmoPack(Game, 1f, Vector3.Zero, new Vector3(54, 2, 72), "AmmoTexture", new Vector3(6, 6, 6), Data.INTERVALLE_MAJ_BASE);
            GameController.ListeDrawableComponents.Add(ammoPack);
            ListeDePack.Add(ammoPack);
            //Health Pack 1 
            HealthPack healthPack = new HealthPack(Game, 1f, Vector3.Zero, new Vector3(178, 17, 365), "HealthPackTexture", new Vector3(6, 6, 6), Data.INTERVALLE_MAJ_BASE);
            GameController.ListeDrawableComponents.Add(healthPack);
            ListeDePack.Add(healthPack);
            // Health Pack 2 
            healthPack = new HealthPack(Game, 1f, Vector3.Zero, new Vector3(464, 17, 34), "HealthPackTexture", new Vector3(6, 6, 6), Data.INTERVALLE_MAJ_BASE);
            GameController.ListeDrawableComponents.Add(healthPack);
            ListeDePack.Add(healthPack);

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
