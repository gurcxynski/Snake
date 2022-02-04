using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Core;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.EasyInput;

namespace Snake.Buttons
{
    class PlayButton : Button
    {
        public PlayButton(Texture2D _texture, Vector2 _position)
        {
            position = _position;
            texture = _texture;
            text = "PLAY";
        }

        override public void Update()
        {
            if (enterButton() && Globals.mouse.ReleasedThisFrame(MouseButtons.Left))
            {
                Globals.menu = false;
            }
        }
    }
}
