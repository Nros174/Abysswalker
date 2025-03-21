using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Abysswalker
{
    public class Enemy : AnimatedTile
    {
        private bool isExplodes;
        private bool playerCollide;
        public float speed;  // เปลี่ยนให้เป็น public เพื่อให้เข้าถึงได้
        private bool isReversed;

        public Enemy(Game game, int size, int x, int y, string path)
            : base(game, size, x, y, path)
        {
            this.rect.Y += size - this.image.Height;
            this.speed = 1f;
            this.isExplodes = false;
            this.playerCollide = false;
            this.isReversed = false;
        }

        public void EnemyExplosion()
        {
            this.frames = Support.ImportFolder(Game.Content, "enemy_explosion_images");
            this.frameIndex = (int)(this.frameIndex + 0.15f); // เพิ่มค่าและแปลงให้เป็น int

            if (this.frameIndex < this.frames.Count)
            {
                this.image = this.frames[(int)this.frameIndex];
            }
            else
            {
                this.Kill();
            }
        }

        public void Move()
        {
            this.rect.X -= (int)this.speed;  // แปลง speed เป็น int
        }

        // คลาส Enemy
        public void ReverseImage()
        {
            if (this.speed > 0 && isReversed)
            {
                // เรียกใช้ spriteBatch ที่ได้ถูกสร้างจาก Game
                SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

                spriteBatch.Begin();
                spriteBatch.Draw(this.image, Vector2.Zero, null, Color.White, (float)Math.PI, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0);
                spriteBatch.End();
            }
        }


        public void Reverse()
        {
            this.speed *= -1;
        }

        public override void Update(GameTime gameTime)
        {
            if (!isExplodes)
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

        private void Kill()
        {
            this.isExplodes = true;
        }
    }

    public class DecorationFish : Enemy
    {
        public DecorationFish(Game game, int size, int x, int y, string path, int minSpeed, int maxSpeed)
            : base(game, size, x, y, path)
        {
            this.speed = new Random().Next(minSpeed, maxSpeed);
        }

        public new void Move()
        {
            this.rect.X -= (int)this.speed; // ใช้ speed แบบสุ่ม
        }
    }
}
