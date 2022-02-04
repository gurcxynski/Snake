using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Core;
using MonoGame.EasyInput;

namespace Snake.Buttons
{
    class ExitButton : Button
    {
        public ExitButton(Texture2D _texture, Vector2 _position)
        {
            position = _position;
            texture = _texture;
            text = "EXIT";
        }

        override public void Update()
        {
            if (enterButton() && Globals.mouse.ReleasedThisFrame(MouseButtons.Left))
            {
                Globals.game.Exit();
            }
        }
    }
}
