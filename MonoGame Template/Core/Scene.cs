using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.EasyInput;
using Snake.Components;
using System.Collections.Generic;
using System.Text;
using static Snake.Components.DirectionComponent;
using System;
using Snake.GameObjects;

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
        int Score = 0;
        bool GameNotOver = true;
        public GameObject AddGameObject(GameObject go)
        {
            _gameObjects.Add(go);
            return go;
        }
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            foreach (var item in _gameObjects)
            {
                item.Draw(spriteBatch);
            }
            spriteBatch.DrawString(font, Score.ToString(), new Vector2(0, 0), Color.Black);
        }

        public T GetObject<T>() where T : GameObject
        {
            foreach (var Object in _gameObjects)
            {
                if (Object.GetType() == typeof(T))
                {
                    return Object as T;
                }
            }
            return null;
        }


        public bool Update(GraphicsDeviceManager graphics, Dictionary<string, Texture2D> textures, float UpdateTime)
        {
            GameObject SnakeHead = GetObject<Head>();
            GameObject Apple = GetObject<Apple>();

            keyboard.Update();

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
                //SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(graphics.PreferredBackBufferWidth - 1, SnakeHead.GetComponent<PositionComponent>().Position.Y);
                GameNotOver = false;
            }
            if (SnakeHead.GetComponent<PositionComponent>().Position.X >= graphics.PreferredBackBufferWidth - 40)
            {
                //SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(0, SnakeHead.GetComponent<PositionComponent>().Position.Y);
                GameNotOver = false;
            }
            if (SnakeHead.GetComponent<PositionComponent>().Position.Y < 0)
            {
                //SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(SnakeHead.GetComponent<PositionComponent>().Position.X, graphics.PreferredBackBufferHeight - 1);
                GameNotOver = false;
            }
            if (SnakeHead.GetComponent<PositionComponent>().Position.Y >= graphics.PreferredBackBufferHeight - 40)
            {
                //SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(SnakeHead.GetComponent<PositionComponent>().Position.X, 0);
                GameNotOver = false;
            }

            SnakeHead.GetComponent<VelocityComponent>().Update(UpdateTime, SnakeHead);
            SnakeHead.GetComponent<PositionComponent>().Update(UpdateTime, SnakeHead.GetComponent<DirectionComponent>().Direction);
            if (Apple.GetComponent<CollisionChecker>().Check((Head)SnakeHead))
            {
                Apple.GetComponent<PositionComponent>().Randomize();
                Score++;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("X: ").Append(SnakeHead.GetComponent<PositionComponent>().RoundedPosition.X).Append(";  Y: ").Append(SnakeHead.GetComponent<PositionComponent>().RoundedPosition.Y);
            sb.Append("   Apple: X: ").Append(Apple.GetComponent<PositionComponent>().RoundedPosition.X).Append(";  Y: ").Append(Apple.GetComponent<PositionComponent>().RoundedPosition.Y);
            
            System.Diagnostics.Debug.WriteLine(sb.ToString());


            return GameNotOver;
        }
    }
}
