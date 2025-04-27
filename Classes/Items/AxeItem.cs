using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.Composite.Components;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Playeren;
using SproutLands.Classes.DesignPatterns.FactoryPattern.Trees;
using SproutLands.Classes.World;
using System.Diagnostics;
using System.Linq;

namespace SproutLands.Classes.Items
{
    public class AxeItem : Item
    {
        private int damage;

        public AxeItem(Texture2D icon)
        {
            Icon = icon;
            DisplayName = "Axe";
            damage = 50;
        }

        public override void Use(Player player)
        {
            player.PlayUseAxeAnimation();

            Collider playerCollider = player.GameObject.GetComponent<Collider>();

            if (playerCollider == null)
            {
                Debug.WriteLine("Player has no collider");
            }

            foreach (GameObject gameObject in GameWorld.Instance.GameObjects)
            {
                Tree tree = gameObject.GetComponent<Tree>();
                Collider treeCollider = gameObject.GetComponent<Collider>();
                
                if(tree == null || treeCollider == null)
                {
                    continue;
                }

                if (playerCollider.CollisionBox.Intersects(treeCollider.CollisionBox) == false)
                {
                    continue;
                }

                bool hit = playerCollider.PixelPerfectRectangles.Any(pr => treeCollider.PixelPerfectRectangles.Any(tr => pr.Rectangle.Intersects(tr.Rectangle)));

                if(hit == true)
                {
                    tree.TakeDamage(damage);
                    Debug.WriteLine($"Tree has taken {damage} damage");
                    break;
                }
            }
        }
    }
}
