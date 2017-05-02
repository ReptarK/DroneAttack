using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame.Entités;
using System.Collections.Generic;
using MyGame;
using AtelierXNA;

namespace MyGame
{
    class StairsZNegatif : CubeTexturé
    {

        public StairsZNegatif(Game game, float homothétieInitiale, Vector3 rotationInitiale, Vector3 positionInitiale, string nomTextureCube, Vector3 dimension)
            : base(game, homothétieInitiale, rotationInitiale, positionInitiale, nomTextureCube, dimension)
        {
        }

        public override void GenererBoundingBox()
        {
            ListeBoundingBox.Add(new BoundingBox(PositionInitiale, PositionInitiale + new Vector3(DeltaX, DeltaY - 1, 0)));
            ListeNormales.Add(-Vector3.UnitZ * Caméra1stPerson.FORCE_JOUEUR);

            ListeBoundingBox.Add(new BoundingBox(PositionInitiale + new Vector3(DeltaX, 0, 0), PositionInitiale + new Vector3(DeltaX, DeltaY - 1, DeltaZ )));
            ListeNormales.Add(Vector3.UnitX * Caméra1stPerson.FORCE_JOUEUR);

            ListeBoundingBox.Add(new BoundingBox(PositionInitiale, PositionInitiale + new Vector3(0, DeltaY - 1, DeltaZ)));
            ListeNormales.Add(-Vector3.UnitX * Caméra1stPerson.FORCE_JOUEUR);

            ListeBoundingBox.Add(new BoundingBox(PositionInitiale + new Vector3(0, 0, DeltaZ), PositionInitiale + new Vector3(DeltaX, DeltaY - 1, DeltaZ)));
            ListeNormales.Add(Vector3.UnitY * Data.GRAVITÉ * 2.4f); // STAIR

            ListeBoundingBox.Add(new BoundingBox(PositionInitiale + new Vector3(0, DeltaY, 0), PositionInitiale + new Vector3(DeltaX, DeltaY, DeltaZ)));
            ListeNormales.Add(Vector3.UnitY * Data.GRAVITÉ);
        }
    }
}
