using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SproutLands.Classes.ComponentPattern
{
    public class Transform : Component
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; } = Vector2.One;
        public float Rotation { get; set; }

        public Transform(GameObject gameObject) : base(gameObject) { }

        public void Translate(Vector2 translation)
        {
            Position += translation;
        }
    }
}
