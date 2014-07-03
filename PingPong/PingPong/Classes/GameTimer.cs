using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PingPong
{
    public class GameTimer : GameComponent
    {
        private SpriteFont _font;
        private string _text;
        private Vector2 _position;
        private float _time;
        private bool _start;
        private bool _pause;
        private bool _stop;

        public GameTimer(Game game, float time)
            : base(game)
        {
            _time = time;
            _text = "";
            _start = false;
            _pause = false;
            _stop = false;
        }

        #region Properties

        public float Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public SpriteFont Font
        {
            get { return _font; }
            set { _font = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public bool Start
        {
            get { return _start; }
            set { _start = value; }
        }

        public bool Pause
        {
            get { return _pause; }
            set { _pause = value; }
        }

        public bool Stop
        {
            get { return _stop; }
            set { _stop = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        #endregion

        public override void Update(GameTime gameTime)
        {
            float DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_start && !_pause)
            {
                if (_time > 0)
                {
                    _time -= DeltaTime;
                }
                else
                {
                    _stop = true;
                }
            }

            TimeSpan timeSpan = TimeSpan.FromSeconds((int)_time);
            _text = (timeSpan.Minutes + ":" + timeSpan.Seconds).ToString();

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, _text, _position, Color.Red);
        }
    }
}