using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.EasyInput;
using Snake.Components;
using Snake.Core;

namespace Snake.GameObjects
{
    internal class Head : GameObject
    {
        Texture2D texture;
        Keys direction;
        public Head(Texture2D texture_arg, Vector2 Position)
        {
            AddComponent(new TextureComponent(texture_arg));
            AddComponent(new PositionComponent(Position));
            AddComponent(new DirectionComponent(Keys.Right));
            AddComponent(new VelocityComponent(this));
            AddComponent(new CollisionChecker(this));

            direction = GetComponent<DirectionComponent>().Direction;
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
            Globals.sb.Append(GetComponent<PositionComponent>().RoundedPosition + "\n");

            UpdateTexture();
            Turn();
            foreach (var item in _components)
            {
                item.Update(UpdateTime);
            }
        }

        public bool Check(GameObject arg)
        {
            return GetComponent<CollisionChecker>().Check(arg);
        }

        public void Turn()
        {
            if (Globals.keyboard.ReleasedThisFrame(Keys.Left) && direction != Keys.Right)
            {
                TurnObject(Keys.Left);
            }
            else if (Globals.keyboard.ReleasedThisFrame(Keys.Right) && direction != Keys.Left)
            {
                TurnObject(Keys.Right);
            }
            else if (Globals.keyboard.ReleasedThisFrame(Keys.Up) && direction != Keys.Down)
            {
                TurnObject(Keys.Up);
            }
            else if (Globals.keyboard.ReleasedThisFrame(Keys.Down) && direction != Keys.Up)
            {
                TurnObject(Keys.Down);
            }
        }
    }
}
