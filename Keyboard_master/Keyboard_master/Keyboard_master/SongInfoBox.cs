using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;
using System.Collections;

namespace Keyboard_master
{
    class SongInfoBox: IAnimatable
    {
        private Vector2 posOffset;
        private float sizeOffset;
        private Color tint;
        private SpriteFont font;

        private List<Animation> animations;

        private OrderedDictionary entries;

        public SongInfoBox(SpriteFont font)
        {
            this.font = font;
            entries = new OrderedDictionary();
            entries.Add("Title:", "None");
            entries.Add("Author:", "None");
            entries.Add("Level:", "None");
            entries.Add("Record:", "None");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 absPos, float absSize)
        {
            /*
            absSize = absSize * Dimensions.Height / 720.0f; // For Widescreen resolution independence. 
            // XXX: Resolution Independence is currently seroiusly broken, especially for other ratios 

            foreach (DictionaryEntry entry in entries)
            {
                
            }

            spriteBatch.DrawString(this.font, infoHeaders, this.songInfoPos, Color.Black, 0.0f, songInfoOrigin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(this.artwork,
                             absPos,
                             this.artwork.Bounds,
                             this.tint,
                             0.0f,
                             new Vector2(this.artwork.Bounds.Width / 2.0f + this.posOffset.X,
                                         this.artwork.Bounds.Height / 2.0f + this.posOffset.Y),
                             new Vector2(absSize + this.sizeOffset, absSize + this.sizeOffset),
                             SpriteEffects.None,
                             0);
             */
        }

        public void Update(GameTime gameTime)
        {
            UpdateAnimations(gameTime);
        }

        public void SetPosX(float x)
        {
            this.posOffset.X = x;
        }

        public void SetPosY(float y)
        {
            this.posOffset.Y = y;
        }

        public void SetScale(float s)
        {
            this.sizeOffset = s;
        }

        public void SetOpacity(byte o)
        {
            this.tint.A = o;
        }

        public void UpdateAnimations(GameTime gameTime)
        {
            List<int> animationIndicesToRemove = new List<int>(); //Could be optimized by making it an instance var, but not necessary unless speed problem

            for (int i = 0; i < this.animations.Count; i++)
            {
                if (this.animations[i].HasCompleted)
                {
                    animationIndicesToRemove.Add(i);
                }
                else
                {
                    this.animations[i].UpdateWithTarget(this, gameTime);
                }
            }

            foreach (int animIndex in animationIndicesToRemove)
            {
                this.animations.RemoveAt(animIndex);
            }
        }

        public void AddAnimation(Animation animation)
        {
            this.animations.Add(animation);
        }

        public void TerminateAllAnimations()
        {
            foreach (Animation anim in this.animations)
            {
                anim.ForceCompletionOnTarget(this);
            }
            this.animations.Clear();
        }
    }
}
