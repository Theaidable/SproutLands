using SproutLands.Classes.ComponentPattern.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutLands.Classes.StatePattern.SoilState
{
    public interface ISoilState
    {
        void OnEnter(Soil soil);
        void Update(Soil soil);
    }
}
