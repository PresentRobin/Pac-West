using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using System.Timers;

namespace ShootDown
{
    public class ShootOut : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Input Input;

        private Texture2D _texture;
        private Sprite _bg;
        private Sprite _enemy;
        private Sprite _player;
        private Sprite _gunL;
        private Sprite _gunR;
        private Sprite _bullet1;
        private Sprite _bullet2;
        private Sprite _enemyShooting;
        private Sprite _playerShooting;
        private Sprite _countDown3;
        private Sprite _countDown2;
        private Sprite _countDown1;
        private Sprite _countDownShoot;
        private Sprite _gameOver;
        private Sprite _gameWin;

        private Boolean gameWin = false;
        private Boolean gameLost = false;
        private Boolean endGame = false;

        private int rightSide = 800;
        private int bottom = 480;

        private float Time = 0;

        public ShootOut()
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
            var enemyTexture = Content.Load<Texture2D>("enemy_regV3");
            var playerTexture = Content.Load<Texture2D>("player_regV2");
            var gunLTexture = Content.Load<Texture2D>("gunLV3");
            var gunRTexture = Content.Load<Texture2D>("gunRV2");
            var bulletTextureL = Content.Load<Texture2D>("bulletL");
            var bulletTextureR = Content.Load<Texture2D>("bulletR");
            var shootingTextureEnemy = Content.Load<Texture2D>("enemy_shootV3");
            var shootingTexturePlayer = Content.Load<Texture2D>("player_shootV2");
            var countDown3 = Content.Load<Texture2D>("3");
            var countDown2 = Content.Load<Texture2D>("2");
            var countDown1 = Content.Load<Texture2D>("1");
            var countDownShoot = Content.Load<Texture2D>("shoot");
            var gameOver = Content.Load<Texture2D>("gameOver");
            var gameWin = Content.Load<Texture2D>("win");

            _bg = new Sprite(bgTexture)
            {
                Position = new Vector2(0, 0),
            };

            _player = new Sprite(playerTexture)
            {
                Position = new Vector2(75, 360),
            };

            _enemy = new Sprite(enemyTexture)
            {
                Position = new Vector2(600, _player.Position.Y),
            };

            _gunL = new Sprite(gunLTexture)
            {
                Position = new Vector2(_player.Position.X, _player.Position.Y - 200),
            };

            _gunR = new Sprite(gunRTexture)
            {
                Position = new Vector2(_enemy.Position.X, _enemy.Position.Y - 200),
            };

            _bullet1 = new Bullet(bulletTextureL)
            {
                Position = new Vector2(_gunL.Position.X + 100, _gunL.Position.Y + 15),
                velocity = 400,
            };

            _bullet2 = new Bullet(bulletTextureR)
            {
                Position = new Vector2(_gunR.Position.X - 25, _gunR.Position.Y + 15),
                velocity = 400,
            };

            _countDown3 = new Sprite(countDown3)
            {
                Position = new Vector2(0, 0),
            };

            _countDown2 = new Sprite(countDown2)
            {
                Position = new Vector2(0, 0),
            };

            _countDown1 = new Sprite(countDown1)
            {
                Position = new Vector2(0, 0),
            };

            _countDownShoot = new Sprite(countDownShoot)
            {
                Position = new Vector2(0, 0),
            };

            _playerShooting = new Sprite(shootingTexturePlayer)
            {
                Position = new Vector2(_player.Position.X, _player.Position.Y),
            };

            _enemyShooting = new Sprite(shootingTextureEnemy)
            {
                Position = new Vector2(_enemy.Position.X, _enemy.Position.Y),
            };

            _gameOver = new Sprite(gameOver)
            {
                Position = new Vector2(0, 0),
            };

            _gameWin = new Sprite(gameWin)
            {
                Position = new Vector2(0, 0),
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //player shooting if space is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && _bullet1.Position.X < rightSide && Time >= 6 && !endGame)
            {
                _bullet1.Position.X += _bullet1.velocity; 
            }

            //enemy shooting if time is X
            if (Time >= 8 && _bullet2.Position.X > 0)
            {
                _bullet2.Position.X -= _bullet2.velocity;
            }

            //winning conditions
            if (_bullet1.Position.X >= _enemy.Position.X && _bullet2.Position.X > _player.Position.X && !endGame)
            {
                gameWin = true;
            }
            //losing conditions
            if (_bullet2.Position.X <= _player.Position.X && _bullet1.Position.X < _enemy.Position.X && !endGame)
            {
                gameLost = true;
            }

            Time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (gameLost || gameWin)
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
            _enemy.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);
            _gunL.Draw(_spriteBatch);
            _gunR.Draw(_spriteBatch);
            _bullet1.Draw(_spriteBatch);
            _bullet2.Draw(_spriteBatch);

            if (Time > 3)
            {
                _countDown3.Draw(_spriteBatch);
            }
            if (Time > 4)
            {
                _countDown3.isRemoved = true;
                _countDown2.Draw(_spriteBatch);
            }
            if (Time > 5)
            {
                _countDown2.isRemoved = true;
                _countDown1.Draw(_spriteBatch);
            }
            if (Time > 6)
            {
                _countDown1.isRemoved = true;
                _countDownShoot.Draw(_spriteBatch);

                _player.isRemoved = true;
                _playerShooting.Draw(_spriteBatch);

                _enemy.isRemoved = true;
                _enemyShooting.Draw(_spriteBatch);
            }

            if (gameWin)
            {
                _bullet2.isRemoved = true;
                _enemyShooting.isRemoved = true;
                _countDownShoot.isRemoved = true;

                _gameWin.Draw(_spriteBatch);
            }

            if (gameLost)
            {
                _bullet1.isRemoved = true;
                _playerShooting.isRemoved = true;
                _countDownShoot.isRemoved = true;

                _gameOver.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
