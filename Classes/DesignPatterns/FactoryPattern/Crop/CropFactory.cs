using Microsoft.Xna.Framework;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Composite.Components;
using SproutLands.Classes.DesignPatterns.State.CropState.CropStates;

namespace SproutLands.Classes.DesignPatterns.FactoryPattern.Crop
{
    public class CropFactory : Factory
    {
        private static CropFactory instance;
        public static CropFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CropFactory();
                }
                return instance;
            }
        }

        private CropFactory() { }

        public override GameObject Create(Vector2 position)
        {
            GameObject cropObject = new GameObject();
            cropObject.Transform.Position = position;

            var spriteRenderer = cropObject.AddComponent<SpriteRenderer>();
            var collider = cropObject.AddComponent<Collider>();
            var crop = cropObject.AddComponent<Crop>();
            spriteRenderer.SetSprite("Assets/ObjectSprites/Crop/Seeded_000");

            return cropObject;
        }
    }
}
