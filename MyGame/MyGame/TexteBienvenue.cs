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
using MyGame.Composants_de_base;
using AtelierXNA;
using MyGame.EntitÈs;

namespace MyGame
{
    public class TexteBienvenue : Microsoft.Xna.Framework.DrawableGameComponent
    {
        LocalPlayer MyPlayer;
        RessourcesManager<SpriteFont> GestionnaireDeFont;
        string Texte¿Afficher { get; set; }
        string NomFont { get; set; }
        float …chelle { get; set; }
        float MargeDroite { get; set; }
        float MargeGauche { get; set; }
        float MargeHaut { get; set; }
        float MargeBas { get; set; }
        float MargeX { get; set; }
        float MargeY { get; set; }
        Rectangle ZoneAffichage { get; set; }
        Color Couleur { get; set; }
        SpriteBatch GestionSprites { get; set; }
        SpriteFont Font { get; set; }
        Vector2 Origine { get; set; }
        Vector2 Position { get; set; }


        // Constructor
        public TexteBienvenue(Game jeu, string texte¿Afficher, string nomFont, Rectangle zoneAffichage, Color couleurTexte, float marge)
            : base(jeu)
        {
            NomFont = nomFont;
            Texte¿Afficher = texte¿Afficher;
            ZoneAffichage = zoneAffichage;
            Couleur = couleurTexte;
            MargeX = marge * Game.Window.ClientBounds.Width;
            MargeY = marge * Game.Window.ClientBounds.Height;
        }
        // Initialize
        public override void Initialize()
        {
            base.Initialize();
            DrawOrder = 1000;
            MargeDroite = Game.Window.ClientBounds.Width - MargeX;
            MargeGauche = 0 + MargeX;
            MargeHaut = 0 + MargeY;
            MargeBas = Game.Window.ClientBounds.Height + MargeY;
            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;
        }
        // LoadContent
        protected override void LoadContent()
        {
            base.LoadContent();
            GestionnaireDeFont = Game.Services.GetService(typeof(RessourcesManager<SpriteFont>)) as RessourcesManager<SpriteFont>;
            Font = GestionnaireDeFont.Find(NomFont);
            GestionSprites = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            Vector2 dimension = Font.MeasureString(Texte¿Afficher);
            Origine = new Vector2(dimension.X / 2, dimension.Y / 2);
            …chelle = MathHelper.Min((ZoneAffichage.Width - MargeX) / dimension.X, (ZoneAffichage.Height - MargeY) / dimension.Y);
            Position = new Vector2(ZoneAffichage.Width / 2, ZoneAffichage.Height / 2);

        }

        float Temps…coulÈ = 0;
        public override void Update(GameTime gameTime)
        {
            if ((Temps…coulÈ += (float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                if(MyPlayer.MyGun != null)
                {
                    Game.Components.Remove(this);
                }
            }
        }

        // Draw
        public override void Draw(GameTime gameTime)
        {
            GestionSprites.Begin();
            GestionSprites.DrawString(Font, Texte¿Afficher, Position, Couleur, 0f, Origine, …chelle, SpriteEffects.None, 0.0f);
            GestionSprites.End();
        }
    }
}
