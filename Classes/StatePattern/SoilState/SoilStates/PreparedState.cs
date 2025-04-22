using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ComponentPattern.Objects;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.StatePattern.SoilState.SoilStates
{
    public class PreparedState : ISoilState
    {
        public void OnEnter(Soil soil)
        {
            var sr = soil.GameObject.GetComponent<SpriteRenderer>();
            Rectangle grassSourceRect = new Rectangle(5, 100, 32, 32);
            sr.SetSprite("Assets/Sprites/Tilesets/Tilled_Dirt", grassSourceRect);
        }

        public void Update(Soil soil) { }
    }
}
