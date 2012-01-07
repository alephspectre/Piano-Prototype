using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Keyboard_master
{

    // Summary:
    //     A SongIcon is a menu item that allows a song to be selected.
    //     It displays an image for the selected song and stores information associated with
    //     the song: the name, artist, level of difficulty, and top score (for the current profile?)
    //     After it has been active for a while (maybe .5 sec), it should start playing the associated 
    //     music.
    class SongIcon: IDisposable
    {
        private Texture2D artwork;
        private Vector2 centerPos;
        float size;

        public SongIcon(IServiceProvider serviceProvider, Vector2 centerPos, float size)
        {
            content = new ContentManager(serviceProvider, "Content");
            this.artwork = Content.Load<Texture2D>("Images/SongIconNotFound");
            this.centerPos = centerPos;
            this.size = size;
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.artwork,
                             this.centerPos,
                             this.artwork.Bounds,
                             Color.White,
                             0.0f,
                             new Vector2(this.artwork.Bounds.Width / 2.0f, this.artwork.Bounds.Height / 2.0f),
                             new Vector2(this.size, this.size),
                             SpriteEffects.None,
                             0);
        }

        public void Update(GameTime gameTime)
        {

        }


    }
}
