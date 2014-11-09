using Microsoft.Xna.Framework;

namespace PlatformerTest.CameraGame
{
    public class CameraTestPlayer : Base.Player
    {
        public CameraTestPlayer()
            : base("cameraPlayer", new Vector2(32, 32))
        {
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
    }
}
