using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Shooting_Game_Attempt_2.Sprites
{
   public class Gun : Sprite
    {
        public Bullet Bullet;

        public int Score = Game1._score;

        public Gun(Texture2D texture)
          : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                _rotation -= MathHelper.ToRadians(RotationVelocity);
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                _rotation += MathHelper.ToRadians(RotationVelocity);

            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Position += Direction * LinearVelocity;

            if (_currentKey.IsKeyDown(Keys.Space) &&
                _previousKey.IsKeyUp(Keys.Space))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            //bullet.Position = this.Position;
            bullet.Position.X += this.Position.X + (float)Math.Sin(this._rotation + 7.5) * _texture.Width / (float)1.2;
            bullet.Position.Y = this.Position.Y - (float)Math.Cos(this._rotation + 7.5) * _texture.Height / (float)1.1;
            bullet.Colour = this.Colour;
            bullet.LinearVelocity = this.LinearVelocity * 1.1f;
            bullet.LifeSpan = 3f;
            bullet.Parent = this;

            Children.Add(bullet);
        }

        public override void OnCollide(Sprite sprite)
          {
            base.OnCollide(sprite);

            if (sprite == this.Parent)
            {
                return;
            }

        }
    }
}
