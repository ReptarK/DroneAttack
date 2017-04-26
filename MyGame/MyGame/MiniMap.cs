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
using AtelierXNA;
using MyGame.Entités;

namespace MyGame
{
    public class MiniMap : Microsoft.Xna.Framework.DrawableGameComponent, Overlay
    {
        Screen Ecran;
        protected SpriteBatch GestionSprites { get; private set; }
        protected RessourcesManager<Texture2D> GestionnaireDeTextures { get; set; }
        protected RessourcesManager<Texture2D> GestionnaireDeTexturesMap { get; set; }

        public Rectangle ZoneAffichage;
        public string NomTexture;

        Texture2D TextureMiniMap;
        Texture2D TextureDot;
        Texture2D TextureCone;

        Vector2 PositionDot;
        Rectangle ZoneAffichageDot;

        Vector2 PositionCone;

        Vector2 Scale;

        public Color Couleur;

        LocalPlayer MyPlayer;

        public MiniMap(Game game, Rectangle zoneAffichage, string nomTexture, Color couleur)
            : base(game)
        {
            ZoneAffichage = zoneAffichage;           
            NomTexture = nomTexture;
            Couleur = couleur;
            Scale = new Vector2(zoneAffichage.Width / Data.DimentionCarte.X, zoneAffichage.Height / Data.DimentionCarte.Z);
        }

        public override void Initialize()
        {
            base.Initialize();

            DrawOrder = 1000;

            MyPlayer = Game.Services.GetService(typeof(LocalPlayer)) as LocalPlayer;

            TextureMiniMap = GestionnaireDeTextures.Find(NomTexture);
            TextureDot = GestionnaireDeTexturesMap.Find("MiniMapPlayerDot");
            TextureCone = GestionnaireDeTexturesMap.Find("MiniMapCone");
        }

        protected override void LoadContent()
        {
            GestionSprites = Game.Services.GetService(typeof(SpriteBatch)) as SpriteBatch;
            GestionnaireDeTextures = Game.Services.GetService(typeof(RessourcesManager<Texture2D>)) as RessourcesManager<Texture2D>;
            GestionnaireDeTexturesMap = new RessourcesManager<Texture2D>(Game, "Textures/MiniMap");
            Ecran = Game.Services.GetService(typeof(Screen)) as Screen;
        }

        float TempsÉcoulé;
        public override void Update(GameTime gameTime)
        {
            if((TempsÉcoulé +=(float)gameTime.ElapsedGameTime.TotalSeconds) > Data.INTERVALLE_MAJ_BASE)
            {
                UpdatePositions();
            }
        }

        void UpdatePositions()
        {
            PositionDot = new Vector2(ZoneAffichage.X + (MyPlayer.Position.Z * Scale.X), ZoneAffichage.Y + (MyPlayer.Position.X * Scale.Y));
            ZoneAffichageDot = new Rectangle((int)PositionDot.X - 2, (int)PositionDot.Y - 2, 4, 4);
        }

        public override void Draw(GameTime gameTime)
        {
                GestionSprites.Begin();
                GestionSprites.Draw(TextureMiniMap, ZoneAffichage, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
                GestionSprites.Draw(TextureDot, ZoneAffichageDot, Color.White);
                GestionSprites.End();
        }
    }
}
