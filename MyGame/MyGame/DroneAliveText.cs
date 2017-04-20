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

namespace MyGame
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class DroneAliveText : TexteAvantPlan, Overlay
    {
        GameController GameController;

        int NbDronesRestants;

        public DroneAliveText(Game game, string texte, string nomFont, Vector2 position, Color couleur)
            : base(game, texte, nomFont, position, couleur)
        { }

        public override void Initialize()
        {
            base.Initialize();

            GameController = Game.Services.GetService(typeof(GameController)) as GameController;
            DrawOrder = 10000;
        }

        float TempsÉcoulé = 0;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if ((TempsÉcoulé += (float)gameTime.ElapsedGameTime.TotalSeconds) > 0.2f)
            {
                Texte = "Drones en vie :" + GameController.NbDronesRestants.ToString(); 
            }
        }
    }
}
