using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerTest
{
    public class ActiveObject
    {
        #region members

        protected Texture2D _texture;
        protected Vector2 _velocity;
        protected Vector2 _position;
        protected Direction _direction = Direction.Left;
        protected int _width;
        protected int _height;
        protected float _rotation = 0f;
        protected Vector2 _hotSpot = new Vector2(0f, 0f);

        #endregion
        #region properties
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)(_position.X - _hotSpot.X), (int)(_position.Y - _hotSpot.Y), _width, _height);
            }
        }
        //public Vector2 Position
        //{
        //    get
        //    {
        //        return _position + _hotSpot;
        //    }
        //}
        #endregion

        public ActiveObject(Texture2D texture, Vector2 position, int width, int height)
        {
            _texture = texture;
            _position = position;
            _width = width;
            _height = height;
        }

        public ActiveObject(Texture2D texture, Rectangle rectangle)
        {
            _texture = texture;
            _position.X = rectangle.X;
            _position.Y = rectangle.Y;
            _width = rectangle.Width;
            _height = rectangle.Height;
        }

        public virtual void Update(float dt)
        {
            _position.X += (int)_direction * _velocity.X;
            _position.Y += _velocity.Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawRectangle = new Rectangle((int)_position.X, (int)_position.Y, _width, _height);
            spriteBatch.Draw(_texture, drawRectangle, Color.White);
            //spriteBatch.Draw(_texture, drawRectangle, null, Color.White, _rotation, _hotSpot, SpriteEffects.None, 0f);
        }
    }
}
