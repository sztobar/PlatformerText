using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTest.Base;

namespace PlatformerTest.CameraGame
{
    class CameraTestEnemy : EnemySprite
    {
        public CameraTestEnemy(Base.Level level, Vector2 position)
            : base(level, "base", new Point(32, 64))
        {
            this.position = position;
            this.direction = new Point(new Random().Next(0, 2) == 0 ? -1 : 1, 0);
            this.velocity = new Vector2(1.0f, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!visible) return;
            spriteBatch.Draw(texture, Rectangle, Color.Red);
        }

        public override void Update(float dt, UserInputState inputState)
        {
            var tileSize = ProgramConfig.tileSize;
            var rectangle = Rectangle;
            var bottom = rectangle.Bottom/tileSize;
            var top = rectangle.Top/tileSize;
            var positionX = direction.X == -1 ? rectangle.Left : rectangle.Right;
            var nextTileX = positionX/tileSize;
            var tiles = level.GetCollisionTiles();
            const int collisionTile = 4;

            for (var y = bottom - 1; y >= top; --y)
            {
                if (tiles[nextTileX][y] == collisionTile)
                {
                    Bounce();
                    return;
                }
            }
            if (tiles[nextTileX][bottom] == 0)
            {
                Bounce();
                return;
            }
            base.Update(dt, inputState);
        }

        private void Bounce()
        {
            this.direction.X = - this.direction.X;
        }
    }
}
