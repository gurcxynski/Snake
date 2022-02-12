using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Snake.Core
{
    abstract class Button
    {
        public Vector2 position;
        public bool beDrawn = true;
        public Texture2D texture;
        public string text;

        public void Draw(SpriteBatch spriteBatch)
        {
            if (beDrawn)
            {
                spriteBatch.Draw(texture, position, Color.White);
                spriteBatch.DrawString(Globals.font, text, new Vector2((position.X + texture.Width / 2) - Globals.font.MeasureString(text).X / 2, position.Y + 5), Color.Black);
            }
        }
        public bool EnterButton()
        {
            if (    Globals.mouse.Position.X < position.X + texture.Width &&
                    Globals.mouse.Position.X > position.X &&
                    Globals.mouse.Position.Y < position.Y + texture.Height &&
                    Globals.mouse.Position.Y > position.Y)
            {
                texture = Globals.textures["button2"];
                return true;
            }
            texture = Globals.textures["button1"];
            return false;
        }
        virtual public void Update()
        {
           
        }
    }
}
