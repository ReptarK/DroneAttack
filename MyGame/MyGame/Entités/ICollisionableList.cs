using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.Entités
{
    public interface ICollisionableList
    {
        List<BoundingBox> ListeBoundingBox { get; set; }

        List<Vector3> ListeNormales { get; set; }

        void GenererBoundingBox();
    }
}
