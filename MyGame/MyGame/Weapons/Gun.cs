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

namespace MyGame.Weapons
{
    public abstract class Gun : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public const string M9_NAME = "M9";
        public const string FAMAS_NAME = "FAMAS";
        public const string UMP45_NAME = "UMP45";
        public const string M16_NAME = "M16";
        public const string SPECTRE_NAME = "SPECTRE";
        public const string M60_NAME = "M60";

        protected RessourcesManager<Texture2D> GestionnaireDeTextures;
        protected RessourcesManager<SoundEffect> GestionnaireDeSons;

        InputManager GestionInput;

        public Texture2D TextureArme;
        public Texture2D TextureScoped;

        public int Prix;
        public int Dommage;

        public int MunitionsParLoad;
        public int Munitions;
        public int TotalMunitions;
        public virtual int MunitionsParPack { get; set; }

        public Rectangle ZoneAffichageGun;
        public Rectangle ZoneAffichageScoped;

        public bool EstReload;


        public Gun(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            GestionnaireDeTextures = new RessourcesManager<Texture2D>(Game, "Textures/Weapons");
            GestionnaireDeSons = new RessourcesManager<SoundEffect>(Game, "Sounds/Weapons");
            GestionInput = Game.Services.GetService(typeof(InputManager)) as InputManager;

            base.Initialize();
        }

        public virtual void GererTirs(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
