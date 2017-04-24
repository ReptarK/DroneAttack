//Les infos du jeu
using AtelierXNA;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyGame
{
    public static class Data
    {
        public const float INTERVALLE_MAJ_BASE = 1 / 60f;

        public const float GRAVITÉ = 4;

        public enum CarteName
        {
            BridgeMap,
            OuaiMap
        }

        public static CarteName NomCarte = CarteName.BridgeMap;

        public static Vector3 DimentionCarte;

        //SENSITIVITY
        static float sensivity;
        public static float Sensivity
        {
            get { return sensivity; }
            set
            {
                if (value < 0.1f)
                {
                    sensivity = 0.1f;
                    return;
                }
                if (value > 2)
                {
                    sensivity = 2;
                    return;
                }
                sensivity = value;
            }
        }

        //MAP
        public const string CARTE_BRIDGE = "Bridge";
        public const string OUAI_MAP = "OUAI";
        public static string NomCarteChoisi { get; set; }

        //CROSSHAIR COLOR
        const int NB_COLOR = 6;
        public static Color[] TableauCouleur;

        static int colorIndex = 0;
        public static int CouleurIndex
        {
            get { return colorIndex; }
            set
            {
                if (value == NB_COLOR)
                {
                    value = 0;
                }
                if (value < 0)
                {
                    value = NB_COLOR - 1;
                }

                colorIndex = value;
            }
        }


        //DIFFICULTÉ
        static int difficultéIndex;
        public static int DifficultéIndex
        {
            get { return difficultéIndex; }
            set
            {
                if (value < 0)
                {
                    value = TableauDifficultés.Length - 1;
                    return;
                }
                if (value == TableauDifficultés.Length)
                {
                    value = 0;
                    return;
                }
                difficultéIndex = value;
            }
        }

        static int[] TableauDifficultés = new int[4] { 0, 1, 2, 3 };
        public static NiveauxDifficultés DifficultéJeu
        {
            get
            {
                switch(DifficultéIndex)
                {
                    case (0): return NiveauxDifficultés.Facile;
                    case (1): return NiveauxDifficultés.Moyen;
                    case (2): return NiveauxDifficultés.Difficile;
                    case (3): return NiveauxDifficultés.Extrême;
                    default: return NiveauxDifficultés.Facile;
                }
            }
        }
        public enum NiveauxDifficultés
        {
            Facile = 0,
            Moyen = 1,
            Difficile = 2,
            Extrême = 3
        }

        public static int ChanceDeTir
        {
            get { return 4 - DifficultéIndex; }
        }

        static Data()
        {
            Sensivity = 1f;
            NomCarteChoisi = "";

            TableauCouleur = new Color[NB_COLOR] { Color.Lime, Color.Black, Color.Yellow, Color.Red, Color.Cyan, Color.Purple };
            CouleurIndex = 0;

            DimentionCarte = new Vector3(500, 0, 500);

        }
    }
}
