using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.Player
{
    public class Player
    {
        private Vector2 _position;

        public void Move(Vector2 direction)
        {
            _position += direction;
        }
    }
}
