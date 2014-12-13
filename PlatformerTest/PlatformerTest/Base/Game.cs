using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTest.Base;
using System.Windows.Forms;
using System.Windows;

namespace PlatformerTest.Base
{
    /// <summary>
    /// Base class for every Game class(level, menu)
    /// </summary>
    public abstract class Game : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected Component[] components;
        public Game()
        {
            this.graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = ProgramConfig.isFullScreen,
                PreferredBackBufferWidth = ProgramConfig.windowWidth,
                PreferredBackBufferHeight = ProgramConfig.windowHeight,
                SupportedOrientations = DisplayOrientation.Default
            };
            Content.RootDirectory = "Content";
            System.Windows.Forms.Form window = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(this.Window.Handle);
            window.MaximizeBox = true;
            

        }

        public FormWindowState WindowState{
            get
            {
                System.Windows.Forms.Form window = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(this.Window.Handle);
                return window.WindowState;
            }
            set
            {
                System.Windows.Forms.Form window = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(this.Window.Handle);
                window.WindowState = value;
            }
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
