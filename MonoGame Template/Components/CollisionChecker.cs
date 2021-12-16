using Snake.Core;
using Snake.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Components
{
    internal class CollisionChecker : Component
    {
        Apple thisApple;
        public CollisionChecker(Apple apple)
        {
            thisApple = apple;
        }
        public bool Check(Head SnakeHead)
        {
            return thisApple.GetComponent<PositionComponent>().RoundedPosition.Equals(SnakeHead.GetComponent<PositionComponent>().RoundedPosition);
        }
    }
}
