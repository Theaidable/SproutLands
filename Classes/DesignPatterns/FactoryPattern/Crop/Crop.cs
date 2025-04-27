using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Composite.Components;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;
using SproutLands.Classes.DesignPatterns.State.CropState;
using SproutLands.Classes.DesignPatterns.State.CropState.CropStates;
using SproutLands.Classes.Items;
using SproutLands.Classes.UI;
using SproutLands.Classes.World;
using System.Linq;

namespace SproutLands.Classes.DesignPatterns.FactoryPattern.Crop
{
    public class Crop : Component
    {
        private ICropState currentState;
        private SpriteRenderer spriteRenderer;

        public Crop(GameObject gameObject): base(gameObject) { }

        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
            SetState(new SeededState());
        }

        public override void Update()
        {
            currentState?.Update(this);
        }

        public void SetState(ICropState state)
        {
            currentState?.Exit(this);
            currentState = state;
            currentState?.Enter(this);
        }

        public void SetSprite(string spritePath, Rectangle sourceRectangle)
        {
            spriteRenderer.SetSprite(spritePath, sourceRectangle);
        }

        public void Harvest()
        {
            GameObject playerObject = GameWorld.Instance.GameObjects.FirstOrDefault(go => go.GetComponent<Player>() != null);

            if (playerObject == null)
            {
                return;
            }

            Inventory inventory = playerObject.GetComponent<Inventory>();

            if (inventory == null)
            {
                return;
            }

            Texture2D cropIcon = GameWorld.Instance.Content.Load<Texture2D>("Assets/ItemSprites/Crop");
            CropItem crop = new CropItem(cropIcon);
            inventory.AddItemToInventory(crop);

            GameWorld.Instance.QueueRemove(GameObject);
        }

    }
}
