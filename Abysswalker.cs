using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Abysswalker
{
    public class Abysswalker : Game
    {
        // ตัวแปรสำหรับจัดการการแสดงผลกราฟิก
        private GraphicsDeviceManager _graphics;
        // ตัวแปรสำหรับจัดการการวาด Sprite บนหน้าจอ
        private SpriteBatch _spriteBatch;
        // ตัวแปรสำหรับจัดการผู้เล่นในเกม
        private Player player;
        // ตัวแปรสำหรับจัดการ UI
        private UI ui;
        // ตัวแปรสำหรับเมนูหลักของเกม
        private MainMenu mainMenu;
        // ตัวแปรสำหรับฉากที่มีการเล่นเกม
        private Level level;
        // ตัวแปรสำหรับฉากโลกภายนอก
        private Overworld overworld;
        // ตัวแปรสำหรับเสียงเมื่อผู้เล่นตาย
        private SoundEffect deathSound;
        // ตัวแปรสำหรับเพลงฉากโลกภายนอก
        private Song overworldMusic;

        // การกำหนดสถานะของเกม (เมนูหลัก, โลกภายนอก, หรือ ระดับเกม)
        private enum GameState { MainMenu, Overworld, Level }
        private GameState currentState = GameState.MainMenu;

        // ตัวแปรสำหรับเก็บข้อมูลระดับสูงสุดที่เล่นได้
        private int maxLevel = 0;
        // ตัวแปรสำหรับเก็บข้อมูลค่าสุขภาพสูงสุดของผู้เล่น
        private int maxHealth = 100;
        // ตัวแปรสำหรับเก็บข้อมูลสุขภาพปัจจุบันของผู้เล่น
        private int currentHealth = 100;
        // ตัวแปรสำหรับเก็บจำนวนเหรียญ
        private int coins = 0;

        public Abysswalker()
        {
            // กำหนดค่าพื้นฐานสำหรับการแสดงผลกราฟิก
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content"; // กำหนดที่อยู่ของไฟล์เนื้อหาต่าง ๆ
            IsMouseVisible = true; // แสดงเคอร์เซอร์ของเมาส์
        }

        protected override void Initialize()
        {
            // เริ่มต้นการตั้งค่าภายในเกม
            base.Initialize();
            // สร้างเมนูหลักและส่งฟังก์ชันการสร้างฉากโลกภายนอก
            mainMenu = new MainMenu(this, CreateOverworld);  // ส่ง CreateOverworld ฟังก์ชัน
            ui = new UI(_spriteBatch); // ส่ง _spriteBatch

        }

        protected override void LoadContent()
        {
            // โหลดเนื้อหาต่าง ๆ ที่จำเป็นสำหรับเกม
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            deathSound = Content.Load<SoundEffect>("death_sound"); // โหลดเสียงเมื่อผู้เล่นตาย
            overworldMusic = Content.Load<Song>("overworld_music"); // โหลดเพลงฉากโลกภายนอก
        }

        private void CreateLevel(int currentLevel)
        {
            // เมื่อสร้างระดับใหม่ ให้กำหนดค่าสุขภาพและเปลี่ยนสถานะเกม
            currentHealth = maxHealth;
            currentState = GameState.Level;
            level = new Level(this, _spriteBatch, currentLevel, CreateOverworld, ChangeCoins, ChangeHealth, CheckGameOver);
        }

        private void CreateOverworld(int currentLevel, int newMaxLevel)
        {
            // เมื่อสร้างฉากโลกภายนอก ให้ปรับระดับสูงสุดหากมีการเปลี่ยนแปลง
            if (newMaxLevel > maxLevel)
                maxLevel = newMaxLevel;
            overworld = new Overworld(this, currentLevel, maxLevel, CreateLevel);
            currentState = GameState.Overworld; // เปลี่ยนสถานะเป็นโลกภายนอก
            MediaPlayer.Stop(); // หยุดเพลงก่อนหน้า
            MediaPlayer.Play(overworldMusic); // เล่นเพลงฉากโลกภายนอก
            MediaPlayer.IsRepeating = true; // ทำให้เพลงเล่นซ้ำ
        }

        private void ChangeCoins(int amount)
        {
            // เปลี่ยนจำนวนเหรียญที่ผู้เล่นมี
            coins += amount;
        }

        private void ChangeHealth(int amount)
        {
            // เปลี่ยนสุขภาพของผู้เล่น
            currentHealth += amount;
        }

        private void CheckGameOver(int currentLevel)
        {
            // ตรวจสอบว่าเกมจบหรือไม่
            if (currentHealth <= 0)
            {
                // หากสุขภาพเป็นศูนย์ ให้รีเซ็ตค่าต่าง ๆ และเล่นเสียง
                currentHealth = maxHealth;
                coins = 0;
                overworld = new Overworld(this, currentLevel, maxLevel, CreateLevel);
                currentState = GameState.Overworld; // เปลี่ยนสถานะเป็นโลกภายนอก
                deathSound.Play(); // เล่นเสียงเมื่อผู้เล่นตาย
                MediaPlayer.Stop(); // หยุดเพลง
                MediaPlayer.Play(overworldMusic); // เล่นเพลงฉากโลกภายนอกใหม่
                MediaPlayer.IsRepeating = true; // ทำให้เพลงเล่นซ้ำ
            }
        }

        protected override void Update(GameTime gameTime)
        {
            // ฟังก์ชันนี้จะถูกเรียกทุกครั้งที่มีการอัพเดตสถานะเกม
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit(); // ถ้ากด Escape ให้ปิดเกม

            // เปลี่ยนสถานะและอัพเดตตามสถานะปัจจุบัน
            switch (currentState)
            {
                case GameState.MainMenu:
                    mainMenu.Update(gameTime); // อัพเดตเมนูหลัก
                    break;
                case GameState.Overworld:
                    overworld.Update(gameTime); // อัพเดตฉากโลกภายนอก
                    break;
                case GameState.Level:
                    level.Update(gameTime); // อัพเดตฉากระดับ
                    ui.ShowHealth(currentHealth, maxHealth); // แสดงสุขภาพ
                    ui.ShowCoins(coins); // แสดงเหรียญ
                    break;
            }
            base.Update(gameTime); // เรียกอัพเดตพื้นฐานจาก Game class
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            switch (currentState)
            {
                case GameState.MainMenu:
                    mainMenu.Draw(gameTime);
                    break;
                case GameState.Overworld:
                    overworld.Draw(gameTime);
                    break;
                case GameState.Level:
                    level.Draw(gameTime);
                    ui.ShowHealth(currentHealth, maxHealth);
                    ui.ShowCoins(coins);
                    break;

            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
