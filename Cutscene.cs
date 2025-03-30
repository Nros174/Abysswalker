using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class CutScene
{
    // อาร์เรย์ของรูปภาพที่จะใช้แสดงในฉาก cutscene
    private Texture2D[] cutsceneImages;
    
    // ตัวแปรเก็บดัชนีของรูปภาพปัจจุบัน
    private int currentImageIndex;
    
    // ตัวแปรเก็บเวลาที่ใช้ในการแสดงแต่ละภาพ
    private float timer;
    
    // ระยะเวลาที่จะแสดงแต่ละภาพ (หน่วยเป็นวินาที)
    private const float imageDuration = 3f;

    // ตัวแปรตรวจสอบว่า cutscene กำลังทำงานอยู่หรือไม่
    private bool isCutSceneActive;

    // ตัวสร้าง (Constructor) เพื่อรับค่า Texture2D ของภาพทั้งหมดที่ต้องการแสดง
    public CutScene(Texture2D[] cutsceneImages)
    {
        this.cutsceneImages = cutsceneImages;
        this.currentImageIndex = 0;  // เริ่มต้นที่รูปภาพแรก
        this.timer = 0f;  // เริ่มต้นเวลาเป็น 0
        this.isCutSceneActive = false;  // เริ่มต้นให้ cutscene ไม่ทำงาน
    }

    // ฟังก์ชันสำหรับเริ่ม cutscene ใหม่
    public void StartCutScene()
    {
        isCutSceneActive = true;  // เริ่มให้ cutscene ทำงาน
        currentImageIndex = 0;  // รีเซ็ทดัชนีรูปภาพให้กลับไปที่รูปแรก
        timer = 0f;  // รีเซ็ทเวลาให้เป็น 0
    }

    // ฟังก์ชันที่จะถูกเรียกในทุกๆ เฟรม เพื่ออัพเดทสถานะการแสดงภาพ
    public void Update(GameTime gameTime)
    {
        if (isCutSceneActive)
        {
            // เพิ่มเวลาที่ผ่านมาในแต่ละเฟรม
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // ถ้าเวลาผ่านไปถึงระยะเวลาที่กำหนดสำหรับการแสดงภาพ
            if (timer >= imageDuration)
            {
                timer = 0f;  // รีเซ็ทเวลา
                currentImageIndex++;  // ไปที่ภาพถัดไป

                // ถ้าภาพหมดแล้วก็หยุด cutscene
                if (currentImageIndex >= cutsceneImages.Length)
                {
                    isCutSceneActive = false;  // หยุดการแสดง cutscene
                }
            }
        }
    }

    // ฟังก์ชันสำหรับการวาดภาพในฉาก cutscene
    public void Draw(SpriteBatch spriteBatch)
    {
        if (isCutSceneActive && cutsceneImages.Length > 0)
        {
            // วาดรูปภาพที่ดัชนีปัจจุบัน
            spriteBatch.Draw(cutsceneImages[currentImageIndex], new Vector2(0, 0), Color.White);
        }
    }

    // ฟังก์ชันสำหรับเช็คว่า cutscene กำลังทำงานหรือไม่
    public bool IsCutSceneActive()
    {
        return isCutSceneActive;
    }
}
