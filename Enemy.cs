using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Abysswalker
{
    public class Enemy : AnimatedTile
    {
        private bool isExplodes;
        private bool playerCollide; // Player collision flag
        protected float speed;
        private bool isReversed;

        public Enemy(Game game, int size, int x, int y, string path) 
            : base(game, size, x, y, path)
        {
            this.rect.Y += size - this.image.Height; // Adjust Y position based on the image height
            this.speed = 1f;
            this.isExplodes = false;
            this.playerCollide = false;
            this.isReversed = false;
        }

        // Explosion animation after destruction
        public void EnemyExplosion()
        {
            this.frames = Support.ImportFolder(Game.Content, "enemy_explosion_images"); // Load explosion frames
            this.frameIndex += 0.15f;

            if (this.frameIndex < this.frames.Count)
            {
                this.image = this.frames[(int)this.frameIndex];
            }
            else
            {
                this.Kill();
            }
        }

        // Move the enemy
        public void Move()
        {
            this.rect.X -= (int)this.speed;
        }

        // Flip the image based on movement direction
        public void ReverseImage()
        {
            if (this.speed > 0 && isReversed)
            {
                this.image = Microsoft.Xna.Framework.Graphics.SpriteBatch.Draw(this.image, Vector2.Zero, null, Microsoft.Xna.Framework.Color.White, (float)Math.PI, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0);
            }
        }

        // Reverse enemy direction
        public void Reverse()
        {
            this.speed *= -1;
        }

        public override void Update(GameTime gameTime)
        {
            if (!isExplodes) // If explosion is not happening
            {
                Animate();
                Move();
                ReverseImage();
            }
            else
            {
                EnemyExplosion();
            }
        }

        // Kill the enemy (deactivate it)
        private void Kill()
        {
            this.isExplodes = true; // Mark as exploded
        }
    }

    // DecorationFish doesn't interact with the player, it just moves randomly
    public class DecorationFish : Enemy
    {
        public DecorationFish(Game game, int size, int x, int y, string path, int minSpeed, int maxSpeed)
            : base(game, size, x, y, path)
        {
            this.speed = new Random().Next(minSpeed, maxSpeed);
        }

        // Override Move method to use random speed
        public new void Move()
        {
            this.rect.X -= (int)this.speed;
        }
    }
}
