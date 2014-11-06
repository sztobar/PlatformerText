using Microsoft.Xna.Framework;
using System;

namespace PlatformerTest.Base
{
    public class MovableSprite : Sprite
    {
        #region variables

        /// <summary>
        /// On each Update method velocity is added to position
        /// </summary>
        protected Vector2 velocity = Vector2.Zero;
        /// <summary>
        /// Each dimension is value from 0 to 1 and it
        /// describes how fast sprite goes from actual velocity
        /// to minVelocity
        /// </summary>
        protected Vector2 deceleration = Vector2.One;
        /// <summary>
        /// Each dimension is value from 0 to 1 and it
        /// describes how fast sprite reaches maxVelocity
        /// </summary>
        protected Vector2 acceleration = Vector2.One;
        /// <summary>
        /// Describes if velocity on x and y dimension is
        /// in positive or negative direction(should have either 1 or -1 as values)
        /// </summary>
        protected Vector2 direction = Vector2.One;
        /// <summary>
        /// Maximum velocity of the sprite
        /// </summary>
        protected Vector2 maxVelocity = Vector2.One;
        /// <summary>
        /// Minimum velocity of the spirte(usually zero)
        /// </summary>
        protected Vector2 minVelocity = Vector2.Zero;

        #endregion

        #region properties

        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        #endregion

        #region constructors

        public MovableSprite(String textureSource, Vector2 hitbox)
            : base(textureSource, hitbox)
        { }

        #endregion

        #region override methods

        /// <summary>
        /// Move object on every Update
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="inputState"></param>
        public override void Update(float dt, UserInputState inputState)
        {
            position += direction * velocity;
        }

        #endregion

        #region own metods

        // Probably methods below will change to velocity properties one day...

        public void AccelerateX()
        {
            float delta = acceleration.X * maxVelocity.X;
            if (velocity.X + delta > maxVelocity.X)
                velocity.X = maxVelocity.X;
            else
                velocity.X += delta;
        }

        public void AccelerateY()
        {
            float delta = acceleration.Y * maxVelocity.Y;
            if (velocity.Y + delta > maxVelocity.Y)
                velocity.Y = maxVelocity.Y;
            else
                velocity.Y += delta;
        }

        public void DecelerateX()
        {
            float delta = deceleration.X * maxVelocity.X;
            if (velocity.X - delta < minVelocity.X)
                velocity.X = minVelocity.X;
            else
                velocity.X -= delta;
        }

        public void DecelerateY()
        {
            float delta = deceleration.Y * maxVelocity.Y;
            if (velocity.Y - delta < minVelocity.Y)
                velocity.Y = minVelocity.Y;
            else
                velocity.Y -= delta;
        }

        #endregion


    }
}
