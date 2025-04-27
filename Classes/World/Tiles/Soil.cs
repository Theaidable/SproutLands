using SproutLands.Classes.DesignPatterns.Composite;
using SproutLands.Classes.DesignPatterns.State.SoilState;

namespace SproutLands.Classes.World.Tiles
{
    public class Soil : Component
    {
        //Property til at tilgå jords nuværende state
        public ISoilState CurrentState { get; private set; }

        public Soil(GameObject gameObject) : base(gameObject) { }

        /// <summary>
        /// Metode til at sætte jordens tilstand
        /// </summary>
        /// <param name="newState"></param>
        public void SetState(ISoilState newState)
        {
            CurrentState = newState;
            CurrentState.Enter(this);
        }

        /// <summary>
        /// Metode til at opdatere jordens tilstand
        /// </summary>
        public override void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.Update(this);
            }
        }
    }
}
