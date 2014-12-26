using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
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
        protected Direction _direction = Direction.Right;
        protected int _width;
        protected int _height;
        protected float _rotation = 0f;
        protected Vector2 _hotSpot = new Vector2(0f, 0f);
        protected SpriteAnimation _animation;

        /// <summary>
        /// Time in which _tickTime is changing _appear flag
        /// </summary>
        protected float _flashDuration;
        /// <summary>
        /// Time between toogling _appear flag
        /// </summary>
        protected float _tickTime;
        /// <summary>
        /// Duration to next _appear toogling
        /// </summary>
        protected float _tickDuration;
        /// <summary>
        /// Whetever Draw ActiveObject or not
        /// </summary>
        protected bool _appear = true;
        /// <summary>
        /// Time in miliseconds in which ActiveObject can't move
        /// </summary>
        protected float _blockDuration;

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
            if (_flashDuration > 0)
            {
                _flashDuration -= dt;
                _tickTime -= dt;
                if (_tickTime <= 0)
                {
                    _appear = !_appear;
                    _tickTime = _tickDuration;
                }
                if (_flashDuration <= 0)
                {
                    _appear = true;
                }
            }

            if (_blockDuration > 0)
            {
                _blockDuration -= dt;
            }
            else
            {
                _position.X += (int)_direction * _velocity.X;
                _position.Y += _velocity.Y;
                _animation.Position = _position;
            }
        }

        /// <summary>
        /// Makes ActiveObject Flash
        /// </summary>
        /// <param name="duration">Time in seconds</param>
        /// <param name="tickTime">Time in seconds</param>
        public void Flash(float duration, float tickTime)
        {
            _flashDuration = duration;
            _appear = false;
            _tickTime = tickTime;
            _tickDuration = _tickTime;
        }

        /// <summary>
        /// Make ActiveObject omit its Update Method
        /// To be used in hurt phase
        /// </summary>
        /// <param name="duration">Time in seconds</param>
        public void Block(float duration)
        {
            _blockDuration = duration;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (!_appear) return;
            Rectangle drawRectangle = new Rectangle((int)_position.X, (int)_position.Y, _width, _height);
            _animation.Draw(spriteBatch, (int)_position.X, (int)_position.Y);
        }
    }
}
