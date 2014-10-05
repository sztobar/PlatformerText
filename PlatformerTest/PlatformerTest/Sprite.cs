using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerTest
{
    public class Sprite
    {
        #region protected
        protected Texture2D _texture;
        protected Rectangle _rectangle;
        // optional
        protected Rectangle _source;

        #endregion
        #region constructors

        public Sprite(Texture2D texture, Rectangle rectangle)
        {
            _texture = texture;
            _rectangle = rectangle;
        }

        public Sprite(Texture2D texture, Rectangle rectangle, Rectangle source)
            : this(texture, rectangle)
        {
            _source = source;
        }

        #endregion


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_source == null)
            {
                spriteBatch.Draw(_texture, _rectangle, Color.White);
            }
            else
            {
                spriteBatch.Draw(_texture, _rectangle, _source, Color.White);
            }
        }


    }
}
