using System;
using Microsoft.Xna.Framework;
using PlatformerTest.Base;

namespace PlatformerTest.CameraGame
{
    public class CameraTestPlayer : Base.Player
    {
        private readonly Base.Level level;

        public CameraTestPlayer(Base.Level level)
            : base("cameraPlayer", new Point(32, 32))
        {
            this.level = level;
            movement = new EightDirectionMovement(this);
        }

        public override void Initialize()
        {
            Position = new Vector2(330, 250);
            acceleration = new Vector2(0.2f, 0.2f);
            deceleration = new Vector2(0.1f, 0.1f);
            maxVelocity = new Vector2(10, 10);
            minVelocity = new Vector2(0, 0);
        }

        protected override void GetCollisions()
        {
            var tileSize = ProgramConfig.tileSize;
            var left = (int)position.X / tileSize;
            var top = (int)position.Y / tileSize;
            var right = ((int)position.X + Width - 1) / tileSize;
            var bottom = ((int)position.Y + Height - 1) / tileSize;

            var tiles = level.GetCollisionTiles();
            var levelRightBoundary = tiles.Length;
            var levelBottomBoundary = tiles[0].Length;

            const int directionLeft = -1;
            const int directionRight = 1;
            const int collisionTile = 4;
            

            //if (direction.X == directionRight)
            //{
            //    for (var y = top; y <= bottom; ++y)
            //    {
            //        if (right + 1 != levelRightBoundary && tiles[right + 1][y] != collisionTile) continue;
            //        velocity.X = Math.Min(velocity.X, ((right + 1) * tileSize) - (position.X + Width));
            //    }
            //}
            //else if (direction.X == directionLeft)
            //{
            //    for (var y = top; y <= bottom; ++y)
            //    {
            //        if (left != 0 && tiles[left - 1][y] != collisionTile) continue;
            //        velocity.X = Math.Min(velocity.X, position.X - (left * tileSize));
            //    }
            //}
        }
    }
}
