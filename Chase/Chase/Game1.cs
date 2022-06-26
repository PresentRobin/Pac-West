using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Chase.Models;
using System;
using System.Collections.Generic;
using static Chase.Models.Obstacle;
using System.Timers;

namespace Chase
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public float _timer1;
        public static int ScreenWidth;
        public static int ScreenHeight;

        public static Random Random;

        // private SpriteFont _font = default;

        private List<Sprite> _sprites;
        private float _timer;
        private bool _hasStarted = false;

        private Camera _camera;
        private Texture2D _backgroundTexture;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Random = new Random();

            ScreenHeight = _graphics.PreferredBackBufferHeight;
            ScreenWidth = _graphics.PreferredBackBufferWidth;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _camera = new Camera();
            _backgroundTexture = Content.Load<Texture2D>("bg");



            // TODO: use this.Content to load your game content here


            Restart();
        }

        private void Restart()
        {

            var playerTexture = Content.Load<Texture2D>("tkkk");

            _sprites = new List<Sprite>()
            {
                new Player(playerTexture)
                {
                   Position = new Vector2((ScreenWidth / 2) - (playerTexture.Width/ 2), ScreenHeight - playerTexture.Height),
                    Input = new Input()
                    {
                        Left = Keys.Left,
                        Right = Keys.Right  ,
                    },
                    Speed = 10f,
                }
            };

            _hasStarted = false;
        }

        protected override void Update(GameTime gameTime)
        {
            //click space button to start the game
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                _hasStarted = true;

            // if game has not started do nothing
            if (!_hasStarted)
                return;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _timer1 += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            if (_timer > 0.8f)
            {
                _timer = 0;
                //_sprites.Add(new Obstacle(Content.Load<Texture2D>("bomb")));
                _sprites.Add(new Obstacles(Content.Load<Texture2D>("eni")));
                _sprites.Add(new Obstacles(Content.Load<Texture2D>("eni2")));
                _sprites.Add(new Obstacles(Content.Load<Texture2D>("eni3")));
            }
            if (_timer1 == 10*60)
            {
                Exit();
            }

            enemyUpdat();

            base.Update(gameTime);
        }

        //spawn enemy
        private void enemyUpdat()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                if (sprite.IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }

                if (sprite is Player)
                {
                    var player = sprite as Player;
                    if (player.hasDied)
                    {
                        Restart();
                    }
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);
            //var font = 10;
            //var i = 0;
            //foreach (var sprite in _sprites)
            //{
            //    if (sprite is Obstacle)
            //       // _spriteBatch.DrawString(_font, string.Format("Player{0}: {1}", ++i, ((Obstacle)sprite).Score), new Vector2(10, font += 20), Color.Black);
            //}
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
