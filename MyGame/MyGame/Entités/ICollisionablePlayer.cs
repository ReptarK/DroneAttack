using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.Entités
{
    public interface ICollisionablePlayer
    {
        BoundingBox BoiteDeCollision { get; }
        bool EstEnCollision(ICollisionableList autreObjet);
    }
}
