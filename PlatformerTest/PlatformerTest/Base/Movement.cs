
namespace PlatformerTest.Base
{
    /// <summary>
    /// Visitor design Pattern, needs MovableSprite to work
    /// </summary>
    public abstract class Movement
    {
        protected MovableSprite sprite;

        public Movement(MovableSprite sprite)
        {
            this.sprite = sprite;
        }

        /// <summary>
        /// Reads user input and make changes(mainly velocity) in MovableSprite
        /// </summary>
        /// <param name="input"></param>
        abstract public void GetInput(UserInputState input);
    }
}
