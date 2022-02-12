using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Core;
using MonoGame.EasyInput;

namespace Snake.Buttons
{
    class SoundButton : Button
    {
        public SoundButton(Texture2D _texture, Vector2 _position)
        {
            position = _position;
            texture = _texture;
            text = "SOUND: " + (Settings.sound ? "ENABLED" : "DISABLED");
        }

        override public void Update()
        {
            if (EnterButton() && Globals.mouse.ReleasedThisFrame(MouseButtons.Left))
            {
                Settings.sound = !Settings.sound;
                text = "SOUND: " + (Settings.sound ? "ENABLED" : "DISABLED");
            }
        }
    }
}
