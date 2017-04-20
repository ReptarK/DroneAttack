using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame.GererPath
{
    public class Case
    {
        public List<Case> ListeParents;

        public Vector3 Position;

        public float H; //Distance From Case to Target
        public float G; //Movement Cost
        public float F { get { return H + G; } }

        public Case(Vector3 position, List<Case> listeParents)
        {
            Position = position;

            ListeParents = listeParents;
        }

        public float DistanceCaseTarget(Case caseTarget)
        {
            return Vector3.Distance(Position, caseTarget.Position);
        }

    }
}
