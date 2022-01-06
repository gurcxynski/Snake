using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.EasyInput;
using Snake.Components;
namespace Snake.Core
{
    abstract class GameObject
    {
        public List<Component> _components = new List<Component>();
        protected Vector2 LastVelocity;
        public int _index;

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

        public void Update(Dictionary<string, Texture2D> textures, float UpdateTime)
        {
            foreach (var component in _components)
            {
                component.Update(textures, UpdateTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetComponent<TextureComponent>()._Texture, new Vector2(GetComponent<PositionComponent>().Position.X - 20, GetComponent<PositionComponent>().Position.Y - 20), Color.White);
        }

        public void Pause()
        {
            LastVelocity = GetComponent<VelocityComponent>().Velocity;
            GetComponent<VelocityComponent>().Velocity = new Vector2(0, 0);
        }

        public void UnPause()
        {
            GetComponent<VelocityComponent>().Velocity = LastVelocity;
        }


        public void TurnObject(Keys Turn)
        {
            int abs_velocity = (int)GetComponent<VelocityComponent>().Velocity.Length();
            GetComponent<DirectionComponent>().Direction = Turn;
            switch (Turn)
            {
                case Keys.Up:
                    GetComponent<VelocityComponent>().Velocity = new Vector2(0, -abs_velocity);
                    break;
                case Keys.Down:
                    GetComponent<VelocityComponent>().Velocity = new Vector2(0, +abs_velocity);
                    break;
                case Keys.Left:
                    GetComponent<VelocityComponent>().Velocity = new Vector2(-abs_velocity, 0);
                    break;
                case Keys.Right:
                    GetComponent<VelocityComponent>().Velocity = new Vector2(abs_velocity, 0);
                    break;
            }
        }
        abstract protected void UpdateTexture(Dictionary<string, Texture2D> textures);
    }
}
