using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snake.Components;
using Snake.Core;

namespace Snake.GameObjects
{
    internal class BodyFragment : GameObject
    {
        readonly GameObject _daddy;

        Keys queuedTurn = Keys.None;
        Vector2 lastRoundedPos;

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
            AddComponent(new VelocityComponent(_daddy.GetComponent<VelocityComponent>().Velocity, this));
        }

        public override void Update(float UpdateTime)
        {
            UpdateTexture();
            GetComponent<VelocityComponent>().Update(UpdateTime);
            UpdateTurn();
            GetComponent<PositionComponent>().Update(UpdateTime);
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

        private void UpdateTurn()
        {
            if (_daddy.turned != Keys.None && queuedTurn == Keys.None)
            {
                queuedTurn = _daddy.turned;
                _daddy.turned = Keys.None;
                lastRoundedPos = GetComponent<PositionComponent>().RoundedPosition;
            }
            bool TurnNow = false;
            Vector2 position = GetComponent<PositionComponent>().Position;

            if(queuedTurn != Keys.None && GetComponent<PositionComponent>().RoundedPosition != lastRoundedPos) switch (GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Left:
                    if (position.X % 40 < 20)
                        TurnNow = true;
                    break;
                case Keys.Right:
                    if (position.X % 40 > 20)
                        TurnNow = true;
                    break;
                case Keys.Up:
                    if (position.Y % 40 < 20)
                        TurnNow = true;
                    break;
                case Keys.Down:
                    if (position.Y % 40 > 20)
                        TurnNow = true;
                    break;
            }
            if (TurnNow)
            {
                switch (queuedTurn)
                {
                    case Keys.Left:
                        TurnObject(Keys.Left);
                        GetComponent<PositionComponent>().Position = new Vector2(_daddy.GetComponent<PositionComponent>().Position.X + 40, _daddy.GetComponent<PositionComponent>().Position.Y);
                        break;
                    case Keys.Right:
                        TurnObject(Keys.Right);
                        GetComponent<PositionComponent>().Position = new Vector2(_daddy.GetComponent<PositionComponent>().Position.X - 40, _daddy.GetComponent<PositionComponent>().Position.Y);
                        break;
                    case Keys.Up:
                        TurnObject(Keys.Up);
                        GetComponent<PositionComponent>().Position = new Vector2(_daddy.GetComponent<PositionComponent>().Position.X, _daddy.GetComponent<PositionComponent>().Position.Y + 40);
                        break;
                    case Keys.Down:
                        TurnObject(Keys.Down);
                        GetComponent<PositionComponent>().Position = new Vector2(_daddy.GetComponent<PositionComponent>().Position.X, _daddy.GetComponent<PositionComponent>().Position.Y - 40);
                        break;
                }
                turned = queuedTurn;
                queuedTurn = Keys.None;
            }
        }
    }
}
