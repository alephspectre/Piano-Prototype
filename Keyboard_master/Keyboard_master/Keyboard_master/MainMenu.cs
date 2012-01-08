using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using InputSymbols;
using Microsoft.Xna.Framework;


namespace Keyboard_master
{
    class MainMenu : Menu
    {
        private List<SongIcon> songs;
        private int songIndex;

        private Vector2 leftIconPos = new Vector2(Dimensions.Width / 5.0f, Dimensions.Height / 2.0f);
        private Vector2 centerIconPos = Dimensions.Center;
        private Vector2 rightIconPos = new Vector2(Dimensions.Width * 4.0f / 5.0f, Dimensions.Height / 2.0f);

        public MainMenu(Game1 game, IServiceProvider serviceProvider) : base(game, serviceProvider)
        {
            this.background = Content.Load<Texture2D>("Images/MainMenuPlaceholder2");

            this.songIndex = 1;

            this.songs = new List<SongIcon>();
            this.songs.Add(new SongIcon(serviceProvider));
            this.songs.Add(new SongIcon(serviceProvider));
            this.songs.Add(new SongIcon(serviceProvider));
        }

        public override void ProcessNavigationCommand(NavigationCommand cmd)
        {
            if (cmd == NavigationCommand.BACK)
            {
                game.QuitGame();
            } 
            else if (cmd == NavigationCommand.LEFT)
            {
                this.songIndex = (this.songIndex - 1) % songs.Count;
            } 
            else if (cmd == NavigationCommand.RIGHT)
            {
                this.songIndex = (this.songIndex + 1) % songs.Count;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (this.songIndex - 1 >= 0) {
                this.songs[this.songIndex - 1].Draw(gameTime, spriteBatch, this.leftIconPos, 0.7f);
            }

            this.songs[this.songIndex].Draw(gameTime, spriteBatch, this.centerIconPos, 1.0f);
            
            if (this.songIndex + 1 < songs.Count)
            {
                this.songs[this.songIndex + 1].Draw(gameTime, spriteBatch, this.rightIconPos, 0.7f);
            }

        }

        public override void Update(GameTime gameTime)
        {
            for (int i = this.songIndex - 1; i < this.songIndex + 2; i++)
            {
                if (i >= 0 && i < this.songs.Count)
                {
                    this.songs[i].Update(gameTime);
                }
            }
        }
    }
}
