using Microsoft.Xna.Framework;
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
        public bool Initalized = false;
        bool add = false;
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
        public void Initialize()
        {
            _gameObjects.Clear();
            AddGameObject(new Apple(Globals.textures["apple_texture"], new Vector2(180, Settings.Size / 2 - 20),
                    (Head)AddGameObject(new Head(Globals.textures["head_right"], new Vector2(100, Settings.Size / 2 - 20)))));
            Initalized = true;

        }
        public void Reset()
        {
            _gameObjects.Clear();
            Globals.menu = true;
            Initalized = false;
            LastAdded = null;
        }
        public void Update(float UpdateTime)
        {
            Globals.keyboard.Update();
            Globals.mouse.Update();

            if (Globals.keyboard.ReleasedThisFrame(Keys.Space))
            {
                IsPaused = !IsPaused;
                foreach (var item in _gameObjects)
                {
                    if (IsPaused) item.Pause();
                    else item.UnPause();
                }
                return;
            }

            if (Globals.keyboard.ReleasedThisFrame(Keys.Escape))
            {
                Reset();
                return;
            }

            if (IsPaused) return;
            
            foreach (var item in _gameObjects)
            {
                if (item.Update(UpdateTime))
                {
                    if (item.GetType() == typeof(Apple)) add = true;
                    else
                    {
                        Reset();
                        return;
                    }
                }
            }

            if (add)
            {
                if (LastAdded == null) LastAdded = (BodyFragment)AddGameObject(new BodyFragment(GetObject<Head>()));
                else LastAdded = (BodyFragment)AddGameObject(new BodyFragment(LastAdded));
                add = false;
            }
            
            foreach (var item in _gameObjects)
            {
                if (item.GetType() == typeof(BodyFragment) && GetObject<Apple>().Check(item))
                    GetObject<Apple>().Randomize(10);
            }
            
            Debug.WriteLine(Globals.sb.ToString());

        }
    }
}
