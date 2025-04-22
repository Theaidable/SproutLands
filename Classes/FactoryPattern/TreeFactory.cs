using SproutLands.Classes.ComponentPattern.Colliders;
using SproutLands.Classes.ComponentPattern.Objects;
using SproutLands.Classes.ComponentPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            var sr = treeObject.AddComponent<SpriteRenderer>();
            Rectangle treeSourceRect = new Rectangle(0, 0, 32, 32);
            treeObject.AddComponent<Collider>();
            treeObject.AddComponent<Tree>();

            sr.SetSprite("Assets/Sprites/Objects/Basic_Grass_Biom_things", treeSourceRect);

            return treeObject;
        }
    }
}
