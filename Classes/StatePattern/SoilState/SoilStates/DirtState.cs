using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ComponentPattern.Objects;

namespace SproutLands.Classes.StatePattern.SoilState.SoilStates
{
    public class DirtState : ISoilState
    {
        public void OnEnter(Soil soil)
        {
            var sr = soil.GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            Rectangle grassSourceRect = new Rectangle(0, 0, 32, 32);
            sr.SetSprite("Assets/Sprites/Tilesets/Grass",grassSourceRect);
        }

        public void Update(Soil soil) { }
    }
}
