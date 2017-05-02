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

namespace MyGame.Entités
{
    public class Entity : Microsoft.Xna.Framework.DrawableGameComponent, ICollisionablePlayer, IDestructible
    {
        BoundingBox boiteDeCollision;
        public BoundingBox BoiteDeCollision
        {
            get { return UpdateBoiteCollision(); }
        }

        BoundingBox UpdateBoiteCollision()
        {
            boiteDeCollision = new BoundingBox(Position + new Vector3(-Caméra1stPerson.PLAYER_WIDTH / 2, 0, -Caméra1stPerson.PLAYER_WIDTH / 2),
                                               Position + new Vector3(Caméra1stPerson.PLAYER_WIDTH / 2, 3, Caméra1stPerson.PLAYER_WIDTH / 2));

            return boiteDeCollision;
        }

        public bool ADétruire { get; set; }

        protected string Nom;
        public Vector3 Position;
        protected int TeamID;
        protected int Health;
        private Game game;

        float TempsÉcoulé;

        public Entity(Game game, string nom, Vector3 position, int health)
            : base(game)
        {
            Nom = nom;
            Position = position;
            Health = health;

            boiteDeCollision = new BoundingBox(Position + new Vector3(-Caméra1stPerson.PLAYER_WIDTH / 2, 0, -Caméra1stPerson.PLAYER_WIDTH / 2),
                                               Position + new Vector3(Caméra1stPerson.PLAYER_WIDTH / 2, 8, Caméra1stPerson.PLAYER_WIDTH / 2));
        }

        public override void Initialize()
        {
            TempsÉcoulé = 0;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public bool EstEnCollision(ICollisionableList autreObjet)
        {
            for (int i = 0; i < autreObjet.ListeBoundingBox.Count; i++)
            {
                if (BoiteDeCollision.Intersects(autreObjet.ListeBoundingBox[i]))
                {
                    if (autreObjet.ListeNormales[i].Y != 0)
                        Caméra1stPerson.EstSol = true;
                    if (!Caméra1stPerson.ListNormaleObjetCollision.Contains(autreObjet.ListeNormales[i]))
                        Caméra1stPerson.ListNormaleObjetCollision.Add(autreObjet.ListeNormales[i]);
                    return true;
                }
            }
            return false;
        }

        
    }
}
