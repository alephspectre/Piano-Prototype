using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PianoGame
{
    public enum NoteType
    {
        quarter,
        half,
        whole
    }

    public class Note
    {
        public Vector2 position; //The position consists of a y position and a time
        public float leftRange;
        public float rightRange; //
        public NoteType type; //The type of the note determines the image associated with it and its positional offset
        public Keys keyToPress; //The correct key associated with this note
        public bool visible = true;
        public Note(Vector2 pos, float lR, float rR, NoteType tpe, Keys kTP)
        {
            position = pos;
            leftRange = lR;
            rightRange = rR;
            type = tpe;
            keyToPress = kTP;
        }
    }
}
