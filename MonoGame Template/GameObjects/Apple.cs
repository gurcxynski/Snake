using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Components;
using Snake.Core;
using System;

namespace Snake.GameObjects
{
    internal class Apple : GameObject
    {
        private Head _Snake;

        public Apple(Texture2D texture, Vector2 Position, Head Snake)
        {
            AddComponent(new TextureComponent(texture));
            AddComponent(new PositionComponent(Position));
            AddComponent(new CollisionChecker(this));
            _Snake = Snake;
        }
        public void Randomize(int range)
        {
            Random rand = new Random();
            int x = rand.Next(0, range - 1);
            int y = rand.Next(0, range - 1);
            GetComponent<PositionComponent>().Position = new Vector2(x * 40 + 20, y * 40 + 20);
        }
        protected override void UpdateTexture()
        {

        }
        public override void Update(float UpdateTime)
        {
            if (GetComponent<CollisionChecker>().Check(_Snake))
            {
                Randomize(10);
                Globals.Score++;
                GetComponent<PositionComponent>().Update(UpdateTime);
            }
        }
        public override void Pause()
        {

        }

        public override void UnPause()
        {

        }
    }
}