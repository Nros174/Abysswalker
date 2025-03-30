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
            this.image = new Texture2D(game.GraphicsDevice, size, size);
            this.rect = new Rectangle(x, y, size, size);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Begin();
            spriteBatch.Draw(image, rect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public class StaticTile : Tile
    {
        public StaticTile(Game game, int size, int x, int y, Texture2D surface) : base(game, size, x, y)
        {
            this.image = surface;
        }
    }

    public class Submarine : StaticTile
    {
        public Submarine(Game game, int size, int x, int y, Texture2D surface) : base(game, size, x, y, surface)
        {
            this.rect = new Rectangle(x - size / 2, y - size / 2, size, size);
        }
    }

    public class AnimatedTile : Tile
    {
        protected List<Texture2D> frames;
        protected float frameIndex; // แก้จาก int เป็น float เพื่อให้ animation ลื่น

        public AnimatedTile(Game game, int size, int x, int y, string path) : base(game, size, x, y)
        {
            frames = LoadFrames(path);
            frameIndex = 0f;
            this.image = frames.Count > 0 ? frames[0] : null;
        }

        private List<Texture2D> LoadFrames(string path)
        {
            var frames = new List<Texture2D>();
            // TODO: เติมโค้ดโหลดภาพจริง ๆ เช่น:
            // frames.Add(Game.Content.Load<Texture2D>(path + "/frame1"));
            return frames;
        }

        public virtual void Animate()
        {
            frameIndex += 0.15f;
            if (frames.Count > 0)
            {
                if (frameIndex >= frames.Count)
                    frameIndex = 0f;
                this.image = frames[(int)frameIndex];
            }
        }

        public override void Update(GameTime gameTime)
        {
            Animate();
            base.Update(gameTime);
        }
    }

    public class Coin : AnimatedTile
    {
        public int Value { get; set; }

        public Coin(Game game, int size, int x, int y, string path, int value) : base(game, size, x, y, path)
        {
            int centerX = x + size / 2;
            int centerY = y + size / 2;
            this.rect = new Rectangle(centerX - size / 2, centerY - size / 2, size, size);
            this.Value = value;
        }
    }

    public class Decoration : DrawableGameComponent
    {
        private Texture2D image;
        protected Rectangle rect;

        public Decoration(Game game, int x, int y, Texture2D surface) : base(game)
        {
            this.image = surface;
            this.rect = new Rectangle(x, y, image.Width, image.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Begin();
            spriteBatch.Draw(image, rect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public class AnimatedPlant : AnimatedTile
    {
        private int yOffsetFactor;

        public AnimatedPlant(Game game, int size, int x, int y, string path, int yOffsetFactor) : base(game, size, x, y, path)
        {
            this.yOffsetFactor = yOffsetFactor;
            int centerX = x + size / 2;
            int centerY = y + size / yOffsetFactor;
            this.rect = new Rectangle(centerX - size / 2, centerY - size / 2, size, size);
        }
    }

    public class Plant : Decoration
    {
        private int yOffsetFactor;

        public Plant(Game game, int size, int x, int y, Texture2D surface, int yOffsetFactor) : base(game, x, y, surface)
        {
            this.yOffsetFactor = yOffsetFactor;
            int centerX = x + size / 2;
            int centerY = y + size / yOffsetFactor;
            this.rect = new Rectangle(centerX - surface.Width / 2, centerY - surface.Height / 2, surface.Width, surface.Height);
        }
    }
} 
