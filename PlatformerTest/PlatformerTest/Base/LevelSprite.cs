using System;
using Microsoft.Xna.Framework;
using PlatformerTest.Interfaces;

namespace PlatformerTest.Base
{
    /// <summary>
    /// Class for sprite dynamically added to Base.Level through AddComponent method
    /// Initialization & LoadContent are invoked in Base.Level right after LevelSprite constructor
    /// </summary>
    public class LevelSprite : Sprite, ILevelDestroyable
    {
        protected Base.Level level;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="textureSource"></param>
        /// <param name="hitbox"></param>
        /// <param name="addComponent">flag that determines if LevelSprite should add itself to level, or is it instanced as AddCompoent argument</param>
        public LevelSprite(Level level, String textureSource, Point hitbox, bool addComponent = false)
            : base(textureSource, hitbox)
        {
            this.level = level;
            if (addComponent)
                level.AddComponent(this);
        }

        #region implements

        /// <summary>
        /// Remove itself from level components list.
        /// LevelSprite should't have references elsewhere
        /// so garbage collector sould clear memory after that
        /// </summary>
        public void Destroy()
        {
            level.DestroyComponent(this);
        }

        #endregion
    }
}
