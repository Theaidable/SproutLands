using Microsoft.Xna.Framework;

namespace SproutLands.Classes.ComponentPattern.Objects
{
    public class Structure : Component
    {
        public string Name { get; private set; }
        public int BuildCost { get; private set; }
        public Vector2 Size { get; private set; }

        public Structure(GameObject gameObject, string name, int buildCost, Vector2 size): base(gameObject)
        {
            Name = name;
            BuildCost = buildCost;
            Size = size;
        }
    }
}
