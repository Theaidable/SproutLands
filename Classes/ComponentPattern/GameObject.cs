﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System;
using SproutLands.Classes.ComponentPattern.Colliders;
using System.Linq;

namespace SproutLands.Classes.ComponentPattern
{
    public class GameObject
    {
        //Liste af components
        private List<Component> components = new List<Component>();

        //Property til at tilgå position
        public Transform Transform { get; private set; }

        /// <summary>
        /// Constructor af et gameobject, hvor vi altid tilføjer en transformer
        /// </summary>
        public GameObject()
        {
            Transform = AddComponent<Transform>();
        }

        /// <summary>
        /// Metode til at awake alle components
        /// </summary>
        public void Awake()
        {
            foreach (var component in components)
            {
                component.Awake();
            }
        }

        /// <summary>
        /// Metode til at kalde alle compenents ved start
        /// </summary>
        public void Start()
        {
            foreach (var component in components)
            {
                component.Start();
            }
        }

        /// <summary>
        /// Metoden opdaterer alle components
        /// </summary>
        public void Update()
        {
            foreach (var component in components)
            {
                component.Update();
            }
        }

        /// <summary>
        /// Metode som tegener gameobjectet ved at tegne dens components
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in components)
            {
                component.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Collision håndtering for gameobjectet
        /// </summary>
        /// <param name="collider"></param>
        public void OnCollisionEnter(Collider collider)
        {
            foreach (var component in components)
            {
                component.OnCollisionEnter(collider);
            }
        }

        /// <summary>
        /// Metode til at tilføje et component til gameobjectet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="additionalParameters"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public T AddComponent<T>(params object[] additionalParameters) where T : Component
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

            //Catch hvis der sker en fejl
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Fejl ved oprettelse af komponenten {componentType.Name}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Metode til at tilgå components
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component
        {
            return components.OfType<T>().FirstOrDefault();
        }

        public Component AddComponentWithExistingValues(Component component)
        {
            components.Add(component);
            return component;
        }
    }
}
