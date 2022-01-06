﻿using Microsoft.Xna.Framework;
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
        bool IsPaused = true;
        int Score = 0;
        //List<BodyFragment> fragments = new List<BodyFragment>();
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

            //StringBuilder sb = new StringBuilder();

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
                item.Update(textures, UpdateTime);
            }

            //Debug.WriteLine(sb.ToString());

            return true;
        }
    }
}
