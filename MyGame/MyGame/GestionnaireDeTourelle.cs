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
using MyGame.Entités;
using MyGame.Composants_de_base;
using AtelierXNA;

namespace MyGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GestionnaireDeTourelle : TextureAvantPlan
    {
        Screen Ecran;
        TexteAvantPlan TexteNbTourelles;

        public GestionnaireDeTourelle(Game game, Rectangle zoneAffichage, string nomTexture, Color couleur)
            : base(game, zoneAffichage, nomTexture, couleur)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            NbTurretTemp = LocalPlayer.NbTurret;
            TexteNbTourelles = new TexteAvantPlan(Game, "x 0", "Pescadero", new Vector2(Ecran.CenterScreen.X * 2 - 45, Ecran.CenterScreen.Y + 50), Couleur);
            Game.Components.Add(TexteNbTourelles);
            GameController.ListeDrawableComponents.Add(TexteNbTourelles);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Ecran = Game.Services.GetService(typeof(Screen)) as Screen;
        }

        bool UpdateTexte;
        int NbTurretTemp;
        float TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                if (NbTurretTemp != LocalPlayer.NbTurret)
                {
                    NbTurretTemp = LocalPlayer.NbTurret;

                    TexteNbTourelles.Texte = "x " + LocalPlayer.NbTurret.ToString();
                }
            }

        }

        public override void Draw(GameTime gameTime)
        {
            GestionSprites.Begin();
            GestionSprites.Draw(TextureAfficher, ZoneAffichage, Couleur);
            GestionSprites.End();
        }
    }
}
