using Microsoft.Xna.Framework;
using Snake.Core;

namespace Snake.Components
{
    class VelocityComponent : Component
    {
        public Vector2 Velocity { get; set; } = new Vector2(0, 0);
        public PositionComponent PositionComponent { get; set; }
        public VelocityComponent(Vector2 arg, GameObject @object)
        {
            Velocity = arg;
            PositionComponent = @object.GetComponent<PositionComponent>();
        }

        public VelocityComponent(GameObject @object)
        {
            Velocity = new Vector2(Settings.BaseVel, 0);
            PositionComponent = @object.GetComponent<PositionComponent>();
        }

        public override void Update(float UpdateTime)
        {
            PositionComponent.Position = new Vector2(PositionComponent.Position.X + Velocity.X * UpdateTime,
            PositionComponent.Position.Y + Velocity.Y * UpdateTime);
        }
    }
}