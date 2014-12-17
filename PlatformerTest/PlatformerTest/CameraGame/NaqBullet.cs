using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTest.Base;
using PlatformerTest.Interfaces;

namespace PlatformerTest.CameraGame
{
    /// <summary>
    /// Class for bullets shot by naq
    /// </summary>
    class NaqBullet : LevelSprite
    {
        private readonly Direction direction;
        private readonly Vector2 velocity = new Vector2(10.0f, 0);

        public NaqBullet(Base.Level level, Vector2 position, Direction direction)
            : base(level, "player/cameraPlayer", new Point(10, 10))
        {
            this.position = position;
            this.direction = direction;
        }

        public override void Update(float dt, UserInputState inputState)
        {
            base.Update(dt, inputState);
            this.position += (int) this.direction*this.velocity;

            if (!level.IsInView(this) || level.IsCollidingObstacle(this))
            {
                Destroy();
                return;
            }

            level.ForEachComponent(sprite =>
            {
                if (sprite == this || !IsColliding(sprite)) return true;
                if (sprite is IEnemy)
                {

                }
                return true;
            });
        }
    }
}
