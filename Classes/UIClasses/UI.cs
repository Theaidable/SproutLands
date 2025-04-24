using SproutLands.Classes.ComponentPattern.Items;
using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ObserverPattern;
using SproutLands.Classes.Playeren;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;


namespace SproutLands.Classes.UIClasses
{
    public class UI : Component, IObserver
    {
        //Item lists
        private List<ItemSlot> hudSlots = new List<ItemSlot>();
        private List<ItemSlot> invSlots = new List<ItemSlot>();

        //Slot Texture
        private Texture2D slotTexture;

        //Hudbar
        private bool showHudbar = true;
        private Rectangle hudSlotSource;
        private Texture2D hudBackgroundTexture;
        private Rectangle hudBackgroundSource;
        private Vector2 hudBackgroundPosition;
        private int hudSlotCount;
        private int hudSlotSpacing;
        private int hudSlotSize;
        private int hudTotalWidth;
        private Vector2 hudStartPos;

        //Inventory
        private bool showInventory = false;
        private Rectangle inventorySlotSource;
        private Texture2D inventoryBackgroundTexture;
        private Rectangle inventoryBackgroundSource;
        private Vector2 inventoryBackgroundPosition;
        private int inventorySlotCount;
        private int inventorySlotRows;
        private int inventorySlotSpacing;
        private int inventorySlotSize;
        private int inventoryTotalWidth;
        private Vector2 invStartPos;

        int screenW = GameWorld.Instance.Window.ClientBounds.Width;
        int screenH = GameWorld.Instance.Window.ClientBounds.Height;

        //Reference to player
        private Player player;

        public UI(GameObject gameObject, Player player) : base(gameObject)
        {
            this.player = player;
            player.Attach(this);
        }

        public override void Start()
        {
            // Load textures
            slotTexture = GameWorld.Instance.Content.Load<Texture2D>("Assets/UI/Sprite Sheets/Sprite sheet for Basic Pack");
            hudBackgroundTexture = GameWorld.Instance.Content.Load<Texture2D>("Assets/UI/Sprite Sheets/Sprite sheet for Basic Pack");
            inventoryBackgroundTexture = GameWorld.Instance.Content.Load<Texture2D>("Assets/UI/Sprite Sheets/Setting menu");

            //Hudbar baggrund + slots rectangler som cuttes fra spritesheetet
            hudSlotSource = new Rectangle(480, 200, 90, 90);
            hudBackgroundSource = new Rectangle(550, 350, 190, 60);

            //Inventory baggrund + slots rectangler som cutter fra spritesheetet
            inventorySlotSource = new Rectangle(480, 200, 90, 90);
            inventoryBackgroundSource = new Rectangle(125, 0, 120, 140);

            hudBackgroundPosition = new Vector2(screenW / 2 - 390, screenH - hudBackgroundSource.Height - 90);
            inventoryBackgroundPosition = new Vector2(screenW / 2 - 515, screenH / 4 - 100);

            //Beregn postion for slots i hudbaren
            hudSlotCount = 9;
            hudSlotSpacing = 20;
            hudSlotSize = 64;
            hudTotalWidth = hudSlotCount * hudSlotSize + (hudSlotCount - 1) * hudSlotSpacing;
            hudStartPos = new Vector2(screenW / 2 - hudTotalWidth / 2, hudBackgroundPosition.Y + (hudBackgroundSource.Height - hudSlotSize) / 2 + 30);

            for (int i = 0; i < hudSlotCount; i++)
            {
                Vector2 hudSlotPos = hudStartPos + new Vector2(i * (hudSlotSize + hudSlotSpacing), 0);
                var hudSlot = new ItemSlot(slotTexture, hudSlotSource, hudSlotPos);
                hudSlots.Add(hudSlot);
            }

            //Beregn position for slots i inventory
            inventorySlotCount = 9;
            inventorySlotRows = 5;
            inventorySlotSpacing = 20;
            inventorySlotSize = 64;
            inventoryTotalWidth = inventorySlotCount * inventorySlotSize + (inventorySlotCount - 1) * inventorySlotSpacing;
            invStartPos = new Vector2(screenW / 2 - inventoryTotalWidth / 2, inventoryBackgroundPosition.Y + (inventoryBackgroundSource.Height - inventorySlotSize) / 2 + 50);
            
            for (int y = 0; y < inventorySlotRows; y++)
            {
                for (int x = 0; x < inventorySlotCount; x++)
                {
                    Vector2 invSlotPos = invStartPos + new Vector2(x * (inventorySlotSize + inventorySlotSpacing), y *(inventorySlotSize + inventorySlotSpacing));
                    var invSlot = new ItemSlot(slotTexture,inventorySlotSource, invSlotPos);
                    invSlots.Add(invSlot);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Desired størrelse af baggrunden for Hud
            float hudBackgroundDesiredWidth = hudBackgroundSource.Width * 4.5f;
            float hudBackgroundDesiredHeight = hudBackgroundSource.Height * 2.3f;
            Rectangle hudBackgroundDestRect = new Rectangle((int)hudBackgroundPosition.X, (int)hudBackgroundPosition.Y, (int)hudBackgroundDesiredWidth, (int)hudBackgroundDesiredHeight);

            //Desired størrelse af baggrunden for Inventory
            float inventoryBackgroundDesiredWidth = inventoryBackgroundSource.Width * 8f;
            float inventoryBackgroundDesiredHeight = inventoryBackgroundSource.Height * 5f;
            Rectangle inventoryBackgroundDestRect = new Rectangle((int)inventoryBackgroundPosition.X, (int)inventoryBackgroundPosition.Y, (int)inventoryBackgroundDesiredWidth, (int)inventoryBackgroundDesiredHeight);

            if (showHudbar == true)
            {
                spriteBatch.Draw(hudBackgroundTexture, hudBackgroundDestRect, hudBackgroundSource, Color.White);

                foreach (var slot in hudSlots)
                {
                    slot.Draw(spriteBatch);
                }
            }

            if (showInventory == true)
            {
                spriteBatch.Draw(inventoryBackgroundTexture, inventoryBackgroundDestRect, inventoryBackgroundSource, Color.White);

                foreach (var slot in invSlots)
                {
                    slot.Draw(spriteBatch);
                }

                //Tegn HUDs slots i bunden af inventory
                float hudSlotsX = screenW / 2 - hudTotalWidth / 2;
                float hudSlotsY = inventoryBackgroundDestRect.Y + inventoryBackgroundDestRect.Height - hudSlotSize - 80f;
                Vector2 hudStartPosInInventory = new Vector2(hudSlotsX, hudSlotsY);

                for (int i = 0; i < hudSlots.Count; i++)
                {
                    var slot = hudSlots[i];
                    Vector2 drawPos = hudStartPosInInventory + new Vector2(i * (hudSlotSize + hudSlotSpacing), 0);
                    spriteBatch.Draw(slot.Texture, drawPos, slot.SourceRect, Color.White);

                    if (slot.Item != null && slot.Item.Icon != null)
                    {
                        spriteBatch.Draw(slot.Item.Icon, drawPos, Color.White);
                    }
                }
            }
        }

        public void Update(ISubject subject)
        {
            if(subject is Player player)
            {
                RefreshHudbar(player.Inventory.Items);
            }
        }

        private void RefreshHudbar(List<Item> items)
        {
            for (int i = 0; i < hudSlots.Count; i++)
            {
                hudSlots[i].Item = (i < items.Count) ? items[i] : null;
            }
        }

        public void ToggleInventory()
        {
            showInventory = !showInventory;
            showHudbar = !showInventory;
        }
    }
}
