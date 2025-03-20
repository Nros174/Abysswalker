using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Abysswalker
{
    public class UI
    {
        // ตัวแปรที่ใช้สำหรับการวาด Sprite และจัดการกราฟิก
        private SpriteBatch spriteBatch;

        // ตัวแปรที่ใช้เก็บภาพของแถบสุขภาพและตำแหน่งของมัน
        private Texture2D healthBar;
        private Vector2 healthBarPosition;
        private int barMaxWidth = 152; // ความกว้างสูงสุดของแถบสุขภาพ
        private int barHeight = 4; // ความสูงของแถบสุขภาพ

        // ตัวแปรสำหรับเหรียญและตำแหน่งของมัน
        private Texture2D coin;
        private Rectangle coinRect;

        // ตัวแปรสำหรับฟอนต์ในการแสดงผลข้อความ (เช่น จำนวนเหรียญ)
        private SpriteFont font;

        // Constructor สำหรับการรับค่า SpriteBatch ที่จะใช้ในการวาดกราฟิก
        public UI(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            // กำหนดตำแหน่งเริ่มต้นของแถบสุขภาพ
            healthBarPosition = new Vector2(54, 39);
        }

        // ฟังก์ชันสำหรับโหลดเนื้อหาต่าง ๆ ที่จำเป็นสำหรับ UI
        public void LoadContent(ContentManager content)
        {
            // โหลดภาพแถบสุขภาพ, เหรียญ และฟอนต์
            healthBar = content.Load<Texture2D>("ui_health_bar");
            coin = content.Load<Texture2D>("ui_coin");
            font = content.Load<SpriteFont>("PixelFont"); // ควรตรวจสอบว่าได้เพิ่มไฟล์ฟอนต์ใน Content Pipeline แล้ว
        }

        // ฟังก์ชันสำหรับแสดงแถบสุขภาพ
        public void ShowHealth(int current, int full)
        {
            // เริ่มการวาดกราฟิก
            spriteBatch.Begin();

            // วาดแถบสุขภาพพื้นหลัง
            spriteBatch.Draw(healthBar, healthBarPosition, Color.White);

            // คำนวณอัตราส่วนสุขภาพปัจจุบันกับสุขภาพเต็ม
            float currentHealthRatio = (float)current / full;
            int currentBarWidth = (int)(barMaxWidth * currentHealthRatio); // ความกว้างของแถบสุขภาพตามอัตราส่วน

            // วาดแถบสุขภาพที่เต็มไปด้วยสีแดง
            Rectangle healthBarRect = new Rectangle((int)healthBarPosition.X, (int)healthBarPosition.Y, currentBarWidth, barHeight);
            spriteBatch.Draw(healthBar, healthBarRect, Color.Red);  // ใช้สีแดงเพื่อเติมแถบสุขภาพ

            // สิ้นสุดการวาด
            spriteBatch.End();
        }

        // ฟังก์ชันสำหรับแสดงจำนวนเหรียญ
        public void ShowCoins(int amount)
        {
            // เริ่มการวาดกราฟิก
            spriteBatch.Begin();

            // วาดภาพเหรียญ
            spriteBatch.Draw(coin, coinRect, Color.White);

            // สร้างข้อความที่จะแสดงจำนวนเหรียญ
            string coinAmountText = amount.ToString();

            // กำหนดตำแหน่งที่จะแสดงจำนวนเหรียญ
            Vector2 coinTextPosition = new Vector2(coinRect.X + 4, coinRect.Y);

            // วาดข้อความที่เป็นจำนวนเหรียญ
            spriteBatch.DrawString(font, coinAmountText, coinTextPosition, Color.Green);

            // สิ้นสุดการวาด
            spriteBatch.End();
        }
    }
}
