using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong
{
    public class PowerUpWall
    {
        private Texture2D _texture;
        private Rectangle _rectTexture;
        private Vector2 _position;

        public Rectangle Bounds
        {
            get { return new Rectangle((int)_position.X, (int)_position.Y, _rectTexture.Width, _rectTexture.Height); }
        }

        public PowerUpWall(Game game, Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = position;
            _rectTexture = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, game.Window.ClientBounds.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectTexture, Color.White);
        }
    }
}
