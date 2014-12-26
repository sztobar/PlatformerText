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
        protected Point direction = new Point(1, 0);
        /// <summary>
        /// Maximum velocity of the sprite
        /// </summary>
        protected Vector2 maxVelocity = Vector2.One;
        /// <summary>
        /// Minimum velocity of the spirte(usually zero)
        /// </summary>
        protected Vector2 minVelocity = Vector2.Zero;
        /// <summary>
        /// Time in which tickTime is changing visible flag
        /// </summary>
        protected float flashDuration;
        /// <summary>
        /// Time between toogling visible flag
        /// </summary>
        protected float tickTime;
        /// <summary>
        /// Duration to next visible toogling
        /// </summary>
        protected float tickDuration;
        /// <summary>
        /// Whetever Draw ActiveObject or not
        /// </summary>
        protected bool visible = true;
        /// <summary>
        /// Time in miliseconds in which ActiveObject can't move
        /// </summary>
        protected float blockDuration;

        #endregion

        #region properties

        public Point Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        #endregion

        #region constructors

        public MovableSprite(String textureSource, Point hitbox)
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
            if (flashDuration > 0)
            {
                if ((tickTime -= dt) <= 0)
                {
                    visible = !visible;
                    tickTime = tickDuration;
                }

                if ((flashDuration -= dt) <= 0)
                {
                    visible = true;
                }
            }

            if (blockDuration > 0)
            {
                blockDuration -= dt;
            }
            else
            {
                position += new Vector2(direction.X*velocity.X, direction.Y*velocity.Y);
            }
        }

        #endregion

        #region own metods

        // Probably methods below will change to velocity properties one day...

        public void AccelerateX()
        {
            var delta = acceleration.X * maxVelocity.X;
            if (velocity.X + delta > maxVelocity.X)
                velocity.X = maxVelocity.X;
            else
                velocity.X += delta;
        }

        public void AccelerateY()
        {
            var delta = acceleration.Y * maxVelocity.Y;
            if (velocity.Y + delta > maxVelocity.Y)
                velocity.Y = maxVelocity.Y;
            else
                velocity.Y += delta;
        }

        public void DecelerateX()
        {
            var delta = deceleration.X * maxVelocity.X;
            if (velocity.X - delta < minVelocity.X)
                velocity.X = minVelocity.X;
            else
                velocity.X -= delta;
        }

        public void DecelerateY()
        {
            var delta = deceleration.Y * maxVelocity.Y;
            if (velocity.Y - delta < minVelocity.Y)
                velocity.Y = minVelocity.Y;
            else
                velocity.Y -= delta;
        }

        /// <summary>
        /// Makes ActiveObject Flash
        /// </summary>
        /// <param name="duration">Time in seconds</param>
        /// <param name="tickTime">Time in seconds</param>
        public void Flash(float duration, float tickTime)
        {
            flashDuration = duration;
            visible = false;
            this.tickTime = tickTime;
            tickDuration = tickTime;
        }

        /// <summary>
        /// Make ActiveObject omit its Update Method
        /// To be used in hurt phase
        /// </summary>
        /// <param name="duration">Time in seconds</param>
        public void Block(float duration)
        {
            blockDuration = duration;
        }

        #endregion
    }
}
