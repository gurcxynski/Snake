using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.EasyInput;
using Snake.Components;
using System;
using System.Collections.Generic;
using System.Text;
using static Snake.Components.DirectionComponent;

namespace Snake.Core
{
    class Scene
    {
        const int SNAKE_VEL = 200;
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        Vector2 PrevVelocity = new Vector2();
        EasyKeyboard keyboard = new EasyKeyboard();
        Keys Turn = Keys.None;

        public GameObject AddGameObject(GameObject go)
        {
            _gameObjects.Add(go);
            return go;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in _gameObjects)
            {
                item.Draw(spriteBatch);
            }
        }

        public void Update(GameObject SnakeHead, Dictionary<string, Texture2D> textures, float UpdateTime)
        {
            keyboard.Update();


            StringBuilder sb = new StringBuilder();
            sb.Append("X: ").Append(SnakeHead.GetComponent<PositionComponent>().Position.X).Append(";  Y: ").Append(SnakeHead.GetComponent<PositionComponent>().Position.Y);
            System.Diagnostics.Debug.WriteLine(sb.ToString());


            if (keyboard.ReleasedThisFrame(Keys.Space))
            {
                if (SnakeHead.GetComponent<VelocityComponent>().Velocity != new Vector2(0, 0))
                {
                    PrevVelocity = SnakeHead.GetComponent<VelocityComponent>().Velocity;
                    SnakeHead.GetComponent<VelocityComponent>().Velocity = new Vector2(0, 0);
                }
                else SnakeHead.GetComponent<VelocityComponent>().Velocity = PrevVelocity;
            }

            if (keyboard.ReleasedThisFrame(Keys.Left) && SnakeHead.GetComponent<DirectionComponent>().Direction != DirectionType.Right)
            {
                SnakeHead.GetComponent<DirectionComponent>().Direction = DirectionType.Left;
            }
            if (keyboard.ReleasedThisFrame(Keys.Right) && SnakeHead.GetComponent<DirectionComponent>().Direction != DirectionType.Left)
            {
                SnakeHead.GetComponent<DirectionComponent>().Direction = DirectionType.Right;
            }
            if (keyboard.ReleasedThisFrame(Keys.Up) && SnakeHead.GetComponent<DirectionComponent>().Direction != DirectionType.Down)
            {
                SnakeHead.GetComponent<DirectionComponent>().Direction = DirectionType.Up;
            }
            if (keyboard.ReleasedThisFrame(Keys.Down) && SnakeHead.GetComponent<DirectionComponent>().Direction != DirectionType.Up)
            {
                SnakeHead.GetComponent<DirectionComponent>().Direction = DirectionType.Down;
            }


            
            switch (SnakeHead.GetComponent<DirectionComponent>().Direction)
            {
                case DirectionComponent.DirectionType.Left:
                    SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_left"];
                    Turn = Keys.Left;
                    break;
                case DirectionComponent.DirectionType.Right: 
                    SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_right"];
                    Turn = Keys.Right;
                    break;
                case DirectionComponent.DirectionType.Up: 
                    SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_up"];
                    Turn = Keys.Up;
                    break;
                case DirectionComponent.DirectionType.Down: 
                    SnakeHead.GetComponent<TextureComponent>()._Texture = textures["head_down"];
                    Turn = Keys.Down;
                    break;
            }


            //if(SnakeHead.GetComponent<PositionComponent>().Position.X)
            //add grid check


            SnakeHead.GetComponent<PositionComponent>().Position = new Vector2(
                SnakeHead.GetComponent<PositionComponent>().Position.X + SnakeHead.GetComponent<VelocityComponent>().Velocity.X * UpdateTime,
                SnakeHead.GetComponent<PositionComponent>().Position.Y + SnakeHead.GetComponent<VelocityComponent>().Velocity.Y * UpdateTime);

        }
        
    }
}
