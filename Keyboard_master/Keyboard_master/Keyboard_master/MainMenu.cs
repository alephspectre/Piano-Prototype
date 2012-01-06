using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;


namespace Keyboard_master
{
    class MainMenu : Menu
    {
        public MainMenu(IServiceProvider serviceProvider): base(serviceProvider)
        {
            background = Content.Load<Texture2D>("Images/Main_Menu_Placeholder");
        }
    }
}
