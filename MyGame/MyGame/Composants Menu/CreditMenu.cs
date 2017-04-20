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
    public class CreditMenu : Microsoft.Xna.Framework.DrawableGameComponent, IMenu
    {
        Screen Ecran;

        public List<DrawableGameComponent> ListeBoxEventMenu;

        public static BoxEventMenu CreditBox;
        public static BoxEventMenu BackBox;

        public CreditMenu(Game game)
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

            CreditBox = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 7, Ecran.CenterScreen.Y / 8),
                                    "Fait par : \n Benjamin Archambault &\n Derek Bernard\n\nCréateur des cartes :\nDerek Bernard", Color.White, Actions.Null);
            ListeBoxEventMenu.Add(CreditBox);

            BackBox = new BoxEventMenu(Game, new Point(Ecran.CenterScreen.X / 7, Ecran.CenterScreen.Y + 200), "BACK", Color.LightGoldenrodYellow, Actions.BackToMainMenu);
            ListeBoxEventMenu.Add(BackBox);

            Game.Components.Add(CreditBox);
            Game.Components.Add(BackBox);
        }

        //UPDATER

        double TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            if ((TempsÉcoulé += gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                foreach (BoxEventMenu box in ListeBoxEventMenu)
                {
                    if (box.bEstClické)
                    {
                        box.OnClick.Invoke(Game);
                        box.bEstClické = false;
                    }
                }

                base.Update(gameTime);
            }
        }

        //MÉTHODES
        public void ToggleMenu(MenuController.MenuState menuState)
        {
            bool state = false;

            if (menuState == MenuController.MenuState.Credits) //MODIFIER
                state = true;

            foreach (DrawableGameComponent c in ListeBoxEventMenu)
            {
                c.Enabled = state;
                c.Visible = state;
            }
        }
    }
}
