using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong
{
    public class PlayerObject : DrawableGameComponent
    {
        protected SpriteFont font;
        protected Texture2D texture;
        protected Rectangle rectTexture;
        protected Vector2 position;
        protected int score;
        protected float speed;
        protected Game game;
        protected bool isLongerPaddle = false;
        protected GameTimer LongerPaddleTimer;
        protected bool isSmallerPaddle = false;
        protected GameTimer SmallerPaddleTimer;
        protected bool hasWall = false;

        public Rectangle Bounds
        {
            get { return new Rectangle((int)position.X, (int)position.Y, rectTexture.Width, rectTexture.Height); }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool HasWall
        {
            get { return hasWall; }
            set { hasWall = value; }
        }

        public bool IsLongerPaddle
        {
            get { return isLongerPaddle; }
            set { isLongerPaddle = value; }
        }

        public bool IsSmallerPaddle
        {
            get { return isSmallerPaddle; }
            set { isSmallerPaddle = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public PlayerObject(Game game) : base(game)
        {

        }
    }
}
