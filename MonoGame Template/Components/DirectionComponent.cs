using Snake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Components
{
    class DirectionComponent : Component
    {
        public enum DirectionType { Left, Right, Up, Down };
        public DirectionType Direction { get; set; } 
        public DirectionComponent()
        {
            Direction = DirectionType.Right;
        }
        public DirectionComponent(DirectionType arg)
        {
            Direction = arg;
        }
    }
}
