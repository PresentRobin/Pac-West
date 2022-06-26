using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TakeAim.Sprites;

namespace TakeAim.States
{
    public class GameState : State
    {
        private List<Sprite> _sprites;

        private float _timer;
        private int _gameTimer = 60 * 60;

        private SpriteFont _font;

        private Texture2D ghostTexture;
        private Texture2D backgroundTexture;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch);

            //presenting the font on the screen

            var currentScore = Game1._score;
            foreach (var sprite in _sprites)
            {
                if (sprite is Gun)
                {
                    spriteBatch.DrawString(_font, string.Format("Total Gametime: 1 min | current time: {0}", _gameTimer / 60), new Vector2(10, 10), Color.Black);
                    spriteBatch.DrawString(_font, "Win: minimum 20 kill", new Vector2(10, 25), Color.Black);
                    spriteBatch.DrawString(_font, string.Format("Player : {0} kill", currentScore), new Vector2(10, 40), Color.Black);
                }
            }
            spriteBatch.End();

        }

        public override void LoadContent()
        {
            var gunTexture = _content.Load<Texture2D>("Gun");
            var bulletTexture = new Bullet(_content.Load<Texture2D>("Bullet"));
            ghostTexture = _content.Load<Texture2D>("conjurer2");
            backgroundTexture = _content.Load<Texture2D>("background");

            _font = _content.Load<SpriteFont>("Fonts/Font");

            _sprites = new List<Sprite>()
            {
                new Gun(gunTexture)
                {
                  Bullet = bulletTexture,
                  Position = new Vector2((Game1.ScreenWidth - ghostTexture.Width) /2, (Game1.ScreenHeight -ghostTexture.Height) /2),
                  Colour = Color.White,
                },


            };

        }

        public override void PostUpdate(GameTime gameTime)
        {
            foreach (var spriteA in _sprites)
            {
                foreach (var spriteB in _sprites)
                {
                    

                    if (spriteA == spriteB)
                    {
                        continue;
                    }


                    if (spriteA.Intersects(spriteB))   
                    {                                   
                                                        
                        spriteA.OnCollide(spriteB);                      
                    }


                }
            }

            int count = _sprites.Count;
            for (int i = 0; i < count; i++)
            {
                foreach (var child in _sprites[i].Children)
                    _sprites.Add(child);

                _sprites[i].Children.Clear();
            }

            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            _gameTimer--;
            if (_gameTimer == 0)
            {
                if(Game1._score >= 20)
                {
                    Game1._gameWon = true;
                    _game.ChangeState(new WinState(_game, _graphicsDevice, _content));
                }
                else
                {
                    Game1._gameWon = false;
                    _game.ChangeState(new LostState(_game, _graphicsDevice, _content));
                }
            }




                foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);


            PostUpdate(gameTime);

            SpawnGhost();

        }

        private void SpawnGhost()
        {
            if (_timer > 1f)
            {
                _timer = 0f;

                var xPos = Game1.Random.Next(0, Game1.ScreenWidth - ghostTexture.Width);
                var yPos = Game1.Random.Next(0, Game1.ScreenHeight - ghostTexture.Height);



                _sprites.Add(new Sprite(ghostTexture)
                {
                    Position = new Vector2(xPos, yPos),
                    Colour = Color.White,
                    removeSpriteAuto = true,
                    //scale = Random.Next(0, 4),
                    //scale = 1,
                });



            }
        }

    }
}
