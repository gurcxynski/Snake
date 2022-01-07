using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Snake.Core
{
    class Scene
    {
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        bool IsPaused = true;
        //Head SnakeHead = null;
        //Apple Apple = null;

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

        public bool Update(float UpdateTime)
        {
            //SnakeHead = GetObject<Head>();
            //StringBuilder sb = new StringBuilder();

            Globals.keyboard.Update();

            if (Globals.keyboard.ReleasedThisFrame(Keys.Space))
            {
                IsPaused = !IsPaused;
                foreach (var item in _gameObjects)
                {
                    if (IsPaused) item.Pause();
                    else item.UnPause();
                }
                return true;
            }

            if (IsPaused) return true;

            foreach (var item in _gameObjects)
            {
                item.Update(UpdateTime);
            }

            Debug.WriteLine(Globals.sb.ToString());

            return true;
        }
    }
}
