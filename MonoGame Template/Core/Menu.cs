using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Snake.Core
{
    class Menu
    {
         private readonly List<Button> _buttons = new List<Button>();

        public Button AddButton(Button button)
        {
            _buttons.Add(button);
            return button;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in _buttons)
            {
                item.Draw(spriteBatch);
            }
        }
        public Button GetObject(string name)
        {
            foreach (var Object in _buttons)
            {
                if (Object.text == name)
                {
                    return Object;
                }
            }
            return null;
        }
        public void Update()
        {
            Globals.mouse.Update();
            foreach (var item in _buttons)
            {
                item.Update();
            }
        }
    }
}
