using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Components;
namespace Snake.Core
{
    class GameObject
    {
        private readonly List<Component> _components = new List<Component>();

        public GameObject AddComponent(Component comp)
        {
            _components.Add(comp);
            return this;
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(T))
                {
                    return component as T;
                }
            }
            return null;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetComponent<TextureComponent>()._Texture, GetComponent<PositionComponent>().Position, Microsoft.Xna.Framework.Color.White);
        }
    }
}
