using System;
using Microsoft.Xna.Framework;
using PlatformerTest.Interfaces;

namespace PlatformerTest.Base
{
    public class EnemySprite : LevelSprite, IEnemy
    {
        /// <summary>
        /// Describing hp of the enemy. Shnould be in decimal
        /// so little damage projectiles(or posion DoT) could be
        /// included later on. 
        /// </summary>
        protected byte lives = 20;

        public byte Lives
        {
            get { return lives; }

            set
            {
                if (value <= 0)
                {
                    Destroy();
                }
                lives = value;
            }
        }

        public EnemySprite(Base.Level level, String textureSource, Point hitbox)
            : base(level, textureSource, hitbox)
        {
        }
    }
}
