using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Components;
using Snake.Core;

namespace Snake.GameObjects
{
    internal class BodyFragment : GameObject
    {
        GameObject _daddy;
        Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
        int _index = 0;
        Vector2 prevVel = new Vector2(0, 0);
        public Keys turn = Keys.None;
        public Vector2 prevRoundedPos = new Vector2(0, 0);
        public BodyFragment(Dictionary<string, Texture2D> textures, GameObject daddy, int index)
        {
            _daddy = daddy;
            _index = index;
            _textures = textures;
            AddComponent(new DirectionComponent(_daddy.GetComponent<DirectionComponent>().Direction));
            switch (GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Left:
                    AddComponent(new TextureComponent(_textures["body_hor"]));
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X + 40, 
                        _daddy.GetComponent<PositionComponent>().Position.Y)));
                    AddComponent(new VelocityComponent(new Vector2(-200, 0)));
                    break;
                case Keys.Right:
                    AddComponent(new TextureComponent(_textures["body_hor"]));
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X - 40, 
                        _daddy.GetComponent<PositionComponent>().Position.Y)));
                    AddComponent(new VelocityComponent(new Vector2(200, 0)));
                    break;
                case Keys.Up:
                    AddComponent(new TextureComponent(_textures["body_ver"]));
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X, 
                        _daddy.GetComponent<PositionComponent>().Position.Y + 40)));
                    AddComponent(new VelocityComponent(new Vector2(0, -200)));
                    break;
                case Keys.Down:
                    AddComponent(new TextureComponent(_textures["body_ver"]));
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X, 
                        _daddy.GetComponent<PositionComponent>().Position.Y - 40)));
                    AddComponent(new VelocityComponent(new Vector2(0, 200)));
                    break;
            }
            
        }
        public void Pause()
        {
            prevVel = GetComponent<VelocityComponent>().Velocity;
            GetComponent<VelocityComponent>().Velocity = new Vector2(0, 0);
        }
        public void Unpause()
        {
            GetComponent<VelocityComponent>().Velocity = prevVel;
        }
        public new bool TurnObject(Dictionary<string, Texture2D> textures, Keys Turn, int SNAKE_VEL)
        {
            switch (Turn)
            {
                case Keys.Up:
                    {
                        GetComponent<VelocityComponent>().Velocity = new Vector2(0, -SNAKE_VEL);
                        GetComponent<PositionComponent>().Position =
                            new Vector2(GetComponent<PositionComponent>().Position.X - GetComponent<PositionComponent>().Position.X % 40,
                            GetComponent<PositionComponent>().Position.Y);
                        GetComponent<DirectionComponent>().Direction = Keys.Up;
                        Turned = Keys.Up;
                        return true;
                    }
                case Keys.Down:
                    {
                        GetComponent<VelocityComponent>().Velocity = new Vector2(0, SNAKE_VEL);
                        GetComponent<PositionComponent>().Position =
                            new Vector2(GetComponent<PositionComponent>().Position.X - GetComponent<PositionComponent>().Position.X % 40,
                            GetComponent<PositionComponent>().Position.Y);
                        GetComponent<DirectionComponent>().Direction = Keys.Down;
                        Turned = Keys.Down;
                        return true;
                    }
                case Keys.Left:
                    {
                        GetComponent<VelocityComponent>().Velocity = new Vector2(-SNAKE_VEL, 0);
                        GetComponent<PositionComponent>().Position =
                            new Vector2(GetComponent<PositionComponent>().Position.X,
                            GetComponent<PositionComponent>().Position.Y - GetComponent<PositionComponent>().Position.Y % 40);
                        GetComponent<DirectionComponent>().Direction = Keys.Left;
                        Turned = Keys.Left;
                        return true;
                    
                    }
                case Keys.Right:
                    {
                        GetComponent<VelocityComponent>().Velocity = new Vector2(SNAKE_VEL, 0);
                        GetComponent<PositionComponent>().Position =
                            new Vector2(GetComponent<PositionComponent>().Position.X,
                            GetComponent<PositionComponent>().Position.Y - GetComponent<PositionComponent>().Position.Y % 40);
                        GetComponent<DirectionComponent>().Direction = Keys.Right;
                        Turned = Keys.Right;
                        return true;
                    }
            }
            return false;
        }
    public void BodyUpdate(int SNAKE_VEL, Dictionary<string, Texture2D> textures)
        {
            if(_daddy.Turned != Keys.None)
            {
                turn = _daddy.Turned;
                prevRoundedPos = new Vector2(GetComponent<PositionComponent>().RoundedPosition.X - 1, GetComponent<PositionComponent>().RoundedPosition.Y - 1);
            }
            if (prevRoundedPos != new Vector2(0,0) && prevRoundedPos != GetComponent<PositionComponent>().RoundedPosition)
            {
                TurnObject(textures, turn, SNAKE_VEL);
                Turned = turn;
                turn = Keys.None;
                _daddy.Turned = Keys.None; 
            }
        }
    }
}
