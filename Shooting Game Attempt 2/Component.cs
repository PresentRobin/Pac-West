using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Shooting_Game_Attempt_2.Sprites;

namespace Shooting_Game_Attempt_2
{
    public abstract class Component
    {
        public abstract void Update(GameTime gameTime, List<Sprite> sprites);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
