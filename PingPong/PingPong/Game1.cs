#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace PingPong
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        Player Player;
        AI AI;
        GameTimer SpawnTimer;
        PowerUpWall PowerUpWallPlayer;
        PowerUpWall PowerUpWallAI;
        GameTimer PowerUpWallPlayerTimer;
        GameTimer PowerUpWallAITimer;
        GameTimer SlowMotionTimer;

        public static SpriteBatch spriteBatch;
        private static Texture2D _powerUpTexture;
        private List<PowerUp> _powerUps = new List<PowerUp>();
        private List<Ball> _balls = new List<Ball>();
        private Texture2D _whiteLine;
        private SpriteFont _defaultFont;
        private Random _random = new Random();

        private SoundEffect _powerUpSound;
        private SoundEffect _ballSound;
        private SoundEffect _loseBall;
        private SoundEffect _newBall;
        private SoundEffect _startGame;

        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 400;
            graphics.PreferredBackBufferWidth = 800;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //defaults
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _defaultFont = Content.Load<SpriteFont>("DefaultFont");

            //textures
            _powerUpTexture = Content.Load<Texture2D>("PowerUps");
            _whiteLine = Content.Load<Texture2D>("WhiteSquare");

            //objects
            _balls.Add(new Ball(this, Content.Load<Texture2D>("Ball"), new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, new Vector2(5, 5)));
            Player = new Player(this, Content.Load<Texture2D>("Player"), new Vector2(20, graphics.PreferredBackBufferHeight / 2), 200f, _defaultFont);
            AI = new AI(this, Content.Load<Texture2D>("Player"), new Vector2(760, graphics.PreferredBackBufferHeight / 2), 200f, _defaultFont);
            SlowMotionTimer = new GameTimer(this, 5f);
            PowerUpWallPlayerTimer = new GameTimer(this, 5f);
            PowerUpWallAITimer = new GameTimer(this, 5f);
            SpawnTimer = new GameTimer(this, 5f);
            SpawnTimer.Start = true;

            //soundeffects
            _powerUpSound = Content.Load<SoundEffect>("Audio/PowerUp.wav");
            _ballSound = Content.Load<SoundEffect>("Audio/Ball.wav");
            _loseBall = Content.Load<SoundEffect>("Audio/LoseBall.wav");
            _newBall = Content.Load<SoundEffect>("Audio/NewBall.wav");
            _startGame = Content.Load<SoundEffect>("Audio/StartGame.wav");
            _startGame.Play();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            PowerUpWallPlayerTimer.Update(gameTime);
            PowerUpWallAITimer.Update(gameTime);
            SlowMotionTimer.Update(gameTime);
            Player.Update(gameTime);
            AI.Update(gameTime);
            SpawnTimer.Update(gameTime);
            if (SpawnTimer.Time <= 0)
            {
                int RandSpeedNumber = _random.Next(-5, 5);
                if (RandSpeedNumber == 0)
                    RandSpeedNumber = _random.Next(-5, 5);
                _powerUps.Add(new PowerUp(this, _powerUpTexture, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, new Vector2(RandSpeedNumber, RandSpeedNumber), _random.Next(1, 5)));
                SpawnTimer.Time = 5f;
            }

            for (int p = 0; p < _powerUps.Count; p++)
            {
                if (p < _powerUps.Count)
                {
                    _powerUps[p].Update(gameTime);

                    switch (_powerUps[p].Type)
                    {
                        case 1:
                            if (_powerUps[p].Bounds.Intersects(Player.Bounds) || _powerUps[p].Bounds.Intersects(AI.Bounds))
                            {
                                _powerUpSound.Play();
                                _newBall.Play();
                                _balls.Add(new Ball(this, Content.Load<Texture2D>("Ball"), new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, new Vector2(5, 5)));
                                _powerUps.RemoveAt(p);
                            }
                            break;
                        case 2:
                                if (_powerUps[p].Bounds.Intersects(Player.Bounds))
                                    Player.IsLongerPaddle = true;
                                else if (_powerUps[p].Bounds.Intersects(AI.Bounds))
                                    AI.IsLongerPaddle = true;
                                if (_powerUps[p].Bounds.Intersects(Player.Bounds) || _powerUps[p].Bounds.Intersects(AI.Bounds))
                                {
                                    _powerUpSound.Play();
                                    _powerUps.RemoveAt(p);
                                }
                                break;
                        case 3:
                                if (_powerUps[p].Bounds.Intersects(Player.Bounds))
                                    Player.HasWall = true;
                                else if (_powerUps[p].Bounds.Intersects(AI.Bounds))
                                    AI.HasWall = true;
                                if (_powerUps[p].Bounds.Intersects(Player.Bounds) || _powerUps[p].Bounds.Intersects(AI.Bounds))
                                {
                                    _powerUpSound.Play();
                                    _powerUps.RemoveAt(p);
                                }
                            break;
                        case 4:
                            if (_powerUps[p].Bounds.Intersects(Player.Bounds) || _powerUps[p].Bounds.Intersects(AI.Bounds))
                            {
                                _balls.ForEach(delegate(Ball i) { i.IsSlowMotion = true; });
                                SlowMotionTimer.Start = true;
                                _powerUpSound.Play();
                                _powerUps.RemoveAt(p);
                            }
                            break;
                        case 5:
                            if (_powerUps[p].Bounds.Intersects(Player.Bounds))
                                Player.IsSmallerPaddle = true;
                            else if (_powerUps[p].Bounds.Intersects(AI.Bounds))
                                AI.IsSmallerPaddle = true;
                            if (_powerUps[p].Bounds.Intersects(Player.Bounds) || _powerUps[p].Bounds.Intersects(AI.Bounds))
                            {
                                _powerUpSound.Play();
                                _powerUps.RemoveAt(p);
                            }
                            break;
                        default:
                            break;
                    }

                    if (SlowMotionTimer.Time <= 0)
                    {
                        _balls.ForEach(delegate(Ball i) { i.IsSlowMotion = false; });
                        SlowMotionTimer = new GameTimer(this, 5f);
                    }

                    if (Player.HasWall == true)
                    {
                        PowerUpWallPlayer = new PowerUpWall(this, Content.Load<Texture2D>("Wall"), new Vector2(0, 0));
                        PowerUpWallPlayerTimer.Start = true;
                        if (PowerUpWallPlayerTimer.Time <= 0)
                        {
                            PowerUpWallPlayerTimer = new GameTimer(this, 5f);
                            PowerUpWallPlayer = null;
                            Player.HasWall = false;
                        }
                    }
                    else if (AI.HasWall == true)
                    {
                        PowerUpWallAI = new PowerUpWall(this, Content.Load<Texture2D>("Wall"), new Vector2(this.Window.ClientBounds.Width - 20, 0));
                        PowerUpWallAITimer.Start = true;
                        if (PowerUpWallAITimer.Time <= 0)
                        {
                            PowerUpWallAITimer = new GameTimer(this, 5f);
                            PowerUpWallAI = null;
                            AI.HasWall = false;
                        }
                    }
                }
            }

            if (_balls.Count == 0 && AI.Score != 5 && Player.Score != 5)
            {
                _balls.Add(new Ball(this, Content.Load<Texture2D>("Ball"), new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth, new Vector2(5, 5)));
                _newBall.Play();
            }

            for (int i = 0; i < _balls.Count; i++)
            {
                if (i < _balls.Count)
                {
                    _balls[i].Update(gameTime);

                    if (Player.Bounds.Intersects(_balls[i].Bounds) || (Player.HasWall == true && PowerUpWallPlayerTimer.Start == true && PowerUpWallPlayer.Bounds.Intersects(_balls[i].Bounds)))
                    {
                        if (_balls[i].IsSlowMotion == true && _balls[i].IsSlowX == true)
                            _balls[i].SlowMotion = new Vector2(1, _balls[i].SlowMotion.Y);
                        else if (_balls[i].IsSlowMotion == true && _balls[i].IsSlowX == false)
                            _balls[i].SlowMotion = new Vector2(-1, _balls[i].SlowMotion.Y);
                        else
                            _balls[i].Speed = new Vector2(5, _balls[i].Speed.Y);
                        _ballSound.Play();
                    }
                    if (AI.Bounds.Intersects(_balls[i].Bounds) || (AI.HasWall == true && PowerUpWallAITimer.Start == true && PowerUpWallAI.Bounds.Intersects(_balls[i].Bounds)))
                    {
                        if (_balls[i].IsSlowMotion == true && _balls[i].IsSlowX == true)
                            _balls[i].SlowMotion = new Vector2(-1, _balls[i].SlowMotion.Y);
                        else if (_balls[i].IsSlowMotion == true && _balls[i].IsSlowX == false)
                            _balls[i].SlowMotion = new Vector2(1, _balls[i].SlowMotion.Y);
                        else
                            _balls[i].Speed = new Vector2(-5, _balls[i].Speed.Y);
                        _ballSound.Play();
                    }

                    if (_balls[i].IsVisable == false)
                    {
                        if (_balls[i].Bounds.X + _balls[i].Bounds.Width >= graphics.PreferredBackBufferWidth && AI.Score != 5 && Player.Score != 5)
                            Player.Score++;
                        else if (_balls[i].Bounds.X <= 0 && Player.Score != 5 && AI.Score != 5)
                            AI.Score++;

                        _loseBall.Play();
                        _balls.RemoveAt(i);
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(_whiteLine, new Rectangle(graphics.PreferredBackBufferWidth / 2, 0, 2, graphics.PreferredBackBufferHeight), Color.White);
            foreach(Ball Ball in _balls)
                Ball.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            AI.Draw(spriteBatch);
            if (Player.HasWall == true && PowerUpWallPlayerTimer.Start == true)
                PowerUpWallPlayer.Draw(spriteBatch);
            if (AI.HasWall == true && PowerUpWallAITimer.Start == true)
                PowerUpWallAI.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            foreach (PowerUp PowerUp in _powerUps)
                PowerUp.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
