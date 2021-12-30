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
    }
}
