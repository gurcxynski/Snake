using Microsoft.Xna.Framework;
using Snake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Components
{
    class VelocityComponent : Component
    {

        public Vector2 Velocity { get; set; } = new Vector2(0, 0);

        public VelocityComponent(Vector2 arg)
        {
            Velocity = arg;
        }

        public VelocityComponent()
        {
            Velocity = new Vector2(0, 0);
        }

        public new void Update(float UpdateTime, GameObject Snake)
        {
            Snake.GetComponent<PositionComponent>().Position = new Vector2(
            Snake.GetComponent<PositionComponent>().Position.X + Snake.GetComponent<VelocityComponent>().Velocity.X * UpdateTime,
            Snake.GetComponent<PositionComponent>().Position.Y + Snake.GetComponent<VelocityComponent>().Velocity.Y * UpdateTime);
        }
    }
}