using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SproutLands.Classes.ComponentPattern.Items;

namespace SproutLands.Classes.Playeren.Tools
{
    public abstract class Tool : Item
    {
        public abstract void Use(Player player);
    }

}
