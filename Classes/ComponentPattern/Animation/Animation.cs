using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SproutLands.Classes.ComponentPattern.Animation
{
    public class Animation
    {
        public float FPS { get; private set; }
        public string Name { get; private set; }
        public Texture2D[] Sprites { get; private set; }
        public Animation(string name, Texture2D[] sprites, float fps)
        {
            this.Name = name;
            this.Sprites = sprites;
            this.FPS = fps;
        }
    }
}
