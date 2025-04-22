using SproutLands.Classes.ComponentPattern.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutLands.Classes.StatePattern.SoilState.SoilStates
{
    public class PreparedState : ISoilState
    {
        public void OnEnter(Soil soil)
        {
            // Sæt prepared-sprite
        }

        public void Update(Soil soil) { }
    }
}
