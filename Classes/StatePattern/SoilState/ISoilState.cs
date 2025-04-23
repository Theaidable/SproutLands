using SproutLands.Classes.ComponentPattern.Objects;

namespace SproutLands.Classes.StatePattern.SoilState
{
    public interface ISoilState
    {
        //Metoder som skal bruges af jordens states
        void OnEnter(Soil soil);
        void Update(Soil soil);
    }
}
