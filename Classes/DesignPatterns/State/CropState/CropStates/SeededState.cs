using SproutLands.Classes.DesignPatterns.FactoryPattern.Crop;
using SproutLands.Classes.World;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.DesignPatterns.State.CropState.CropStates
{
    public class SeededState : ICropState
    {
        private float growTimer;

        public void Enter(Crop crop)
        {
            crop.SetSprite("Assets/ObjectSprites/Crop/Seeded_000");
            growTimer = 3.0f;
        }

        public void Update(Crop crop)
        {
            growTimer -= GameWorld.Instance.DeltaTime;

            if (growTimer <= 0)
            {
                crop.SetState(new GrowingState());
            }
        }

        public void Exit(Crop crop) { }
    }
}
