using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;  // เพิ่มบรรทัดนี้


namespace Abysswalker
{
    public class Level
    {
        private Game game;
        private SpriteBatch spriteBatch;
        private Camera camera;
        private Texture2D background;
        private Texture2D midground;
        public Player player;
        private List<Enemy> enemies;
        private List<Tile> tiles;
        private List<Coin> coins;
        private List<Decoration> decorations;
        private List<Plant> plants;
        private int levelWidth;
        private int levelHeight;
        private bool paused;
        private bool generationDone;
        private GameState status;

        private Action<int, int> createOverworld;
        private Action<int> changeCoins;
        private Action<int> changeHealth;
        private Action<int> checkGameOver;
        private int currentLevel;

        public class Camera
        {
            private Matrix _transform;
            private Viewport _viewport;
            private Vector2 _position;

            public Camera(int width, int height)
            {
                _viewport = new Viewport(0, 0, width, height);
                _position = Vector2.Zero;
            }

            public void Update(Vector2 playerPosition, int levelWidth, int levelHeight)
            {
                _position.X = MathHelper.Clamp(playerPosition.X, _viewport.Width / 2, levelWidth - _viewport.Width / 2);
                _position.Y = MathHelper.Clamp(playerPosition.Y, _viewport.Height / 2, levelHeight - _viewport.Height / 2);
                _transform = Matrix.CreateTranslation(new Vector3(-_position, 0));
            }

            public Matrix Transform => _transform;
        }

        public Level(Game game, SpriteBatch spriteBatch, int currentLevel, Action<int, int> createOverworld, Action<int> changeCoins, Action<int> changeHealth, Action<int> checkGameOver)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.currentLevel = currentLevel;
            this.createOverworld = createOverworld;
            this.changeCoins = changeCoins;
            this.changeHealth = changeHealth;
            this.checkGameOver = checkGameOver;

            camera = new Camera(800, 600);
            enemies = new List<Enemy>();
            tiles = new List<Tile>();
            coins = new List<Coin>();
            decorations = new List<Decoration>();
            plants = new List<Plant>();
            paused = false;
            generationDone = false;
            status = GameState.Load;
        }

        public void LoadLevelContent(ContentManager content)
        {
            background = content.Load<Texture2D>("background");
            midground = content.Load<Texture2D>("midground");
            player = new Player(game, new Vector2(400, 300), spriteBatch, changeHealth);  // สร้าง Player ใหม่
            player.LoadContent(content);  // ใช้ LoadContent() ของ Player แทนการใช้ DrawableGameComponent
        }


        public void GenerateLevel()
        {
            if (!generationDone)
            {
                generationDone = true;
                status = GameState.Game;
            }
        }

       public void Update(GameTime gameTime)
        {
            if (status == GameState.Load)
            {
                GenerateLevel();
            }
            else if (!paused && status == GameState.Game)
            {
                camera.Update(player.Position, levelWidth, levelHeight);
                player.Update(gameTime);  // ส่ง gameTime ให้กับ player

                foreach (var enemy in enemies)
                    enemy.Update(gameTime);

                foreach (var coin in coins)
                    coin.Update(gameTime);

                foreach (var plant in plants)
                    plant.Update(gameTime);

                checkGameOver(currentLevel);
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(midground, Vector2.Zero, Color.White);
            player.Draw(gameTime);
            foreach (var enemy in enemies)
                enemy.Draw(gameTime);
            foreach (var coin in coins)
                coin.Draw(gameTime);
            foreach (var decoration in decorations)
                decoration.Draw(gameTime);
            spriteBatch.End();
        }
    }

    public enum GameState
    {
        Load,
        Game,
        Pause
    }
}
