using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BullRidingGame
{
    public class bullRiding : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Input input;

        private Sprite _bg;
        private Sprite _bull;
        private Sprite _player;

        private Sprite _commandLeft;
        private Sprite _commandRight;
        private Sprite _commandUp;
        private Sprite _commandDown;

        private Sprite _gameOver;
        private Sprite _gameWin;

        private float Time = 0;
        private int rightSide = 800;
        private int bottom = 480;

        private Boolean endGame = false;
        private Boolean lostGame = false;
        private Boolean wonGame = false;

        float timer = 1;         //Initialize a 1 second timer
        const float TIMER = 1;

        public bullRiding()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            var bgTexture = Content.Load<Texture2D>("ShootoutBG");
            var bullTexture = Content.Load<Texture2D>("bullV2");
            var player = Content.Load<Texture2D>("player_straight_aheadV2");
            var right = Content.Load<Texture2D>("right");
            var left = Content.Load<Texture2D>("left");
            var up = Content.Load<Texture2D>("up");
            var down = Content.Load<Texture2D>("down");
            var gameOver = Content.Load<Texture2D>("gameOver");
            var gameWin = Content.Load<Texture2D>("win");

            _bg = new Sprite(bgTexture)
            {
                Position = new Vector2(0,0),
            };

            _bull = new Sprite(bullTexture)
            {
                Position = new Vector2(350, 250),
                velocity = 20,
            };

            _player = new Sprite(player)
            {
                Position = new Vector2(_bull.Position.X, _bull.Position.Y - 100),
                velocity = _bull.velocity / 2
            };

            _gameOver = new Sprite(gameOver)
            {
                Position = new Vector2(0, 0),
            };
            

            _gameWin = new Sprite(gameWin)
            {
                Position = new Vector2(0, 0),
            };

            //the arrows on the sides
            _commandRight = new Sprite(right);
            _commandRight.Position = new Vector2
                ((790 - _commandRight._texture.Width),
                ((480 / 2) - (_commandRight._texture.Height / 2)));

            _commandLeft = new Sprite(left);
            _commandLeft.Position = new Vector2
                ((10),
                ((480 / 2) - (_commandLeft._texture.Height / 2)));

            _commandUp = new Sprite(up);
            _commandUp.Position = new Vector2
                ((800 / 2 - (_commandUp._texture.Width / 2)),
                (470 - _commandUp._texture.Height));

            _commandDown = new Sprite(down);
            _commandDown.Position = new Vector2
                ((800 / 2 - (_commandDown._texture.Width / 2)),
                (10));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0 && !endGame)
            {
                //Timer expired, execute action
                Random rnd = new Random();
                int movement = rnd.Next(1, 5);

                //up
                if (movement == 1) 
                {
                    if (_bull.Position.Y > 0)
                    {
                        _bull.Position.Y -= _bull.velocity;
                    } else
                    {
                        _bull.Position.Y += _bull.velocity;
                    }
                }
                //down
                if (movement == 2)
                {
                    if (_bull.Position.Y < (bottom - _bull._texture.Height))
                    {
                        _bull.Position.Y += _bull.velocity;
                    }
                    else
                    {
                        _bull.Position.Y -= _bull.velocity;
                    }
                }
                //right
                if (movement == 3)
                {
                    if (_bull.Position.X < (rightSide - _bull._texture.Width))
                    {
                        _bull.Position.X -= _bull.velocity;
                    }
                    else
                    {
                        _bull.Position.X += _bull.velocity;
                    }
                }
                //left
                if (movement == 4)
                {
                    if (_bull.Position.X > 0)
                    {
                        _bull.Position.X -= _bull.velocity;
                    }
                    else
                    {
                        _bull.Position.X += _bull.velocity;
                    }
                }
                timer = TIMER;   //Reset Timer
            }

            //player movement
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && !endGame && _player.Position.Y > 0)
            {
                _player.Position.Y -= _player.velocity;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && !endGame && _player.Position.Y < (bottom - _player._texture.Height))
            {
                _player.Position.Y += _player.velocity;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && !endGame && _player.Position.X > 0)
            {
                _player.Position.X -= _player.velocity;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && !endGame && _player.Position.X < (rightSide - _player._texture.Width)) 
            {
                _player.Position.X += _player.velocity;
            }

            //losing condition
            if ((Math.Abs(_bull.Position.X - _player.Position.X)) > 60 
                || (Math.Abs(_bull.Position.Y - (_player.Position.Y + 100))) >= 60 
                || (Math.Abs(_bull.Position.X - _player.Position.X) + (Math.Abs(_bull.Position.Y - (_player.Position.Y + 100))) > 80 ))
            {
                lostGame = true;
            }

            //winning condition
            else 
            {
                Time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Time >= 10)
                {
                    wonGame = true;
                }
            }

            if (wonGame || lostGame)
            {
                endGame = true;
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _bg.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);
            _bull.Draw(_spriteBatch);

            _commandLeft.Draw(_spriteBatch);
            _commandRight.Draw(_spriteBatch);
            _commandUp.Draw(_spriteBatch);
            _commandDown.Draw(_spriteBatch);

            if(wonGame)
            {
                _gameWin.Draw(_spriteBatch);
            }

            if (lostGame)
            {
                _gameOver.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
