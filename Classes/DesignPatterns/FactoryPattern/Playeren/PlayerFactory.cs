using Microsoft.Xna.Framework;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Composite.Components;

namespace SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren
{
    public class PlayerFactory : Factory
    {
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
    }
}
