using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Components;
using Snake.Core;

namespace Snake.GameObjects
{
    internal class Head : GameObject
    {
        private Texture2D texture;
        private Keys direction;
        private Keys queuedTurn = Keys.None;
        private Vector2 position;
        public Head(Texture2D texture_arg, Vector2 Position)
        {
            AddComponent(new TextureComponent(texture_arg));
            AddComponent(new PositionComponent(Position));
            AddComponent(new DirectionComponent(Keys.Right));
            AddComponent(new VelocityComponent(this));
            AddComponent(new CollisionChecker(this));

            direction = GetComponent<DirectionComponent>().Direction;
            position = GetComponent<PositionComponent>().Position;

            _index = -1;
            LastVelocity = new Vector2(Globals.BaseVel, 0);
        }

        override protected void UpdateTexture()
        {
            switch (GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Up:
                    texture = Globals.textures["head_up"];
                    break;
                case Keys.Down:
                    texture = Globals.textures["head_down"];
                    break;
                case Keys.Left:
                    texture = Globals.textures["head_left"];
                    break;
                case Keys.Right:
                    texture = Globals.textures["head_right"];
                    break;
            }

            GetComponent<TextureComponent>()._Texture = texture;
        }

        public override void Update(float UpdateTime)
        {
            direction = GetComponent<DirectionComponent>().Direction;
            position = GetComponent<PositionComponent>().Position;

            UpdateTexture();

            if (queuedTurn == Keys.None) QueueTurn();
            else TryTurn(queuedTurn);

            foreach (var item in _components)
            {
                item.Update(UpdateTime);
            }


            if (GetComponent<PositionComponent>().Position.X < 0 || GetComponent<PositionComponent>().Position.X > Globals.Size
                || GetComponent<PositionComponent>().Position.Y < 0 || GetComponent<PositionComponent>().Position.Y > Globals.Size) Globals.GameRunning = false;

        }

        public bool Check(GameObject arg)
        {
            return GetComponent<CollisionChecker>().Check(arg);
        }

        private Keys QueueTurn()
        {
            if (Globals.keyboard.ReleasedThisFrame(Keys.Left) && direction != Keys.Right)
            {
                queuedTurn = Keys.Left;
            }
            else if (Globals.keyboard.ReleasedThisFrame(Keys.Right) && direction != Keys.Left)
            {
                queuedTurn = Keys.Right;
            }
            else if (Globals.keyboard.ReleasedThisFrame(Keys.Up) && direction != Keys.Down)
            {
                queuedTurn = Keys.Up;
            }
            else if (Globals.keyboard.ReleasedThisFrame(Keys.Down) && direction != Keys.Up)
            {
                queuedTurn = Keys.Down;
            }
            return queuedTurn;
        }

        private bool TryTurn(Keys turn)
        {
            switch (turn)
            {
                case Keys.Left:
                    if ((position.Y + 30) % 40 < 20)
                    {
                        TurnObject(Keys.Left);
                        GetComponent<PositionComponent>().Position = new Vector2(position.X, 40 * GetComponent<PositionComponent>().RoundedPosition.Y + 20);
                        queuedTurn = Keys.None;
                        return true;
                    }
                    break;
                case Keys.Right:
                    if ((position.Y + 30) % 40 < 20)
                    {
                        TurnObject(Keys.Right);
                        GetComponent<PositionComponent>().Position = new Vector2(position.X, 40 * GetComponent<PositionComponent>().RoundedPosition.Y + 20);
                        queuedTurn = Keys.None;
                        return true;
                    }
                    break;
                case Keys.Up:
                    if ((position.X + 30) % 40 < 20)
                    {
                        TurnObject(Keys.Up);
                        GetComponent<PositionComponent>().Position = new Vector2(40 * GetComponent<PositionComponent>().RoundedPosition.X + 20, position.Y);
                        queuedTurn = Keys.None;
                        return true;
                    }
                    break;
                case Keys.Down:
                    if ((position.X + 30) % 40 < 20)
                    {
                        TurnObject(Keys.Down);
                        GetComponent<PositionComponent>().Position = new Vector2(40 * GetComponent<PositionComponent>().RoundedPosition.X + 20, position.Y);
                        queuedTurn = Keys.None;
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}
