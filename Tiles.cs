using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Abysswalker
{
    // คลาสพื้นฐานสำหรับ Tile
    public class Tile : DrawableGameComponent
    {
        protected Texture2D image;
        protected Rectangle rect;

        public Tile(Game game, int size, int x, int y) : base(game)
        {
            this.image = new Texture2D(game.GraphicsDevice, size, size); // สร้างพื้นผิวสำหรับ tile
            this.rect = new Rectangle(x, y, size, size); // กำหนดตำแหน่งและขนาดของ rect
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Begin();
            spriteBatch.Draw(image, rect, Color.White); // วาดพื้นผิว
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    // คลาสสำหรับ StaticTile (Tile ที่ไม่เคลื่อนไหว)
    public class StaticTile : Tile
    {
        public StaticTile(Game game, int size, int x, int y, Texture2D surface) : base(game, size, x, y)
        {
            this.image = surface; // กำหนดภาพจาก surface ที่ให้มา
        }
    }

    // คลาส Submarine ที่ปรับเปลี่ยนตำแหน่ง rect
    public class Submarine : StaticTile
    {
        public Submarine(Game game, int size, int x, int y, Texture2D surface) : base(game, size, x, y, surface)
        {
            this.rect = new Rectangle(x - size / 2, y - size / 2, size, size); // ปรับตำแหน่งให้ตรงกลาง
        }
    }

    // คลาสสำหรับ AnimatedTile (Tile ที่เคลื่อนไหว)
    public class AnimatedTile : Tile
    {
        protected List<Texture2D> frames; // รายการของภาพ
        protected int frameIndex;

        public AnimatedTile(Game game, int size, int x, int y, string path) : base(game, size, x, y)
        {
            frames = LoadFrames(path); // โหลดภาพจาก folder
            frameIndex = 0;
            this.image = frames[frameIndex];
        }

        // ฟังก์ชันสำหรับโหลดภาพจาก folder (สามารถใช้ ContentManager ใน MonoGame)
        private List<Texture2D> LoadFrames(string path)
        {
            var frames = new List<Texture2D>();
            // สมมติว่าคุณมีโค้ดเพื่อโหลดภาพจากโฟลเดอร์
            // frames.Add(game.Content.Load<Texture2D>(path + "/frame1"));
            // frames.Add(game.Content.Load<Texture2D>(path + "/frame2"));
            // ...
            return frames;
        }

        // ฟังก์ชันสำหรับทำให้ภาพเคลื่อนไหว
        public void Animate()
        {
            frameIndex += 1;
            if (frameIndex >= frames.Count)
                frameIndex = 0;
            this.image = frames[frameIndex];
        }

        public override void Update(GameTime gameTime)
        {
            Animate(); // เรียกฟังก์ชัน Animate
            base.Update(gameTime);
        }
    }

    // คลาสสำหรับ Coin (เหรียญ) ที่มีการปรับ rect และจัดการค่า
    public class Coin : AnimatedTile
    {
        public int Value { get; set; }

        public Coin(Game game, int size, int x, int y, string path, int value) : base(game, size, x, y, path)
        {
            int centerX = x + size / 2;
            int centerY = y + size / 2;
            this.rect = new Rectangle(centerX - size / 2, centerY - size / 2, size, size); // จัดตำแหน่งใหม่ให้ตรงกลาง
            this.Value = value;
        }
    }

    // คลาสสำหรับ Decoration (ตกแต่ง) ซึ่งใช้ภาพจากไฟล์และตำแหน่ง
    public class Decoration : DrawableGameComponent
    {
        private Texture2D image;
        protected Rectangle rect;

        public Decoration(Game game, int x, int y, Texture2D surface) : base(game)
        {
            this.image = surface;
            this.rect = new Rectangle(x, y, image.Width, image.Height); // ตั้งตำแหน่งและขนาดตามภาพ
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Begin();
            spriteBatch.Draw(image, rect, Color.White); // วาดภาพ
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    // คลาส AnimatedPlant ที่มีการเคลื่อนไหว
    public class AnimatedPlant : AnimatedTile
    {
        private int yOffsetFactor;

        public AnimatedPlant(Game game, int size, int x, int y, string path, int yOffsetFactor) : base(game, size, x, y, path)
        {
            this.yOffsetFactor = yOffsetFactor;
            int centerX = x + size / 2;
            int centerY = y + size / yOffsetFactor; // ปรับตำแหน่ง Y
            this.rect = new Rectangle(centerX - size / 2, centerY - size / 2, size, size); // จัดตำแหน่งให้ตรงกลาง
        }
    }

    // คลาสสำหรับ Plant (พืช) ที่ไม่เคลื่อนไหว
    public class Plant : Decoration
    {
        private int yOffsetFactor;

        public Plant(Game game, int size, int x, int y, Texture2D surface, int yOffsetFactor) : base(game, x, y, surface)
        {
            this.yOffsetFactor = yOffsetFactor;
            int centerX = x + size / 2;
            int centerY = y + size / yOffsetFactor; // ปรับตำแหน่ง Y
            this.rect = new Rectangle(centerX - surface.Width / 2, centerY - surface.Height / 2, surface.Width, surface.Height); // จัดตำแหน่งให้ตรงกลาง
        }
    }
}
