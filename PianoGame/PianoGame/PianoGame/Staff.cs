using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PianoGame
{
    enum SongStatus
    {
        loading,
        waiting,
        stopped,
        playing
    }

    class Staff
    {
        public float speedFactor; //How fast notes move from left to right. This should be scaled by screenwidth at some point
        public float pointerLoc;

        public float leftTimeSpan; //The time used to take elapsed notes into account for rendering and processing
        public float rightTimeSpan; //The time delta considered to be on screen at once
        public double musicTime; //The time the music is currently

        public List<Note> noteList; //All of the notes in the song
        public int currentIndex; //The index in noteList that roughly corresponds to musicTime

        public byte status;

        Song song;

        public Staff()
        {
            noteList = new List<Note>();
            currentIndex = 0;
            musicTime = 0.0d;

            status = (byte)SongStatus.loading;

            //TODO: Figure out what values these should have
            leftTimeSpan = 10.0f;
            rightTimeSpan = 30.0f;

            speedFactor = 0.2f;
            pointerLoc = 236.0f;
        }

        public void PlayMusic(Song sng)
        {
            song = sng;
            MediaPlayer.Play(song);
            status = (byte)SongStatus.waiting;
        }

        public void PlayMusic()
        {
            MediaPlayer.Play(song);
            status = (byte)SongStatus.waiting;
        }

        public void AddNote(Note note)
        {
            noteList.Add(note);
        }

        public List<Note> GetCurrentNotes()
        {
            //TODO: Optimize this using currentIndex 
            return noteList;
        }

        public float GetNoteX(float noteTime)
        {

            return pointerLoc + (noteTime - (float)musicTime) * speedFactor;
        }

        public void Update(GameTime gameTime)
        {
            if (status == (byte)SongStatus.waiting && MediaPlayer.State == MediaState.Playing)
            {
                status = (byte)SongStatus.playing;
            }

            musicTime = MediaPlayer.PlayPosition.TotalMilliseconds;
            if (noteList.Count > 0)
            {

                while ((float)musicTime > noteList[currentIndex].position.X && (currentIndex < noteList.Count - 1)) 
                    {
                        currentIndex++;
                        Console.WriteLine(currentIndex);
                    }
            }

            /*KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.A))
            {
                foreach (Note note in GetCurrentNotes())
                {
                    if (Math.Abs(note.position.X - (float)musicTime) < 200.0f) {
                        note.visible = false;
                    }
                }
            }*/

            //Console.WriteLine(musicTime);
        }

        public void NotifyKeyDown(Keys aKey)
        {
            foreach (Note note in GetCurrentNotes())
            {
                if (Math.Abs(note.position.X - (float)musicTime) < 200.0f)
                {
                    note.visible = false;
                }
            }
        }

        public void NotifyKeyUp(Keys aKey)
        {
            /*foreach (Note note in GetCurrentNotes())
            {
                if (Math.Abs(note.position.X - (float)musicTime) < 200.0f)
                {
                    note.visible = false;
                }
            }*/
        }
    
    }
}
