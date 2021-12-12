using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.EasyInput;
using Snake.Components;
using System.Collections.Generic;
using System.Text;
using static Snake.Components.DirectionComponent;
using System;

namespace Snake.Core
{
    class Scene
    {
        const int SNAKE_VEL = 200;
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        Vector2 PrevVelocity = new Vector2();
        EasyKeyboard keyboard = new EasyKeyboard();
        Keys Turn = Keys.None;
        bool IsPaused = true;
        Random random = new Random();   
        public GameObject AddGameObject(GameObject go)
        {
            _gameObjects.Add(go);
            return go;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in _gameObjects)
            {
                item.Draw(spriteBatch);
            }
        }

        public void Update(GraphicsDeviceManager graphics, GameObject SnakeHead, GameObject Apple, Dictionary<string, Texture2D> textures, float UpdateTime)
        {
            keyboard.Update();


            StringBuilder sb = new StringBuilder();
            sb.Append("X: ").Append(SnakeHead.GetComponent<PositionComponent>().Position.X).Append(";  Y: ").Append(SnakeHead.GetComponent<PositionComponent>().Position.Y).
                Append(" ").Append(IsPaused);
            System.Diagnostics.Debug.WriteLine(sb.ToString());


            if (keyboard.ReleasedThisFrame(Keys.Space))
            {
                if (IsPaused)
                {
                    SnakeHead.GetComponent<VelocityComponent>().Velocity = PrevVelocity;
                    IsPaused = false;
                }
                else
                {
                    PrevVelocity = SnakeHead.GetComponent<VelocityComponent>().Velocity;
                    IsPaused = true;
                }
            }

            if (!IsPaused)
            {

                if (keyboard.ReleasedThisFrame(Keys.Left) && SnakeHead.GetComponent<DirectionComponent>().Direction != DirectionType.Right)
                {
                    SnakeHead.GetComponent<DirectionComponent>().Direction = DirectionType.Left;
                }
                if (keyboard.ReleasedThisFrame(Keys.Right) && SnakeHead.GetComponent<DirectionComponent>().Direction != DirectionType.Left)
                {
                    SnakeHead.GetComponent<DirectionComponent>().Direction = DirectionType.Right;
                }
                if (keyboard.ReleasedThisFrame(Keys.Up) && SnakeHead.GetComponent<DirectionComponent>().Direction != DirectionType.Down)
                {
                    SnakeHead.GetComponent<DirectionComponent>().Direction = DirectionType.Up;
                }
                if (keyboard.ReleasedThisFrame(Keys.Down) && SnakeHead.GetComponent<DirectionComponent>().Direction != DirectionType.Up)
                {
                    SnakeHead.GetComponent<DirectionComponent>().Direction = DirectionType.Down;
                }
            }


            
            switch (SnakeHead.GetComponent<DirectionComponent>().Direction)
            {
                case DirectionComponent.DirectionType.Left:
                    SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_left"];
                    Turn = Keys.Left;
                    SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(-SNAKE_VEL, 0);
                    break;
                case DirectionComponent.DirectionType.Right: 
                    SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_right"];
                    SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(SNAKE_VEL, 0);
                    Turn = Keys.Right;
                    break;
                case DirectionComponent.DirectionType.Up: 
                    SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_up"];
                    SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(0, -SNAKE_VEL);
                    Turn = Keys.Up;
                    break;
                case DirectionComponent.DirectionType.Down: 
                    SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_down"];
                    SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(0, SNAKE_VEL);
                    Turn = Keys.Down;
                    break;
            }

            if(IsPaused) SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(0, 0);


            if (SnakeHead.GetComponent<PositionComponent>().Position.X == Apple.GetComponent<PositionComponent>().Position.X 
                && SnakeHead.GetComponent<PositionComponent>().Position.Y == Apple.GetComponent<PositionComponent>().Position.Y)
            {
                Apple.GetComponent<PositionComponent>().Position = new Vector2(
                    random.Next(0, graphics.PreferredBackBufferWidth),
                    random.Next(0, graphics.PreferredBackBufferHeight));
            }
                
            
            if(Turn != Keys.None && !IsPaused)
            {
                switch (Turn){
                    case Keys.Up:
                        {
                            break;
                        }
                    case Keys.Down:
                        {
                            break;
                        }
                    case Keys.Left:
                        {
                            break;
                        }
                    case Keys.Right:
                        {
                            break;
                        }
                }

            }



            if(SnakeHead.GetComponent<PositionComponent>().Position.X < 0)
            {
                SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(graphics.PreferredBackBufferWidth, SnakeHead.GetComponent<PositionComponent>().Position.Y);
            }
            if (SnakeHead.GetComponent<PositionComponent>().Position.X > graphics.PreferredBackBufferWidth)
            {
                SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(0, SnakeHead.GetComponent<PositionComponent>().Position.Y);
            }
            if (SnakeHead.GetComponent<PositionComponent>().Position.Y < 0)
            {
                SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(SnakeHead.GetComponent<PositionComponent>().Position.X, graphics.PreferredBackBufferHeight);
            }
            if (SnakeHead.GetComponent<PositionComponent>().Position.Y > graphics.PreferredBackBufferHeight)
            {
                SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(SnakeHead.GetComponent<PositionComponent>().Position.X, 0);
            }


            SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(
                SnakeHead.GetComponent<PositionComponent>().Position.X + SnakeHead.GetComponent<VelocityComponent>().Velocity.X * UpdateTime,
                SnakeHead.GetComponent<PositionComponent>().Position.Y + SnakeHead.GetComponent<VelocityComponent>().Velocity.Y * UpdateTime);

        }
        
    }
}
