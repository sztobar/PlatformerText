using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTest.Base;

namespace PlatformerTest.Base
{
    /// <summary>
    /// Base class for every Game class(level, menu)
    /// </summary>
    public abstract class Game : Microsoft.Xna.Framework.Game
    {
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected Component[] components;
        public Game()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = ProgramConfig.isFullScreen,
                PreferredBackBufferWidth = ProgramConfig.windowWidth,
                PreferredBackBufferHeight = ProgramConfig.windowHeight
            };
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.SetRenderTarget(null);
            base.Draw(gameTime);
        }
    }
}
