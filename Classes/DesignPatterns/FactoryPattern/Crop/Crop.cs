using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Composite.Components;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;
using SproutLands.Classes.DesignPatterns.State.CropState;
using SproutLands.Classes.DesignPatterns.State.CropState.CropStates;
using SproutLands.Classes.Items;
using SproutLands.Classes.UI;
using SproutLands.Classes.World;
using System;
using System.Linq;

namespace SproutLands.Classes.DesignPatterns.FactoryPattern.Crop
{
    public class Crop : Component
    {
        public ICropState CurrentState { get; private set; }
        private SpriteRenderer spriteRenderer;

        public Crop(GameObject gameObject): base(gameObject) { }

        
        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
            SetState(new SeededState());
        }
        

        public override void Update()
        {
            CurrentState?.Update(this);
        }

        public void SetState(ICropState state)
        {
            CurrentState?.Exit(this);
            CurrentState = state;
            CurrentState?.Enter(this);
        }

        public void SetSprite(string spritePath)
        {
            try
            {
                Texture2D sprite = GameWorld.Instance.Content.Load<Texture2D>(spritePath);
                spriteRenderer.Sprite = sprite;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl under loading af sprite: {spritePath}. Fejl: {ex.Message}");
            }
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
