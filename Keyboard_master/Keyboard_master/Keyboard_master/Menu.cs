using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text;


namespace Keyboard_master
{
    class Menu : IDisposable
    {
        protected Texture2D background;

        public Menu(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Content");
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
        }

        public void Update(GameTime gameTime)
        {
        
        }

        public ContentManager Content 
        {
            get { return content; }  
        }
        ContentManager content;

        public void Dispose()
        {
            Content.Unload();
        }



    }
}
