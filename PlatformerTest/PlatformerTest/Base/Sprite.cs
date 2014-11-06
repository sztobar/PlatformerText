using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PlatformerTest.Base
{
    public class Sprite : Component
    {
        #region protected

        protected Texture2D texture;
        /// <summary>
        /// Because hitbox rectangle starts from position, its represented
        /// only in vector(where X is width and Y is height)
        /// </summary>
        protected Vector2 hitbox;
        protected Vector2 position;
        /// <summary>
        /// Path to texture content
        /// </summary>
        protected string textureSource;

        #endregion

        #region properties

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion

        #region constructors

        protected Sprite (string textureSource, Vector2 hitbox)
        {
            this.textureSource = textureSource;
            this.hitbox = hitbox;
        }

        #endregion

        #region overrides

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureSource);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture,
                new Rectangle((int)position.X, (int)position.Y, (int)hitbox.X, (int)hitbox.Y),
                Color.White);
        }

        #endregion

    }
}
