using Microsoft.Xna.Framework;
using PlatformerTest.Base;

namespace PlatformerTest.CameraGame
{
    public class CameraTestCamera : Base.Camera
    {
        public override void SetPosition(Vector2 target)
        {
            //position = sprite.Position - new Vector2(ProgramConfig.windowWidth / 2, ProgramConfig.windowHeight / 2);
            position.X = target.X - (ProgramConfig.windowWidth / 2.0f);
            position.Y = target.Y - (ProgramConfig.windowHeight / 2.0f);

            position.X = position.X >= 0 ? position.X : 0;
            position.Y = position.Y >= 0 ? position.Y : 0;
        }

        public override Rectangle GetRectangle()
        {
            return new Rectangle((int)position.X, (int)position.Y, ProgramConfig.windowWidth, ProgramConfig.windowHeight);
        }

        public override Matrix GetTranslationMatrix()
        {
            return Matrix.CreateTranslation(-position.X, -position.Y, 0f);
        }
    }
}
