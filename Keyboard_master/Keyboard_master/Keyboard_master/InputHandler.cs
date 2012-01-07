using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using InputSymbols;

namespace Keyboard_master
{
    class InputHandler
    {
        public bool computerKeyboardActive;
        public bool musicalKeyboardActive;

        private KeyboardState oldCmpKeyboardState;

        public InputHandler()
        {
            computerKeyboardActive = true;
            musicalKeyboardActive = false;
            oldCmpKeyboardState = Keyboard.GetState();
        }

        public void UpdateHandlerForScreen(Screen screen)
        {
            if (computerKeyboardActive)
            {
                KeyboardState newState = Keyboard.GetState();
                if (HasCmpKeyDownChanged(newState, Keys.Escape))
                { 
                    //Escape down
                    screen.ProcessNavigationCommand(NavigationCommand.BACK);
                }
                if (HasCmpKeyDownChanged(newState, Keys.Left))
                {
                    //Escape down
                    screen.ProcessNavigationCommand(NavigationCommand.LEFT);
                }
                if (HasCmpKeyDownChanged(newState, Keys.Right))
                {
                    //Escape down
                    screen.ProcessNavigationCommand(NavigationCommand.RIGHT);
                }
                oldCmpKeyboardState = newState;
            }
        
        }

        public void Reset()
        {
            oldCmpKeyboardState = Keyboard.GetState();
        }

        private bool HasCmpKeyDownChanged(KeyboardState newState, Keys key) {
            return (newState.IsKeyDown(key) && !oldCmpKeyboardState.IsKeyDown(key));
        }

        private bool HasCmpKeyUpChanged(KeyboardState newState, Keys key)
        {
            return (newState.IsKeyUp(key) && !oldCmpKeyboardState.IsKeyUp(key));
        }

    }
}
