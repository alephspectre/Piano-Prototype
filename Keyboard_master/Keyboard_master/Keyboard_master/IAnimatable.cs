using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Keyboard_master
{
    interface IAnimatable
    {
        void UpdateAnimations(GameTime gameTime);
        void TerminateAllAnimations();
        void AddAnimation(Animation animation);

        void SetPosX(float x);
        void SetPosY(float y);
        void SetScale(float s);
        void SetOpacity(byte o);
    }
}
