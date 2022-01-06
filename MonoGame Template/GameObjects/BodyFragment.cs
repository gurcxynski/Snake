using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.EasyInput;
using Snake.Components;
using Snake.Core;

namespace Snake.GameObjects
{
    internal class BodyFragment : GameObject
    {
        readonly GameObject _daddy;
        private readonly Dictionary<string, Texture2D> _textures;

        public BodyFragment(Dictionary<string, Texture2D> textures, GameObject daddy)
        {
            _daddy = daddy;
            _index = _daddy._index + 1;
            _textures = textures;
            AddComponent(new DirectionComponent(_daddy.GetComponent<DirectionComponent>().Direction));
            AddComponent(new TextureComponent(textures["body_ver"]));

            switch (GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Left:
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X + 40, 
                        _daddy.GetComponent<PositionComponent>().Position.Y)));
                    AddComponent(new VelocityComponent(new Vector2(-200, 0), this));
                    break;
                case Keys.Right:
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X - 40, 
                        _daddy.GetComponent<PositionComponent>().Position.Y)));
                    AddComponent(new VelocityComponent(new Vector2(200, 0), this));
                    break;
                case Keys.Up:
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X, 
                        _daddy.GetComponent<PositionComponent>().Position.Y + 40)));
                    AddComponent(new VelocityComponent(new Vector2(0, -200), this));
                    break;
                case Keys.Down:
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X, 
                        _daddy.GetComponent<PositionComponent>().Position.Y - 40)));
                    AddComponent(new VelocityComponent(new Vector2(0, 200), this));
                    break;
            }
        }

        public new void Update(Dictionary<string, Texture2D> textures, float UpdateTime)
        {
            UpdateTexture(textures);
            
        }

        override protected void UpdateTexture(Dictionary<string, Texture2D> textures)
        {
            switch (GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Up:
                    GetComponent<TextureComponent>()._Texture = textures["body_ver"];
                    break;
                case Keys.Down:
                    GetComponent<TextureComponent>()._Texture = textures["body_ver"];
                    break;
                case Keys.Left:
                    GetComponent<TextureComponent>()._Texture = textures["body_hor"];
                    break;
                case Keys.Right:
                    GetComponent<TextureComponent>()._Texture = textures["body_hor"];
                    break;
            }
        }
    }
}
