using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEngine.Systems
{
    public class InputSystem : ISystem
    {
        private KeyboardState currentKeyState;
        private KeyboardState previousKeyState;
        private Dictionary<Keys, Command> keyBindings;
        public InputSystem()
        {
            keyBindings = new Dictionary<Keys, Command>
            {
                {Keys.W, Command.up },
                {Keys.A, Command.left },
                {Keys.S, Command.down },
                {Keys.D, Command.right },
                {Keys.Space, Command.jump }
            };
        }

        public void Update(Scene scene, GameTime gameTime)
        {
            InputBuffer.Instance.Purge();
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            // if key for a command is down, add it to command buffer
            foreach (var keyBind in keyBindings)
            {
                if (IsPressed(keyBind.Key))
                {
                    InputBuffer.Instance.Add(keyBind.Value);
                    Debug.WriteLine($"---\n{keyBind.Key}: {keyBind.Value}");
                }
            }

   


        }

        private bool IsPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        private bool JustPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }

        private bool JustRelease(Keys key)
        {
            return !currentKeyState.IsKeyDown(key) && previousKeyState.IsKeyDown(key);
        }

    }
}
