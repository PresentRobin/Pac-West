using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Sprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_1
{
    public class Player
    {
        public Vector2 pos;
        private AnimatedSprite[] playerSprite;
        private SpriteSheet sheet;
        private float moveSpeed = 1.5f;
        public Rectangle playerBounds;//For the collisions
        public bool isIdle=false;

        public Player()
        {
            playerSprite = new AnimatedSprite[10];
            
            pos = new Vector2(100, 50);
            playerBounds = new Rectangle((int)pos.X-8/*centered at centre*/,(int)pos.Y-8,16,17);

        }
        public void Load(SpriteSheet[] spriteSheets)
        {
            for (int i =0; i<spriteSheets.Length;i++)
            {
                sheet = spriteSheets[i];
                playerSprite[i] = new AnimatedSprite(sheet);
            }


        }

        public void Update(GameTime gameTime)
        {
            isIdle = true;
           

            string animation = "";
            var keyboardstate = Keyboard.GetState();
            if(keyboardstate.IsKeyDown(Keys.Right))//Move right
            {
                animation = "walkRight";
                pos.X += moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.Left))//Move left
            {
                animation = "walkLeft";
                pos.X -= moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.Up))//Move up 
            {
                animation = "walkUp";
                pos.Y -= moveSpeed;
                isIdle = false;
            }
            if (keyboardstate.IsKeyDown(Keys.Down))//Move down
            {
                animation = "walkDown";
                pos.Y += moveSpeed;
                isIdle = false;
            }
            if (!isIdle)
            {
                playerSprite[1].Play(animation);
                playerSprite[1].Update(gameTime);
            }
            playerBounds.X = (int)pos.X-8;//Apparently by default the rectangle gets centred at the player's centre when using monogame extended's draw function.
            playerBounds.Y = (int)pos.Y-8;
            playerSprite[0].Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch,Matrix matrix)

        {
            spriteBatch.Begin(//All of these need to be here :(
                SpriteSortMode.Deferred,
                samplerState:SamplerState.PointClamp,
                effect:null,
                blendState:null,
                rasterizerState:null,
                depthStencilState:null,
                transformMatrix:matrix/*<-This is the main thing*/);
            if (isIdle)
                spriteBatch.Draw(playerSprite[0], pos);
            else
                spriteBatch.Draw(playerSprite[1], pos);
            spriteBatch.End();
        }
    }
}
