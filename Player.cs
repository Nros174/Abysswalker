using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;  // เพิ่มบรรทัดนี้

namespace Abysswalker
{
    public class Player : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Dictionary<string, List<Texture2D>> animations;
        private Texture2D image;
        private int frameIndex;
        private float animationSpeed;
        private string status;
        private bool racingRight;
        private Vector2 direction;
        private float speedX;
        private float speedY;
        private bool sprintActive;
        private bool rushAnimationPlayed;
        private bool tookDamage;
        private bool invincible;
        private int invincibilityDuration;
        private int hurtTime;
        private Action<int> changeHealth;
        private bool onGround;

        private KeyboardState prevState;
        private int sprintCooldownTimer;
        private int sprintTimer;
        private Vector2 position;
        public Vector2 Position { get; set; }  // เพิ่ม Position ให้เข้าถึงได้จากภายนอก


        public Player(Game game, Vector2 pos, SpriteBatch spriteBatch, Action<int> changeHealth)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.changeHealth = changeHealth;

            // Set default values
            this.frameIndex = 0;
            this.animationSpeed = 0.15f;
            this.status = "idle";
            this.racingRight = true;
            this.direction = Vector2.Zero;
            this.speedX = 5;
            this.speedY = 5;
            this.sprintActive = false;
            this.rushAnimationPlayed = false;
            this.tookDamage = false;
            this.invincible = false;
            this.invincibilityDuration = 800;
            this.sprintCooldownTimer = 0;
            this.sprintTimer = 0;

            this.importCharacterAssets();
            this.image = this.animations["idle"][frameIndex];
            this.position = pos;
            this.Position = pos;

        }

        public void importCharacterAssets()
        {
            // Placeholder method for loading animations
            // You'll load actual images here using ContentManager in MonoGame
            this.animations = new Dictionary<string, List<Texture2D>>()
            {
                { "idle", new List<Texture2D>() }, 
                { "default_swimming", new List<Texture2D>() },
                { "fast_swimming", new List<Texture2D>() },
                { "hurt", new List<Texture2D>() },
                { "rush_for_bonus_start", new List<Texture2D>() }
            };
            // Load animations here using Game.Content.Load<Texture2D>("path_to_image");
        }

        public void animate()
        {
            List<Texture2D> animation = this.animations[this.status];
            this.frameIndex += (int)animationSpeed;
            if (this.frameIndex >= animation.Count)
            {
                this.frameIndex = 0;
            }
            this.image = animation[(int)this.frameIndex];

            if (!this.racingRight)
            {
                this.image = FlipImage(this.image); // Flip image horizontally if moving left
            }
        }

        private Texture2D FlipImage(Texture2D originalImage)
        {
            // This will flip the image horizontally
            SpriteEffects spriteEffects = this.racingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            return originalImage; // Adjust as needed in MonoGame
        }

        public void getInput()
        {
            if (!this.invincible)
            {
                var keyboardState = Keyboard.GetState();
                this.direction.X = 0;
                this.direction.Y = 0;

                // Right/Left movement
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    this.direction.X = 1;
                    this.racingRight = true;
                }
                else if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    this.direction.X = -1;
                    this.racingRight = false;
                }

                // Up/Down movement
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
                {
                    this.direction.Y = -1;
                }
                else if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
                {
                    this.direction.Y = 1;
                }

                // Sprinting
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    this.sprintActive = true;
                    this.sprintTimer = 200;  // Example duration
                    this.sprintCooldownTimer = 1000; // Example cooldown
                }
            }
        }

        public void getStatus()
        {
            if (this.tookDamage)
            {
                this.status = "hurt";
            }
            else if (this.direction.Y < 0 && this.direction.X == 0)
            {
                this.status = "idle";
            }
            else if (this.direction.Y > 1 || this.direction.X == 0)
            {
                this.status = "idle";
            }
            else
            {
                if (this.sprintActive)
                {
                    this.status = "fast_swimming";
                }
                else
                {
                    this.status = "default_swimming";
                }
            }
        }

        public void getDamage(int damageVal)
        {
            if (!this.invincible)
            {
                // Handle taking damage here
                this.changeHealth(damageVal);
                this.invincible = true;
                this.hurtTime = Environment.TickCount;
            }
        }

        public void update()
        {
            getInput();
            getStatus();
            animate();
            // Implement additional update logic (e.g., movement, collision detection)
        }

         public override void Draw(GameTime gameTime)  // ใช้ override แทน new
        {
            // วาด Player
            spriteBatch.Begin();
            spriteBatch.Draw(this.image, this.Position, Color.White);
            spriteBatch.End();
        }
        public void LoadContent(ContentManager content)
        {
            // โหลดเนื้อหาของ Player ที่นี่
            this.image = content.Load<Texture2D>("player_image");
        }
    }

    
}
