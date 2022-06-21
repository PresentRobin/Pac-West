using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShootDown
{
    class Bullet : Sprite
    {
        private float _timer;
        public float lifespan = 3f;
        public Bullet(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(_timer > lifespan)
            {
                isRemoved = true;
            }
            Position += Direction * velocity;
        }
    }
}
