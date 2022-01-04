using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Components;
using Snake.Core;

namespace Snake.GameObjects
{
    internal class Head : GameObject
    {
        public Head(Texture2D texture, Vector2 Position)
        {
            AddComponent(new TextureComponent(texture));
            AddComponent(new PositionComponent(Position));
            AddComponent(new DirectionComponent(Keys.Right));
            AddComponent(new VelocityComponent());
        }
        public void TextureUpdate(Dictionary<string, Texture2D> textures)
        {
            switch (GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Up:
                    GetComponent<TextureComponent>()._Texture = textures["head_up"];
                    break;
                case Keys.Down:
                    GetComponent<TextureComponent>()._Texture = textures["head_down"];
                    break;
                case Keys.Left:
                    GetComponent<TextureComponent>()._Texture = textures["head_left"];
                    break;
                case Keys.Right:
                    GetComponent<TextureComponent>()._Texture = textures["head_right"];
                    break;
            }
        }
    }
}
