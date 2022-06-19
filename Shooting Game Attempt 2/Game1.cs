using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Shooting_Game_Attempt_2.Sprites;

namespace Shooting_Game_Attempt_2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Random Random;
        public static int _score;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private List<Sprite> _sprites;

        private float _timer;
        private int _gameTimer;

        private SpriteFont _font;

        private Texture2D ghostTexture;
        private Texture2D backgroundTexture;

        //gamestates
        const int WIN = 1, LOSE = 2, PLAY = 3;
        int gameState;   //keep track of what state we're in


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Random = new Random();

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _gameTimer = 60 * 60;

            gameState = PLAY;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            var gunTexture = Content.Load<Texture2D>("Gun");
            var bulletTexture = new Bullet(Content.Load<Texture2D>("Bullet"));
            ghostTexture = Content.Load<Texture2D>("conjurer2");
            backgroundTexture = Content.Load<Texture2D>("background");

            _font = Content.Load<SpriteFont>("Font");

            _sprites = new List<Sprite>()
            {
                new Gun(gunTexture)
                {
                  Bullet = bulletTexture,
                  Position = new Vector2((ScreenWidth - ghostTexture.Width) /2, (ScreenHeight -ghostTexture.Height) /2),
                  Colour = Color.White,
                },
                

            };

        }

        protected override void Update(GameTime gameTime)
        {


            // TODO: Add your update logic here

            _gameTimer--;
            if (_gameTimer == 0)
            {
                /* if(_score  < 5)
                 {
                     gameState = LOSE;
                 }
                 else
                 {
                     gameState = WIN;
                 }*/

                Exit();

            }
                

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var sprite in _sprites)
                sprite.Update(gameTime,  _sprites);

            
            PostUpdate(gameTime);

           SpawnGhost();

            base.Update(gameTime);
        }

        protected void PostUpdate(GameTime gameTime)
        {
            // 1. Check collision between all current "Sprites"
            // 2. Add "Children" to the list of "_sprites" and clear
            // 3. Remove all "IsRemoved" sprites

            foreach (var spriteA  in _sprites)
            {
                foreach (var spriteB in _sprites)
                {
                    //foreach (var spriteC in _sprites)
                    //{

                        if (spriteA == spriteB)
                        {
                            continue;
                        }

                        /*if(spriteA == spriteC)
                        {
                            continue;
                        }

                        if(spriteB == spriteC)
                        {
                            continue;
                        }*/

                        if (spriteA.Intersects(spriteB))   //it seems to be the gun 
                        {                                   // it shoud have been the bullet vs the ghost 
                           // _score++;
                            spriteA.OnCollide(spriteB);     //it seems to be the ghost                  
                        }

                       /* if (spriteB.Intersects(spriteC))
                        {
                            //_score++;
                            spriteB.OnCollide(spriteC);
                        }*/
                    //}

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

        private void SpawnGhost()
        {
            if (_timer > 1f)
            {
                _timer = 0f;

                var xPos = Random.Next(0, ScreenWidth - ghostTexture.Width);
                var yPos = Random.Next(0, ScreenHeight - ghostTexture.Height);

                

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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, _spriteBatch);

            //presenting the font on the screen
               
            var currentScore = _score;
            foreach(var sprite in _sprites)
            {
                if (sprite is Gun)
                {
                    _spriteBatch.DrawString(_font, string.Format("Total Gametime: 1 min | current time: {0}", _gameTimer/60), new Vector2(10, 10), Color.Black);
                    _spriteBatch.DrawString(_font, "Win: minimum 20 kill", new Vector2(10, 25), Color.Black);
                    _spriteBatch.DrawString(_font, string.Format("Player : {0} kill", currentScore ), new Vector2(10, 40), Color.Black);
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
