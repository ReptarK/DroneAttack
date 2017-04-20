using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    interface ILadder
    {
        BoundingBox BoiteDeCollision { get; set; }
    }
}
