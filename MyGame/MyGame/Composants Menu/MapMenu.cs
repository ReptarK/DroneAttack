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
using System.IO;
using System.Windows.Forms;
using MyGame.Composants_de_base;

namespace AtelierXNA.Composant_Menu
{
    public class MapMenu : Microsoft.Xna.Framework.DrawableGameComponent, IMenu
    {
        StreamWriter EcritureFichier;

        Data.CarteName OldCarteName;

        Screen Ecran;

        public List<DrawableGameComponent> ListeBoxEventMenu;

        public static BoxTextureMenu BridgeMap;
        public static BoxTextureMenu Map2;
        public static BoxEventMenu BackBox;
        public static TexteCarte TexteLoadedMap;

        public MapMenu(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            base.Initialize();
            Ecran = Game.Services.GetService(typeof(Screen)) as Screen;

            EcritureFichier = new StreamWriter("MapName.txt", false);
            OldCarteName = Data.NomCarte;
            InitialiserBoites();
        }



        void InitialiserBoites()
        {
            ListeBoxEventMenu = new List<DrawableGameComponent>();

            BridgeMap = new BoxTextureMenu(Game, new Point(Ecran.Size.Width / 10, Ecran.Size.Height / 8),
                                           new Point(Ecran.Size.Width / 3, Ecran.Size.Height / 2), "Carte :\nBRIDGE", Color.White,"BridgeMap", Actions.CarteBridge);
            ListeBoxEventMenu.Add(BridgeMap);

            Map2 = new BoxTextureMenu(Game, new Point(Ecran.Size.Width / 10 + Ecran.Size.Width / 2, Ecran.Size.Height / 8),
                                           new Point(Ecran.Size.Width / 3, Ecran.Size.Height / 2), "Carte :\nOUAI", Color.White, "OuaiMap", Actions.OuaiMap);
            ListeBoxEventMenu.Add(Map2);

            BackBox = new BoxEventMenu(Game, new Point(Ecran.Size.Width / 20, Ecran.Size.Height - 40), "BACK", Color.LightGoldenrodYellow, Actions.BackToMainMenu);
            ListeBoxEventMenu.Add(BackBox);

            TexteLoadedMap = new TexteCarte(Game, "", "Arial_Box", Vector2.Zero, Color.WhiteSmoke);
            

            Game.Components.Add(BridgeMap);
            Game.Components.Add(Map2);
            Game.Components.Add(BackBox);
            Game.Components.Add(TexteLoadedMap);
        }

        //UPDATER

        double TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            if ((TempsÉcoulé += gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                if(OldCarteName != Data.NomCarte)
                {
                    OldCarteName = Data.NomCarte;
                    EcritureFichier.WriteLine(OldCarteName.ToString());
                    EcritureFichier.Dispose();
                    EcritureFichier.Close();
                    Application.Restart();
                }

                foreach (IBoxMenu box in ListeBoxEventMenu)
                {
                    if (box.bEstClické)
                    {
                        box.OnClick.Invoke(Game);
                        box.bEstClické = false;
                    }
                }

                base.Update(gameTime);
                TempsÉcoulé = 0;
            }
        }

        public void ToggleMenu(MenuController.MenuState menuState)
        {
            bool state = false;

            if (menuState == MenuController.MenuState.Map)
                state = true;

            foreach (DrawableGameComponent c in ListeBoxEventMenu)
            {
                c.Enabled = state;
                c.Visible = state;
            }
        }
    }
}
