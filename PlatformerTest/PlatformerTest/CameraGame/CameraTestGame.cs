using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTest.Base;

namespace PlatformerTest.CameraGame
{
    public class CameraTestGame : Base.Game
    {
        Base.Player player;
        Base.Level level;
        Base.Camera camera;

        public CameraTestGame()
            : base()
        {
            player = new CameraTestPlayer();
            camera = new CameraTestCamera();
            level = new CameraTestLevel(camera);
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
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var inputState = UserInput.GetState();

            player.Update(dt, inputState);
            level.Update(dt, inputState);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            camera.SetPosition(player);

            var cameraTransform = camera.GetTranslationMatrix();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, cameraTransform);

            level.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
