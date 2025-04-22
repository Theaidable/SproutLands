using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SproutLands.Classes.ComponentPattern.Colliders;

namespace SproutLands.Classes.ComponentPattern
{
    public abstract class Component
    {
        //Property
        public GameObject gameObject { get; private set; }

        //Constructor
        public Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public virtual void Awake()
        {

        }
        public virtual void Start()
        {

        }
        public virtual void Update()
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
        public virtual void OnCollisionEnter(Collider collider)
        {

        }

        public void SetNewGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
