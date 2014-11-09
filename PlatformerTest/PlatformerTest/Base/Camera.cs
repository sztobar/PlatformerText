using Microsoft.Xna.Framework;

namespace PlatformerTest.Base
{
    public abstract class Camera
    {
        protected Vector2 position;

        public Vector2 Position { get { return position; } }

        abstract public void SetPosition(Sprite sprite);

        abstract public Matrix GetTranslationMatrix();

        public virtual Rectangle GetRectangle() { return Rectangle.Empty; }
    }
}
