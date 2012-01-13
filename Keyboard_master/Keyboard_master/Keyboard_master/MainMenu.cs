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
        private Vector2 songInfoPos = new Vector2(Dimensions.Width/2.0f, Dimensions.Height * 3.0f/4.0f);
        private const String infoHeaders = "Title:\n" +
                                           "Author:\n" +
                                           "Level:\n" +
                                           "Record:";
        private Vector2 songInfoOrigin;

        public MainMenu(Game1 game, IServiceProvider serviceProvider) : base(game, serviceProvider)
        {
            this.background = Content.Load<Texture2D>("Images/MainMenuPlaceholder2");

            this.songIndex = 1;

            this.songs = new List<SongIcon>();


            // TODO: SongIcons should be loaded from disk here (Nontrivial):
            for (int i = 0; i < 7; i++)
            {
                this.songs.Add(new SongIcon(serviceProvider));
            }

            this.songs.Add(new SongIcon(serviceProvider, "Songs/MaryHadALittleLamb/art")); // XXX: Hardcoded for now

            this.songs[this.songIndex].AddAnimation(new Animation(AnimationType.BOUNCE_SIZE, 0.15f, 700.0f, true));

            songInfoOrigin = new Vector2(this.font.MeasureString(infoHeaders).X,0.0f);// + new Vector2(Dimensions.Width/2.0f, Dimensions.Height/10.0f);

        }

        public override void ProcessNavigationCommand(NavigationCommand cmd)
        {
            if (cmd == NavigationCommand.BACK)
            {
                game.QuitGame();
            } 
            else if (cmd == NavigationCommand.LEFT)
            {
                this.songs[this.songIndex].TerminateAllAnimations();
                // C# uses a completely non-standard modulus operator, which accounts for the following line:
                this.songIndex = (this.songIndex + songs.Count - 1) % songs.Count;
                // Note for porting to a different language: this will still work, but "this.songIndex = (this.songIndex - 1) % songs.Count" is shorter
                this.songs[this.songIndex].AddAnimation(new Animation(AnimationType.BOUNCE_SIZE, 0.15f, 700.0f, true));
            } 
            else if (cmd == NavigationCommand.RIGHT)
            {
                this.songs[this.songIndex].TerminateAllAnimations();
                this.songIndex = (this.songIndex + 1) % songs.Count;
                this.songs[this.songIndex].AddAnimation(new Animation(AnimationType.BOUNCE_SIZE, 0.15f, 700.0f, true));
            }
            else if (cmd == NavigationCommand.SELECT)
            {
                game.SwitchToLevel(); //TODO: Pass in selected song data as parameter(s)    
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Wrap around display of the carousel (does not depend on other changes)
            base.Draw(gameTime, spriteBatch);

            if (this.songIndex - 1 >= 0) {
                this.songs[this.songIndex - 1].Draw(gameTime, spriteBatch, this.leftIconPos, 0.7f);
            }

                this.songs[this.songIndex].Draw(gameTime, spriteBatch, this.centerIconPos, 1.0f);
                spriteBatch.DrawString(this.font, infoHeaders, this.songInfoPos, Color.Black, 0.0f, songInfoOrigin, 1.0f, SpriteEffects.None, 0);

            //Adds a string to a batch of sprites for rendering using the specified font, text, position, color, rotation, origin, scale, effects and layer.
            
            if (this.songIndex + 1 < songs.Count)
            {
                this.songs[this.songIndex + 1].Draw(gameTime, spriteBatch, this.rightIconPos, 0.7f);
            }

        }

        public override void BeginTransitionIn(double duration)
        {
            base.BeginTransitionIn(duration);
            for (int i = this.songIndex - 1; i < this.songIndex + 2; i++)
            {
                if (i >= 0 && i < this.songs.Count)
                {
                    this.songs[i].AddAnimation(new Animation(AnimationType.FADE_IN, 255.0f, duration));
                }
            }
        }

        public override void BeginTransitionOut(double duration) {
            base.BeginTransitionOut(duration);
            for (int i = this.songIndex - 1; i < this.songIndex + 2; i++)
            {
                if (i >= 0 && i < this.songs.Count)
                {
                    this.songs[i].AddAnimation(new Animation(AnimationType.FADE_OUT, 255.0f, duration));
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
