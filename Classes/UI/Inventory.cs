using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Observer;
using SproutLands.Classes.Items;
using SproutLands.Classes.World;
using System.Collections.Generic;


namespace SproutLands.Classes.UI
{
    public class Inventory : Component, IObserver
    {
        private bool showInventory = false;
        private Hudbar hudbar;

        private List<ItemSlot> invSlots = new List<ItemSlot>();

        private int slotCount = 9;
        private int slotRows = 5;
        private int slotSpacing = 20;
        private int slotSize = 64;
        private int totalWidth;

        private Texture2D invBackgroundTexture;
        private Rectangle invBackgroundSource;
        private Vector2 invBackgroundPosition;

        int screenW = GameWorld.Instance.Window.ClientBounds.Width;
        int screenH = GameWorld.Instance.Window.ClientBounds.Height;

        public Inventory(GameObject gameObject) : base(gameObject) { }

        public override void Start()
        {
            hudbar = GameObject.GetComponent<Hudbar>();
            invBackgroundTexture = GameWorld.Instance.Content.Load<Texture2D>("Assets/UI/Setting menu");
            invBackgroundSource = new Rectangle(125, 0, 120, 140);
            invBackgroundPosition = new Vector2(screenW / 2 - 515, screenH / 4 - 100);

            totalWidth = slotCount * slotSize + (slotCount - 1) * slotSpacing;
            Vector2 invStartPosition = new Vector2(screenW / 2 - totalWidth / 2, invBackgroundPosition.Y + (invBackgroundSource.Height - slotSize) / 2 + 50);

            for (int y = 0; y < slotRows; y++)
            {
                for (int x = 0; x < slotCount; x++)
                {
                    Vector2 slotPosition = invStartPosition + new Vector2(x * (slotSize + slotSpacing), y * (slotSize + slotSpacing));
                    ItemSlot slot = new ItemSlot(slotPosition);
                    slot.Start();
                    invSlots.Add(slot);
                }
            }
        }

        public void AddItemToInventory(Item item)
        {
            foreach (var slot in invSlots)
            {
                if (slot.StoredItem == null)
                {
                    slot.AddItem(item);
                    break;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Desired størrelse af baggrunden for Inventory
            float invBackgroundDesiredWidth = invBackgroundSource.Width * 8f;
            float invBackgroundDesiredHeight = invBackgroundSource.Height * 5f;
            Rectangle invBackgroundDestRect = new Rectangle((int)invBackgroundPosition.X, (int)invBackgroundPosition.Y, (int)invBackgroundDesiredWidth, (int)invBackgroundDesiredHeight);

            if (showInventory == true)
            {
                spriteBatch.Draw(invBackgroundTexture, invBackgroundDestRect, invBackgroundSource, Color.White);

                foreach (var slot in invSlots)
                {
                    slot.Draw(spriteBatch);
                }

                //Tegn HUDs slots i bunden af inventory
                Vector2 hudStartPosInInventory = new Vector2(screenW / 2 - totalWidth / 2, invBackgroundDestRect.Y + invBackgroundDestRect.Height - slotSize - 80f);
                for (int i = 0; i < hudbar.HudSlots.Count; i++)
                {
                    ItemSlot slot = hudbar.HudSlots[i];
                    Vector2 drawPosition = hudStartPosInInventory + new Vector2(i * (slotSize + slotSpacing), 0);
                    spriteBatch.Draw(slot.SlotTexture, drawPosition, slot.SlotSource, Color.White);

                    if (slot.StoredItem != null)
                    {
                        spriteBatch.Draw(slot.StoredItem.Icon, new Rectangle((int)drawPosition.X + 25, (int)drawPosition.Y + 20, 42, 42), Color.White);
                    }
                }
            }
        }

        public void Updated()
        {

        }

        public void ToggleInventory()
        {
            showInventory = !showInventory;
            hudbar.ShowHudbar = !showInventory;
        }
    }
}
