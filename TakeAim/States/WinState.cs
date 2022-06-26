using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TakeAim.Content.Controls;
using TakeAim.Sprites;

namespace TakeAim.States
{
    public class WinState : State
    {
        private List<Component> _components;

        private SpriteFont _font;
        private Texture2D backgroundTexture;

        public WinState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            backgroundTexture = _content.Load<Texture2D>("background");

            var returnButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "Return to main game",
                PenColour = Color.Black,
            };

            returnButton.Click += ReturnButton_Click;

            _components = new List<Component>()
            {
                returnButton,
            };
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Begin();


            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

            _font = _content.Load<SpriteFont>("Fonts/Font");
            spriteBatch.DrawString(_font, "You have Won the Game", new Vector2(300, 150), Color.Black);


            spriteBatch.End();


            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.DrawString(_font, "Win: minimum 20 kill", new Vector2(10, 25), Color.Black);
            spriteBatch.End();
        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Fonts/Font");


        }
            


        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            foreach (var component in _components)
                component.Update(gameTime, sprites);
        }
    }
}
