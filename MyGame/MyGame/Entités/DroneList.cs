using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MyGame.Entités;
using Microsoft.Xna.Framework.Graphics;
using MyGame;
using MyGame.GererPath;
using Microsoft.Xna.Framework.Audio;
using AtelierXNA;

namespace MyGame.Entités
{
    public class RedDrone : Drone
    {
        public const int IndexDrone = 0;
        const int DEGATS_BASE = 4;
        Color COULEUR_BASE = Color.Red;
        const float HEALTH_BASE_COOEFICIENT = 1f;

        const int BORNE_MIN = 100;
        const int BORNE_MAX = 110;

        public RedDrone(Game jeu, string nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ, Color couleur, Case startCase, float divisionDeplacement, int health)
            : base(jeu, nomModèle, échelleInitiale, rotationInitiale, positionInitiale, intervalleMAJ, couleur, startCase, divisionDeplacement, health)
        {
            GenerateurRandom = Game.Services.GetService(typeof(Random)) as Random;
            Degats = DEGATS_BASE;
            Couleur = COULEUR_BASE;
            DivisionDeplacement = GenerateurRandom.Next(BORNE_MIN, BORNE_MAX);
            Health = (int)(Health * HEALTH_BASE_COOEFICIENT);
        }
    }

    public class YellowDrone : Drone
    {
        public const int IndexDrone = 1;
        const int DEGATS_BASE = 3;
        Color COULEUR_BASE = Color.Yellow;
        const float HEALTH_BASE_COOEFICIENT = 0.8f;

        const int BORNE_MIN = 70;
        const int BORNE_MAX = 80;

        public YellowDrone(Game jeu, string nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ, Color couleur, Case startCase, float divisionDeplacement, int health)
            : base(jeu, nomModèle, échelleInitiale, rotationInitiale, positionInitiale, intervalleMAJ, couleur, startCase, divisionDeplacement, health)
        {
            GenerateurRandom = Game.Services.GetService(typeof(Random)) as Random;
            Degats = DEGATS_BASE;
            Couleur = COULEUR_BASE;
            DivisionDeplacement = GenerateurRandom.Next(BORNE_MIN, BORNE_MAX);
            Health = (Health * HEALTH_BASE_COOEFICIENT);
        }
    }

    public class BlueDrone : Drone
    {
        public const int IndexDrone = 2;
        const int DEGATS_BASE = 6;
        const float HEALTH_BASE_COOEFICIENT = 2f;
        Color COULEUR_BASE = Color.Blue;

        const int BORNE_MIN = 160;
        const int BORNE_MAX = 170;

        public BlueDrone(Game jeu, string nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ, Color couleur, Case startCase, float divisionDeplacement, int health)
            : base(jeu, nomModèle, échelleInitiale, rotationInitiale, positionInitiale, intervalleMAJ, couleur, startCase, divisionDeplacement, health)
        {
            GenerateurRandom = Game.Services.GetService(typeof(Random)) as Random;
            Degats = DEGATS_BASE;
            Couleur = COULEUR_BASE;
            DivisionDeplacement = GenerateurRandom.Next(BORNE_MIN, BORNE_MAX);
            Health = (int)(Health * HEALTH_BASE_COOEFICIENT);
        }
    }

    public class InvisibleDrone : Drone
    {
        public const int IndexDrone = 3;
        const int DEGATS_BASE = 4;
        Color COULEUR_BASE = Color.Black;
        const float HEALTH_BASE_COOEFICIENT = 0.8f;

        public InvisibleDrone(Game jeu, string nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ, Color couleur, Case startCase, float divisionDeplacement, int health)
            : base(jeu, nomModèle, échelleInitiale, rotationInitiale, positionInitiale, intervalleMAJ, couleur, startCase, divisionDeplacement, health)
        {
            GenerateurRandom = Game.Services.GetService(typeof(Random)) as Random;
            Degats = DEGATS_BASE;
            DivisionDeplacement = GenerateurRandom.Next(70, 80);
            Health = (int)(Health * HEALTH_BASE_COOEFICIENT);
        }

        float TempsÉcoulé;
        bool EstInvisible = false;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > 1f)
            {
                EstInvisible = !EstInvisible;
                TempsÉcoulé = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (!EstInvisible)
                base.Draw(gameTime);
        }
    }
}
