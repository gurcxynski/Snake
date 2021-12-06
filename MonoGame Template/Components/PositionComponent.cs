using Microsoft.Xna.Framework;
using Snake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Components
{
    class PositionComponent : Component
    {
        public Vector2 _Position { get; set; } = new Vector2();

        public PositionComponent(Vector2 position)
        {
            _Position = position;
        }
    }
}