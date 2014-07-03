using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong
{
    public class PowerUp : BallObject
    {
        private int _type;

        //animation
        private int _frameCount, _switchFrame;
        private Vector2 _currentFrame, _amountOfFrames;
        private Rectangle _sourceRect;
        private bool _active;

        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int FrameWidth
        {
            get { return texture.Width / (int)_amountOfFrames.X; }
        }

        public int FrameHeight
        {
            get { return texture.Height / (int)_amountOfFrames.Y; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, FrameWidth, FrameHeight); }
        }

        public PowerUp(Game game, Texture2D Texture, Vector2 Position, int StageHeight, int StageWidth, Vector2 Speed, int Type) : base(game)
        {
            texture = Texture;
            position = Position;
            stageHeight = StageHeight;
            stageWidth = StageWidth;
            speed = Speed;
            _type = Type;
            this.game = game;

            //animation
            _active = true;
            _switchFrame = 20;
            _amountOfFrames = new Vector2(8, 6);
        }

        public override void Update(GameTime gameTime)
        {
            position.X += speed.X;
            position.Y += speed.Y;

            if (_active)
                _frameCount += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            else
                _frameCount = 0;

            if (_frameCount >= _switchFrame)
            {
                _frameCount = 0;
                _currentFrame.X += FrameWidth;
                if (_currentFrame.X >= texture.Width)
                    _currentFrame.X = 0;
            }
            _sourceRect = new Rectangle((int)_currentFrame.X, (int)_currentFrame.Y * FrameHeight, FrameWidth, FrameHeight);

            switch(_type)
            {
                case 1:
                    _currentFrame.Y = 1;
                    break;
                case 2:
                    _currentFrame.Y = 2;
                    break;
                case 3:
                    _currentFrame.Y = 3;
                    break;
                case 4:
                    _currentFrame.Y = 5;
                    break;
                case 5:
                    _currentFrame.Y = 4;
                    break;
                default:
                    _active = false;
                    break;
            }

            if (position.Y + FrameHeight >= stageHeight)
                speed.Y = -speed.Y;
            else if (position.Y <= 0)
                speed.Y = 5;
             
            if (position.X + FrameWidth >= stageWidth)
                speed.X = -speed.X;
            else if (position.X <= 0)
                speed.X = 5;

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, _sourceRect, Color.White);
        }
    }
}
