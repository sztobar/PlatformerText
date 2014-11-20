using Microsoft.Xna.Framework;
using PlatformerTest.Base;

namespace PlatformerTest.CameraGame
{
    public class EightDirectionMovement : Base.Movement
    {
        public EightDirectionMovement(MovableSprite sprite)
            : base(sprite)
        { }

        override public void GetInput(UserInputState input)
        {
            const float right = 1;
            const float down = 1;
            const float left = -1;
            const float up = -1;
            var direction = sprite.Direction;

            if (!input.turnLeft && !input.turnRight)
            {
                sprite.DecelerateX();
            }
            else
            {
                sprite.AccelerateX();
                if (input.turnLeft && direction.X == right)
                    sprite.Direction = new Vector2(left, direction.Y);
                else if (input.turnRight && direction.X == left)
                    sprite.Direction = new Vector2(right, direction.Y);
            }

            if (!input.turnUp && !input.turnDown)
            {
                sprite.DecelerateY();
            }
            else
            {
                sprite.AccelerateY();
                if (input.turnUp && direction.Y == down)
                    sprite.Direction = new Vector2(direction.X, up);
                else if (input.turnDown && direction.Y == up)
                    sprite.Direction = new Vector2(direction.X, down);
            }
        }
    }
}
