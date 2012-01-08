using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

#if DEBUG
using System.Diagnostics;
#endif

namespace Keyboard_master
{
    class Animation
    {
        public enum AnimationType
        {
            SLIDE_LEFT,
            SLIDE_RIGHT,
            SLIDE_UP,
            SLIDE_DOWN,
            BOUNCE
        };

        public AnimationType Type
        {
            get {return animType;}
        }
        private AnimationType animType;

        public double RemainingTime //In ms
        { 
            get {return remainingDuration;}
        }
        
        private float amplitude;
        private float frequency;
        private double remainingDuration;
        private double delay;
        
        public Animation(AnimationType type, float magnitude, double duration)
        {
            this.animType = type;
            this.amplitude = magnitude;
            this.remainingDuration = duration;
            this.delay = 0.0d;
        }

        public void Update(GameTime gameTime)
        {
            if (this.delay > 0.0d)
            {
                this.delay -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                this.remainingDuration -= gameTime.ElapsedGameTime.TotalMilliseconds;
                switch (this.animType)
                {
                    case AnimationType.SLIDE_LEFT:
                        break;
                    default:
                        Debug.Assert(false, "Unhandled Animation Type");
                        break;
                }
            }
        }
    }
}
