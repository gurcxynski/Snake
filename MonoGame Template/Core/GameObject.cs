using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public void Update(float UpdateTime)
        {
            foreach (var component in _components)
            {
                component.Update(UpdateTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GetComponent<TextureComponent>()._Texture, GetComponent<PositionComponent>().Position, Microsoft.Xna.Framework.Color.White);
        }

        public void TurnObject(Dictionary<string, Texture2D> textures, Keys Turn, int SNAKE_VEL)
        {
            switch (Turn)
            {
                case Keys.Up:
                    {
                        if (this.GetComponent<PositionComponent>().Position.X % 40 < 10)
                        {
                            this.GetComponent<VelocityComponent>().Velocity = new Vector2(0, -SNAKE_VEL);
                            Turn = Keys.None;
                            this.GetComponent<PositionComponent>().Position =
                                new Vector2(this.GetComponent<PositionComponent>().Position.X - this.GetComponent<PositionComponent>().Position.X % 40,
                                this.GetComponent<PositionComponent>().Position.Y);
                            this.GetComponent<TextureComponent>()._Texture = textures["head_up"];
                            this.GetComponent<DirectionComponent>().Direction = Keys.Up;
                        }
                        break;
                    }
                case Keys.Down:
                    {
                        if (this.GetComponent<PositionComponent>().Position.X % 40 < 10)
                        {
                            this.GetComponent<VelocityComponent>().Velocity = new Vector2(0, SNAKE_VEL);
                            Turn = Keys.None;
                            this.GetComponent<PositionComponent>().Position =
                                new Vector2(this.GetComponent<PositionComponent>().Position.X - this.GetComponent<PositionComponent>().Position.X % 40,
                                this.GetComponent<PositionComponent>().Position.Y);
                            this.GetComponent<TextureComponent>()._Texture = textures["head_down"];
                            this.GetComponent<DirectionComponent>().Direction = Keys.Down;
                        }
                        break;
                    }
                case Keys.Left:
                    {
                        if (this.GetComponent<PositionComponent>().Position.Y % 40 < 10)
                        {
                            this.GetComponent<VelocityComponent>().Velocity = new Vector2(-SNAKE_VEL, 0);
                            Turn = Keys.None;
                            this.GetComponent<PositionComponent>().Position =
                                new Vector2(this.GetComponent<PositionComponent>().Position.X,
                                this.GetComponent<PositionComponent>().Position.Y - this.GetComponent<PositionComponent>().Position.Y % 40);
                            this.GetComponent<TextureComponent>()._Texture = textures["head_left"];
                            this.GetComponent<DirectionComponent>().Direction = Keys.Left;
                        }
                        break;
                    }
                case Keys.Right:
                    {
                        if (this.GetComponent<PositionComponent>().Position.Y % 40 < 10)
                        {
                            this.GetComponent<VelocityComponent>().Velocity = new Vector2(SNAKE_VEL, 0);
                            Turn = Keys.None;
                            this.GetComponent<PositionComponent>().Position =
                                new Vector2(this.GetComponent<PositionComponent>().Position.X,
                                this.GetComponent<PositionComponent>().Position.Y - this.GetComponent<PositionComponent>().Position.Y % 40);
                            this.GetComponent<TextureComponent>()._Texture = textures["head_right"];
                            this.GetComponent<DirectionComponent>().Direction = Keys.Right;
                        }
                        break;
                    }
            }
        }
    }
}
