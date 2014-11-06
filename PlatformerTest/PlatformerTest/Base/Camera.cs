using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformerTest.Base
{
    abstract class Camera
    {
        protected Vector2 position;

        abstract public void SetPosition(Sprite sprite);

        abstract public Matrix GetTranslationMatrix();
    }
}
