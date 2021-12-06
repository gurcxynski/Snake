using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Components;
using System;
using System.Collections.Generic;
using System.Text;
using static Snake.Components.DirectionComponent;

namespace Snake.Core
{
    class Scene
    {
        private readonly List<GameObject> _gameObjects = new List<GameObject>();
        char turn = 'n';
        public GameObject AddGameObject(GameObject go)
        {
            _gameObjects.Add(go);
            return go;
        }
        public void RemoveGameObject(GameObject go)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in _gameObjects)
            {
                item.Draw(spriteBatch);
            }
        }
        public void Update(KeyboardState kstate, GameObject go, Dictionary<string, Texture2D> textures, float UpdateTime)
        {
            go.GetComponent<PositionComponent>().Position = new Vector2(
                go.GetComponent<PositionComponent>().Position.X + go.GetComponent<VelocityComponent>().Velocity.X * UpdateTime,
                go.GetComponent<PositionComponent>().Position.Y + go.GetComponent<VelocityComponent>().Velocity.Y * UpdateTime);


            if (kstate.IsKeyDown(Keys.Left) && go.GetComponent<DirectionComponent>().Direction != DirectionType.Right)
            {
                turn = 'l';
            }
            else if (kstate.IsKeyDown(Keys.Right) && go.GetComponent<DirectionComponent>().Direction != DirectionType.Left)
            {
                turn = 'r';
            }
            else if (kstate.IsKeyDown(Keys.Up) && go.GetComponent<DirectionComponent>().Direction != DirectionType.Down)
            {
                turn = 'u';
            }
            else if (kstate.IsKeyDown(Keys.Down) && go.GetComponent<DirectionComponent>().Direction != DirectionType.Up)
            {
                turn = 'd';
            }
            else if (kstate.IsKeyDown(Keys.Space))
            {
                go.GetComponent<VelocityComponent>().Velocity = new Microsoft.Xna.Framework.Vector2(0,0);
            }
            if(turn != 'n')
            {
                switch (turn)
                {
                    case 'l':
                        go.GetComponent<DirectionComponent>().Direction = DirectionComponent.DirectionType.Left;
                        go.GetComponent<VelocityComponent>().Velocity = new Microsoft.Xna.Framework.Vector2(-240, 0);
                        go.GetComponent<PositionComponent>().Position =
                            new Vector2(go.GetComponent<PositionComponent>().Position.X,
                            go.GetComponent<PositionComponent>().Position.Y - (go.GetComponent<PositionComponent>().Position.Y % 60) + 10);
                        turn = 'n'; break;
                    case 'r':
                        go.GetComponent<DirectionComponent>().Direction = DirectionComponent.DirectionType.Right;
                        go.GetComponent<VelocityComponent>().Velocity = new Microsoft.Xna.Framework.Vector2(240, 0);
                        go.GetComponent<PositionComponent>().Position =
                            new Vector2(go.GetComponent<PositionComponent>().Position.X,
                            go.GetComponent<PositionComponent>().Position.Y - (go.GetComponent<PositionComponent>().Position.Y % 60) + 10);
                        turn = 'n'; break;
                    case 'u':
                        go.GetComponent<DirectionComponent>().Direction = DirectionComponent.DirectionType.Up;
                        go.GetComponent<VelocityComponent>().Velocity = new Microsoft.Xna.Framework.Vector2(0, -240);
                        go.GetComponent<PositionComponent>().Position =
                            new Vector2(go.GetComponent<PositionComponent>().Position.X - (go.GetComponent<PositionComponent>().Position.X % 60) + 10,
                            go.GetComponent<PositionComponent>().Position.Y);
                        turn = 'n'; break;
                    case 'd':
                        go.GetComponent<DirectionComponent>().Direction = DirectionComponent.DirectionType.Down;
                        go.GetComponent<VelocityComponent>().Velocity = new Microsoft.Xna.Framework.Vector2(0, 240);
                        go.GetComponent<PositionComponent>().Position =
                            new Vector2(go.GetComponent<PositionComponent>().Position.X - (go.GetComponent<PositionComponent>().Position.X % 60) + 10,
                            go.GetComponent<PositionComponent>().Position.Y);
                        turn = 'n'; break;

                }
            }
            


            switch (go.GetComponent<DirectionComponent>().Direction)
            {
                case DirectionComponent.DirectionType.Left: go.GetComponent<TextureComponent>()._Texture = textures["head_left"]; break;
                case DirectionComponent.DirectionType.Right: go.GetComponent<TextureComponent>()._Texture = textures["head_right"]; break;
                case DirectionComponent.DirectionType.Up: go.GetComponent<TextureComponent>()._Texture = textures["head_up"]; break;
                case DirectionComponent.DirectionType.Down: go.GetComponent<TextureComponent>()._Texture = textures["head_down"]; break;
            }

            

        }
    }
}
