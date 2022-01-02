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
        Keys turn = Keys.None;
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
        public void BodyUpdate(int SNAKE_VEL, Dictionary<string, Texture2D> textures)
        {
            //GetComponent<DirectionComponent>().Direction = _daddy.GetComponent<DirectionComponent>().Direction;
            if(_daddy.GetComponent<DirectionComponent>().Direction != GetComponent<DirectionComponent>().Direction)
            {
                turn = _daddy.GetComponent<DirectionComponent>().Direction;
                TurnObject(textures, turn, SNAKE_VEL);
                turn = Keys.None;
            }
            switch (GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Left:
                    GetComponent<TextureComponent>()._Texture = _textures["body_hor"];
                    GetComponent<PositionComponent>().Position= 
                        new Vector2(_daddy.GetComponent<PositionComponent>().Position.X + 40, _daddy.GetComponent<PositionComponent>().Position.Y);
                    GetComponent<VelocityComponent>().Velocity = new Vector2(-200, 0);
                    break;
                case Keys.Right:
                    GetComponent<TextureComponent>()._Texture = _textures["body_hor"];
                    GetComponent<PositionComponent>().Position = 
                        new Vector2(_daddy.GetComponent<PositionComponent>().Position.X - 40, _daddy.GetComponent<PositionComponent>().Position.Y);
                    GetComponent<VelocityComponent>().Velocity = new Vector2(200, 0);
                    break;
                case Keys.Up:
                    GetComponent<TextureComponent>()._Texture = _textures["body_ver"];
                    GetComponent<PositionComponent>().Position =
                        new Vector2(_daddy.GetComponent<PositionComponent>().Position.X, _daddy.GetComponent<PositionComponent>().Position.Y + 40);
                    GetComponent<VelocityComponent>().Velocity = new Vector2(0, -200);
                    break;
                case Keys.Down:
                    GetComponent<TextureComponent>()._Texture = _textures["body_ver"];
                    GetComponent<PositionComponent>().Position =
                        new Vector2(_daddy.GetComponent<PositionComponent>().Position.X, _daddy.GetComponent<PositionComponent>().Position.Y - 40);
                    GetComponent<VelocityComponent>().Velocity = new Vector2(0, 200);
                    break;
            }
        }
    }
}
