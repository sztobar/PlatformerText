using Microsoft.Xna.Framework;

namespace PlatformerTest.Base
{
    public abstract class Player : MovableSprite
    {
        protected Movement movement;
        
        public Player(string textureSource, Vector2 hitbox)
            : base("player/" + textureSource, hitbox)
        {
        }

        #region override methods

        public override void Update(float dt, UserInputState inputState)
        {
            base.Update(dt, inputState);
            movement.GetInput(inputState);
        }

        #endregion
    }
}
