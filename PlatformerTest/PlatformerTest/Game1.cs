using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PlatformerTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        public static Level level;
        private float cameraPositionX;
        private float cameraPositionY;
        public int WindowWidth = 640;
        public int WindowHeight = 480;
        public Rectangle viewportRect;
        SoundManager bgMusic;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            level = new Level();
            Content.RootDirectory = "Content";

            //byæ mo¿e do przeniesienia w inne miejsce
            bgMusic = new SoundManager(Content, true);
            bgMusic.addSound("music1");
            bgMusic.addSound("music2"); 
            bgMusic.loadAllSounds();
            //bgMusic.playSound("music2");
            //bgMusic = Content.Load<SoundEffect>("music2").CreateInstance();
            //bgMusic.Play();
            //bgMusic.Volume = 0.5f;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(this.Content.Load<Texture2D>("player/sprite"));
            player._collisionTexture = this.Content.Load<Texture2D>("collision");
            level.SetTileset(this.Content.Load<Texture2D>("tileset/test"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds * 10f;
            level.Update(gameTime, keyState, spriteBatch);
            player.Update(dt, keyState, gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetRenderTarget(null);

            if (player.Sprite.Position.X - player._playerOffset < 0)
            {
                cameraPositionX = 0;
            }
            else {
                if (player.Sprite.Position.X + player._playerOffset < ((level.mapWidth*64) - 100 )) {
                    cameraPositionX = player.Sprite.Position.X - player._playerOffset;
                }                
            }

            if (player.Sprite.Position.Y - player._playerHeightOffset < 0) { 
                //top border!
                cameraPositionY = 0;
            }else{
                if(player.Sprite.Position.Y + player._playerHeightOffset  < ((level.mapHeight*64) +130 )){
                    cameraPositionY = player.Sprite.Position.Y - player._playerHeightOffset;
                }
            }

            Matrix cameraTransform = Matrix.CreateTranslation(-cameraPositionX, -cameraPositionY, 0.0f);
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend,SamplerState.LinearClamp,DepthStencilState.None,RasterizerState.CullCounterClockwise,null, cameraTransform);
            level.Draw(spriteBatch);
            player.Draw(spriteBatch);
            level.DrawGrid(spriteBatch);
            level.DrawIntersectingGrid(spriteBatch, player);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
