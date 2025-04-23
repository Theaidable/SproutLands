using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SharpDX.Win32;
using System.Collections.Generic;


namespace SproutLands.Classes.CommandPattern
{
    public class InputHandler
    {
        private static InputHandler instance;

        public static InputHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputHandler();
                }
                return instance;
            }
        }
        private InputHandler() { }

        private Dictionary<Keys, ICommand> keybindsUpdate = new Dictionary<Keys, ICommand>();
        private Dictionary<Keys, ICommand> keybindsButtonDown = new Dictionary<Keys, ICommand>();
        private Dictionary<Keys,ICommand> keybindsButtonUp = new Dictionary<Keys, ICommand>();
        private KeyboardState previousKeyState;

        private Stack<ICommand> executedCommands = new Stack<ICommand>();

        private Stack<ICommand> unExecutedCommands = new Stack<ICommand>();
        public void AddUpdateCommand(Keys inputKey, ICommand command)
        {
            keybindsUpdate.Add(inputKey, command);
        }
        public void AddButtonDownCommand(Keys inputKey, ICommand command)
        {
            keybindsButtonDown.Add(inputKey, command);
        }

        public void AddButtonUpCommand(Keys inputKey,ICommand command)
        {
            keybindsButtonUp.Add(inputKey, command);
        }

        public void Execute()
        {
            KeyboardState keyState = Keyboard.GetState();


            foreach (var pressedKey in keyState.GetPressedKeys())
            {
                if (keybindsUpdate.TryGetValue(pressedKey, out ICommand cmd))
                {
                    cmd.Execute();
                }

                if (!previousKeyState.IsKeyDown(pressedKey) && keyState.IsKeyDown(pressedKey))
                {
                    if (keybindsButtonDown.TryGetValue(pressedKey, out ICommand cmdBd))
                    {
                        cmdBd.Execute();
                        executedCommands.Push(cmdBd);
                        unExecutedCommands.Clear();
                    }
                }
            }

            foreach (var pressedKey in previousKeyState.GetPressedKeys())
            {
                if (!keyState.IsKeyDown(pressedKey) && keybindsButtonUp.TryGetValue(pressedKey, out ICommand cmdBu))
                {
                    cmdBu.Execute();
                }
            }


            previousKeyState = keyState;

        }
    }
}
