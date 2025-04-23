using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SproutLands.Classes.ComponentPattern.Colliders;

namespace SproutLands.Classes.ComponentPattern
{
    public abstract class Component
    {
        //Properties for component
        public GameObject GameObject { get; private set; }
        public Transform Transform => GameObject.Transform;

        /// <summary>
        /// Components constructor
        /// </summary>
        /// <param name="gameObject"></param>
        public Component(GameObject gameObject)
        {
            this.GameObject = gameObject;
        }

        //Virtuelle metoder som skal bruges af components children
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual void OnCollisionEnter(Collider collider) { }


        public void SetNewGameObject(GameObject gameObject)
        {
            this.GameObject = gameObject;
        }
    }
}
