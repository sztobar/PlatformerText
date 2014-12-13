using Microsoft.Xna.Framework;

namespace PlatformerTest.Base
{
    public abstract class Player : MovableSprite
    {
        protected Movement movement;
        
        protected Player(string textureSource, Point hitbox)
            : base("player/" + textureSource, hitbox)
        {
        }

        #region override methods

        public override void Update(float dt, UserInputState inputState)
        {
            movement.GetInput(inputState);
            GetCollisions();
            base.Update(dt, inputState);
        }

        protected virtual void GetCollisions() { }

        #endregion
    }
}
