using SproutLands.Classes.Items;
using SproutLands.Classes.World;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Observer;

namespace SproutLands.Classes.UI
{
    public class Hudbar : Component, IObserver
    {
        public bool ShowHudbar { get; set; } = true;
        public List<ItemSlot> HudSlots { get; private set; } = new List<ItemSlot>();

        private int slotCount = 9;
        private int slotSpacing = 20;
        private int slotSize = 64;
        private int totalWidth;

        private Texture2D hudBackgroundTexture;
        private Rectangle hudBackgroundSource;
        private Vector2 hudBackgroundPosition;

        public Hudbar(GameObject gameObject): base(gameObject) { }

        public override void Start()
        {
            int screenW = GameWorld.Instance.Window.ClientBounds.Width;
            int screenH = GameWorld.Instance.Window.ClientBounds.Height;

            //Load texture
            hudBackgroundTexture = GameWorld.Instance.Content.Load<Texture2D>("Assets/UI/Sprite sheet for Basic Pack");
            hudBackgroundSource = new Rectangle(550, 350, 190, 60);
            hudBackgroundPosition = new Vector2(screenW / 2 - 390, screenH - hudBackgroundSource.Height - 100);

            totalWidth = slotCount * slotSize + (slotCount - 1) * slotSpacing;
            Vector2 hudStartPos = new Vector2(screenW / 2 - totalWidth / 2, hudBackgroundPosition.Y + (hudBackgroundSource.Height - slotSize) / 2 + 30);

            for (int i = 0; i < slotCount; i++)
            {
                Vector2 slotPosition = hudStartPos + new Vector2(i * (slotSize + slotSpacing), 0);
                ItemSlot slot = new ItemSlot(slotPosition);
                slot.Start();
                HudSlots.Add(slot);
            }
        }

        public void AddItemToHud(Item item)
        {
            foreach (ItemSlot slot in HudSlots)
            {
                if(slot.StoredItem == null)
                {
                    slot.AddItem(item);
                    break;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float hudBackgroundDesiredWidth = hudBackgroundSource.Width * 4.5f;
            float hudBackgroundDesiredHeight = hudBackgroundSource.Height * 2.3f;
            Rectangle hudBackgroundDestRect = new Rectangle((int)hudBackgroundPosition.X, (int)hudBackgroundPosition.Y, (int)hudBackgroundDesiredWidth, (int)hudBackgroundDesiredHeight);

            if (ShowHudbar == true)
            {
                spriteBatch.Draw(hudBackgroundTexture, hudBackgroundDestRect, hudBackgroundSource, Color.White);

                foreach (var slot in HudSlots)
                {
                    slot.Draw(spriteBatch);
                }
            }
        }

        public void Updated()
        {

        }

    }
}
