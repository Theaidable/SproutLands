using SproutLands.Classes.DesignPatterns.Composite.Components;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using SproutLands.Classes.World.Tiles;
using SproutLands.Classes.DesignPatterns.State.SoilState;

namespace SproutLands.Classes.DesignPatterns.State.SoilState.SoilStates
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
        private PreparedType type;
        private Rectangle sourceRectangle;
        private static readonly Dictionary<PreparedType, Rectangle> preparedRectangles = new Dictionary<PreparedType, Rectangle>()
        {
            { PreparedType.Prepared1, new Rectangle(0, 250, 64, 64) },
            { PreparedType.Prepared2, new Rectangle(32, 250, 64, 64) },
        };


        public PreparedState(PreparedType type)
        {
            this.type = type;
        }

        public void Enter(Soil soil)
        {
            sourceRectangle = preparedRectangles[type];

            var spriteRenderer = soil.GameObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.SetSprite("Assets/Sprites/Tilesets/Tilled_Dirt_Wide_v2", sourceRectangle);
            }
            else
            {
                throw new Exception("SpriteRenderer mangle på soil GameObject");
            }
        }

        public void Update(Soil soil) { }
        public void Exit(Soil soil) { }
    }
}
