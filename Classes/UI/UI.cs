using SproutLands.Classes.ComponentPattern.Items;
using SproutLands.Classes.ObserverPattern;
using SproutLands.Classes.Playeren;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.ComponentPattern;
using Microsoft.Xna.Framework.Content;


namespace SproutLands.Classes.UI
{
    public class UI : Component, IObserver
    {
        private List<ItemSlot> hudSlots = new List<ItemSlot>();
        private Texture2D slotTexture;
        private Rectangle slotSource;
        private Texture2D panelTexture;
        private Rectangle panelSource;
        private Vector2 panelPosition;
        private Player player;

        public UI(GameObject gameObject, Player player) : base(gameObject)
        {
            this.player = player;
            player.Attach(this);
        }

        public override void Start()
        {
            // Load texture
            slotTexture = GameWorld.Instance.Content.Load<Texture2D>("Assets/UI/Sprite Sheets/Sprite sheet for Basic Pack");
            panelTexture = slotTexture;

            slotSource = new Rectangle(480, 200, 80, 80);
            panelSource = new Rectangle(550, 350, 190, 60);

            int screenW = GameWorld.Instance.Window.ClientBounds.Width;
            int screenH = GameWorld.Instance.Window.ClientBounds.Height;

            panelPosition = new Vector2(screenW / 2 - panelSource.Width / 2, screenH - panelSource.Height - 150);

            //Beregn startposition: centreret i bunden
            int slotCount = 9;
            int spacing = 10;
            int slotSize = 64;
            int totalWidth = slotCount * slotSize + (slotCount - 1) * spacing;
            Vector2 startPos = new Vector2(screenW / 2 - totalWidth / 2,panelPosition.Y + (panelSource.Height - slotSize) / 2);

            for (int i = 0; i < slotCount; i++)
            {
                Vector2 pos = startPos + new Vector2(i * (slotSize + spacing), 0);
                var slot = new ItemSlot(slotTexture, slotSource, pos);
                hudSlots.Add(slot);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int desiredWidth = panelSource.Width * 6;
            float desiredHeight = panelSource.Height * 2.5f;

            Rectangle destRect = new Rectangle((int)panelPosition.X, (int)panelPosition.Y, desiredWidth, (int)desiredHeight);

            spriteBatch.Draw(panelTexture, destRect, panelSource, Color.White);

            /*
            foreach (var slot in hudSlots)
            {
                slot.Draw(spriteBatch);
            }
            */
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
    }
}
