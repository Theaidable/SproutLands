using SproutLands.Classes.DesignPatterns.FactoryPattern.Crop;
using SproutLands.Classes.World;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.DesignPatterns.State.CropState.CropStates
{
    public class GrowingState : ICropState
    {
        private float growTimer;

        public void Enter(Crop crop)
        {
            crop.SetSprite("Assets/ObjectSprites/Crop/Growing_000");
            growTimer = 5f;
        }

        public void Update(Crop crop)
        {
            growTimer -= GameWorld.Instance.DeltaTime;

            if(growTimer <= 2.5f && growTimer > 0)
            {
                crop.SetSprite("Assets/ObjectSprites/Crop/Growing_001");
            }

            if(growTimer <= 0)
            {
                crop.SetState(new HarvestableState());
            }
        }

        public void Exit(Crop crop) { }
    }
}
