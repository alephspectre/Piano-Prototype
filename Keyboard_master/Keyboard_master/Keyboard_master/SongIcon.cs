using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Keyboard_master
{

    // Summary:
    //     A SongIcon is a menu item that allows a song to be selected.
    //     It displays an image for the selected song and stores information associated with
    //     the song: the name, artist, level of difficulty, and top score (for the current profile?)
    //     After it has been active for a while (maybe .5 sec), it should start playing the associated 
    //     music.
    //     SongIcons don't have absolute positions, but rather offsets used for animation.
    class SongIcon: IDisposable, IAnimatable
    {
        private Texture2D artwork;
        private Vector2 posOffset;
        private float sizeOffset;
        private Color tint;

        private List<Animation> animations;

        public SongIcon(IServiceProvider serviceProvider, String imageName = "Images/SongIconNotFound")
        {
            content = new ContentManager(serviceProvider, "Content");
            this.artwork = Content.Load<Texture2D>(imageName);
            this.posOffset = Vector2.Zero;
            this.sizeOffset = 0.0f;
            this.animations = new List<Animation>();
            this.tint = new Color(255,255,255,255);
        }

        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public void Dispose()
        {
            Content.Unload();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 absPos, float absSize)
        {
            absSize = absSize * Dimensions.Height / 720.0f; // For Widescreen resolution independence. 
                                                            // XXX: Resolution Independence is currently seroiusly broken, especially for other ratios 
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

            for (int i = 0; i < this.animations.Count; i++ )
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
