using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace Abysswalker
{
    public class Node : DrawableGameComponent
    {
        private Texture2D image;
        private List<Texture2D> frames;
        private int frameIndex;
        private string status;
        private Rectangle rect;
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
            // frames.Add(Game.Content.Load<Texture2D>(path + "/frame1"));
            // frames.Add(Game.Content.Load<Texture2D>(path + "/frame2"));
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
        private Vector2 position;
        private Rectangle rect;

        public Icon(Game game, Vector2 position) : base(game)
        {
            this.position = position;
            image = Game.Content.Load<Texture2D>("overworld_icon_image"); // Load the icon image
            rect = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
        }

        public override void Update(GameTime gameTime)
        {
            rect.Center = position.ToPoint();
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
        private float speed;
        private bool levelLoading;

        public Overworld(Game game, int startLevel, int maxLevel) : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            background = Game.Content.Load<Texture2D>("background_image");
            midground = Game.Content.Load<Texture2D>("midground_image");
            midgroundPosition = Vector2.Zero;
            this.maxLevel = maxLevel;
            this.currentLevel = startLevel;
            nodes = new List<Node>();
            icon = new Icon(game, Vector2.Zero);
            speed = 8f;
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
            Vector2 start = new Vector2(nodes[currentLevel].rect.Center.X, nodes[currentLevel].rect.Center.Y);
            Vector2 end = (target == "next") ? new Vector2(nodes[currentLevel + 1].rect.Center.X, nodes[currentLevel + 1].rect.Center.Y)
                                             : new Vector2(nodes[currentLevel - 1].rect.Center.X, nodes[currentLevel - 1].rect.Center.Y);

            return Vector2.Normalize(end - start);
        }

        public void UpdateIconPosition()
        {
            if (moving && moveDirection != Vector2.Zero)
            {
                icon.position += moveDirection * speed;
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
