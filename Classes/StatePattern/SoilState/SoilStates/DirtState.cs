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
        private readonly DirtType _variant;
        private Rectangle _sourceRect;

        public DirtState(DirtType variant)
        {
            _variant = variant;
        }

        public void OnEnter(Soil soil)
        {
            switch (_variant)
            {
                case DirtType.Dirt1:
                    _sourceRect = new Rectangle(40, 40, 64, 64);
                    break;

                case DirtType.Dirt2:
                    _sourceRect = new Rectangle(0, 0, 64, 64);
                    break;

                case DirtType.Dirt3:
                    _sourceRect = new Rectangle(0, 80, 64, 64);
                    break;

                case DirtType.Dirt4:
                    _sourceRect = new Rectangle(80, 80, 64, 64);
                    break;

                case DirtType.Dirt5:
                    _sourceRect = new Rectangle(80, 2, 64, 64);
                    break;

                case DirtType.Dirt6:
                    _sourceRect = new Rectangle(0, 40, 64, 64);
                    break;

                case DirtType.Dirt7:
                    _sourceRect = new Rectangle(40, 80, 64, 64);
                    break;

                case DirtType.Dirt8:
                    _sourceRect = new Rectangle(80, 40, 64, 64);
                    break;

                case DirtType.Dirt9:
                    _sourceRect = new Rectangle(40, 0, 64, 64);
                    break;
                case DirtType.Dirt10:
                    _sourceRect = new Rectangle(0, 300, 64, 64);
                    break;
            }

            var sr = soil.GameObject.GetComponent<SpriteRenderer>();
            sr.SetSprite("Assets/Sprites/Tilesets/Grass", _sourceRect);
        }

        public void Update(Soil soil) { }
    }
}
