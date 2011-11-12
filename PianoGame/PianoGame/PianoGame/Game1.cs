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

namespace PianoGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D noteTex;
        Song aSong;

        Staff staff;
        
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
            // TODO: Add your initialization logic here
            staff = new Staff();
            Note a = new Note(new Vector2(2000.0f, 0.0f), 0.0f, 0.0f, 0, 0);
            Note b = new Note(new Vector2(5000.0f, 100.0f), 0.0f, 0.0f, 0, 0);
            staff.AddNote(a);
            staff.AddNote(b);
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
            noteTex = Content.Load<Texture2D>("note");
            // TODO: use this.Content to load your game content here

            aSong = Content.Load<Song>("test");
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (aSong != null && staff.status == (byte)SongStatus.loading)
            { 
                staff.PlayMusic(aSong);
            }

            staff.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            DrawNotes();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawNotes()
        {
            foreach (Note note in staff.GetCurrentNotes())
            {
                DrawNote(note);
            }
        }

        public void DrawNote(Note note)
        {
            Texture2D tex;
            switch (note.type)
            {
                default:
                    tex = noteTex;
                    break;
            }

            Vector2 newPos = new Vector2(staff.GetNoteX(note.position.X), note.position.Y);
            spriteBatch.Draw(tex, newPos, Color.White);
        }
    }
}
