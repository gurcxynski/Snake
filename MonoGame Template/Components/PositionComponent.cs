using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snake.Core;
using System;
using System.Collections.Generic;
using System.Text;
using static Snake.Components.DirectionComponent;

namespace Snake.Components
{
    class PositionComponent : Component
    {
        public Vector2 Position { get; set; } = new Vector2();

        public Vector2 RoundedPosition { get; set; } = new Vector2();

        public PositionComponent(Vector2 position)
        {
            Position = position;
            int newPosX = (int)Position.X / 40;
            int newPosY = (int)Position.Y / 40;
            /*if (newPosX == 10) newPosX = 0;
            if (newPosY == 10) newPosY = 0;*/
            RoundedPosition = new Vector2(newPosX, newPosY); 
        }
        public void Update(float UpdateTime, Keys direction)
        {
            int newPosX = (int)Position.X / 40;
            int newPosY = (int)Position.Y / 40;
            
                switch (direction)
                {
                case Keys.Right:
                     newPosX++;
                     break;
                case Keys.Down:
                     newPosY++;
                     break;
                }
            RoundedPosition = new Vector2(newPosX, newPosY);
        }
        public void Randomize()
        {
            Random rand = new Random();
            int x = rand.Next(0, 9);
            int y = rand.Next(0, 9);
            Position = new Vector2(x * 40, y * 40);
            RoundedPosition = new Vector2(x, y);
        }
    }
}