using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BullRidingGame
{
    class Sprite
    {
        public Texture2D _texture;

        public Vector2 Position;

        public float RotationVelocity = 3f;
        public float velocity;
        public float lifespan = 3f;

        public bool isRemoved = false;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isRemoved == false)
            {
                spriteBatch.Draw(_texture, Position, null, Color.White);
            }
        }
    }
}
