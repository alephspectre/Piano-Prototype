using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Robots
{
    class Robot
    {
        public Vector2 Pos;
        Vector2 Velocity;

        public Robot(Vector2 p, Vector2 v) 
        {
            Pos = p;
            Velocity = v;
        }

        public void Draw(SpriteBatch s, Texture2D tex)
        {
            s.Draw(tex, Pos, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Right))
            {
                Velocity.X = Math.Min(Velocity.X + gameTime.ElapsedGameTime.Milliseconds * 0.01f, 0.5f);
              
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                Velocity.X = Velocity.X - gameTime.ElapsedGameTime.Milliseconds * 0.01f;
            }

            if (this.Pos.X <= 450.0f)
            {

                this.Pos = this.Pos + gameTime.ElapsedGameTime.Milliseconds * this.Velocity; //this.Pos.X + gameTime.ElapsedGameTime.Milliseconds * 0.5f;
                this.Pos.X = Math.Min(this.Pos.X, 450.0f);

            }
            

        }
    }
}
