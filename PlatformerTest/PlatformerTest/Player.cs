using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using PlatformerTest.Base;
using PlatformerTest.CameraGame;
using PlatformerTest.Interfaces;

namespace PlatformerTest
{
    public class Player : ActiveObject
    {
        #region variables

        float _acceleration = 10f;
        float _deceleration = 10f;
        float _gravity = 15f;
        float _jumping = 6f;
        float _maxXVelocity = 5f;
        float _maxYVelocity = 30f;
        bool _standing = true;
        bool _climbing = false;
        bool _jumpingStatus = false;
        bool _landing = false;
        float _climbingVelocity = 0f;
        Direction _lastDirection;
        int _playerFrameHeight = 65;//130;
        int _playerFrameWidth = 50;//100;
        float _playerFrameLength = 0.1f;//0.1f;
        public float _playerOffset = 231;//462;
        public float _playerHeightOffset = 256.5f;//513;
        /// <summary>
        /// Time left in seconds to next shoot enabled
        /// </summary>
        private float _shootingDelayLeft = 0;
        /// <summary>
        /// Seconds intervals between player shooting
        /// </summary>
        private const float _shootingDelay = 0.5f;

        #endregion
        #region helper

        public Texture2D _collisionTexture;

        /// <summary>
        /// Vector2 describing point where projectiles visible(when player shoots)
        /// </summary>
        public Vector2 ActionPoint { get { return _position + new Vector2(_width/2.0f, _height/2.0f); } }

        #endregion

        public Player(Texture2D texture)
            : base(texture, new Vector2(60, 60), 32, 64)
        {
            #region animations

            // run animations
            Sprite.AddAnimation("runLeft", 400, 65, _playerFrameWidth, _playerFrameHeight, 8, _playerFrameLength);
            Sprite.AddAnimation("runRight", 0, 65, _playerFrameWidth, _playerFrameHeight, 8, _playerFrameLength);
            // idle animations 
            Sprite.AddAnimation("rightStop", 0, 130, _playerFrameWidth, _playerFrameHeight, 1, 1f, "rightStop2");
            Sprite.AddAnimation("rightStop2", 50, 130, _playerFrameWidth, _playerFrameHeight, 1, 1f, "rightStop");
            Sprite.AddAnimation("leftStop", 150, 130, _playerFrameWidth, _playerFrameHeight, 1, 1f, "leftStop2");
            Sprite.AddAnimation("leftStop2", 200, 130, _playerFrameWidth, _playerFrameHeight, 1, 1f, "leftStop");
            // jump right animations
            Sprite.AddAnimation("preJumpRight", 0, 0, _playerFrameWidth, _playerFrameHeight, 3, _playerFrameLength, "jumpUpLoopRight");
            Sprite.AddAnimation("jumpUpLoopRight", 150, 0, _playerFrameWidth, _playerFrameHeight, 1, _playerFrameLength);
            Sprite.AddAnimation("jumpMaxPointRight", 200, 0, _playerFrameWidth, _playerFrameHeight, 1, _playerFrameLength);
            Sprite.AddAnimation("jumpDownLoopRight", 250, 0, _playerFrameWidth, _playerFrameHeight, 1, _playerFrameLength);
            Sprite.AddAnimation("landingRight", 300, 0, _playerFrameWidth, _playerFrameHeight, 4, _playerFrameLength, "rightStop");
            // jump left animations
            Sprite.AddAnimation("preJumpLeft", 500, 0, _playerFrameWidth, _playerFrameHeight, 3,  _playerFrameLength, "jumpUpLoopLeft");
            Sprite.AddAnimation("jumpUpLoopLeft", 650, 0,  _playerFrameWidth, _playerFrameHeight, 1, _playerFrameLength);
            Sprite.AddAnimation("jumpMaxPointLeft", 700, 0,  _playerFrameWidth, _playerFrameHeight, 1, _playerFrameLength);
            Sprite.AddAnimation("jumpDownLoopLeft", 750, 0, _playerFrameWidth, _playerFrameHeight, 1,_playerFrameLength);
            Sprite.AddAnimation("landingLeft", 800,0, _playerFrameWidth, _playerFrameHeight,4,_playerFrameLength);
            // end of animations

            #endregion

            _animation.CurrentAnimation = "rightStop";
            _animation.Position = _position;
        }

        private void GetInput(float dt, KeyboardState keyState)
        {
            IsClimbing(dt, keyState);
            if (!_climbing)
            {
                IsWalking(dt, keyState);
                IsJumping(dt, keyState);
                IsShooting(dt, keyState);
            }
            GetDirection(dt, keyState);
        }

        private void IsJumping(float dt, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.X) &&
                _velocity.Y == 0 && _standing)
            {
                _velocity.Y = -_jumping;
                _jumpingStatus = true;
                if (_direction == Direction.Left)
                {
                    Sprite.CurrentAnimation = "preJumpLeft";
                }
                else {
                    Sprite.CurrentAnimation = "preJumpRight";
                }
                
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
                    //int TILE_SIZE = Level.TILE_SIZE;
                    //int left = (int)_position.X / TILE_SIZE;
                    //int top = (int)_position.Y / TILE_SIZE;
                    //int right = ((int)_position.X + _width - 1) / TILE_SIZE;
                    //int bottom = ((int)_position.Y + _height - 1) / TILE_SIZE;

                    //int leftCheck = ((int)_position.X - (TILE_SIZE / 4)) / TILE_SIZE;
                    //int rightCheck = ((int)_position.X + _width - 1 + (TILE_SIZE / 4)) / TILE_SIZE;
                    
                    //int [,] tiles = Game1.level.Tiles;
                    //if (rightCheck <= Game1.level.mapWidth-1 && leftCheck>=0) {
                    //    if (tiles[rightCheck, top] == 3)
                    //    {
                    //        _climbing = true;
                    //        _velocity.Y = 0;
                    //        _position.X = rightCheck * TILE_SIZE;
                    //    }
                    //    else if (tiles[leftCheck, top] == 3)
                    //    {
                    //        _climbing = true;
                    //        _velocity.Y = 0;
                    //        _position.X = leftCheck * TILE_SIZE;
                    //    }
                    //}                  

                }
            }
        }

        private void IsShooting(float dt, KeyboardState keyState)
        {
            if (_shootingDelayLeft > 0) _shootingDelayLeft -= dt;
            if (keyState.IsKeyDown(Keys.Z) && _shootingDelayLeft <= 0)
            {
                var bulletDirection = new Point((int) _direction, 0);
                var bullet = new NaqBullet(ProgramConfig.CurrentLevel, ActionPoint, bulletDirection);
                ProgramConfig.CurrentLevel.AddComponent(bullet);
                _shootingDelayLeft = _shootingDelay;
            }
        }

        private void GetDirection(float dt, KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Right) &&
                _direction == Direction.Left)
            {
                _direction = Direction.Right;
            }
            else if (keyState.IsKeyDown(Keys.Left) &&
                _direction == Direction.Right)
            {
                _direction = Direction.Left;
            }
            if (!keyState.IsKeyDown(Keys.Right) && !keyState.IsKeyDown(Keys.Left) && !_jumpingStatus)
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
            //int [,] tiles = Game1.level.Tiles;
            int[,] tiles = ProgramConfig.LevelTiles;
            int levelRightBoundary = tiles.GetLength(0);
            int levelBottomBoundary = tiles.GetLength(1);
            const int collisionTile = 4;

            if (_direction == Direction.Right)
            {
                for (int y = top; y <= bottom; ++y)
                {
                    if (right + 1 == levelRightBoundary || tiles[right + 1, y] == collisionTile)
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
                    if (left == 0 || tiles[left - 1, y] == collisionTile)
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
                    if (bottom + 1 == levelBottomBoundary || tiles[x, bottom + 1] == collisionTile || tiles[x, bottom + 1] == 2)
                    {
                        _velocity.Y = Math.Min(_velocity.Y, ((bottom + 1) * TILE_SIZE) - _position.Y - _height);
                        _standing = _velocity.Y == 0;
                        //play landing animation
                        if (_jumpingStatus) 
                        {
                            if (_direction == Direction.Left
                                && _standing
                                && Sprite.CurrentAnimation != "landingLeft"
                                )
                            {
                                if (Sprite.CurrentAnimation == "jumpDownLoopLeft" || Sprite.CurrentAnimation == "jumpDownLoopRight" || Sprite.CurrentAnimation == "rightStop" || Sprite.CurrentAnimation == "leftStop")
                                {
                                    Sprite.CurrentAnimation = "landingLeft";
                                    _jumpingStatus = false;
                                }
                            }else if (_standing
                             && Sprite.CurrentAnimation != "landingRight"
                            )
                            {
                                if (Sprite.CurrentAnimation == "jumpDownLoopLeft" || Sprite.CurrentAnimation == "jumpDownLoopRight" || Sprite.CurrentAnimation == "rightStop" || Sprite.CurrentAnimation == "leftStop")
                                {
                                    Sprite.CurrentAnimation = "landingRight";
                                    _jumpingStatus = false;
                                }
                            }
                        }                          

                        break;
                    }
                }
            }
            else if (_velocity.Y < 0)
            {
                _standing = false;
                for (int x = left; x <= right; ++x)
                {
                    if (top == 0 || tiles[x, top - 1] == collisionTile)
                    {
                        _velocity.Y = Math.Max(_velocity.Y, (top * TILE_SIZE) - _position.Y);
                        break;
                    }
                }
            }
            _velocity.Y = Math.Min(_velocity.Y, _maxYVelocity);
        }

        /// <summary>
        /// If Naq arent flashing(invicibility delay after hit)
        /// Check if he is colliding any enemy
        /// </summary>
        private void CheckEnemyCollisions()
        {
            if (_flashDuration > 0) return;
            var level = ProgramConfig.CurrentLevel;
            var enemies = level.GetEnemiesInViewport();
            var boundingBox = BoundingBox;
            if (enemies.Select(enemy => enemy.Rectangle).Any(boundingBox.Intersects))
            {
                const float duration = 1.5f;
                Flash(duration * 2, 0.1f);
                Block(duration);
            }
        }

        public void Update(float dt, KeyboardState keyState, GameTime gameTime)
        {
            if (!(_blockDuration > 0))
            {
                GetInput(dt, keyState);
                CheckCollisions(dt);
                CheckEnemyCollisions();

                //checking direction to figureout which animation have to be played:
                if (!_climbing && !_landing && !_jumpingStatus)
                {
                    if (_velocity.X > 0 && _velocity.Y == 0
                        && Sprite.CurrentAnimation != "runRight"
                        && _direction == Direction.Right)
                    {
                        //play run right animation
                        Sprite.CurrentAnimation = "runRight";
                    }
                    else if (_velocity.X > 0 && _velocity.Y == 0
                             && Sprite.CurrentAnimation != "runLeft"
                             && _direction == Direction.Left)
                    {
                        //play run left animation
                        Sprite.CurrentAnimation = "runLeft";
                    }
                    else if (_velocity.Y > 0)
                    {
                        if (_direction == Direction.Left)
                        {
                            Sprite.CurrentAnimation = "jumpDownLoopLeft";
                        }
                        else
                        {
                            Sprite.CurrentAnimation = "jumpDownLoopRight";
                        }
                        _jumpingStatus = true;
                    }
                }
                else
                {
                    if (_jumpingStatus)
                    {

                        if (_direction == Direction.Right
                            && Sprite.CurrentAnimation != "preJumpRight"
                            && Sprite.CurrentAnimation != "preJumpLeft"
                            && Sprite.CurrentAnimation != "jumpUpLoopRight"
                            && _velocity.Y > -3f
                            )
                        {
                            //jump loop up right
                            Sprite.CurrentAnimation = "jumpUpLoopRight";
                        }

                        if (_direction == Direction.Right
                            && _velocity.Y > -3f
                            && _velocity.Y < 3f
                            && Sprite.CurrentAnimation != "jumpMaxPointRight"
                            )
                        {
                            // jump Max point right
                            Sprite.CurrentAnimation = "jumpMaxPointRight";
                        }

                        if (_direction == Direction.Right
                            && _velocity.Y < 3f
                            && Sprite.CurrentAnimation != "landingRight"
                            && Sprite.CurrentAnimation != "jumpDownLoopRight"
                            )
                        {
                            //jump loop down right
                            Sprite.CurrentAnimation = "jumpDownLoopRight";
                        }

                        // left side

                        if (_direction == Direction.Left
                            && Sprite.CurrentAnimation != "preJumpRight"
                            && Sprite.CurrentAnimation != "preJumpLeft"
                            && Sprite.CurrentAnimation != "jumpUpLoopLeft"
                            && _velocity.Y > -3f
                            )
                        {
                            //jump loop up Left
                            Sprite.CurrentAnimation = "jumpUpLoopLeft";
                        }

                        if (_direction == Direction.Left
                            && _velocity.Y > -3f
                            && _velocity.Y < 3f
                            && Sprite.CurrentAnimation != "jumpMaxPointLeft"
                            )
                        {
                            // jump Max point Left
                            Sprite.CurrentAnimation = "jumpMaxPointLeft";
                        }

                        if (_direction == Direction.Left
                            && _velocity.Y < 3f
                            && Sprite.CurrentAnimation != "landingLeft"
                            && Sprite.CurrentAnimation != "jumpDownLoopLeft"
                            )
                        {
                            //jump loop down Left
                            Sprite.CurrentAnimation = "jumpDownLoopLeft";
                        }

                    }
                }
            }
            _animation.Update(gameTime);
            base.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
