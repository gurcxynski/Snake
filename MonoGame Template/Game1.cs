using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Snake.Core;
namespace Snake

{
    /// This is the main type for your game.
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene GameScene = new Scene();
        Texture2D grass;
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

            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 900;

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(255,183,197));

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(grass, new Vector2(0, 0), Color.White);
            GameScene.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
