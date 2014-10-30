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
        protected SpriteAnimation _animation;
        public SpriteAnimation Sprite
        {
            get { return _animation; }
        } 

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
            _animation = new SpriteAnimation(texture);
        }

        public ActiveObject(Texture2D texture, Rectangle rectangle)
        {
            _texture = texture;
            _position.X = rectangle.X;
            _position.Y = rectangle.Y;
            _width = rectangle.Width;
            _height = rectangle.Height;
            _animation = new SpriteAnimation(texture);
        }

        public virtual void Update(float dt)
        {
            _position.X += (int)_direction * _velocity.X;
            _position.Y += _velocity.Y;
            _animation.Position = _position;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Rectangle drawRectangle = new Rectangle((int)_position.X, (int)_position.Y, _width, _height);
            _animation.Draw(spriteBatch, (int)_position.X, (int)_position.Y);
            //spriteBatch.Draw(_texture, new Rectangle(_position.X,_position.Y,_width,_height), null, Color.White, 0f, _position,null,0);

            //spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, _width, _height), Color.White);
        }
    }
}
