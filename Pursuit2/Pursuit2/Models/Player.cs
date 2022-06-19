using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pursuit2.Models
{
  public class Player: Sprite
    {
        public bool hasDied = false;
       // public int Score;

        public Player(Texture2D texture) : base(texture)
        {
           
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();

            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if (sprite.Rectangle.Intersects(this.Rectangle))
                {
                    this.hasDied = true;
                }
            }

            Position += Velocity;
            //keep the sprite on the screen
            Position.X = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - Rectangle.Width);

            //Reset the velocity for when the user isn't holding a key'
            Velocity = Vector2.Zero;
        }

        private void Move()
        {
            if (Input == null)
                throw new Exception("Plese assign a value to Input");
    
             if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;

            else if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X -= Speed;
        }
    }
}
