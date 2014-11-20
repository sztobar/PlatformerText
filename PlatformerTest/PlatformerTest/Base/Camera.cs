using Microsoft.Xna.Framework;

namespace PlatformerTest.Base
{
    public abstract class Camera
    {
        protected Vector2 position;

        public Vector2 Position { get { return position; } }

        public float X { get { return position.X; } }

        public float Y { get { return position.Y; } }

        abstract public void SetPosition(Sprite sprite);

        abstract public Matrix GetTranslationMatrix();

        public virtual Rectangle GetRectangle() { return Rectangle.Empty; }
    }
}
