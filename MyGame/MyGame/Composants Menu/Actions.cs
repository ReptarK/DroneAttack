using AtelierXNA.Composant_Menu;
using Microsoft.Xna.Framework;
using MyGame;
using MyGame.Composants_Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AtelierXNA
{
    class Actions
    {
        public static void StartGame(Game game)
        {
            if (MenuController.bChoseMap == false)
            {
                MenuController.State = MenuController.MenuState.Map;
                return;
            }

            
            MenuController.State = MenuController.MenuState.Jeu;
            MainGame.MainGameState = MainGame.GameState.Jeu;

            MainGame.DoGameInitialize = true;

            GameController.DoInitialize = true;
        }

        public static void Null(Game game)
        {

        }
        public static void QuitGame(Game game)
        {
            game.Exit();
        }

        public static void RestartGame(Game game)
        {
            Application.Restart();
        }

        public static void OpenOptionMenu(Game game)
        {
            MenuController.State = MenuController.MenuState.Option;
        }

        public static void OpenCreditsMenu(Game game)
        {
            MenuController.State = MenuController.MenuState.Credits;
        }

        public static void OpenMapMenu(Game game)
        {
            MenuController.State = MenuController.MenuState.Map;
        }

        public static void BackToMainMenu(Game game)
        {
            MenuController.State = MenuController.MenuState.Main;
        }

        //SET Sensivity
        public static void SensivityUP(Game game)
        {
            Data.Sensivity += 0.1f;
        }

        public static void SensitivityDOWN(Game game)
        {
            Data.Sensivity -= 0.1f;
        }

        //MAP
        public static void CarteBridge(Game game)
        {
            MenuController.bChoseMap = true;
            Data.NomCarte = Data.CarteName.BridgeMap;
            MenuController.State = MenuController.MenuState.Main;
        }

        public static void OuaiMap(Game game)
        {
            MenuController.bChoseMap = true;
            Data.NomCarte = Data.CarteName.OuaiMap;

            MenuController.State = MenuController.MenuState.Main;
        }

        //BOTS
        public static void AugmentDifficulty(Game game)
        {
            if (!GameController.DoInitialize)
                Data.DifficultéIndex++;
        }

        public static void LowerDifficulty(Game game)
        {
            if (!GameController.DoInitialize)
                Data.DifficultéIndex--;
        }

        //COLOR INDEX
        public static void ColorIndexUP(Game game)
        {
            Data.CouleurIndex++;
            OptionMenu.CrossHairColor.Couleur = Data.TableauCouleur[Data.CouleurIndex];
        }

        public static void ColorIndexDOWN(Game game)
        {
            Data.CouleurIndex--;
            OptionMenu.CrossHairColor.Couleur = Data.TableauCouleur[Data.CouleurIndex];
        }

    }
}
