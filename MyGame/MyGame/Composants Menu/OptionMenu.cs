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
    public class OptionMenu : Microsoft.Xna.Framework.DrawableGameComponent, IMenu
    {
        Screen Ecran;

        public List<DrawableGameComponent> ListeBoxEventMenu;

        public static BoxEventMenu SensivityBox;
        public static BoxTextureMenu AugmentSensivityBox;
        public static BoxTextureMenu LowerSensivityBox;

        public static BoxEventMenu DifficultéTexte;
        public static BoxTextureMenu PlusDifficileBoite;
        public static BoxTextureMenu MoinsDifficileBoite;

        public static TextureAvantPlan CrossHairColor;
        public static BoxTextureMenu ChangeColorRIGHT;
        public static BoxTextureMenu ChangeColorLEFT;

        public static BoxEventMenu BackBox;
        public OptionMenu(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            Ecran = Game.Services.GetService(typeof(Screen)) as Screen;

            InitialiserBoites();
        }

        void InitialiserBoites()
        {
            ListeBoxEventMenu = new List<DrawableGameComponent>();

            SensivityBox = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 4, Ecran.CenterScreen.Y / 4), "SENSIBILITÉ : " + Data.Sensivity, Color.White, Actions.Null);
            ListeBoxEventMenu.Add(SensivityBox);

            AugmentSensivityBox = new BoxTextureMenu(Game, new Point(Ecran.CenterScreen.X + 80, Ecran.CenterScreen.Y / 4), new Point(40,40), " +", Color.White, "Boxtexture", Actions.SensivityUP);
            LowerSensivityBox = new BoxTextureMenu(Game, new Point(Ecran.CenterScreen.X + 130, Ecran.CenterScreen.Y / 4), new Point(40, 40), "  -", Color.White, "Boxtexture", Actions.SensitivityDOWN);

            DifficultéTexte = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 2, Ecran.CenterScreen.Y / 4 + 70), Data.DifficultéJeu.ToString(), Color.White, Actions.Null);
            PlusDifficileBoite = new BoxTextureMenu(Game, new Point(Ecran.CenterScreen.X / 2 + 125, Ecran.CenterScreen.Y / 4 + 70), new Point(40, 40), "->", Color.White, "Boxtexture", Actions.AugmentDifficulty);
            MoinsDifficileBoite = new BoxTextureMenu(Game, new Point(Ecran.CenterScreen.X / 2 - 60, Ecran.CenterScreen.Y / 4 + 70), new Point(40, 40), "<-", Color.White, "Boxtexture", Actions.LowerDifficulty);
            ListeBoxEventMenu.Add(DifficultéTexte);
            ListeBoxEventMenu.Add(PlusDifficileBoite);
            ListeBoxEventMenu.Add(MoinsDifficileBoite);

            CrossHairColor = new TextureAvantPlan(Game, new Rectangle(Ecran.CenterScreen.X / 2, Ecran.CenterScreen.Y / 4 + 125, 100, 100), "CrossHair", Data.TableauCouleur[Data.CouleurIndex]);
            Game.Components.Add(CrossHairColor);
            ChangeColorRIGHT = new BoxTextureMenu(Game, new Point(Ecran.CenterScreen.X / 2 + 110, Ecran.CenterScreen.Y / 4 + 150), new Point(40, 40), "->", Color.White, "Boxtexture", Actions.ColorIndexUP);
            ChangeColorLEFT = new BoxTextureMenu(Game, new Point(Ecran.CenterScreen.X / 2 - 50, Ecran.CenterScreen.Y / 4 + 150), new Point(40, 40), "<-", Color.White, "Boxtexture", Actions.ColorIndexDOWN);
            ListeBoxEventMenu.Add(ChangeColorRIGHT);
            ListeBoxEventMenu.Add(ChangeColorLEFT);

            ListeBoxEventMenu.Add(AugmentSensivityBox);
            ListeBoxEventMenu.Add(LowerSensivityBox);

            BackBox = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 4, Ecran.CenterScreen.Y + 100), "BACK", Color.LightGoldenrodYellow, Actions.BackToMainMenu);
            ListeBoxEventMenu.Add(BackBox);

            foreach (GameComponent box in ListeBoxEventMenu)
                Game.Components.Add(box);
        }

        //UPDATER

        float Sensitivity = Data.Sensivity;
        Data.NiveauxDifficultés difficultéActuelle;
        double TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            foreach (IBoxMenu box in ListeBoxEventMenu)
            {
                if (box.bEstClické)
                {
                    box.OnClick.Invoke(Game);
                    box.bEstClické = false;
                }
            }

            if ((TempsÉcoulé += gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                if (Sensitivity != Data.Sensivity)
                {
                    SensivityBox.Initialize();
                    SensivityBox.Texte = "SENSIBILITÉ : " + Math.Round(Data.Sensivity, 1);
                    Sensitivity = Data.Sensivity;
                }

                if (difficultéActuelle != Data.DifficultéJeu)
                {
                    DifficultéTexte.Initialize();
                    DifficultéTexte.Texte = Data.DifficultéJeu.ToString();
                    difficultéActuelle = Data.DifficultéJeu;
                }


                base.Update(gameTime);
                TempsÉcoulé = 0;
            }
        }

        //MÉTHODES
        public void ToggleMenu(MenuController.MenuState menuState)
        {
            bool state = false;

            if (menuState == MenuController.MenuState.Option)
                state = true;

            foreach (DrawableGameComponent c in ListeBoxEventMenu)
            {
                c.Enabled = state;
                c.Visible = state;
            }

            CrossHairColor.Enabled = state;
            CrossHairColor.Visible = state;
        }
    }
}
