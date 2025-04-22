using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SproutLands.Classes.ComponentPattern.Objects;

namespace SproutLands.Classes.StatePattern.SoilState.SoilStates
{
    public class DirtState : ISoilState
    {
        public void OnEnter(Soil soil)
        {
            // Sæt dirt-sprite
        }

        public void Update(Soil soil) { }
    }
}
