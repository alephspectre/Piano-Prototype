using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

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

            speedFactor = 0.1f;
            pointerLoc = 30.0f;
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

            Console.WriteLine(musicTime);
        }
    
    }
}
