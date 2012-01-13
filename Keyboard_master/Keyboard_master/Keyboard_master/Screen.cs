using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using InputSymbols;

namespace Keyboard_master
{
    abstract class Screen: IDisposable
    {
        protected enum TransitionState
        {
            IN,
            NONE,
            OUT
        };

        protected Game1 game;
        protected double transitionDuration; // Only used during a transition; set when begin In/Out
        protected TransitionState transitionState;
        protected SpriteFont font;

        public Screen(Game1 game, IServiceProvider serviceProvider)
        {
            this.game = game;
            content = new ContentManager(serviceProvider, "Content");
            this.transitionDuration = 0.0f;
            this.transitionState = TransitionState.NONE;
            this.font = Content.Load<SpriteFont>("Fonts/Classica/Classica");
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

        public virtual void BeginTransitionIn(double duration)
        {
            this.transitionDuration = duration; //milliseconds
            this.transitionState = TransitionState.IN;
        }

        public virtual void BeginTransitionOut(double duration)
        {
            this.transitionDuration = duration; //milliseconds
            this.transitionState = TransitionState.OUT;
        }

        public abstract void ProcessNavigationCommand(NavigationCommand cmd);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);

    }
}
