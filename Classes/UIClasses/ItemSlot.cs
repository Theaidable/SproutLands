using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.ComponentPattern.Items;

namespace SproutLands.Classes.UIClasses
{
    public class ItemSlot
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle SourceRect { get; set; }
        public Item Item { get; set; }

        public ItemSlot(Texture2D texture, Rectangle sourceRect, Vector2 position)
        {
            Texture = texture;
            SourceRect = sourceRect;
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRect, Color.White);

            if (Item != null && Item.Icon != null)
            {
                spriteBatch.Draw(Item.Icon, Position + new Vector2(4, 4), Color.White);
            }
        }
    }
}
