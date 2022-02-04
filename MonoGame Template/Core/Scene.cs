using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.GameObjects;
using System.Collections.Generic;
using System.Diagnostics;

namespace Snake.Core
{
    class Scene
    {
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        BodyFragment LastAdded;
        bool IsPaused = true;
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
            
            if(Globals.Score == 1 && LastAdded == null)
            {
                LastAdded = (BodyFragment)AddGameObject(new BodyFragment(GetObject<Head>()));
            }
            else if (LastAdded != null && Globals.Score > LastAdded._index + 1)
            {
                LastAdded = (BodyFragment)AddGameObject(new BodyFragment(LastAdded));
            }

            foreach (var item in _gameObjects)
            {
                item.Update(UpdateTime);
            }

            foreach (var item in _gameObjects)
            {
                if (item.GetType() == typeof(BodyFragment) && GetObject<Apple>().Check(item))
                    GetObject<Apple>().Randomize(10);
            }
            
            Globals.sb.Append('\n'); 
            Debug.WriteLine(Globals.sb.ToString());

            return Globals.GameRunning;
        }
    }
}
