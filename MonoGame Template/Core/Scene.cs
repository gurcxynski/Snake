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
        int Score = 0;
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

        public int Update(GraphicsDeviceManager graphics, GameObject SnakeHead, GameObject Apple, Dictionary<string, Texture2D> textures, float UpdateTime)
        {
            keyboard.Update();


            StringBuilder sb = new StringBuilder();
            sb.Append("X: ").Append(SnakeHead.GetComponent<PositionComponent>().Position.X).Append(";  Y: ").Append(SnakeHead.GetComponent<PositionComponent>().Position.Y).
                Append(" ").Append(IsPaused).Append(" Score: ").Append(Score);
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
                    Turn = Keys.Left;
                    break;
                case DirectionComponent.DirectionType.Right: 
                    Turn = Keys.Right;
                    break;
                case DirectionComponent.DirectionType.Up: 
                    Turn = Keys.Up;
                    break;
                case DirectionComponent.DirectionType.Down: 
                    Turn = Keys.Down;
                    break;
            }

            if(IsPaused) SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(0, 0);


            if (Math.Abs(SnakeHead.GetComponent<PositionComponent>().Position.X - Apple.GetComponent<PositionComponent>().Position.X) < 1 &&
                Math.Abs(SnakeHead.GetComponent<PositionComponent>().Position.Y - Apple.GetComponent<PositionComponent>().Position.Y) < 1)
            {
                Apple.GetComponent<PositionComponent>().Position = new Vector2(
                    40 * random.Next(0, graphics.PreferredBackBufferWidth/40),
                    40 * random.Next(0, graphics.PreferredBackBufferHeight/40));
                Score++;
            }
                
            
            if(Turn != Keys.None && !IsPaused)
            {
                switch (Turn){
                    case Keys.Up:
                        {
                            if (SnakeHead.GetComponent<PositionComponent>().Position.X % 40 < 10)
                            {
                                SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(0, -SNAKE_VEL);
                                Turn = Keys.None;
                                SnakeHead.GetComponent<PositionComponent>().Position =
                                    new Vector2(SnakeHead.GetComponent<PositionComponent>().Position.X - SnakeHead.GetComponent<PositionComponent>().Position.X % 40,
                                    SnakeHead.GetComponent<PositionComponent>().Position.Y);
                                SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_up"];
                            }
                            break;
                        }
                    case Keys.Down:
                        {
                            if (SnakeHead.GetComponent<PositionComponent>().Position.X % 40 < 10)
                            {
                                SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(0, SNAKE_VEL);
                                Turn = Keys.None;
                                SnakeHead.GetComponent<PositionComponent>().Position = 
                                    new Vector2(SnakeHead.GetComponent<PositionComponent>().Position.X - SnakeHead.GetComponent<PositionComponent>().Position.X % 40,
                                    SnakeHead.GetComponent<PositionComponent>().Position.Y);
                                SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_down"];
                            }
                            break;
                        }
                    case Keys.Left:
                        {
                            if (SnakeHead.GetComponent<PositionComponent>().Position.Y % 40 < 10)
                            {
                                SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(-SNAKE_VEL, 0);
                                Turn = Keys.None;
                                SnakeHead.GetComponent<PositionComponent>().Position = 
                                    new Vector2(SnakeHead.GetComponent<PositionComponent>().Position.X,
                                    SnakeHead.GetComponent<PositionComponent>().Position.Y - SnakeHead.GetComponent<PositionComponent>().Position.Y % 40);
                                SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_left"];
                            }
                            break;
                        }
                    case Keys.Right:
                        {
                            if (SnakeHead.GetComponent<PositionComponent>().Position.Y % 40 < 10)
                            {
                                SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(SNAKE_VEL, 0);
                                Turn = Keys.None;
                                SnakeHead.GetComponent<PositionComponent>().Position = 
                                    new Vector2(SnakeHead.GetComponent<PositionComponent>().Position.X,
                                    SnakeHead.GetComponent<PositionComponent>().Position.Y - SnakeHead.GetComponent<PositionComponent>().Position.Y % 40);
                                SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_right"];
                            }
                            break;
                        }
                }

            }



            if(SnakeHead.GetComponent<PositionComponent>().Position.X < 0)
            {
                SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(graphics.PreferredBackBufferWidth - 1, SnakeHead.GetComponent<PositionComponent>().Position.Y);
            }
            if (SnakeHead.GetComponent<PositionComponent>().Position.X >= graphics.PreferredBackBufferWidth)
            {
                SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(0, SnakeHead.GetComponent<PositionComponent>().Position.Y);
            }
            if (SnakeHead.GetComponent<PositionComponent>().Position.Y < 0)
            {
                SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(SnakeHead.GetComponent<PositionComponent>().Position.X, graphics.PreferredBackBufferHeight - 1);
            }
            if (SnakeHead.GetComponent<PositionComponent>().Position.Y >= graphics.PreferredBackBufferHeight)
            {
                SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(SnakeHead.GetComponent<PositionComponent>().Position.X, 0);
            }


            SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(
                SnakeHead.GetComponent<PositionComponent>().Position.X + SnakeHead.GetComponent<VelocityComponent>().Velocity.X * UpdateTime,
                SnakeHead.GetComponent<PositionComponent>().Position.Y + SnakeHead.GetComponent<VelocityComponent>().Velocity.Y * UpdateTime);

            return Score;
        }
    }
}
