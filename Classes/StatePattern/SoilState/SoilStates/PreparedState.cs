using SproutLands.Classes.ComponentPattern;
using SproutLands.Classes.ComponentPattern.Objects;
using Microsoft.Xna.Framework;

namespace SproutLands.Classes.StatePattern.SoilState.SoilStates
{
    /// <summary>
    /// Enum til at bestemme hvilken type prepared jord der skal bruges
    /// </summary>
    public enum PreparedType
    {
        Prepared1,
        Prepared2
    }

    public class PreparedState : ISoilState
    {
        //Fields som bruges til at oprette jordens prepared state
        private readonly PreparedType _variant;
        private Rectangle _sourceRect;

        /// <summary>
        /// Opretter dens state hvor man bestemmer hvilken preparedtype der skal bruges
        /// </summary>
        /// <param name="variant"></param>
        public PreparedState(PreparedType variant)
        {
            _variant = variant;
        }

        /// <summary>
        /// Bestemmer sprite efter hvilken type der vælges
        /// </summary>
        /// <param name="soil"></param>
        public void OnEnter(Soil soil)
        {
            var sr = soil.GameObject.GetComponent<SpriteRenderer>();

            switch (_variant)
            {
                case PreparedType.Prepared1:
                    _sourceRect = new Rectangle(0, 250, 64, 64);
                    break;
                case PreparedType.Prepared2:
                    _sourceRect = new Rectangle(32, 250, 64, 64);
                    break;
            }

            sr.SetSprite("Assets/Sprites/Tilesets/Tilled_Dirt_Wide_v2", _sourceRect);
        }

        /// <summary>
        /// Update af jorden
        /// </summary>
        /// <param name="soil"></param>
        public void Update(Soil soil) { }
    }

}
