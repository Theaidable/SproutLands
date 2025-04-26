

namespace SproutLands.Classes.World.Tiles
{
    public interface ISoilState
    {
        //Metoder som skal bruges af jordens states
        void SetType(Soil soil);
        void Update();
    }
}
