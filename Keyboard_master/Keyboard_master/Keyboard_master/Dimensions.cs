using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Keyboard_master
{
    static class Dimensions
    {
        private static int _screenWidth;
        private static int _screenHeight;
        private static bool _fullscreen;

        public static void Initialize()
        {
            _screenWidth = 1280;
            _screenHeight = 720;
            _fullscreen = true;
        }

        public static void Initialize(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _fullscreen = true;
        }

        public static void Initialize(int screenWidth, int screenHeight, bool fullscreen)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _fullscreen = fullscreen;
        }

        public static int Width {
            get { return _screenWidth; }
        }

        public static int Height
        {
            get { return _screenHeight; }
        }
        
        public static Vector2 Center
        {
            get { return new Vector2(_screenWidth / 2.0f, _screenHeight / 2.0f); }
        }

        public static bool FullScreen
        {
            get { return _fullscreen; }
        }
    }
}
