using Microsoft.Xna.Framework.Graphics;
using MonoGame.EasyInput;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake.Core
{
    abstract class Component
    {
        public virtual void Update(Dictionary<string, Texture2D> textures, float UpdateTime)
        {

        }
    }
}
