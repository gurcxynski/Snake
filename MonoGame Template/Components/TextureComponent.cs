using Microsoft.Xna.Framework.Graphics;
using Snake.Core;

namespace Snake.Components
{
    class TextureComponent : Component
    {
        public Texture2D _Texture { get; set; }
        public TextureComponent(Texture2D texture)
        {
            _Texture = texture;
        }
    }
}
