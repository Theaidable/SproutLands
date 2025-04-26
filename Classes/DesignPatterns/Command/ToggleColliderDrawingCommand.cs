﻿using SproutLands.Classes.DesignPatterns.Composite.Components;
using SproutLands.Classes.DesignPatterns.Composite;
using System.Collections.Generic;

namespace SproutLands.Classes.DesignPatterns.Command
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
    }
}
