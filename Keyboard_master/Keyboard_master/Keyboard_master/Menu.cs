using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;


namespace Keyboard_master
{
    class Menu : Screen
    {
        protected Texture2D background;


        public Menu(Game1 game, IServiceProvider serviceProvider): base(game, serviceProvider)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(this.background, Vector2.Zero, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        
        }
    }
}
