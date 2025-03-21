using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;

namespace Abysswalker
{
    public static class Support
    {
        // ฟังก์ชันสำหรับโหลดภาพจากโฟลเดอร์
        public static List<Texture2D> ImportFolder(ContentManager content, string path)
        {
            List<Texture2D> surfaceList = new List<Texture2D>();
            string[] imageFiles = Directory.GetFiles(path);

            foreach (var image in imageFiles)
            {
                Texture2D imageSurf = content.Load<Texture2D>(image); // โหลดภาพจาก Content
                surfaceList.Add(imageSurf);
            }

            return surfaceList;
        }

        // ฟังก์ชันสำหรับโหลดเลย์เอาต์ของ CSV และแปลงเป็นเทอร์เรนท์
        public static string[,] ImportCsvLayout(string path)
        {
            List<string[]> terrainList = new List<string[]>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    terrainList.Add(line.Split(',')); // แยกแต่ละแถวและคอลัมน์
                }
            }

            int rows = terrainList.Count;
            int columns = terrainList[0].Length;
            string[,] terrainMap = new string[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    terrainMap[i, j] = terrainList[i][j]; // เก็บข้อมูลแผนที่
                }
            }

            return terrainMap;
        }

        // ฟังก์ชันสำหรับตัดกราฟิกจากภาพที่มีหลายส่วน (เช่น ไทล์) ให้เป็นส่วนเล็ก ๆ
        public static List<Texture2D> ImportCutGraphics(Game game, ContentManager content, string path, int tileSize)
        {
            Texture2D surface = content.Load<Texture2D>(path);
            int tileNumX = surface.Width / tileSize;
            int tileNumY = surface.Height / tileSize;
            List<Texture2D> cutTiles = new List<Texture2D>();

            for (int row = 0; row < tileNumY; row++)
            {
                for (int col = 0; col < tileNumX; col++)
                {
                    Rectangle sourceRect = new Rectangle(col * tileSize, row * tileSize, tileSize, tileSize);
                    Texture2D newSurf = new Texture2D(game.GraphicsDevice, tileSize, tileSize); // ใช้ game.GraphicsDevice
                    Color[] data = new Color[tileSize * tileSize];

                    surface.GetData(0, sourceRect, data, 0, data.Length);
                    newSurf.SetData(data);

                    cutTiles.Add(newSurf);
                }
            }

            return cutTiles;
        }

    }
}
