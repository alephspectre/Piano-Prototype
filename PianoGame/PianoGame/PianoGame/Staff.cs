using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PianoGame
{
    class Staff
    {
        public float leftTimeSpan; //The time used to take elapsed notes into account for rendering and processing
        public float rightTimeSpan; //The time delta considered to be on screen at once
        public float musicTime; //The time the music is currently

        public List<Note> noteList; //All of the notes in the song
        public int currentIndex; //The index in noteList that roughly corresponds to musicTime

        public Staff()
        { 
            noteList = new List<Note>();
            currentIndex = 0;
            musicTime = 0.0f;

            //TODO: Figure out what values these should have
            leftTimeSpan = 10.0f;
            rightTimeSpan = 30.0f;
        }

        public void AddNote(Note note)
        {
            noteList.Add(note);
        }

        public List<Note> GetCurrentNotes()
        {
            return noteList;
        }
    }
}
