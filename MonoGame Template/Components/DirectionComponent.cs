using Microsoft.Xna.Framework.Input;
using Snake.Core;

namespace Snake.Components
{
    class DirectionComponent : Component
    {
        public Keys Direction { get; set; } 
        public DirectionComponent()
        {
            Direction = Keys.None;
        }
        public DirectionComponent(Keys arg)
        {
            Direction = arg;
        }
    }
}
