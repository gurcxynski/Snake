using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Core
{
    class Scene
    {
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        public GameObject AddGameObject(GameObject go)
        {
            _gameObjects.Add(go);
            return go;
        }
        public void RemoveGameObject(GameObject go)
        {
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in _gameObjects)
            {
                item.Draw(spriteBatch);
            }
        }
        public void Update()
        {

        }   
    }
}
