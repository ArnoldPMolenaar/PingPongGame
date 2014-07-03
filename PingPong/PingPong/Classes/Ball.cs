using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong
{
    public class Ball : BallObject
    {
        private bool _isVisable = true;
        private bool _isSlowMotion = false;
        private Vector2 _slowMotion = new Vector2(1, 1);
        private Vector2 _oldPosition;
        private int _moving;
        private bool _checkMoving = true;
        private bool _isSlowX = true;
        public bool IsSlowX { get { return _isSlowX; } }

        public Vector2 SlowMotion 
        { 
            get { return _slowMotion; }
            set { _slowMotion = value; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }
        }

        public bool IsSlowMotion
        {
            get { return _isSlowMotion; }
            set { _isSlowMotion = value; }
        }

        public bool IsVisable
        {
            get { return _isVisable; }
            set { _isVisable = value; }
        }

        public Ball(Game game, Texture2D Texture, Vector2 Position, int StageHeight, int StageWidth, Vector2 Speed) : base(game)
        {
            texture = Texture;
            position = Position;
            stageHeight = StageHeight;
            stageWidth = StageWidth;
            speed = Speed;
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {
            if(_checkMoving == true)
            {
                position.X += speed.X;
                position.Y += speed.Y;
            }

            if (_isSlowMotion == false)
            {
                if (position.Y + texture.Height >= stageHeight)
                    speed.Y = -speed.Y;
                else if (position.Y <= 0)
                    speed.Y = 5;

                _oldPosition = position;
                _checkMoving = true;
            }
            else
            {
                if (position.X > _oldPosition.X && position.Y > _oldPosition.Y && _checkMoving == true)
                {
                    _moving = 1;
                    _checkMoving = false;
                }
                else if (position.X < _oldPosition.X && position.Y < _oldPosition.Y && _checkMoving == true)
                {
                    _moving = 2;
                    _checkMoving = false;
                }
                else if (position.X < _oldPosition.X && position.Y > _oldPosition.Y && _checkMoving == true)
                {
                    _moving = 3;
                    _checkMoving = false;
                }
                else if (position.X > _oldPosition.X && position.Y < _oldPosition.Y && _checkMoving == true)
                {
                    _moving = 4;
                    _checkMoving = false;
                }
                if (_checkMoving == false)
                {
                    switch (_moving)
                    {
                        case 1:
                            position.X += _slowMotion.X;
                            position.Y += _slowMotion.Y;
                            _isSlowX = true;
                            if (position.Y + texture.Height >= stageHeight)
                                _slowMotion.Y = -_slowMotion.Y;
                            else if (position.Y <= 0)
                                _slowMotion.Y = 1;
                            break;
                        case 2:
                            position.X -= _slowMotion.X;
                            position.Y -= _slowMotion.Y;
                            _isSlowX = false;
                            if (position.Y + texture.Height >= stageHeight)
                                _slowMotion.Y = 1;
                            else if (position.Y <= 0)
                                _slowMotion.Y = -_slowMotion.Y;
                            break;
                        case 3:
                            position.X -= _slowMotion.X;
                            position.Y += _slowMotion.Y;
                            _isSlowX = false;
                            if (position.Y + texture.Height >= stageHeight)
                                _slowMotion.Y = -_slowMotion.Y;
                            else if (position.Y <= 0)
                                _slowMotion.Y = 1;
                            break;
                        case 4:
                            position.X += _slowMotion.X;
                            position.Y -= _slowMotion.Y;
                            _isSlowX = false;
                            if (position.Y + texture.Height >= stageHeight)
                                _slowMotion.Y = 1;
                            else if (position.Y <= 0)
                                _slowMotion.Y = -_slowMotion.Y;
                            break;
                        default:
                            position.X += _slowMotion.X;
                            position.Y += _slowMotion.Y;
                            if (position.Y + texture.Height >= stageHeight)
                                _slowMotion.Y = -_slowMotion.Y;
                            else if (position.Y <= 0)
                                _slowMotion.Y = 1;
                            break;
                    }
                }
            }

            if (position.X >= stageWidth || position.X <= 0)
                _isVisable = false;

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
