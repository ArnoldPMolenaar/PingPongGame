using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong
{
    public class Player : PlayerObject
    {
        public Player(Game game, Texture2D Texture, Vector2 Position, float Speed, SpriteFont Font) : base(game)
        {
            texture = Texture;
            position = Position;
            speed = Speed;
            font = Font;
            this.game = game;
            LongerPaddleTimer = new GameTimer(game, 10f);
            SmallerPaddleTimer = new GameTimer(game, 10f);
        }

        public override void Update(GameTime gameTime)
        {
            LongerPaddleTimer.Update(gameTime);
            SmallerPaddleTimer.Update(gameTime);

            if (isLongerPaddle == true)
            {
                isSmallerPaddle = false;
                LongerPaddleTimer.Start = true;
                rectTexture = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height + 50);
                if (LongerPaddleTimer.Time <= 0)
                {
                    LongerPaddleTimer = new GameTimer(game, 10f);
                    isLongerPaddle = false;
                }
            }

            if (isSmallerPaddle == true)
            {
                isLongerPaddle = false;
                SmallerPaddleTimer.Start = true;
                rectTexture = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height - 50);
                if (SmallerPaddleTimer.Time <= 0)
                {
                    SmallerPaddleTimer = new GameTimer(game, 10f);
                    isSmallerPaddle = false;
                }
            }

            if (isSmallerPaddle == false && isLongerPaddle == false)
                rectTexture = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                position.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (position.Y <= 0)
                {
                    position = new Vector2(20, 0);
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                position.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (isSmallerPaddle == false && isLongerPaddle == false && position.Y >= 300)
                    position = new Vector2(20, 300);
                else if (isLongerPaddle == true && position.Y >= 250)
                    position = new Vector2(20, 250);
                else if (isSmallerPaddle == true && position.Y >= 350)
                    position = new Vector2(20, 350);
            }

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectTexture, Color.White);
            spriteBatch.DrawString(font, score.ToString(), new Vector2(game.Window.ClientBounds.Width / 2 / 2, 10), Color.White);
        }
    }
}
