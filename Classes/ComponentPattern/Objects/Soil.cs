using SproutLands.Classes.StatePattern.SoilState.SoilStates;
using SproutLands.Classes.StatePattern.SoilState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutLands.Classes.ComponentPattern.Objects
{
    public class Soil : Component
    {
        public ISoilState CurrentState { get; private set; }

        public Soil(GameObject gameObject) : base(gameObject)
        {
            SetState(new DirtState());
        }

        public void SetState(ISoilState newState)
        {
            CurrentState = newState;
            CurrentState.OnEnter(this);
        }

        public override void Update()
        {
            CurrentState.Update(this);
        }
    }
}
