using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace Abysswalker
{
    public class Enemy : AnimatedTile
    {
        private bool isExplodes;
        private bool playerCollide; // ใช้ตรวจสอบการชนกับผู้เล่น
        protected float speed;
        private bool isReversed;
        protected SpriteEffects spriteEffect = SpriteEffects.None; // ใช้สำหรับ Flip รูปภาพ

        public Enemy(Game game, int size, int x, int y, string path)
            : base(game, size, x, y, path)
        {
            this.rect.Y += size - this.image.Height; // ปรับตำแหน่ง Y ให้รูปอยู่บนพื้น
            this.speed = 1f;
            this.isExplodes = false;
            this.playerCollide = false;
            this.isReversed = false;
        }

        // เรียกเมื่อศัตรูถูกทำลาย เพื่อแสดงอนิเมชันระเบิด
        public void EnemyExplosion()
        {
            this.frames = Support.ImportFolder(Game.Content, "enemy_explosion_images"); // โหลดภาพระเบิด
            frameIndex += 0.15f; // เพิ่ม index แบบลื่นไหล

            if (this.frameIndex < this.frames.Count)
            {
                this.image = this.frames[(int)this.frameIndex]; // แสดงภาพที่ตรงกับ index
            }
            else
            {
                this.Kill(); // จบการระเบิด
            }
        }

        // เคลื่อนที่ศัตรูไปทางซ้าย
        public virtual void Move()
        {
            this.rect.X -= (int)this.speed;
        }

        // ตั้งค่าการ Flip รูปภาพตามทิศการเคลื่อนที่
        public void ReverseImage()
        {
            if (this.speed > 0 && isReversed)
            {
                this.spriteEffect = SpriteEffects.FlipHorizontally;
            }
            else
            {
                this.spriteEffect = SpriteEffects.None;
            }
        }

        // เปลี่ยนทิศการเคลื่อนที่
        public void Reverse()
        {
            this.speed *= -1;
            this.isReversed = !this.isReversed;
        }

        public override void Update(GameTime gameTime)
        {
            if (!isExplodes)
            {
                Animate(); // เรียกอนิเมชันปกติ
                Move(); // เคลื่อนที่
                ReverseImage(); // อัปเดตสถานะการกลับภาพ
            }
            else
            {
                EnemyExplosion(); // แสดงภาพระเบิดเมื่อศัตรูตาย
            }
        }

        // ปิดการทำงานของศัตรู
        private void Kill()
        {
            this.Visible = false; // หรือสามารถลบออกจากเกมได้ภายหลัง
            this.Enabled = false;
        }

        // ฟังก์ชันสำหรับวาดศัตรู
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            if (this.Visible)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(this.image, rect, null, Color.White, 0f, Vector2.Zero, this.spriteEffect, 0f);
                spriteBatch.End();
            }
        }

    }

    // ปลาตกแต่งที่ว่ายไปมาแบบสุ่ม ไม่ชนผู้เล่น
    public class DecorationFish : Enemy
    {
        public DecorationFish(Game game, int size, int x, int y, string path, int minSpeed, int maxSpeed)
            : base(game, size, x, y, path)
        {
            Random rand = new Random();
            this.speed = rand.Next(minSpeed, maxSpeed); // สุ่มความเร็ว
        }

        // เขียน Move ใหม่ให้เร็วแบบสุ่ม
        public override void Move()
        {
            this.rect.X -= (int)this.speed;
        }
    }
}
