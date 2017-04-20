using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace AtelierXNA
{
    class Drone : ObjetDeDémo
    {
        float Compteur { get; set; }
        public Drone(Game jeu, string nomModèle, float échelleInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, float intervalleMAJ, Color couleur) 
            : base(jeu, nomModèle, échelleInitiale, rotationInitiale, positionInitiale, intervalleMAJ, couleur)
        {
        }

        protected override void EffectuerMiseÀJour()
        {
            Compteur += 0.05f;
            Vector3 hauteur = new Vector3(0, (float)Math.Sin(Compteur), 0);
            Position += hauteur * 0.1f; //CHANGER AU BESOIN
            base.EffectuerMiseÀJour();
            base.CalculerMonde();
        }
    }
}
