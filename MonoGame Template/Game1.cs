using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Core;
using Snake.GameObjects;
using System.Collections.Generic;

namespace Snake

{
    /// This is the main type for your game.
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene GameScene = new Scene();
        Texture2D grass;
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        SpriteFont font;
        bool GameOver = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = 400;
            graphics.PreferredBackBufferHeight = 400;

            graphics.ApplyChanges();

            base.Initialize();
        }

        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
            grass = Content.Load<Texture2D>("grass");
            textures["head_down"] = Content.Load<Texture2D>("head_down");
            textures["head_up"] = Content.Load<Texture2D>("head_up");
            textures["head_left"] = Content.Load<Texture2D>("head_left");
            textures["head_right"] = Content.Load<Texture2D>("head_right");
            textures["apple_texture"] = Content.Load<Texture2D>("apple");
            font = Content.Load<SpriteFont>("Score");

            GameScene.AddGameObject(new Head(textures["head_right"], new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight / 2)));
            GameScene.AddGameObject(new Apple(textures["apple_texture"], new Vector2(graphics.PreferredBackBufferWidth / 2 + 80, graphics.PreferredBackBufferHeight / 2)));
        }

        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        protected override void Update(GameTime gameTime)
        {
            GameOver = !GameScene.Update(graphics, textures, (float)gameTime.ElapsedGameTime.TotalSeconds);
            
            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            if (!GameOver)
            {
                GraphicsDevice.Clear(new Color(255, 183, 197));

                spriteBatch.Draw(grass, new Vector2(0, 0), Color.White);

                GameScene.Draw(spriteBatch, font);
                
            }
            else
            {
                spriteBatch.DrawString(font, "GAME OVER", new Vector2(150, 180), Color.Black);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
