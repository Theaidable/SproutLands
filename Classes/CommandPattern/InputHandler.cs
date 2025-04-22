using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Windows.Input;

namespace SproutLands.Classes.CommandPattern
{
    public class InputHandler
    {
        private Dictionary<Keys, ICommand> _keyBindings;

        public InputHandler()
        {
            _keyBindings = new Dictionary<Keys, ICommand>();
        }

        public void BindKey(Keys key, ICommand command)
        {
            _keyBindings[key] = command;
        }

        public void HandleInput()
        {
            var keyboardState = Keyboard.GetState();
            foreach (var key in _keyBindings.Keys)
            {
                if (keyboardState.IsKeyDown(key))
                {
                    _keyBindings[key].Execute();
                }
            }
        }
    }
}
