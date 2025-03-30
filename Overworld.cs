using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using System;

namespace Abysswalker
{
    public class Node : DrawableGameComponent
    {
        private Texture2D image;
        private List<Texture2D> frames;
        private int frameIndex;
        private string status;
        public Rectangle rect;
        private Rectangle detectionZone;

        public Node(Game game, Vector2 position, string status, float iconSpeed, string path) : base(game)
        {
            frames = LoadFrames(path);
            frameIndex = 0;
            image = frames[frameIndex];
            this.status = status;
            rect = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
            detectionZone = new Rectangle(rect.Center.X - (int)(iconSpeed / 2), rect.Center.Y - (int)(iconSpeed / 2), (int)iconSpeed, (int)iconSpeed);
        }

        private List<Texture2D> LoadFrames(string path)
        {
            var frames = new List<Texture2D>();
            // สมมติว่าเรามีโค้ดในการโหลดภาพจาก path
            return frames;
        }

        private void Animate()
        {
            frameIndex += 1;
            if (frameIndex >= frames.Count)
                frameIndex = 0;
            image = frames[frameIndex];
        }

        public override void Update(GameTime gameTime)
        {
            if (status == "available")
            {
                Animate();
            }
            else
            {
                // Tint the image black if unavailable
                Texture2D tintedImage = new Texture2D(Game.GraphicsDevice, image.Width, image.Height);
                Color[] tintData = new Color[image.Width * image.Height];
                image.GetData(tintData);
                for (int i = 0; i < tintData.Length; i++)
                    tintData[i] = Color.Black; // Apply black tint
                tintedImage.SetData(tintData);
                image = tintedImage;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Begin();
            spriteBatch.Draw(image, rect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public bool IsCollidingWith(Vector2 position)
        {
            return detectionZone.Contains(position);
        }
    }

    public class Icon : DrawableGameComponent
    {
        private Texture2D image;
        public Vector2 position;  // ต้องใช้ Vector2 สำหรับตำแหน่ง
        private Rectangle rect;
        private Vector2 moveDirection;  // ตัวแปรสำหรับทิศทางการเคลื่อนที่
        private float speed;  // ความเร็ว

        public Icon(Game game, Vector2 position) : base(game)
        {
            this.position = position;
            image = Game.Content.Load<Texture2D>("overworld_icon_image"); // Load the icon image
            rect = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
            moveDirection = Vector2.Zero;  // เริ่มต้นทิศทางเป็นศูนย์
            speed = 8f;  // ความเร็วเริ่มต้น
        }

        public override void Update(GameTime gameTime)
        {
            // คำนวณตำแหน่ง rect ใหม่จาก position
            rect.X = (int)(position.X - rect.Width / 2);
            rect.Y = (int)(position.Y - rect.Height / 2);

            // อัปเดตตำแหน่งของ icon ตาม moveDirection
            position += moveDirection * speed; // เพิ่มตำแหน่งตามทิศทางการเคลื่อนที่และความเร็ว

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Begin();
            spriteBatch.Draw(image, rect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SetMoveDirection(Vector2 direction)
        {
            moveDirection = direction;
        }
    }

    public class Overworld : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private Texture2D midground;
        private Vector2 midgroundPosition;
        private List<Node> nodes;
        private Icon icon;
        private int currentLevel;
        private int maxLevel;
        private bool moving;
        private Vector2 moveDirection;
        private bool levelLoading;

        public Overworld(Game game, int startLevel, int maxLevel, Action<int> createLevel) : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            background = Game.Content.Load<Texture2D>("background_image");
            midground = Game.Content.Load<Texture2D>("midground_image");
            midgroundPosition = Vector2.Zero;
            this.maxLevel = maxLevel;
            this.currentLevel = startLevel;
            nodes = new List<Node>();
            icon = new Icon(game, Vector2.Zero);
            levelLoading = false;

            // Create nodes (level points) from level data
            SetupNodes();
        }

        private void SetupNodes()
        {
            // Create nodes (levels)
            for (int i = 0; i <= maxLevel; i++)
            {
                Vector2 position = new Vector2(100 * i, 100); // Example position for each node
                string status = (i <= maxLevel) ? "available" : "locked";
                Node node = new Node(Game, position, status, 32f, "node_graphics_path"); // Add actual path here
                nodes.Add(node);
            }
        }

        public void UpdateBackground()
        {
            midgroundPosition.X -= 1f; // Move midground to simulate scrolling
            if (midgroundPosition.X <= -midground.Width)
            {
                midgroundPosition.X = 0;
            }
        }

        public void Input()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (!moving)
            {
                if (keyboardState.IsKeyDown(Keys.Right) && currentLevel < maxLevel)
                {
                    moveDirection = GetMovementData("next");
                    currentLevel++;
                    moving = true;
                }
                else if (keyboardState.IsKeyDown(Keys.Left) && currentLevel > 0)
                {
                    moveDirection = GetMovementData("previous");
                    currentLevel--;
                    moving = true;
                }
                else if (keyboardState.IsKeyDown(Keys.Space) || keyboardState.IsKeyDown(Keys.Enter))
                {
                    levelLoading = true;
                    // Trigger level creation or transition here
                }
            }
        }

        private Vector2 GetMovementData(string target)
        {
            // คำนวณจุดกึ่งกลางของ rect สำหรับตำแหน่งเริ่มต้น (start)
            Vector2 start = new Vector2(
                nodes[currentLevel].rect.X + nodes[currentLevel].rect.Width / 2,
                nodes[currentLevel].rect.Y + nodes[currentLevel].rect.Height / 2
            );

            // คำนวณจุดกึ่งกลางของ rect สำหรับตำแหน่งสิ้นสุด (end)
            Vector2 end = (target == "next")
                ? new Vector2(
                    nodes[currentLevel + 1].rect.X + nodes[currentLevel + 1].rect.Width / 2,
                    nodes[currentLevel + 1].rect.Y + nodes[currentLevel + 1].rect.Height / 2
                )
                : new Vector2(
                    nodes[currentLevel - 1].rect.X + nodes[currentLevel - 1].rect.Width / 2,
                    nodes[currentLevel - 1].rect.Y + nodes[currentLevel - 1].rect.Height / 2
                );

            return Vector2.Normalize(end - start); // คำนวณทิศทางระหว่างตำแหน่งเริ่มต้นและตำแหน่งสิ้นสุด
        }


        public void UpdateIconPosition()
        {
            if (moving && moveDirection != Vector2.Zero)
            {
                icon.SetMoveDirection(moveDirection);  // กำหนดทิศทางให้ icon
                icon.position += moveDirection * 8f;  // อัปเดตตำแหน่งตามทิศทางและความเร็ว

                Node targetNode = nodes[currentLevel];
                if (targetNode.IsCollidingWith(icon.position))
                {
                    moving = false;
                    moveDirection = Vector2.Zero;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Input();
            UpdateBackground();
            UpdateIconPosition();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.Draw(midground, midgroundPosition, Color.White);

            foreach (var node in nodes)
            {
                node.Draw(gameTime);
            }

            icon.Draw(gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
