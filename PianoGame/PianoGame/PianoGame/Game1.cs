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
    /// 

    public enum GameStatus //There is a lot of potential to perform some sort of clean 
                           //encapsulation of game state after the prototype stage.
                           // It will get much too messy with multiple levels.
    {
        mainMenu,
        playing,
        endScreen
    }

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;

        Vector2 textOffset = new Vector2(100, 40);

        Texture2D menuTex;
        Texture2D quarterNoteTex;
        Texture2D halfNoteTex;
        Texture2D wholeNoteTex;
        Texture2D staffTex;
#if MONOMAC
#else
        SpriteFont scoreFont;
#endif
        Song aSong;
        float interruptTimer = 0.0f; //Nice for people and also fixes problem where switching to game loses points

        GameStatus gameStatus = GameStatus.mainMenu;

        Staff staff;
        KeyboardManager keyboardManager;
		
		SoundEffect soundEngine;
		SoundEffectInstance soundEngineInstance;
        
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
			
#if MONOMAC
			graphics.PreferredBackBufferWidth = (int)(1280*0.8);
            graphics.PreferredBackBufferHeight = (int)(720*0.8);
#else
            graphics.PreferredBackBufferWidth = (int)(1280);
            graphics.PreferredBackBufferHeight = (int)(720);
			graphics.IsFullScreen = true;
#endif
            
            

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

            menuTex = Content.Load<Texture2D>("splash");

            staffTex = Content.Load<Texture2D>("staff");
            quarterNoteTex = Content.Load<Texture2D>("quarter_note");
            halfNoteTex = Content.Load<Texture2D>("half_note");
            wholeNoteTex = Content.Load<Texture2D>("whole_note");
			
#if MONOMAC
#else
	scoreFont = Content.Load<SpriteFont>("ClassicaBold");
#endif

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here

            switch (gameStatus)
            {
                case GameStatus.mainMenu:
                    if (Keyboard.GetState().GetPressedKeys().Length > 0)
                    {
                        gameStatus = GameStatus.playing;
                        interruptTimer = (float)gameTime.TotalGameTime.TotalMilliseconds;
                    }
                    break;
                case GameStatus.playing:
                    if ((float)gameTime.TotalGameTime.TotalMilliseconds - interruptTimer >= 3000.0f)
                    {
                        if (aSong != null && staff.status == SongStatus.loading)
                        {
                            staff.PlayMusic(aSong);
                            keyboardManager.Reset();
                        }
                        else
                        {
                            keyboardManager.Update(gameTime);
                        }
                        staff.Update(gameTime);
                        if (staff.status == SongStatus.finished)
                        {
                            interruptTimer = (float)gameTime.TotalGameTime.TotalMilliseconds;
                            gameStatus = GameStatus.endScreen;
                        }
                    }
                    break;
                case GameStatus.endScreen:
                    if ((float)gameTime.TotalGameTime.TotalMilliseconds - interruptTimer >= 200.0f)
                    {
                        if (Keyboard.GetState().GetPressedKeys().Length > 0)
                        {
                            gameStatus = GameStatus.playing;
                            staff.Reset();
                            keyboardManager.Reset();
                            interruptTimer = (float)gameTime.TotalGameTime.TotalMilliseconds;
                        }
                    }
                    break;
                default:
                    break;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
			
			//Console.WriteLine(gameTime.ElapsedGameTime.TotalMilliseconds);
			

            switch (gameStatus)
            {
                case GameStatus.mainMenu:
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    spriteBatch.Draw(menuTex, Vector2.Zero, Color.White);
                    spriteBatch.End();
                    break;
                case GameStatus.playing:
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    

                    if ((float)gameTime.TotalGameTime.TotalMilliseconds - interruptTimer >= 3000.0f)
                    {
                        spriteBatch.Draw(staffTex, Vector2.Zero, Color.White);

                        DrawNotes();

                        string scoreString = staff.songScore.ToString("N0");

                        // Score
#if MONOMAC
#else
                        spriteBatch.DrawString(scoreFont, scoreString, textOffset, Color.Black,
                            0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
#endif
                    }
                    else
                    {
                        string countDown = (3.0f - ((float)gameTime.TotalGameTime.TotalMilliseconds - interruptTimer)/1000.0).ToString("N0");

                        // Score
#if MONOMAC
#else
                        spriteBatch.DrawString(scoreFont, countDown, new Vector2(1280/2,720/2-50.0f), Color.Black,
                            0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
#endif
                    }
                    spriteBatch.End();
                    break;
                case GameStatus.endScreen:

                    string conclusion = "You scored: " + (staff.songScore / staff.songPerfectScore).ToString("P2") + "\n Press any key to try again.";
                    spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                    // Score
#if MONOMAC
#else
                    spriteBatch.DrawString(scoreFont, conclusion, textOffset, Color.Black,
                        0, Vector2.Zero, 1.0f, SpriteEffects.None, 0.5f);
#endif
                    spriteBatch.End();
                    break;
                default:
                    break;
            }

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
