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

        public MainMenu(Game1 game, IServiceProvider serviceProvider) : base(game, serviceProvider)
        {
            this.background = Content.Load<Texture2D>("Images/MainMenuPlaceholder2");

            this.songIndex = 0;

            this.songs = new List<SongIcon>();
            this.songs.Add(new SongIcon(serviceProvider, Dimensions.Center,1.0f));
            this.songs.Add(new SongIcon(serviceProvider, new Vector2(Dimensions.Width/5.0f,Dimensions.Height/2.0f), 0.7f));
            this.songs.Add(new SongIcon(serviceProvider, new Vector2(Dimensions.Width*4.0f/5.0f,Dimensions.Height/2.0f), 0.7f));
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

            for (int i = this.songIndex - 1; i < this.songIndex + 3; i++)
            {
                if (i >= 0 && i < this.songs.Count)
                {
                    this.songs[i].Draw(gameTime, spriteBatch);
                }
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
