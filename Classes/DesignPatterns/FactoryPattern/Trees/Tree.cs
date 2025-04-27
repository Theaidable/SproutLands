using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;
using SproutLands.Classes.Items;
using SproutLands.Classes.UI;
using SproutLands.Classes.World;
using System.Linq;


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
                DropRessources();
                GameWorld.Instance.QueueRemove(GameObject);
            }
        }

        private void DropRessources()
        {
            GameObject playerObject = GameWorld.Instance.GameObjects.FirstOrDefault(go => go.GetComponent<Player>() != null);

            if(playerObject == null)
            {
                return;
            }

            Inventory inventory = playerObject.GetComponent<Inventory>();

            if(inventory == null)
            {
                return;
            }

            Texture2D woodIcon = GameWorld.Instance.Content.Load<Texture2D>("Assets/ItemSprites/BigLog");
            WoodItem wood = new WoodItem(woodIcon);
            inventory.AddItemToInventory(wood);
        }
    }
}
