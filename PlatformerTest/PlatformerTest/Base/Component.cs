using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerTest.Base
{
    /// <summary>
    /// Inspired by Microsoft.Xna.Framework.DrawableGameComponent
    /// Made for comfortable organising all objects in game.
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// Here we initialize important values so our
        /// constructors arent too broad
        /// </summary>
        public virtual void Initialize() { }
        /// <summary>
        /// He're we're loading sound effects, texture etc etc
        /// </summary>
        /// <param name="content"></param>
        abstract public void LoadContent(ContentManager content);
        /// <summary>
        /// In BaseComponent Update we want elapsed time in miliseconds from last Update
        /// and our userInputState
        /// </summary>
        /// <param name="dt">time in miliseconds from last Update</param>
        /// <param name="inputState">user input</param>
        public virtual void Update(float dt, UserInputState inputState) { }
        /// <summary>
        /// Draw function with spriteBattch provided
        /// </summary>
        /// <param name="spritebatch"></param>
        abstract public void Draw(SpriteBatch spritebatch);
    }
}
