using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Snake.Core;
using System;
using System.Collections.Generic;
using System.Text;
using MonoGame.EasyInput;

namespace Snake.Buttons
{
    class SpeedButton : Button
    {
        public SpeedButton(Texture2D _texture, Vector2 _position)
        {
            position = _position;
            texture = _texture;
            text = "SPEED:" + Settings.BaseVel;
        }

        override public void Update()
        {
            if (enterButton() && Globals.mouse.ReleasedThisFrame(MouseButtons.Left))
            {
                Settings.BaseVel += 20;
                if (Settings.BaseVel > 300) Settings.BaseVel = 100;
                text = "SPEED:" + Settings.BaseVel;
            }
        }
    }
}
