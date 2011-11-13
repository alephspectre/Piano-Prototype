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

        Texture2D quarterNoteTex;
        Texture2D halfNoteTex;
        Texture2D wholeNoteTex;
        Texture2D staffTex;
        Song aSong;

        Staff staff;
        KeyboardManager keyboardManager;
        
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
            //Set the screen height and width       
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            

            //Apply the changes made to the device
            graphics.ApplyChanges();


            staff = new Staff();

            const float EPOS = 251.0f;
            const float DPOS = 251.0f + 27.0f;
            const float CPOS = 251.0f + 54.0f;

            const Keys E_KEY = Keys.D;
            const Keys D_KEY = Keys.S;
            const Keys C_KEY = Keys.A;

            //BEGIN MARY HAD A LITTLE LAMB

            //dsasddd-sss-ddd- dsasddddssdsa a= middle C s = D d =E on piano

            staff.AddNote(new Note(new Vector2(2050.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(2550.0f, DPOS), 0.0f, 0.0f, NoteType.quarter, D_KEY)); //D
            staff.AddNote(new Note(new Vector2(3050.0f, CPOS), 0.0f, 0.0f, NoteType.quarter, C_KEY)); //C
            staff.AddNote(new Note(new Vector2(3550.0f, DPOS), 0.0f, 0.0f, NoteType.quarter, D_KEY)); //D
            staff.AddNote(new Note(new Vector2(4050.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(4550.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(5050.0f, EPOS), 0.0f, 0.0f, NoteType.half, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(6050.0f, DPOS), 0.0f, 0.0f, NoteType.quarter, D_KEY)); //D
            staff.AddNote(new Note(new Vector2(6550.0f, DPOS), 0.0f, 0.0f, NoteType.quarter, D_KEY)); //D
            staff.AddNote(new Note(new Vector2(7050.0f, DPOS), 0.0f, 0.0f, NoteType.half, D_KEY)); //D
            staff.AddNote(new Note(new Vector2(8050.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(8550.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(9050.0f, EPOS), 0.0f, 0.0f, NoteType.half, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(10050.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(10550.0f, DPOS), 0.0f, 0.0f, NoteType.quarter, D_KEY)); //D
            staff.AddNote(new Note(new Vector2(11050.0f, CPOS), 0.0f, 0.0f, NoteType.quarter, C_KEY)); //C
            staff.AddNote(new Note(new Vector2(11550.0f, DPOS), 0.0f, 0.0f, NoteType.quarter, D_KEY)); //D
            staff.AddNote(new Note(new Vector2(12051.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(12550.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(13050.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(13550.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(14050.0f, DPOS), 0.0f, 0.0f, NoteType.quarter, D_KEY)); //D
            staff.AddNote(new Note(new Vector2(14550.0f, DPOS), 0.0f, 0.0f, NoteType.quarter, D_KEY)); //D
            staff.AddNote(new Note(new Vector2(15050.0f, EPOS), 0.0f, 0.0f, NoteType.quarter, E_KEY)); //E
            staff.AddNote(new Note(new Vector2(15550.0f, DPOS), 0.0f, 0.0f, NoteType.quarter, D_KEY)); //D
            staff.AddNote(new Note(new Vector2(16050.0f, CPOS), 0.0f, 0.0f, NoteType.whole, C_KEY)); //C
            //END MARY HAD A LITTLE LAMB

            keyboardManager = new KeyboardManager(staff);

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
            staffTex = Content.Load<Texture2D>("staff");
            quarterNoteTex = Content.Load<Texture2D>("quarter_note");
            halfNoteTex = Content.Load<Texture2D>("half_note");
            wholeNoteTex = Content.Load<Texture2D>("whole_note");
            // TODO: use this.Content to load your game content here

            aSong = Content.Load<Song>("MHALL");
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

            keyboardManager.Update(gameTime);
            staff.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(staffTex, Vector2.Zero, Color.White);
            DrawNotes();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawNotes()
        {
            foreach (Note note in staff.GetCurrentNotes())
            {
                if (note.visible)
                {
                    DrawNote(note);
                }
            }
        }

        public void DrawNote(Note note)
        {
            Texture2D tex;
            switch (note.type)
            {
                case NoteType.quarter:
                    tex = quarterNoteTex;
                    break;
                case NoteType.half:
                    tex = halfNoteTex;
                    break;
                case NoteType.whole:
                    tex = wholeNoteTex;
                    break;
                default:
                    tex = quarterNoteTex;
                    break;
            }

            Vector2 newPos = new Vector2(staff.GetNoteX(note.position.X), note.position.Y);
            spriteBatch.Draw(tex, newPos, Color.White);
        }
    }
}
