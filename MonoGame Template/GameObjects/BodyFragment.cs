using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Components;
using Snake.Core;

namespace Snake.GameObjects
{
    internal class BodyFragment : GameObject
    {
        GameObject daddy;
        public BodyFragment(Dictionary<string, Texture2D> textures, GameObject daddy)
        {
            this.daddy = daddy;
            AddComponent(new DirectionComponent(daddy.GetComponent<DirectionComponent>().Direction));
            switch (daddy.GetComponent<DirectionComponent>().Direction)
            {
                case Keys.Left:
                    AddComponent(new TextureComponent(textures["body_hor"]));
                    AddComponent(new PositionComponent(new Vector2(daddy.GetComponent<PositionComponent>().Position.X + 40, 
                        daddy.GetComponent<PositionComponent>().Position.Y)));
                    AddComponent(new VelocityComponent(new Vector2(-200, 0)));
                    break;
                case Keys.Right:
                    AddComponent(new TextureComponent(textures["body_hor"]));
                    AddComponent(new PositionComponent(new Vector2(daddy.GetComponent<PositionComponent>().Position.X - 40, 
                        daddy.GetComponent<PositionComponent>().Position.Y)));
                    AddComponent(new VelocityComponent(new Vector2(200, 0)));
                    break;
                case Keys.Up:
                    AddComponent(new TextureComponent(textures["body_ver"]));
                    AddComponent(new PositionComponent(new Vector2(daddy.GetComponent<PositionComponent>().Position.X, 
                        daddy.GetComponent<PositionComponent>().Position.Y + 40)));
                    AddComponent(new VelocityComponent(new Vector2(0, -200)));
                    break;
                case Keys.Down:
                    AddComponent(new TextureComponent(textures["body_ver"]));
                    AddComponent(new PositionComponent(new Vector2(daddy.GetComponent<PositionComponent>().Position.X, 
                        daddy.GetComponent<PositionComponent>().Position.Y - 40)));
                    AddComponent(new VelocityComponent(new Vector2(0, 200)));
                    break;
            }
        }
    }
}
