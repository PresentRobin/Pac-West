using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chase.Models
{
    class Obstacle
    {

        public class Obstacles : Sprite
        {
            public int Score;
            public Obstacles(Texture2D texture) : base(texture)
            {
                Position = new Vector2(Game1.Random.Next(0, Game1.ScreenHeight - _texture.Width), -_texture.Height);
                Speed = Game1.Random.Next(3, 10);
            }

            public override void Update(GameTime gameTime, List<Sprite> sprites)
            {
                Position.Y += Speed;

                //if we hit the bottom of the window
                if (Rectangle.Bottom >= Game1.ScreenHeight)
                {
                    Score++;
                    IsRemoved = true;
                }

            }
        }
    }
}
