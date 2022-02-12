using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Core;
using MonoGame.EasyInput;

namespace Snake.Buttons
{
    class GodModeButton : Button
    {
        public GodModeButton(Texture2D _texture, Vector2 _position)
        {
            position = _position;
            texture = _texture;
            text = "GODMODE: " + (Settings.godmode ? "ENABLED" : "DISABLED");
        }

        override public void Update()
        {
            if (EnterButton() && Globals.mouse.ReleasedThisFrame(MouseButtons.Left))
            {
                Settings.godmode = !Settings.godmode;
                text = "GODMODE: " + (Settings.godmode ? "ENABLED" : "DISABLED");
            }
        }
    }
}
