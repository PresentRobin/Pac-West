using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShootDown
{
    class Sprite
    {
        private Texture2D _texture;

        public Vector2 Position;
        public Vector2 orgin;
        public Vector2 Direction;
        public float RotationVelocity = 3f;
        public float velocity;
        
        public Sprite parent;

        public bool isRemoved = false;

        protected float _rotation;
        protected KeyboardState currentKey;
        protected KeyboardState previousKey;

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
                spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, orgin, 1, SpriteEffects.None, 0);
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
