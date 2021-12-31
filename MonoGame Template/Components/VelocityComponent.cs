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

        public void Update(float UpdateTime, GameObject Object)
        {
            Object.GetComponent<PositionComponent>().Position = new Vector2(
            Object.GetComponent<PositionComponent>().Position.X + Object.GetComponent<VelocityComponent>().Velocity.X * UpdateTime,
            Object.GetComponent<PositionComponent>().Position.Y + Object.GetComponent<VelocityComponent>().Velocity.Y * UpdateTime);
        }
    }
}