using Microsoft.Xna.Framework;
using System.Collections.Generic;
using SproutLands.Classes.DesignPatterns.Composite;
using System.DirectoryServices;
using SproutLands.Classes.DesignPatterns.Composite.Components;

namespace SproutLands.Classes.DesignPatterns.FactoryPattern.Trees
{
    /// <summary>
    /// Enum som bruges til at bestemme hvilken type træ der skal bruges
    /// </summary>
    public enum TreeType
    {
        Tree1,
        Tree2,
        Tree3
    }

    public class TreeFactory : Factory
    {
        private Rectangle sourceRectangle;
        private static readonly Dictionary<TreeType, Rectangle> treeRectangles = new Dictionary<TreeType, Rectangle>()
        {
            { TreeType.Tree1, new Rectangle(0, 0, 60, 110) },
            { TreeType.Tree2, new Rectangle(70, 0, 110, 130) },
            { TreeType.Tree3, new Rectangle(195, 0, 120, 130) }
        };

        //Oprettelse af Singleton for TreeFactory
        private static TreeFactory instance;
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

        private TreeFactory() { }

        public override GameObject Create(Vector2 position)
        {
            return Create(position,TreeType.Tree1);
        }

        public GameObject Create(Vector2 position, TreeType treeType)
        {
            var treeObject = new GameObject();
            var spriteRenderer = treeObject.AddComponent<SpriteRenderer>();
            treeObject.AddComponent<Collider>();
            treeObject.AddComponent<Tree>();
            treeObject.Transform.Position = position;
            sourceRectangle = treeRectangles[treeType];
            spriteRenderer.SetSprite("Assets/Sprites/Objects/Basic_Grass_Biom_things", sourceRectangle);
            return treeObject;
        }
    }
}
