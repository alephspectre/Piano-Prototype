﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InputSymbols;

namespace Keyboard_master
{
    class Level: Screen
    {

        public Level(Game1 game, IServiceProvider serviceProvider): base(game, serviceProvider)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
        
        }

        public override void ProcessNavigationCommand(NavigationCommand cmd)
        {
            if (cmd == NavigationCommand.BACK)
            {
                game.SwitchToMainMenu();
            } 
        }

    }
}
