using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SproutLands.Classes.ComponentPattern.Colliders;
using SproutLands.Classes.ComponentPattern.Objects;

namespace SproutLands.Classes.Playeren.Tools
{
    public class Axe : Tool
    {
        public Axe(Texture2D icon)
        {
            this.Icon = icon;
        }

        public override void Use(Player player)
        {
            var playerCollider = player.GameObject.GetComponent<Collider>();
            if (playerCollider == null)
            {
                Debug.WriteLine("Player has no collider!");
                return;
            }

            foreach (var gameObject in GameWorld.Instance.GameObjects)
            {
                Tree tree = gameObject.GetComponent<Tree>();
                Collider treeCollider = gameObject.GetComponent<Collider>();

                if (tree == null || treeCollider == null)
                    continue;

                if (!playerCollider.CollisionBox.Intersects(treeCollider.CollisionBox))
                    continue;

                bool hit = playerCollider.PixelPerfectRectangles
                    .Any(pr => treeCollider.PixelPerfectRectangles
                    .Any(tr => pr.Rectangle.Intersects(tr.Rectangle)));

                if (hit)
                {
                    tree.TakeDamage(50);
                    Debug.WriteLine($"Tree chopped!");
                    break;
                }
            }
        }
    }

}
