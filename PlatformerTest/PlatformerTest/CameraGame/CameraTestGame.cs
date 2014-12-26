using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerTest.Base;

namespace PlatformerTest.CameraGame
{
    public class CameraTestGame : Base.Game
    {
        Base.Player player;
        Base.Level level;
        Base.Camera camera;
        private Player naq;
        Matrix SpriteScale;

        public CameraTestGame()
            : base()
        {
            camera = new CameraTestCamera();
            level = new CameraTestLevel(camera);
            player = new CameraTestPlayer(level);
        }

        protected override void Initialize()
        {
            base.Initialize();
            player.Initialize();
            level.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            player.LoadContent(Content);
            level.LoadContent(Content);
            // old naq, will be replace by player inheriting from Base.Player
            // Default resolution is 480x320; scale sprites up or down based on
            // current viewport
            float screenscale =
                (float)graphics.GraphicsDevice.Viewport.Width / 480f;
            // Create the scale transform for Draw. 
            // Do not scale the sprite depth (Z=1).
            SpriteScale = Matrix.CreateScale(screenscale, screenscale, 1);
            naq = new Player(this.Content.Load<Texture2D>("player/sprite"))
            {
                _collisionTexture = this.Content.Load<Texture2D>("collision")
            };
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var inputState = UserInput.GetState();

            naq.Update(dt, Keyboard.GetState(), gameTime);
            player.Update(dt, inputState);
            level.Update(dt, inputState);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            // these functions should be used
            camera.SetPosition(naq.Sprite.Position);
           // camera.SetPosition(player.Position);
            var cameraTransform = camera.GetTranslationMatrix();
            
            //float cameraPositionX = 0;
            //float cameraPositionY = 0;
            //if (naq.Sprite.Position.X - naq._playerOffset < 0)
            //{
            //    cameraPositionX = 0;
            //}
            //else if (naq.Sprite.Position.X + naq._playerOffset < ((level.Width * ProgramConfig.tileSize) - 100))
            //{
            //    cameraPositionX = naq.Sprite.Position.X - naq._playerOffset;
            //}

            //if (naq.Sprite.Position.Y - naq._playerHeightOffset < 0)
            //{
            //    cameraPositionY = 0;
            //}
            //else if (naq.Sprite.Position.Y + naq._playerHeightOffset < ((level.Height * ProgramConfig.tileSize) + 130))
            //{
            //    cameraPositionY = naq.Sprite.Position.Y - naq._playerHeightOffset;
            //}
            //Matrix cameraTransform = Matrix.CreateTranslation(-cameraPositionX, -cameraPositionY, 0.0f);
            

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, cameraTransform);

            level.Draw(spriteBatch);
            //player.Draw(spriteBatch);
            naq.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
