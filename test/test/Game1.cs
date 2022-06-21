using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using test.Content.Models;

using test.Content.Management;
using TiledSharp;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Content;




namespace test
{
    public class Game1 : Game
    {
        private List<Rectangle> collisionObjects;
        private GraphicsDeviceManager _graphics;
        private Player player;
        private SpriteBatch spriteBatch;
        private TmxMap map;
        private TileMapManager mapManager;
        private Matrix matrix;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            _graphics.PreferredBackBufferWidth = 576* 2;//Making the window size twice our tilemap size
            _graphics.PreferredBackBufferHeight = 384 * 2;
            _graphics.ApplyChanges();
            var Width = _graphics.PreferredBackBufferWidth;
            var Height = _graphics.PreferredBackBufferHeight;
            var WindowSize = new Vector2(Width, Height);
            var mapSize = new Vector2(576, 384);//Our tile map size
            matrix = Matrix.CreateScale(new Vector3(WindowSize / mapSize, 1));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //map draw fun time
            map = new TmxMap("Player/map.tmx");
            var tileset = Content.Load<Texture2D>("Map/" + map.Tilesets[0].Name.ToString());
            var tileWidth = map.Tilesets[0].TileWidth;
            var tileHeight = map.Tilesets[0].TileHeight;
            var TileSetTilesWide = tileset.Width / tileWidth;
            mapManager = new TileMapManager(spriteBatch, map, tileset, TileSetTilesWide, tileWidth, tileHeight);
            collisionObjects = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Collisions"].Objects)
            {
                collisionObjects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }
            SpriteSheet[] sheets = {Content.Load<SpriteSheet>("Player/playerSheetWalk.sf",new JsonContentLoader())};
            player.Load(sheets);
       
        }

        protected override void Update(GameTime gameTime)
        {
            if ( Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var initpos = player.pos;
            player.Update(gameTime);
            foreach (var rect in collisionObjects)
            {
                if (rect.Intersects(player.playerBounds))
                {
                    player.pos = initpos;
                    player.isIdle = true;
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            mapManager.Draw(matrix);
            player.Draw(spriteBatch, matrix);
            // TODO: Add your drawing code here

            base.Draw(gameTime);                                                                                                                                    
        }
    }
}
