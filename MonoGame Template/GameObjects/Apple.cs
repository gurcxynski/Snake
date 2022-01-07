using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Components;
using Snake.Core;
using System;

namespace Snake.GameObjects
{
    internal class Apple : GameObject
    {
        public Apple(Texture2D texture, Vector2 Position)
        {
            AddComponent(new TextureComponent(texture));
            AddComponent(new PositionComponent(Position));
            AddComponent(new CollisionChecker(this));
        }
        public void Randomize(int range)
        {
            Random rand = new Random();
            int x = rand.Next(0, range - 1);
            int y = rand.Next(0, range - 1);
            GetComponent<PositionComponent>().Position = new Vector2(x * 40 + 20, y * 40 + 20);
        }
        public bool Check(GameObject arg)
        {
            return GetComponent<CollisionChecker>().Check(arg);
        }
        protected override void UpdateTexture()
        {

        }
        public override void Update(float UpdateTime)
        {
            
        }
        public override void Pause()
        {

        }

        public override void UnPause()
        {

        }
    }
}