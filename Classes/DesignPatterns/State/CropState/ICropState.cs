using SproutLands.Classes.DesignPatterns.FactoryPattern.Crop;

namespace SproutLands.Classes.DesignPatterns.State.CropState
{
    public interface ICropState
    {
        void Enter(Crop crop);
        void Update(Crop crop);
        void Exit(Crop crop);
    }
}
