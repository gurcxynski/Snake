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
        EasyKeyboard keyboard = new EasyKeyboard();
        Keys Turn = Keys.None;
        bool IsPaused = true;
        int Score = 0;
        List<BodyFragment> fragments = new List<BodyFragment>();
        Head SnakeHead = null;
        Apple Apple = null;
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
            if(SnakeHead == null || Apple == null)
            {
               SnakeHead = GetObject<Head>();
               Apple = GetObject<Apple>();
            }  

            StringBuilder sb = new StringBuilder();

            keyboard.Update();

            if (keyboard.ReleasedThisFrame(Keys.Space))
            {
                IsPaused = !IsPaused;
                if (IsPaused)
                    foreach (var item in _gameObjects)
                    {
                        item.Pause();
                        return true;
                    }
                else
                    foreach (var item in _gameObjects)
                    {
                        item.UnPause();
                        return true;
                    }
            }

            foreach (var item in _gameObjects)
            {
                item.Update(keyboard, textures, UpdateTime);
            }

            if (SnakeHead.GetComponent<PositionComponent>().RoundedPosition.X < 0 || SnakeHead.GetComponent<PositionComponent>().RoundedPosition.X > 10 ||
               SnakeHead.GetComponent<PositionComponent>().RoundedPosition.Y < 0 || SnakeHead.GetComponent<PositionComponent>().RoundedPosition.Y > 10)
            {
                IsPaused = true;
                return false;
            }

            SnakeHead.GetComponent<VelocityComponent>().Update(UpdateTime, SnakeHead);
            SnakeHead.GetComponent<PositionComponent>().Update(UpdateTime, SnakeHead.GetComponent<DirectionComponent>().Direction);
            SnakeHead.TextureUpdate(textures);

            if (Apple.GetComponent<CollisionChecker>().Check(SnakeHead))
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
                sb.Append(item.prevRoundedPos).Append(item.GetComponent<PositionComponent>().RoundedPosition).Append(item.turn).Append(item.Turned).Append("\n");
                
                item.GetComponent<PositionComponent>().Update(UpdateTime, item.GetComponent<DirectionComponent>().Direction);
            }

            if (!IsPaused)Debug.WriteLine(sb.ToString());

            return true;
        }
    }
}
