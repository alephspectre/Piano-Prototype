using System;

namespace PianoGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (KeyboardMaster game = new KeyboardMaster())
            {
                game.Run();
            }
        }
    }
#endif
}

