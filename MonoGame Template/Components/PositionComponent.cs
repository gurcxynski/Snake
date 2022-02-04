using Microsoft.Xna.Framework;
using Snake.Core;

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
            if (Position.X < 0) Position = new Vector2(Settings.Size, Position.Y);
            if (Position.X > Settings.Size) Position = new Vector2(0, Position.Y);
            if (Position.Y < 0) Position = new Vector2(Position.X, Settings.Size);
            if (Position.Y > Settings.Size) Position = new Vector2(Position.X, 0);

            RoundedPosition = new Vector2((int)Position.X / 40, (int)Position.Y / 40);
        }
        public void Update()
        {
            RoundedPosition = new Vector2((int)Position.X / 40, (int)Position.Y / 40);
        }
    }
}