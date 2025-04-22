using SproutLands.Classes.ComponentPattern.Colliders;
using SproutLands.Classes.ComponentPattern.Objects;
using SproutLands.Classes.ComponentPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.StatePattern.SoilState.SoilStates;

namespace SproutLands.Classes.FactoryPattern
{
    public enum TreeType
    {
        Tree1,
        Tree2,
        Tree3
    }

    public class TreeFactory : Factory
    {
        private Rectangle _sourceRect;

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
            return Create(position, TreeType.Tree1);
        }

        public GameObject Create(Vector2 position, TreeType type)
        {
            var treeObject = new GameObject();
            treeObject.Transform.Position = position;
            var sr = treeObject.AddComponent<SpriteRenderer>();
            treeObject.AddComponent<Collider>();
            treeObject.AddComponent<Tree>();

            switch (type)
            {
                case TreeType.Tree1:
                    _sourceRect = new Rectangle(0, 250, 64, 64);
                    break;
                case TreeType.Tree2:
                    _sourceRect = new Rectangle(32, 250, 64, 64);
                    break;
                case TreeType.Tree3:
                    _sourceRect = new Rectangle(32, 250, 64, 64);
                    break;
            }

            sr.SetSprite("Assets/Sprites/Objects/Basic_Grass_Biom_things", _sourceRect);

            return treeObject;
        }
    }
}
