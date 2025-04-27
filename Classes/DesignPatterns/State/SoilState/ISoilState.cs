using SproutLands.Classes.World.Tiles;

namespace SproutLands.Classes.DesignPatterns.State.SoilState
{
    public interface ISoilState
    {
        //Metoder som skal bruges af jordens states
        void Enter(Soil soil);
        void Update(Soil soil);
        void Exit(Soil soil);
    }
}
