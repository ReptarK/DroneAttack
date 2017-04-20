using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    public abstract class LoadMap : GameComponent, ILoadMap
    {
        public LoadMap(Game game)
            :base(game)
        {
        }
        public List<IPack> ListeDePack { get; set; }
        public virtual void LoadCarte() { }
        public virtual void InitialiserMurs() { }
        public virtual void InitialiserCrates() { }
        public virtual void InitialiserCadresGuns() { }
        public virtual void InitialiserPlayers() { }
        public virtual void InitialiserComposants() { }

        public virtual void UnloadCarte() { }
    }
}
