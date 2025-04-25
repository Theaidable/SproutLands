using DesignPatterns.ComponentPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.CommandPattern
{
    public class ToggleColliderDrawingCommand : ICommand
    {
        private List<GameObject> gameObjects;
        private bool shouldDraw;
        public ToggleColliderDrawingCommand(List<GameObject> gameObjects)
        {
            this.gameObjects = gameObjects;
        }
        public void Execute()
        {
            shouldDraw = !shouldDraw;
            List<Collider> colliders = new List<Collider>();
            foreach (GameObject gameObject in gameObjects)
            {
                Collider collider = gameObject.GetComponent<Collider>() as Collider;
                if (collider != null)
                {
                    collider.ToggleDrawing(shouldDraw);
                }
            }
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
