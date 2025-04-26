
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.Items;
using SproutLands.Classes.World;

namespace SproutLands.Classes.UI
{
    public class ItemSlot
    {
        public Vector2 Position { get; private set; }
        public Item StoredItem { get; private set; }

        public Texture2D SlotTexture { get; private set; }
        public Rectangle SlotSource { get; private set; }

        public ItemSlot(Vector2 position)
        {
            Position = position;
        }

        public void Start()
        {
            SlotTexture = GameWorld.Instance.Content.Load<Texture2D>("Assets/UI/Sprite sheet for Basic Pack");
            SlotSource = new Rectangle(480, 200, 90, 90);
        }

        public void AddItem(Item item)
        {
            StoredItem = item;
        }

        public void RemoveItem()
        {
            StoredItem = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SlotTexture, new Rectangle((int)Position.X, (int)Position.Y, SlotSource.Width, SlotSource.Height), SlotSource, Color.White);

            if (StoredItem != null)
            {
                spriteBatch.Draw(StoredItem.Icon, new Rectangle((int)Position.X, (int)Position.Y, SlotSource.Width, SlotSource.Height), Color.White);
            }
        }
    }
}
