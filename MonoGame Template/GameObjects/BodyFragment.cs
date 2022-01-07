using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snake.Components;
using Snake.Core;

namespace Snake.GameObjects
{
    internal class BodyFragment : GameObject
    {
        readonly GameObject _daddy;

        public BodyFragment(GameObject daddy)
        {
            _daddy = daddy;
            _index = _daddy._index + 1;
            AddComponent(new DirectionComponent(_daddy.GetComponent<DirectionComponent>().Direction));
            AddComponent(new TextureComponent(Globals.textures["body_ver"]));

            switch (GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Left:
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X + 40, 
                        _daddy.GetComponent<PositionComponent>().Position.Y)));
                    AddComponent(new VelocityComponent(new Vector2(-Globals.BaseVel, 0), this));
                    break;
                case Keys.Right:
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X - 40, 
                        _daddy.GetComponent<PositionComponent>().Position.Y)));
                    AddComponent(new VelocityComponent(new Vector2(Globals.BaseVel, 0), this));
                    break;
                case Keys.Up:
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X, 
                        _daddy.GetComponent<PositionComponent>().Position.Y + 40)));
                    AddComponent(new VelocityComponent(new Vector2(0, -Globals.BaseVel), this));
                    break;
                case Keys.Down:
                    AddComponent(new PositionComponent(new Vector2(_daddy.GetComponent<PositionComponent>().Position.X, 
                        _daddy.GetComponent<PositionComponent>().Position.Y - 40)));
                    AddComponent(new VelocityComponent(new Vector2(0, Globals.BaseVel), this));
                    break;
            }
        }

        public override void Update(float UpdateTime)
        {
            UpdateTexture();
        }

        override protected void UpdateTexture()
        {
            switch (GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Up:
                    GetComponent<TextureComponent>()._Texture = Globals.textures["body_ver"];
                    break;
                case Keys.Down:
                    GetComponent<TextureComponent>()._Texture = Globals.textures["body_ver"];
                    break;
                case Keys.Left:
                    GetComponent<TextureComponent>()._Texture = Globals.textures["body_hor"];
                    break;
                case Keys.Right:
                    GetComponent<TextureComponent>()._Texture = Globals.textures["body_hor"];
                    break;
            }
        }
    }
}
