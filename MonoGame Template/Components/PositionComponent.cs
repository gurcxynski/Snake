using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Core;
using System.Collections.Generic;

namespace Snake.Components
{
    class PositionComponent : Component
    {
        public Vector2 Position { get; set; } = new Vector2();

        public Vector2 RoundedPosition { get; set; } = new Vector2();

        public PositionComponent(Vector2 position)
        {
            Position = position;
            RoundedPosition = new Vector2((int)Position.X / 40, (int)Position.Y / 40);
        }
        public override void Update(float UpdateTime)
        {
            RoundedPosition = new Vector2((int)Position.X / 40, (int)Position.Y / 40);
        }
    }
}