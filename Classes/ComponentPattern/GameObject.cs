using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using SproutLands.Classes.ComponentPattern.Colliders;

namespace SproutLands.Classes.ComponentPattern
{
    public class GameObject
    {
        //Fields
        private List<Component> components = new List<Component>();

        //Property
        public Transform Transform { get; private set; }

        public GameObject()
        {
            Transform = (Transform)AddComponent<Transform>();
        }

        public void Awake()
        {
            foreach (var component in components)
            {
                component.Awake();
            }
        }

        public void Start()
        {
            foreach (var component in components)
            {
                component.Start();
            }
        }

        public void Update()
        {
            foreach (var component in components)
            {
                component.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }
        }

        public void OnCollisionEnter(Collider collider)
        {
            foreach (var component in components)
            {
                component.OnCollisionEnter(collider);
            }
        }

        public Component AddComponent<T>(params object[] additionalParameters) where T : Component
        {
            Type componentType = typeof(T);
            try
            {
                // Opret en instans ved hjælp af den fundne konstruktør og leverede parametre
                object[] allParameters = new object[1 + additionalParameters.Length];
                allParameters[0] = this;
                Array.Copy(additionalParameters, 0, allParameters, 1, additionalParameters.Length);

                T component = (T)Activator.CreateInstance(componentType, allParameters);
                components.Add(component);
                return component;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Fejl ved oprettelse af komponenten {componentType.Name}: {ex.Message}", ex);
            }
        }

        public Component GetComponent<T>() where T : Component
        {
            return components.Find(x => x.GetType() == typeof(T));
        }

        public Component AddComponentWithExistingValues(Component component)
        {
            components.Add(component);
            return component;
        }
    }
}
