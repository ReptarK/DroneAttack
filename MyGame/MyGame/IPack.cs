﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    public interface IPack
    {
        bool EstDétruit { get; set; }
        BoundingBox BoiteDeCollision { get; }
    }
}
