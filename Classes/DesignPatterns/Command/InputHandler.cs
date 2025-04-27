using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace SproutLands.Classes.DesignPatterns.Command
{
    public enum MouseButton
    {
        Left,
        Right,
        Middle
    }

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

        private Dictionary<Keys, ICommand> keybindsUpdate = new Dictionary<Keys, ICommand>();
        private Dictionary<Keys, ICommand> keybindsButtonDown = new Dictionary<Keys, ICommand>();
        private Dictionary<Keys, ICommand> keybindsButtonUp = new Dictionary<Keys, ICommand>();
        private Dictionary<MouseButton, ICommand> mouseButtonDownBinds = new Dictionary<MouseButton, ICommand>();

        private KeyboardState previousKeyState;
        private MouseState previousMouseState;

        private InputHandler() { }

        //Update keybind, altså hvor man kan holde knappen nede
        public void AddUpdateCommand(Keys inputKey, ICommand command)
        {
            keybindsUpdate.Add(inputKey, command);
        }

        //ButtonDown keybind, altså når man klikker på en knap
        public void AddButtonDownCommand(Keys inputKey, ICommand command)
        {
            keybindsButtonDown.Add(inputKey, command);
        }

        //ButtonUp keybind, altså når man slipper en knap
        public void AddButtonUpCommand(Keys inputKey, ICommand command)
        {
            keybindsButtonUp.Add(inputKey, command);
        }

        //MouseButtonDown, altså når man klikker med musen
        public void AddMouseButtonDownCommand(MouseButton button, ICommand command)
        {
            mouseButtonDownBinds[button] = command;
        }

        public void Execute()
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            //Execute Update
            foreach (var key in keybindsUpdate.Keys)
            {
                if (keyState.IsKeyDown(key))
                {
                    keybindsUpdate[key].Execute();
                    break;
                }
            }

            //Execute ButtonDown
            foreach (var key in keybindsButtonDown.Keys)
            {
                if (!previousKeyState.IsKeyDown(key) && keyState.IsKeyDown(key))
                {
                    keybindsButtonDown[key].Execute();
                }
            }

            //Execute ButtonUp
            foreach (var key in keybindsButtonUp.Keys)
            {
                if (previousKeyState.IsKeyDown(key) && !keyState.IsKeyDown(key))
                {
                    keybindsButtonUp[key].Execute();
                }
            }

            //Execute MouseButtonDown
            if (previousMouseState.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
            {
                if (mouseButtonDownBinds.TryGetValue(MouseButton.Left, out var cmd))
                {
                    cmd.Execute();
                }
            }

            previousKeyState = keyState;
            previousMouseState = mouseState;
        }
    }
}
