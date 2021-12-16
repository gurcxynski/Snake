using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Components;
using Snake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.GameObjects
{
    internal class Apple : GameObject
    {
        public Apple(Texture2D texture, Vector2 Position)
        {
            AddComponent(new TextureComponent(texture));
            AddComponent(new PositionComponent(Position));
            AddComponent(new CollisionChecker(this));
        }
    }
}
