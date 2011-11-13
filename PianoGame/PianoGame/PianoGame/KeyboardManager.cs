using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace PianoGame
{
    class KeyboardManager
    {
        /*
         * There is only one instance of this class in the entire game. It keeeps track of
         * which notes were pressed in the past and notifies registered objects whenever a key event
         * (key up/down) happens.
         */
        public Dictionary<Keys, bool> possibleKeys;
        public List<Staff> registeredListeners; //TODO: change list type to accept any objects that implement a listening interface

        public KeyboardManager(Staff aStaff)
        { 
            possibleKeys = new Dictionary<Keys,bool>();
            registeredListeners = new List<Staff>();
            registeredListeners.Add(aStaff);
            SetupWithComputerKeyboard();
        }

        public void SetupWithComputerKeyboard ()
        {
            possibleKeys[Keys.A] = false;
            possibleKeys[Keys.S] = false;
            possibleKeys[Keys.D] = false;
            possibleKeys[Keys.F] = false;
            possibleKeys[Keys.G] = false;
            possibleKeys[Keys.H] = false;
            possibleKeys[Keys.J] = false;
            possibleKeys[Keys.K] = false;
            possibleKeys[Keys.L] = false;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();

            for (int i = 0; i < possibleKeys.Count; i++)
            {
                Keys aKey = possibleKeys.ElementAt(i).Key;
                if (kb.IsKeyDown(aKey) && possibleKeys[aKey] == false)
                {
                    Console.WriteLine("Key down!");
                    possibleKeys[aKey] = true;
                    foreach (Staff aStaff in registeredListeners)
	                {
                        aStaff.NotifyKeyDown(aKey);
	                }
                    
                }
                else if (kb.IsKeyUp(aKey) && possibleKeys[aKey] == true)
                {
                    Console.WriteLine("Key up!");
                    possibleKeys[aKey] = false;
                    foreach (Staff aStaff in registeredListeners)
                    {
                        aStaff.NotifyKeyUp(aKey);
                    }
                }
            }

        }
    }
}
