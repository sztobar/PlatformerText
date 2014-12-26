using System.Linq;
using Microsoft.Xna.Framework;
using PlatformerTest.Base;
using PlatformerTest.Interfaces;

namespace PlatformerTest.CameraGame
{
    /// <summary>
    /// Class for bullets shot by naq
    /// </summary>
    class NaqBullet : LevelSprite
    {
        private const byte power = 10;

        public NaqBullet(Base.Level level, Vector2 position, Point direction)
            : base(level, "player/cameraPlayer", new Point(10, 10))
        {
            this.position = position;
            this.direction = direction;
            this.velocity = new Vector2(10.0f, 0);
        }

        public override void Update(float dt, UserInputState inputState)
        {
            base.Update(dt, inputState);

            if (!level.IsInView(this) || level.IsCollidingObstacle(this))
            {
                Destroy();
                return;
            }

            var enemies = level.GetEnemiesInViewport();
            foreach (var enemy in enemies)
            {
                if (IsColliding(enemy))
                {
                    enemy.Lives -= power;
                    enemy.Flash(0.5f, 0.1f);
                    Destroy();
                    break;
                }
            }
        }
    }
}
