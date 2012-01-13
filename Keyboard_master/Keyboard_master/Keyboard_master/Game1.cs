using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Keyboard_master
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        enum GameState // XXX: May actually only need two states. Address this again later
        {
            MENU,
            LEVEL,
            LEVEL_TO_MENU,
            MENU_TO_LEVEL,
            MENU_TO_MENU
        };

        Screen activeScreen;
        Screen lastScreen;

        double transitionCounter; //Used in transitions between states. Switch happens when timer reaches 0.

        GameState currState;

        InputHandler inputHandler;
        

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Initial Screen resolution
            Dimensions.Initialize(1280, 720, false);
            //Dimensions.Initialize(640, 480, false);

            ConfirmScreenDimensions();

            currState = GameState.MENU;
            transitionCounter = 0.0d;
            inputHandler = new InputHandler();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadMainMenu();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            inputHandler.UpdateHandlerForScreen(activeScreen);

            switch (currState)
            {
                case GameState.MENU:
                    activeScreen.Update(gameTime);
                    break;
                case GameState.LEVEL:
                    //TODO
                    break;
                case GameState.LEVEL_TO_MENU:
                    //TODO
                    lastScreen.Update(gameTime);
                    activeScreen.Update(gameTime);
                    UpdateTransitionCounter(gameTime);
                    break;
                case GameState.MENU_TO_LEVEL:
                    //TODO
                    lastScreen.Update(gameTime);
                    activeScreen.Update(gameTime);
                    UpdateTransitionCounter(gameTime);
                    break;
                case GameState.MENU_TO_MENU:
                    //TODO
                    UpdateTransitionCounter(gameTime);
                    break;
                default:
                    //IF IT GETS HERE, SOMETHING IS VERY BROKEN :(
                    break;
            }
                    

            base.Update(gameTime);
        }

        private void UpdateTransitionCounter(GameTime gameTime)
        {
            transitionCounter -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (transitionCounter <= 0.0d)
            {
                lastScreen.Dispose();
                lastScreen = null;
                switch (currState) {
                    case GameState.LEVEL_TO_MENU:
                        currState = GameState.MENU;
                        break;
                    case GameState.MENU_TO_LEVEL:
                        currState = GameState.LEVEL;
                        break;
                    case GameState.MENU_TO_MENU:
                        currState = GameState.MENU;
                        break;
                    default:
                        Console.WriteLine("Unhandled Transition Encountered when terminating transition");
                        break;
                }
            }
        }

        private void LoadMainMenu()
        {
            activeScreen = new MainMenu(this, Services);
        }

        private bool SetTransitionToMenuWithDuration(Menu newMenu, double duration)
        {
            // Sets currState appropriately; Returns whether transition is valid
            switch (currState)
            {
                case GameState.LEVEL:
                    currState = GameState.LEVEL_TO_MENU;
                    break;
                case GameState.MENU:
                    currState = GameState.MENU_TO_MENU;
                    break;
                default:
                    //Unable to transition
#if DEBUG
                    Console.WriteLine("Menu transition attempted unsuccessfully");
#endif
                    return false;
            }

            //For all transitions
            lastScreen = activeScreen;
            activeScreen = newMenu;

            transitionCounter = duration;
            lastScreen.BeginTransitionOut(duration);
            activeScreen.BeginTransitionIn(duration);
            return true;
        }

        public void ConfirmScreenDimensions()
        {
            graphics.PreferredBackBufferWidth = Dimensions.Width;
            graphics.PreferredBackBufferHeight = Dimensions.Height;
            graphics.IsFullScreen = Dimensions.FullScreen;
            graphics.ApplyChanges();
        }

        public void SwitchToMainMenu() //Only switches if game is in level or other menu
        {
#if DEBUG
            Console.WriteLine("Attempting transition to Settings Menu");
#endif
            SetTransitionToMenuWithDuration(new MainMenu(this, Services), 500.0d); // .5 second transition    
        }

        public void SwitchToSettingsMenu() //Only switches if game is in level or other menu
        {
#if DEBUG
            Console.WriteLine("Attempting transition to Main Menu");
#endif
            SetTransitionToMenuWithDuration(new SettingsMenu(this, Services), 500.0d); // .5 second transition    
        }

        public void SwitchToLevel()
        { 
            //TODO: Integrate with actual game files so that we can load a different level
#if DEBUG
            Console.WriteLine("Attempting to transition to Debug Level; proper level not coded");
#endif
            // Sets currState appropriately; Returns whether transition is valid
            switch (currState)
            {
                case GameState.MENU:
                    currState = GameState.MENU_TO_LEVEL;
                    break;
                default:
                    //Unable to transition
#if DEBUG
                    Console.WriteLine("Level transition attempted unsuccessfully");
#endif
                    return;
            }

            //For all transitions
            lastScreen = activeScreen;
            activeScreen = new Level(this, Services);

            double transitionDuration = 500.0d;
            transitionCounter = transitionDuration;
            lastScreen.BeginTransitionOut(transitionDuration);
            activeScreen.BeginTransitionIn(transitionDuration);
        }

        public void QuitGame()
        {
            this.Exit();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            switch (currState)
            {
                case GameState.MENU:
                    activeScreen.Draw(gameTime, spriteBatch);
                    break;
                case GameState.LEVEL:
                    //TODO
                    break;
                case GameState.LEVEL_TO_MENU:
                    //TODO
                    lastScreen.Draw(gameTime, spriteBatch);
                    activeScreen.Draw(gameTime, spriteBatch);
                    break;
                case GameState.MENU_TO_LEVEL:
                    //TODO
                    lastScreen.Draw(gameTime, spriteBatch);
                    activeScreen.Draw(gameTime, spriteBatch);
                    break;
                case GameState.MENU_TO_MENU:
                    //TODO
                    lastScreen.Draw(gameTime, spriteBatch);
                    activeScreen.Draw(gameTime, spriteBatch);
                    break;
                default:
                    //IF IT GETS HERE, SOMETHING IS VERY BROKEN :(
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
