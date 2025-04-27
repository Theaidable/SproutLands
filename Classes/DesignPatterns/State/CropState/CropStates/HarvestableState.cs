using Microsoft.Xna.Framework;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Crop;

namespace SproutLands.Classes.DesignPatterns.State.CropState.CropStates
{
    public class HarvestableState : ICropState
    {
        public void Enter(Crop crop)
        {
            crop.SetSprite("Assets/ObjectSprites/Crop/Harvest_000");
        }

        public void Update(Crop crop) { }

        public void Exit(Crop crop) { }
    }
}
