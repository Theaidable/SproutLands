using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ComponentPattern.Objects;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.StatePattern.SoilState.SoilStates
{
    public class PreparedState : ISoilState
    {
        public void OnEnter(Soil soil)
        {
            var sr = soil.GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            Rectangle grassSourceRect = new Rectangle(0, 0, 32, 32);
            sr.SetSprite("Assets/Sprites/Tilesets/Grass", grassSourceRect);
        }

        public void Update(Soil soil) { }
    }
}
