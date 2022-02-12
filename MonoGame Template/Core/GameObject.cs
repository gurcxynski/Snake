using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Components;
namespace Snake.Core
{
    abstract class GameObject
    {
        public List<Component> _components = new List<Component>();
        protected Vector2 LastVelocity;
        public int _index;
        public Keys turned;
        public bool beDrawn = true;
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

        abstract public bool Update(float UpdateTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            if(beDrawn) spriteBatch.Draw(GetComponent<TextureComponent>()._Texture, new Vector2(GetComponent<PositionComponent>().Position.X - 20, GetComponent<PositionComponent>().Position.Y - 20), Color.White);
            spriteBatch.DrawString(Globals.font, GetComponent<PositionComponent>().RoundedPosition.X.ToString() + ", " + GetComponent<PositionComponent>().RoundedPosition.Y.ToString(), new Vector2(GetComponent<PositionComponent>().Position.X - 20, GetComponent<PositionComponent>().Position.Y - 20), Color.Black);
        }

        public virtual void Pause()
        {
            LastVelocity = GetComponent<VelocityComponent>().Velocity;
            GetComponent<VelocityComponent>().Velocity = new Vector2(0, 0);
        }

        public virtual void UnPause()
        {
            GetComponent<VelocityComponent>().Velocity = LastVelocity;
        }


        public void TurnObject(Keys Turn)
        {
            GetComponent<DirectionComponent>().Direction = Turn;
            switch (Turn)
            {
                case Keys.Up:
                    GetComponent<VelocityComponent>().Velocity = new Vector2(0, -Settings.BaseVel);
                    break;
                case Keys.Down:
                    GetComponent<VelocityComponent>().Velocity = new Vector2(0, +Settings.BaseVel);
                    break;
                case Keys.Left:
                    GetComponent<VelocityComponent>().Velocity = new Vector2(-Settings.BaseVel, 0);
                    break;
                case Keys.Right:
                    GetComponent<VelocityComponent>().Velocity = new Vector2(Settings.BaseVel, 0);
                    break;
            }
            turned = Turn;
        }
        abstract protected void UpdateTexture();

    }
}
