using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.EasyInput;
using Snake.Components;
using System.Collections.Generic;
using System.Text;
using System;
using Snake.GameObjects;
using System.Diagnostics;

namespace Snake.Core
{
    class Scene
    {
        const int SNAKE_VEL = 200;
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        Vector2 PrevVelocity = new Vector2(SNAKE_VEL, 0);
        EasyKeyboard keyboard = new EasyKeyboard();
        Keys Turn = Keys.None;
        bool IsPaused = true;
        int Score = 0;
        List<BodyFragment> fragments = new List<BodyFragment>();

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

            StringBuilder sb = new StringBuilder();

            keyboard.Update();

            if (keyboard.ReleasedThisFrame(Keys.Space))
            {
                if (IsPaused)
                {
                    SnakeHead.GetComponent<VelocityComponent>().Velocity = PrevVelocity;
                    foreach (var item in fragments)
                    {
                        item.Unpause();
                    }
                    IsPaused = false;
                }
                else
                {
                    PrevVelocity = SnakeHead.GetComponent<VelocityComponent>().Velocity;
                    foreach (var item in fragments)
                    {
                        item.Pause();
                    }
                    IsPaused = true;
                }
            }

            if (IsPaused) SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(0, 0);


            if (!IsPaused && Turn == Keys.None)
            {

                if (keyboard.ReleasedThisFrame(Keys.Left) && SnakeHead.GetComponent<DirectionComponent>().Direction != Keys.Right)
                {
                    Turn = Keys.Left;
                    sb.Append(Turn.ToString());
                }
                if (keyboard.ReleasedThisFrame(Keys.Right) && SnakeHead.GetComponent<DirectionComponent>().Direction != Keys.Left)
                {
                    Turn = Keys.Right;
                    sb.Append(Turn.ToString());
                }
                if (keyboard.ReleasedThisFrame(Keys.Up) && SnakeHead.GetComponent<DirectionComponent>().Direction != Keys.Down)
                {
                    Turn = Keys.Up;
                    sb.Append(Turn.ToString());
                }
                if (keyboard.ReleasedThisFrame(Keys.Down) && SnakeHead.GetComponent<DirectionComponent>().Direction != Keys.Up)
                {
                    Turn = Keys.Down;
                    sb.Append(Turn.ToString());
                }
            }

            if (!IsPaused && Turn != Keys.None)
            {
                SnakeHead.TurnObject(textures, Turn, SNAKE_VEL);
                Turn = Keys.None;
            }

            if (SnakeHead.GetComponent<PositionComponent>().RoundedPosition.X < 0 || SnakeHead.GetComponent<PositionComponent>().RoundedPosition.X > 10 ||
               SnakeHead.GetComponent<PositionComponent>().RoundedPosition.Y < 0 || SnakeHead.GetComponent<PositionComponent>().RoundedPosition.Y > 10)
            {
                IsPaused = true;
                return false;
            }

            SnakeHead.GetComponent<VelocityComponent>().Update(UpdateTime, SnakeHead);
            SnakeHead.GetComponent<PositionComponent>().Update(UpdateTime, SnakeHead.GetComponent<DirectionComponent>().Direction);

            if (Apple.GetComponent<CollisionChecker>().Check((Head)SnakeHead))
            {
                Apple.GetComponent<PositionComponent>().Randomize();
                Score++;
                if (Score == 1)
                {
                    BodyFragment newFragment = new BodyFragment(textures, SnakeHead, Score - 1);
                    fragments.Add(newFragment);
                    AddGameObject(newFragment);
                }
                else
                {
                    BodyFragment newFragment = new BodyFragment(textures, fragments[Score - 2], Score - 1);
                    fragments.Add(newFragment);
                    AddGameObject(newFragment);
                }
            }


            foreach (var item in fragments)
            {
                item.GetComponent<VelocityComponent>().Update(UpdateTime, item);
                if (!IsPaused) item.BodyUpdate(SNAKE_VEL, textures);
            }

            
            //sb.Append("X: ").Append(SnakeHead.GetComponent<PositionComponent>().RoundedPosition.X).Append(";  Y: ").Append(SnakeHead.GetComponent<PositionComponent>().RoundedPosition.Y);
            //sb.Append("   Apple: X: ").Append(Apple.GetComponent<PositionComponent>().RoundedPosition.X).Append(";  Y: ").Append(Apple.GetComponent<PositionComponent>().RoundedPosition.Y);
            //sb.Append("   Pause: ").Append(IsPaused);
            //if (fragments.Count > 0) sb.Append(fragments[0].GetComponent<DirectionComponent>().Direction);


            Debug.WriteLine(sb.ToString());

            return true;
        }
    }
}
