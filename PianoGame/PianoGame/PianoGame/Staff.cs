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
    public enum SongStatus
    {
        loading,
        waiting,
        stopped,
        playing,
        finished
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

        public SongStatus status;

        public float msLeeway = 150.0f; //How far off you can be to get a note

        public float songScore;
        public float songPerfectScore;

        Song song;
        double songDuration;

        public Staff()
        {
            songScore = 0.0f;
            songPerfectScore = 0.0f;

            noteList = new List<Note>();
            currentIndex = 0;
            musicTime = 0.0d;

            status = SongStatus.loading;

            //TODO: Figure out what values these should have
            leftTimeSpan = 10.0f;
            rightTimeSpan = 30.0f;

            speedFactor = 0.2f;
            pointerLoc = 306.0f;
        }

        public void PlayMusic(Song sng)
        {
            song = sng;
            songDuration = sng.Duration.TotalMilliseconds;
            MediaPlayer.Play(song);
            status = SongStatus.waiting;
        }

        public void PlayMusic()
        {
            MediaPlayer.Play(song);
            status = SongStatus.waiting;
        }

        public void AddNote(Note note)
        {
            noteList.Add(note);
            songPerfectScore += 10.0f;
        }

        public List<Note> GetCurrentNotes()
        {
            //TODO: Optimize this using currentIndex 
            return noteList;
        }

        public List<Note> GetCurrentNotesForKey(Keys aKey)
        {
            //TODO: Optimize this using currentIndex
            List<Note> currNoteList = new List<Note>();
            foreach (Note note in noteList)
            {
                if (note.keyToPress == aKey)
                {
                    currNoteList.Add(note);
                }
            }
            return currNoteList;
        }

        public float GetNoteX(float noteTime)
        {

            return pointerLoc + (noteTime - (float)musicTime) * speedFactor;
        }

        public void Update(GameTime gameTime)
        {
            if (status == SongStatus.waiting && MediaPlayer.State == MediaState.Playing)
            {
                status = SongStatus.playing;
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

            if (musicTime >= songDuration - 1000.0d) //We should make sure that every song has at least 1s blank at end
            {
                status = SongStatus.finished;
            }
        }

        public void NotifyKeyDown(Keys aKey)
        {
            bool gotANote = false;
            foreach (Note note in GetCurrentNotesForKey(aKey))
            {
                if (Math.Abs(note.position.X - (float)musicTime) < msLeeway)
                {
                    note.visible = false;
                    gotANote = true;
                }
            }

            if (gotANote)
            {
                songScore += 10.0f;
            }
            else
            {
                songScore -= 10.0f;
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

        public void Reset()
        {
            songScore = 0.0f;
            currentIndex = 0;
            musicTime = 0.0d;
            foreach (Note note in noteList)
            {
                note.visible = true;
            }
            status = SongStatus.loading;
        }
    
    }
}
