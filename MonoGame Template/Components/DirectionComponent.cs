using Microsoft.Xna.Framework.Input;
using Snake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Components
{
    class DirectionComponent : Component
    {
        public Keys Direction { get; set; } 
        public DirectionComponent()
        {
            Direction = Keys.Right;
        }
        public DirectionComponent(Keys arg)
        {
            Direction = arg;
        }
    }
}
