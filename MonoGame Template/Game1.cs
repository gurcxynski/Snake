using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.EasyInput;
using Snake.Buttons;
using Snake.Core;
using System.Collections.Generic;
using System.Text;

namespace Snake

{
    /// This is the main type for your game.
    public static class Globals
    {
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        public static EasyKeyboard keyboard = new EasyKeyboard();
        public static EasyMouse mouse = new EasyMouse();
        public static StringBuilder sb = new StringBuilder();
        public static SpriteFont font;
        public static SoundEffect bloop;
        public static bool menu = true;
        public static Game1 game;
    }
    public static class Settings
    {
        public static int BaseVel = 200;
        public static int Size = 400;
        public static bool godmode = false;
        public static bool sound = true;
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        readonly Scene GameScene = new Scene();
        readonly Menu StartMenu = new Menu();
        Texture2D grass;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Globals.game = this;
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = Settings.Size;
            graphics.PreferredBackBufferHeight = Settings.Size;

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
            Globals.textures["button1"] =       Content.Load<Texture2D>("buttons");
            Globals.textures["button2"] =       Content.Load<Texture2D>("buttons2");
            Globals.textures["button3"] =       Content.Load<Texture2D>("buttons3");
            Globals.font =                      Content.Load<SpriteFont>("Score");
            Globals.bloop =                     Content.Load<SoundEffect>("bloop");   

            
            StartMenu.AddButton(new PlayButton(Globals.textures["button1"], new Vector2(140, 80)));
            StartMenu.AddButton(new GodModeButton(Globals.textures["button1"], new Vector2(140, 120)));
            StartMenu.AddButton(new SpeedButton(Globals.textures["button1"], new Vector2(140, 160)));
            StartMenu.AddButton(new SoundButton(Globals.textures["button1"], new Vector2(140, 200)));
            StartMenu.AddButton(new ExitButton(Globals.textures["button1"], new Vector2(140, 300)));
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
            if (Globals.menu) StartMenu.Update();
            else
            {
                if(!GameScene.Initalized) GameScene.Initialize();
                GameScene.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            GraphicsDevice.Clear(Color.White);

            spriteBatch.Draw(grass, new Vector2(0, 0), Color.White);

            if(Globals.menu) StartMenu.Draw(spriteBatch);
            else GameScene.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
