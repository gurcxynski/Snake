using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.EasyInput;
using Snake.Core;
using Snake.GameObjects;
using System.Collections.Generic;
using System.Text;

namespace Snake

{
    /// This is the main type for your game.
    public static class Globals
    {
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static EasyKeyboard keyboard = new EasyKeyboard();
        public static int BaseVel = 175;
        public static int Score = 0;
        public static StringBuilder sb = new StringBuilder();
        public static SpriteFont font;
        public static bool GameRunning = true;
        public static int Size = 400;
        public static SoundEffect bloop;
    }

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

            graphics.PreferredBackBufferWidth = Globals.Size;
            graphics.PreferredBackBufferHeight = Globals.Size;

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
            
            grass =                             Content.Load<Texture2D>("grass");
            Globals.textures["head_down"] =     Content.Load<Texture2D>("head_down");
            Globals.textures["head_up"] =       Content.Load<Texture2D>("head_up");
            Globals.textures["head_left"] =     Content.Load<Texture2D>("head_left");
            Globals.textures["head_right"] =    Content.Load<Texture2D>("head_right");
            Globals.textures["apple_texture"] = Content.Load<Texture2D>("apple");
            Globals.textures["body_hor"] =      Content.Load<Texture2D>("body_horizontal");
            Globals.textures["body_ver"] =      Content.Load<Texture2D>("body_vertical");
            Globals.font =                      Content.Load<SpriteFont>("Score");
            Globals.bloop =                     Content.Load<SoundEffect>("bloop");   


            Head temp = (Head)GameScene.AddGameObject(new Head(Globals.textures["head_right"], new Vector2(100, Globals.Size / 2 - 20)));
            GameScene.AddGameObject(new Apple(Globals.textures["apple_texture"], new Vector2(180, Globals.Size / 2 - 20), temp));
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
            if(Globals.GameRunning) Globals.GameRunning = GameScene.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            
            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            GraphicsDevice.Clear(Color.White);

            spriteBatch.Draw(grass, new Vector2(0, 0), Color.White);

            GameScene.Draw(spriteBatch);

            //if(!GameRunning) spriteBatch.DrawString(font, "GAME OVER", new Vector2(150, 180), Color.Black);
        
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
