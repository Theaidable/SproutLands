using Microsoft.Xna.Framework;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Composite.Components;

namespace SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren
{
    public class PlayerFactory : Factory
    {
        //Oprettelse af Singleton for PlayerFactory
        private static PlayerFactory instance;
        public static PlayerFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerFactory();
                }
                return instance;
            }
        }

        private PlayerFactory() { }

        public override GameObject Create(Vector2 position)
        {
            var playerObject = new GameObject();
            var spriteRenderer = playerObject.AddComponent<SpriteRenderer>();
            var animator = playerObject.AddComponent<Animator>();
            var player = playerObject.AddComponent<Player>();

            playerObject.Transform.Position = position;

            return playerObject;
        }
    }
}
