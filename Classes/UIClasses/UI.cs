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
        private int slotCount = 9;
        private int slotSpacing = 20;
        private int slotSize = 64;
        private int totalWidth;

        //Hudbar
        private bool showHudbar = true;
        private Rectangle hudSlotSource;
        private Texture2D hudBackgroundTexture;
        private Rectangle hudBackgroundSource;
        private Vector2 hudBackgroundPosition;
        private Vector2 hudStartPos;
        private Vector2 hudSlotPos;

        //Inventory
        private bool showInventory = false;
        private Rectangle inventorySlotSource;
        private Texture2D inventoryBackgroundTexture;
        private Rectangle inventoryBackgroundSource;
        private Vector2 inventoryBackgroundPosition;
        private int inventorySlotRows = 5;
        private Vector2 invStartPos;
        private Vector2 invSlotPos;

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

            //Beregning af den totale bredde af slots
            totalWidth = slotCount * slotSize + (slotCount - 1) * slotSpacing;

            //Beregn postion for slots i hudbaren
            hudStartPos = new Vector2(screenW / 2 - totalWidth / 2, hudBackgroundPosition.Y + (hudBackgroundSource.Height - slotSize) / 2 + 30);
            for (int i = 0; i < slotCount; i++)
            {
                hudSlotPos = hudStartPos + new Vector2(i * (slotSize + slotSpacing), 0);
                var hudSlot = new ItemSlot(slotTexture, hudSlotSource, hudSlotPos);
                hudSlots.Add(hudSlot);
            }

            //Beregn position for slots i inventory
            invStartPos = new Vector2(screenW / 2 - totalWidth / 2, inventoryBackgroundPosition.Y + (inventoryBackgroundSource.Height - slotSize) / 2 + 50);
            for (int y = 0; y < inventorySlotRows; y++)
            {
                for (int x = 0; x < slotCount; x++)
                {
                    invSlotPos = invStartPos + new Vector2(x * (slotSize + slotSpacing), y *(slotSize + slotSpacing));
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
                Vector2 hudStartPosInInventory = new Vector2(screenW / 2 - totalWidth / 2, inventoryBackgroundDestRect.Y + inventoryBackgroundDestRect.Height - slotSize - 80f);
                for (int i = 0; i < slotCount; i++)
                {
                    var slot = hudSlots[i];
                    Vector2 drawPos = hudStartPosInInventory + new Vector2(i * (slotSize + slotSpacing), 0);
                    spriteBatch.Draw(slot.Texture, drawPos, slot.SourceRect, Color.White);
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
