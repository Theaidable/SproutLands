using SproutLands.Classes.Playeren;
using SproutLands.Classes.Playeren.Tools;

using SproutLands.Classes.ComponentPattern.Objects;
using Microsoft.Xna.Framework;
using System.Linq;
using System;
using System.Diagnostics;
using SproutLands.Classes.ComponentPattern.Colliders;

namespace SproutLands.Classes.CommandPattern
{
    public class UseToolCommand : ICommand
    {
        private Player _player;
        private Vector2 _direction;
        private int _damage = 50;

        public UseToolCommand(Player player)
        {
            _player = player;
        }

        public void Execute()
        {
            if (_player.EquippedTool is not Axe)
            {
                Debug.WriteLine("No axe equipped.");
                return;
            }

            if (_direction == new Vector2(0, -1))
            {
                _player.SetState(PlayerState.UseAxeUp);
            }
            else if (_direction == new Vector2(0, 1))
            {
                _player.SetState(PlayerState.UseAxeDown);
            }
            else if (_direction == new Vector2(-1, 0))
            {
                _player.SetState(PlayerState.UseAxeLeft);
            }
            else if (_direction == new Vector2(1, 0))
            {
                _player.SetState(PlayerState.UseAxeRight);
            }

            // 2) Hent spillerens collider
            var playerCollider = _player.GameObject.GetComponent<Collider>();
            if (playerCollider == null)
            {
                Debug.WriteLine("Player has no collider!");
                return;
            }

            var hit = GameWorld.Instance.GameObjects
                .Select(go => new
                {
                    Tree = go.GetComponent<Tree>(),
                    Collider = go.GetComponent<Collider>()
                })
                .Where(x => x.Tree != null && x.Collider != null)
                .FirstOrDefault(x =>
                    x.Collider.CollisionBox.Intersects(playerCollider.CollisionBox)
                    &&
                    x.Collider.PixelPerfectRectangles
                        .Any(rd => rd.Rectangle.Intersects(playerCollider.CollisionBox))
                );

            if (hit != null)
            {
                hit.Tree.TakeDamage(_damage);
                Debug.WriteLine($"Tree chopped! Damage: {_damage}");
            }
            else
            {
                Debug.WriteLine("No tree in collision range.");
            }
        }
    }
}
