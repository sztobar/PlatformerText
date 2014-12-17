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
        /// Because hitbox rectangle starts from position, hitbox is represented
        /// only in Point(where X is width and Y is height)
        /// </summary>
        protected Point hitbox;
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

        public Texture2D Texture {
            get { return texture; }
            set { texture = value; }
        }

        public int Width { get { return hitbox.X; }}

        public int Height { get { return hitbox.Y; } }

        public Rectangle Rectangle { get { return new Rectangle((int) position.X, (int) position.Y, hitbox.X, hitbox.Y); } }

        #endregion

        #region constructors

        /// <param name="textureSource">path to load texture</param>
        /// <param name="hitbox">Width and Height of the sprite</param>
        protected Sprite (string textureSource, Point hitbox)
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
            spriteBatch.Draw(texture, Rectangle, Color.White);
        }

        #endregion

        #region public methods

        public bool IsColliding(Sprite sprite)
        {
            return this.Rectangle.Intersects(sprite.Rectangle);
        }

        #endregion

    }
}
