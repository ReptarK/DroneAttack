using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MyGame.Composants_Menu;

namespace AtelierXNA
{
    class Texte : DrawableGameComponent, IBoxMenu
    {
        //FIELDS

        SpriteBatch SpriteBatch { get; set; }

        SpriteFont SpriteFont { get; set; }

        public string Message { get; set; }

        public Vector2 Postition { get; set; }

        public Color Color { get; set; }

        public float Rotation { get; set; }

        public Vector2 Origin { get; set; }

        public float Scale { get; set; }

        public SpriteEffects SpriteEffect { get; set; }

        public float LayerDepth { get; set; }

        //INTERFACE
        public bool bEstClické { get; set; }

        public Action<Game> OnClick { get; set; }


        //CONSTRUCTOR
        public Texte(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, string message, Vector2 position, Color color, Action<Game> onClick)
            :base(game)
        {
            SpriteBatch = spriteBatch;
            SpriteFont = spriteFont;
            Message = message;
            Postition = position;
            Color = color;
            Rotation = 0f;
            Origin = new Vector2(0, 0);
            Scale = 1f;
            SpriteEffect = SpriteEffects.None;
            LayerDepth = 0f;
            OnClick = onClick;
        }

        public Texte(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, string message, Vector2 position, Color color,
                     float rotation, Vector2 origin, float scale, SpriteEffects spriteEffect,
                     float layerDepth)
            :base(game)
        {
            SpriteBatch = spriteBatch;
            SpriteFont = spriteFont;
            Message = message;
            Postition = position;
            Color = color;
            Rotation = rotation;
            Origin = origin;
            Scale = scale;
            SpriteEffect = spriteEffect;
            LayerDepth = layerDepth;
        }

        //METHODS

        //UPDATE & DRAW
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.DrawString(SpriteFont, Message, Postition, Color, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
            SpriteBatch.End();
        }
    }
}
