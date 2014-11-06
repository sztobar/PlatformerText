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
            float Right = 1, Down = 1, Left = -1, Up = -1;
            Vector2 direction = sprite.Direction;

            if (!input.turnLeft && !input.turnRight)
            {
                sprite.DecelerateX();
            }
            else
            {
                sprite.AccelerateX();
                if (input.turnLeft && direction.X == Right)
                    sprite.Direction = new Vector2(Left, direction.Y);
                else if (input.turnRight && direction.X == Left)
                    sprite.Direction = new Vector2(Right, direction.Y);
            }

            if (!input.turnUp && !input.turnDown)
            {
                sprite.DecelerateY();
            }
            else
            {
                sprite.AccelerateY();
                if (input.turnUp && direction.Y == Down)
                    sprite.Direction = new Vector2(direction.X, Up);
                else if (input.turnDown && direction.Y == Up)
                    sprite.Direction = new Vector2(direction.X, Down);
            }
        }
    }
}
