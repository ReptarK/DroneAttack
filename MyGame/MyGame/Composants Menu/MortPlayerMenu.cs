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
using AtelierXNA;
using AtelierXNA.Composant_Menu;
using MyGame.Entit�s;

namespace MyGame
{
    public class MortPlayerMenu : Microsoft.Xna.Framework.DrawableGameComponent, IMenu
    {
        Screen Ecran;
        RessourcesManager<SpriteFont> GestionnaireDeFonts;
        SpriteBatch GestionnaireDesSprites;
        SpriteFont TexteFont;
        LocalPlayer MyPlayer;

        public static BoxEventMenu ScoreBoardBox;
        public static BoxEventMenu RestartBox;

        public static bool ShowDeathRecap;

        public List<DrawableGameComponent> ListeBoxEventMenu;

        public MortPlayerMenu(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
            Ecran = Game.Services.GetService(typeof(Screen)) as Screen;
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;

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

            RestartBox = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 14, Ecran.CenterScreen.Y + 200), "RECOMMENCER LA PARTIE", Color.LightGoldenrodYellow, Actions.RestartGame);
            ListeBoxEventMenu.Add(RestartBox);

            string scoreBoardtexte = "R�CAPITULATIF DE LA PARTIE : " + "\n\n" +
                    "- Niveau de difficult� de la partie : " + Data.Difficult�Jeu.ToString() + "\n" +
                    "- Vague(s) de Drones surv�cue(s) : " + GameController.WaveNo.ToString() + "\n" +
                "- Nombre de Drones tu�s: " + MyPlayer.NbKillDrones.ToString() + "\n" +
                "- Nombre d'argent r�colt� : " + MyPlayer.Monney.ToString();

            ScoreBoardBox = new BoxEventMenu(Game, new Point(20, Ecran.CenterScreen.Y / 3), scoreBoardtexte, Color.LightGoldenrodYellow, Actions.Null);
            ListeBoxEventMenu.Add(ScoreBoardBox);

            foreach(DrawableGameComponent c in ListeBoxEventMenu)
            {
                Game.Components.Add(c);
            }
        }

        float Temps�coul�;
        public override void Update(GameTime gameTime)
        {
            if ((Temps�coul� += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                ScoreBoardBox.Texte = "R�CAPITULATIF DE LA PARTIE : " + "\n\n" +
                    "- Niveau de difficult� de la partie : " + Data.Difficult�Jeu.ToString() + "\n" +
                    "- Vague(s) de Drones surv�cue(s) : " + GameController.WaveNo.ToString() + "\n" +
                "- Nombre de Drones tu�s: " + MyPlayer.NbKillDrones.ToString() + "\n" +
                "- Nombre d'argent r�colt� : " + MyPlayer.Monney.ToString();

                foreach (IBoxMenu box in ListeBoxEventMenu)
                {
                    if (box.bEstClick�)
                    {
                        box.OnClick.Invoke(Game);
                        box.bEstClick� = false;
                    }
                }

                base.Update(gameTime);
            }
        }

        public void ToggleMenu(MenuController.MenuState menuState)
        {
            bool state = false;

            if (menuState == MenuController.MenuState.ScoreBoard)
                state = true;

            foreach (DrawableGameComponent c in ListeBoxEventMenu)
            {
                c.Enabled = state;
                c.Visible = state;
            }
        }
    }
}
