using SproutLands.Classes.ComponentPattern.Colliders;
using SproutLands.Classes.ComponentPattern.Objects;
using SproutLands.Classes.ComponentPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.StatePattern.SoilState.SoilStates;

namespace SproutLands.Classes.FactoryPattern
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
        //Rectangel som skal bruges til at bestemme udsnit af spritesheet
        private Rectangle _sourceRect;

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

        /// <summary>
        /// Override af hovedklassens metode
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public override GameObject Create(Vector2 position)
        {
            return Create(position, TreeType.Tree1);
        }

        /// <summary>
        /// Metode som bruges til at oprette et træ
        /// </summary>
        /// <param name="position"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public GameObject Create(Vector2 position, TreeType type)
        {
            //Opretter objektet og tilføjer components
            var treeObject = new GameObject();
            treeObject.Transform.Position = position;
            var sr = treeObject.AddComponent<SpriteRenderer>();
            treeObject.AddComponent<Collider>();
            treeObject.AddComponent<Tree>();

            //Switch case over de forskellige typer træer med hver deres udsnit i spritesheetet
            switch (type)
            {
                case TreeType.Tree1:
                    _sourceRect = new Rectangle(-60, -3, 130, 130);
                    break;
                case TreeType.Tree2:
                    _sourceRect = new Rectangle(70, -1, 130, 130);
                    break;
                case TreeType.Tree3:
                    _sourceRect = new Rectangle(185, 0, 130, 130);
                    break;
            }

            //Sætter sprite
            sr.SetSprite("Assets/Sprites/Objects/Basic_Grass_Biom_things", _sourceRect);

            return treeObject;
        }
    }
}
