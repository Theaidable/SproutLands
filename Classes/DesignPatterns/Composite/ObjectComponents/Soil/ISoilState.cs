using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutLands.Classes.DesignPatterns.Composite.ObjectComponents.Soil
{
    public interface ISoilState
    {
        //Metoder som skal bruges af jordens states
        void SetType(Soil soil);
        void Update();
    }
}
