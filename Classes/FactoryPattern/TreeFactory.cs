using SproutLands.Classes.ComponentPattern.Colliders;
using SproutLands.Classes.ComponentPattern.Objects;
using SproutLands.Classes.ComponentPattern;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.FactoryPattern
{
    public class TreeFactory : Factory
    {
        private static TreeFactory instance;

        //Oprettelse af Singleton af GameWorld
        public static TreeFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TreeFactory();
                }
                return instance;
            }
        }

        public override GameObject Create(Vector2 position)
        {
            var treeObject = new GameObject();
            treeObject.Transform.Position = position;

            var renderer = treeObject.AddComponent<SpriteRenderer>();

            treeObject.AddComponent<Collider>();
            treeObject.AddComponent<Tree>();

            return treeObject;
        }
    }
}
