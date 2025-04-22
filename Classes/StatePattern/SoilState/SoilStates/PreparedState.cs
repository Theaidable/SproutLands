using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ComponentPattern.Objects;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.StatePattern.SoilState.SoilStates
{
    public enum PreparedType
    {
        Prepared1,
        Prepared2,
        Prepared3,
        Prepared4,
        Prepared5,
        Prepared6,
        Prepared7,
        Prepared8,
        Prepared9
    }

    public class PreparedState : ISoilState
    {
        private readonly PreparedType _variant;
        private Rectangle _sourceRect;

        public PreparedState(PreparedType variant)
        {
            _variant = variant;
        }

        public void OnEnter(Soil soil)
        {
            switch (_variant)
            {
                case PreparedType.Prepared1:
                    _sourceRect = new Rectangle(0, 250, 64, 64);
                    break;
                case PreparedType.Prepared2:
                    _sourceRect = new Rectangle(32, 250, 64, 64);
                    break;
            }

            var sr = soil.GameObject.GetComponent<SpriteRenderer>();
            sr.SetSprite("Assets/Sprites/Tilesets/Tilled_Dirt_Wide_v2", _sourceRect);
        }

        public void Update(Soil soil) { }
    }

}
