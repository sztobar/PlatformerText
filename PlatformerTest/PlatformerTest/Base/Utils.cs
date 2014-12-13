using Microsoft.Xna.Framework;

namespace PlatformerTest.Base
{
    public static class Utils
    {
        public static Point ToPoint(this Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }
    }
}
