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
    enum AnimationType
    {
        SLIDE_LEFT,
        SLIDE_RIGHT,
        SLIDE_UP,
        SLIDE_DOWN,
        BOUNCE_VERT,
        BOUNCE_HORIZ,
        BOUNCE_SIZE
    };

    class Animation
    {

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
        private double totalDuration;
        private double remainingDuration;
        private double delay;
        public bool Loop; //Can be set externally to allow looping animation to finish properly
        public bool HasCompleted; // Read externally to clean up a completed animation
        
        public Animation(AnimationType type, float magnitude, double duration, bool loop=false)
        {
            this.animType = type;
            this.amplitude = magnitude;
            this.totalDuration = duration;
            this.remainingDuration = duration;
            this.delay = 0.0d;
            this.Loop = loop;
            this.HasCompleted = false;
        }

        // Summary:
        //     Make sure that the animation gets marked as finished or loops properly
        private void CompleteWithTarget(IAnimatable target)
        {
            //These two lines fix overshoot
            this.remainingDuration = 0.0d;
            ComputeUpdateOnTarget(target);

            if (this.Loop)
            {
                this.remainingDuration = this.totalDuration;
            }
            else
            {
                this.HasCompleted = true;
            }
        }

        // Summary:
        //     Ignore loop flag and force instant completion
        public void ForceCompletionOnTarget(IAnimatable target)
        {
            this.Loop = false;
            this.remainingDuration = 0.0d;
            ComputeUpdateOnTarget(target);
            this.HasCompleted = true;
        }

        // Summary:
        //     Animate the target based on the animType
        private void ComputeUpdateOnTarget(IAnimatable target) 
        {
            switch (this.animType)
            {
                case AnimationType.BOUNCE_SIZE:
                    double fractionCompleted = (this.totalDuration - this.remainingDuration) / this.totalDuration * 2.0d * Math.PI; // From 0 to 2PI
                    const double sinOffset = 3.0d / 2.0d * Math.PI;
                    target.SetScale((1.0f + (float)Math.Sin(fractionCompleted + sinOffset)) * this.amplitude / 2.0f); // Scales from (0 to 1 to 0) * amplitude
                    break;
                default:
                    Debug.Assert(false, "Unhandled Animation Type");
                    break;
            }
        }

        // Summary:
        //     Call this in the UpdateAnimations method in the target 
        public void UpdateWithTarget(IAnimatable target, GameTime gameTime)
        {
            if (this.delay > 0.0d)
            {
                this.delay -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                if (this.remainingDuration > 0.0d)
                {
                    this.remainingDuration -= gameTime.ElapsedGameTime.TotalMilliseconds;
                    ComputeUpdateOnTarget(target);
                }
                else
                {
                    CompleteWithTarget(target);
                }
            }
        }
    }
}
