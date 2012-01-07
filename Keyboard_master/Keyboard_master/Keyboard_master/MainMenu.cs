using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using InputSymbols;


namespace Keyboard_master
{
    class MainMenu : Menu
    {
        public MainMenu(Game1 game, IServiceProvider serviceProvider) : base(game, serviceProvider)
        {
            background = Content.Load<Texture2D>("Images/MainMenuPlaceholder");
        }

        public override void ProcessNavigationCommand(NavigationCommand cmd)
        {
            if (cmd == NavigationCommand.BACK)
            {
                game.QuitGame();
            }
        }
    }
}
