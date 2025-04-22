using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SproutLands.Classes.ComponentPattern.Objects
{
    public class Tree
    {
        public int Health { get; private set; }
        public bool IsChopped { get; private set; }
        public string ResourceType { get; private set; }
        public int ResourceAmount { get; private set; }
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _sourceRectangle;

        public Tree(Texture2D texture, Vector2 position, Rectangle sourceRectangle, int health = 100, string resourceType = "Wood", int resourceAmount = 5)
        {
            _texture = texture;
            _position = position;
            _sourceRectangle = sourceRectangle;
            Health = health;
            ResourceType = resourceType;
            ResourceAmount = resourceAmount;
            IsChopped = false;
        }

        public void Interact()
        {
            if (IsChopped) return;

            Health -= 10; // Example: Reduce health by 10 per interaction
            if (Health <= 0)
            {
                IsChopped = true;
                DropResources();
            }
        }

        public void SetTexture(Texture2D texture)
        {
            _texture = texture;
        }

        public bool ContainsPoint(Vector2 point)
        {
            var treeRectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
            return treeRectangle.Contains(point);
        }

        private void DropResources()
        {
            // Logic to spawn resources in the game world
            // Example: Notify the game world to add resources
            System.Console.WriteLine($"{ResourceAmount} {ResourceType} dropped!");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsChopped)
            {
                spriteBatch.Draw(_texture, _position, _sourceRectangle, Color.White);
            }
        }
    }
}
