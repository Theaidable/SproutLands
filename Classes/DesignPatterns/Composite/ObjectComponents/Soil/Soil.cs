
namespace SproutLands.Classes.DesignPatterns.Composite.ObjectComponents.Soil
{
    public class Soil : Component
    {
        //Property til at tilgå jords nuværende state
        public ISoilState CurrentState { get; private set; }

        /// <summary>
        /// Tom Constructor som skal være der, da soil er en component
        /// </summary>
        /// <param name="gameObject"></param>
        public Soil(GameObject gameObject) : base(gameObject)
        {
        }

        /// <summary>
        /// Metode til at sætte jordens tilstand
        /// </summary>
        /// <param name="newState"></param>
        public void SetState(ISoilState newState)
        {
            CurrentState = newState;
            CurrentState.SetType(this);
        }

        /// <summary>
        /// Metode til at opdatere jordens tilstand
        /// </summary>
        public override void Update()
        {
            if(CurrentState != null)
            {
                CurrentState.Update();
            }
        }
    }
}
