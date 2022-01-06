﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.EasyInput;
using Snake.Components;
using Snake.Core;

namespace Snake.GameObjects
{
    internal class Head : GameObject
    {
        Texture2D texture;
        public Head(Texture2D texture_arg, Vector2 Position)
        {
            AddComponent(new TextureComponent(texture_arg));
            AddComponent(new PositionComponent(Position));
            AddComponent(new DirectionComponent(Keys.Right));
            AddComponent(new VelocityComponent(this));
            AddComponent(new CollisionChecker(this));

            texture = GetComponent<TextureComponent>()._Texture;
            _index = -1;
            LastVelocity = new Vector2(200, 0);
        }

        override protected void UpdateTexture(Dictionary<string, Texture2D> textures)
        {
            switch (GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Up:
                    texture = textures["head_up"];
                    break;
                case Keys.Down:
                    texture = textures["head_down"];
                    break;
                case Keys.Left:
                    texture = textures["head_left"];
                    break;
                case Keys.Right:
                    texture = textures["head_right"];
                    break;
            }

            GetComponent<TextureComponent>()._Texture = texture;
        }

        public new void Update(Dictionary<string, Texture2D> textures, float UpdateTime)
        {
            UpdateTexture(textures);
            foreach (var item in _components)
            {
                item.Update(textures, UpdateTime);
            }
        }

        public bool Check(GameObject arg)
        {
            return GetComponent<CollisionChecker>().Check(arg);
        }
    }
}
