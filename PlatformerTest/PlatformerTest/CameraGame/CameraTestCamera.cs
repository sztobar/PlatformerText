using Microsoft.Xna.Framework;
using PlatformerTest.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformerTest.CameraGame
{
    class CameraTestCamera : Base.Camera
    {
        public override void SetPosition(Sprite sprite)
        {
            position = sprite.Position - new Vector2(ProgramConfig.windowWidth / 2, ProgramConfig.windowHeight / 2);
        }

        public override Matrix GetTranslationMatrix()
        {
            return Matrix.CreateTranslation(-position.X, -position.Y, 0f);
        }
    }
}
