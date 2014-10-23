using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace PlatformerTest
{
    public class Player : ActiveObject
    {
        #region variables

        float _acceleration = 10f;
        float _deceleration = 10f;
        float _gravity = 6f;
        float _jumping = 15f;
        float _maxXVelocity = 5f;
        float _maxYVelocity = 6f;
        bool _standing = true;
        bool _climbing = false;
        float _climbingVelocity = 0f;
        Direction _lastDirection;

        #endregion
        #region helper

        public Texture2D _collisionTexture;

        #endregion

        public Player(Texture2D texture)
            : base(texture, new Vector2(60, 60), 32, 64)
        {
            _hotSpot = new Vector2(_width / 2, _height);
            //animacje
            Sprite.AddAnimation("jump", 0, 0, 100, 130, 8, 0.1f);
            Sprite.AddAnimation("leftStop",900,130,100,130,1,0.1f);
            Sprite.AddAnimation("runLeft", 900, 130, 100, 130, 8, 0.1f);
            Sprite.AddAnimation("rightStop", 0, 130, 100, 130, 1, 0.1f);
            Sprite.AddAnimation("runRight", 0, 130, 100, 130, 8, 0.1f);
            //end of animacje
            _animation.CurrentAnimation = "rightStop";
            _animation.Position = _position;
            

        }

        public void GetInput(float dt, KeyboardState keyState)
        {
            IsClimbing(dt, keyState);
            if (!_climbing)
            {
                IsWalking(dt, keyState);
                IsJumping(dt, keyState);
            }
            GetDirection(dt, keyState);
        }

        private void IsJumping(float dt, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.X) &&
                _velocity.Y == 0 && _standing)
            {
                _velocity.Y = -_jumping;
            }
        }

        private void IsWalking(float dt, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Right) ||
                keyState.IsKeyDown(Keys.Left))
            {
                _velocity.X = Math.Min(_velocity.X + (_acceleration * dt), _maxXVelocity);
            }
            else
            {
                _velocity.X = Math.Max(_velocity.X - (_deceleration * dt), 0);
            }
        }

        private void IsClimbing(float dt, KeyboardState keyState)
        {
            if (_climbing)
            {
                int direction = 1;
                if (keyState.IsKeyDown(Keys.Up) ||
                    keyState.IsKeyDown(Keys.Down))
                {
                    _climbingVelocity += _acceleration * dt;
                }
                else
                {
                    _climbingVelocity = 0f;
                }

                if (keyState.IsKeyDown(Keys.Up))
                {
                    direction = -1;
                }
                else if (keyState.IsKeyDown(Keys.X))
                {
                    _climbing = false;
                }
                _velocity.Y = direction * _climbingVelocity;
            }
            else
            {
                if (keyState.IsKeyDown(Keys.Up))
                {
                    int TILE_SIZE = Level.TILE_SIZE;
                    int left = (int)_position.X / TILE_SIZE;
                    int top = (int)_position.Y / TILE_SIZE;
                    int right = ((int)_position.X + _width - 1) / TILE_SIZE;
                    int bottom = ((int)_position.Y + _height - 1) / TILE_SIZE;

                    int leftCheck = ((int)_position.X - (TILE_SIZE / 4)) / TILE_SIZE;
                    int rightCheck = ((int)_position.X + _width - 1 + (TILE_SIZE / 4)) / TILE_SIZE;
                    
                    int [,] tiles = Game1.level.Tiles;

                    if (tiles[rightCheck, top] == 3)
                    {
                        _climbing = true;
                        _velocity.Y = 0;
                        _position.X = rightCheck * TILE_SIZE;
                    }
                    else if (tiles[leftCheck, top] == 3)
                    {
                        _climbing = true;
                        _velocity.Y = 0;
                        _position.X = leftCheck * TILE_SIZE;
                    }

                }
            }
        }

        private void GetDirection(float dt, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Right) &&
                _direction == Direction.Left)
            {
                _direction = Direction.Right;
                Sprite.CurrentAnimation = "runRight";
            }
            else if (keyState.IsKeyDown(Keys.Left) &&
                _direction == Direction.Right)
            {
                _direction = Direction.Left;
                Sprite.CurrentAnimation = "runLeft";
            }
            if (!keyState.IsKeyDown(Keys.Right) && !keyState.IsKeyDown(Keys.Left))
            {
                if (_lastDirection == Direction.Left)
                {
                    Sprite.CurrentAnimation = "leftStop";
                }
                else
                {
                    Sprite.CurrentAnimation = "rightStop";
                }
                
            }
            _lastDirection = _direction;
        }

        public void CheckCollisions(float dt)
        {
            int TILE_SIZE = Level.TILE_SIZE;
            int left = (int)_position.X / TILE_SIZE;
            int top = (int)_position.Y / TILE_SIZE;
            int right = ((int)_position.X + _width - 1) / TILE_SIZE;
            int bottom = ((int)_position.Y + _height - 1) / TILE_SIZE;
            int [,] tiles = Game1.level.Tiles;
            int levelRightBoundary = tiles.GetLength(0);
            int levelBottomBoundary = tiles.GetLength(1);

            if (_direction == Direction.Right)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    if (right + 1 == levelRightBoundary || tiles[right + 1, y] == 1)
                    {
                        _velocity.X = Math.Min(_velocity.X, ((right + 1) * TILE_SIZE) - (_position.X + _width));
                        break;
                    }
                }
            }
            else if (_direction == Direction.Left)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    if (left == 0 || tiles[left - 1, y] == 1)
                    {
                        _velocity.X = Math.Min(_velocity.X, _position.X - (left * TILE_SIZE));
                        break;
                    }
                }
            }

            int xPosition = (int)_position.X + ((int)_direction * (int)_velocity.X);
            left = xPosition / TILE_SIZE;
            right = (xPosition + _width - 1) / TILE_SIZE;
            _velocity.Y += _gravity * dt;

            if (_velocity.Y >= 0)
            {
                for (int x = left; x <= right; ++x)
                {
                    if (bottom + 1 == levelBottomBoundary || tiles[x, bottom + 1] == 1 || tiles[x, bottom + 1] == 2)
                    {
                        _velocity.Y = Math.Min(_velocity.Y, ((bottom + 1) * TILE_SIZE) - _position.Y - _height);
                        _standing = _velocity.Y == 0;
                        break;
                    }
                }
            }
            else if (_velocity.Y < 0)
            {
                _standing = false;
                for (int x = left; x <= right; ++x)
                {
                    if (top == 0 || tiles[x, top - 1] == 1)
                    {
                        _velocity.Y = Math.Max(_velocity.Y, (top * TILE_SIZE) - _position.Y);
                        break;
                    }
                }
            }
            _velocity.Y = Math.Min(_velocity.Y, _maxYVelocity);
        }

        public void Update(float dt, KeyboardState keyState, GameTime gameTime)
        {
            GetInput(dt, keyState);
            CheckCollisions(dt);
            _animation.Update(gameTime);
            base.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
