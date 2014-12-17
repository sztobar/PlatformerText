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
            const short right = 1;
            const short down = 1;
            const short left = -1;
            const short up = -1;
            var direction = sprite.Direction;

            if (!input.turnLeft && !input.turnRight)
            {
                sprite.DecelerateX();
            }
            else
            {
                sprite.AccelerateX();
                if (input.turnLeft && direction.X == right)
                    sprite.Direction = new Point(left, direction.Y);
                else if (input.turnRight && direction.X == left)
                    sprite.Direction = new Point(right, direction.Y);
            }

            if (!input.turnUp && !input.turnDown)
            {
                sprite.DecelerateY();
            }
            else
            {
                sprite.AccelerateY();
                if (input.turnUp && direction.Y == down)
                    sprite.Direction = new Point(direction.X, up);
                else if (input.turnDown && direction.Y == up)
                    sprite.Direction = new Point(direction.X, down);
            }
        }
    }
}
