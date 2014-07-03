using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong
{
    public class BallObject : DrawableGameComponent
    {
        protected Vector2 position;
        protected Vector2 speed;
        protected int stageHeight, stageWidth;
        protected Texture2D texture;
        protected Game game;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public BallObject(Game game) : base(game)
        {

        }
    }
}
