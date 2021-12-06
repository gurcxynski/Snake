using Microsoft.Xna.Framework.Graphics;
using Snake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Components
{
    class TextureComponent : Component
    {
        public Texture2D _Texture { get; }
        public TextureComponent(Texture2D texture)
        {
            _Texture = texture;
        }
    }
}
