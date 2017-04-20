using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    interface ILoadMap
    {
        List<IPack> ListeDePack { get; set; }
    }
}
