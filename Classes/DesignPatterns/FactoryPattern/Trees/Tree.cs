using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.World;


namespace SproutLands.Classes.DesignPatterns.FactoryPattern.Trees
{
    public class Tree : Component
    {
        public int Health { get;private set; }
        public bool IsChopped {  get; private set; }

        public Tree(GameObject gameObject): base(gameObject)
        {
            Health = 100;
            IsChopped = false;
        }

        public void TakeDamage(int amount)
        {
            if(IsChopped == true)
            {
                return;
            }

            Health -= amount;

            if(Health <= 0)
            {
                IsChopped = true;
                GameWorld.Instance.GameObjects.Remove(GameObject);
            }
        }

        //public void DropRessources() metode skal laves når collider mellem axe og tree virker
    }
}
