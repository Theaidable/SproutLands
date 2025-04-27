using SproutLands.Classes.DesignPatterns.Composite.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using SproutLands.Classes.World.Tiles;
using SproutLands.Classes.DesignPatterns.State.SoilState;

namespace SproutLands.Classes.DesignPatterns.State.SoilState.SoilStates
{
    /// <summary>
    /// Enum til at bestemme hvilken type normal jord der skal bruges
    /// </summary>
    public enum DirtType
    {
        Dirt1,
        Dirt2,
        Dirt3,
        Dirt4,
        Dirt5,
        Dirt6,
        Dirt7,
        Dirt8,
        Dirt9,
        Dirt10
    }
    public class DirtState : ISoilState
    {
        private DirtType type;
        private Rectangle sourceRectangle;
        private static readonly Dictionary<DirtType, Rectangle> dirtRectangles = new Dictionary<DirtType, Rectangle>()
        {
            { DirtType.Dirt1, new Rectangle(40, 40, 64, 64) },
            { DirtType.Dirt2, new Rectangle(0, 0, 64, 64) },
            { DirtType.Dirt3, new Rectangle(0, 80, 64, 64) },
            { DirtType.Dirt4, new Rectangle(80, 80, 64, 64) },
            { DirtType.Dirt5, new Rectangle(80, 0, 64, 64) },
            { DirtType.Dirt6, new Rectangle(0, 40, 64, 64) },
            { DirtType.Dirt7, new Rectangle(40, 80, 64, 64) },
            { DirtType.Dirt8, new Rectangle(80, 40, 64, 64) },
            { DirtType.Dirt9, new Rectangle(40, 0, 64, 64) },
            { DirtType.Dirt10, new Rectangle(0, 300, 64, 64) }
        };

        public DirtState(DirtType type)
        {
            this.type = type;
        }

        public void Enter(Soil soil)
        {

            sourceRectangle = dirtRectangles[type];

            var spriteRenderer = soil.GameObject.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                spriteRenderer.SetSprite("Assets/Sprites/Tilesets/Grass", sourceRectangle);
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
