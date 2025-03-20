using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Abysswalker
{
    // คลาสสำหรับสร้างปุ่ม
    public class Button : DrawableGameComponent
    {
        private Texture2D buttonTexture;  // ตัวแปรเก็บภาพของปุ่ม
        private Rectangle buttonRect;     // ตัวแปรเก็บข้อมูลขนาดและตำแหน่งของปุ่ม
        private Vector2 position;         // ตำแหน่งของปุ่ม
        private Vector2 size;             // ขนาดของปุ่ม

        // คอนสตรัคเตอร์ที่ใช้รับค่าในการสร้างปุ่ม
        public Button(Game game, string image, Vector2 position, Vector2 size) : base(game)
        {
            this.position = position;
            this.size = size;
            this.buttonTexture = Game.Content.Load<Texture2D>(image); // โหลดภาพปุ่ม
            this.buttonRect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y); // กำหนดตำแหน่งและขนาดของปุ่ม
        }

        // ฟังก์ชันอัพเดตปุ่มเมื่อมีการเคลื่อนที่ของเมาส์
        public void Update(GameTime gameTime, MouseState mouseState)
        {
            if (buttonRect.Contains(mouseState.Position))
            {
                // ตัวอย่างการเปลี่ยนภาพปุ่มเมื่อเมาส์ชี้ไปที่มัน
            }
        }

        // ฟังก์ชันในการวาดปุ่มลงบนหน้าจอ
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); // ดึงข้อมูล SpriteBatch ที่ใช้ในการวาด
            spriteBatch.Begin();
            spriteBatch.Draw(buttonTexture, buttonRect, Color.White); // วาดปุ่ม
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    // คลาสสำหรับการสร้างข้อความ
    public class Text : DrawableGameComponent
    {
        private SpriteFont font;  // ฟอนต์ที่ใช้ในการแสดงข้อความ
        private string text;      // ข้อความที่จะแสดง
        private Vector2 position; // ตำแหน่งที่จะแสดงข้อความ
        private Color color;      // สีของข้อความ

        // คอนสตรัคเตอร์ที่รับข้อความ, ตำแหน่ง, ขนาดฟอนต์, และสี
        public Text(Game game, string text, Vector2 position, int size, Color color) : base(game)
        {
            this.text = text;
            this.position = position;
            this.color = color;
            this.font = Game.Content.Load<SpriteFont>("PixelFont"); // โหลดฟอนต์จาก Content
        }

        // ฟังก์ชันในการวาดข้อความลงบนหน้าจอ
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); // ดึงข้อมูล SpriteBatch ที่ใช้ในการวาด
            spriteBatch.Begin();
            spriteBatch.DrawString(font, text, position, color); // วาดข้อความ
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    // คลาสสำหรับเมนูหลักของเกม
    public class MainMenu : GameComponent
    {
        private Button playButton;       // ปุ่มเล่น
        private Button exitButton;       // ปุ่มออก
        private Text titleText;          // ข้อความหัวเรื่อง
        private Text subtitleText;       // ข้อความรอง

        private Song menuMusic;          // เพลงของเมนู

        // คอนสตรัคเตอร์ที่ใช้ในการกำหนดค่าพื้นฐาน
        public MainMenu(Game game) : base(game)
        {
            menuMusic = Game.Content.Load<Song>("menu_music"); // โหลดเพลงเมนู
            MediaPlayer.Play(menuMusic);  // เริ่มเล่นเพลงเมนู
            MediaPlayer.IsRepeating = true; // ให้เพลงเล่นซ้ำ

            // สร้างปุ่มเล่นและปุ่มออก
            playButton = new Button(game, "play_button_non_active", new Vector2(400, 300), new Vector2(400, 200));
            exitButton = new Button(game, "exit_button_non_active", new Vector2(400, 600), new Vector2(400, 200));

            // สร้างข้อความหัวเรื่องและรอง
            titleText = new Text(game, "UNDERWATER", new Vector2(400, 100), 110, Color.Green);
            subtitleText = new Text(game, "ADVENTURE", new Vector2(400, 250), 110, Color.Green);
        }

        // ฟังก์ชันอัพเดตปุ่มและตรวจสอบการคลิก
        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState(); // ดึงข้อมูลการเคลื่อนที่ของเมาส์
            playButton.Update(gameTime, mouseState);   // อัพเดตปุ่มเล่น
            exitButton.Update(gameTime, mouseState);   // อัพเดตปุ่มออก

            if (mouseState.LeftButton == ButtonState.Pressed) // หากปุ่มเมาส์ซ้ายถูกคลิก
            {
                if (playButton.Rectangle.Contains(mouseState.Position)) // หากคลิกปุ่มเล่น
                {
                    // เริ่มเกม (สร้างฉากโลกหรือระดับ)
                }
                else if (exitButton.Rectangle.Contains(mouseState.Position)) // หากคลิกปุ่มออก
                {
                    Game.Exit(); // ออกจากเกม
                }
            }

            base.Update(gameTime);
        }

        // ฟังก์ชันวาดข้อความและปุ่มบนหน้าจอ
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); // ดึงข้อมูล SpriteBatch ที่ใช้ในการวาด
            spriteBatch.Begin();

            titleText.Draw(gameTime);    // วาดข้อความหัวเรื่อง
            subtitleText.Draw(gameTime); // วาดข้อความรอง
            playButton.Draw(gameTime);   // วาดปุ่มเล่น
            exitButton.Draw(gameTime);   // วาดปุ่มออก

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
