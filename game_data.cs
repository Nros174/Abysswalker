using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Abysswalker
{
    public class LevelData
    {
        public string Terrain { get; set; }
        public string Coins { get; set; }
        public string Enemies { get; set; }
        public string Decorations { get; set; }
        public string Player { get; set; }
        public string Constrains { get; set; }
        public string Plants { get; set; }
        public string DecorationFishes { get; set; }
        public string DecorationFishesConstrains { get; set; }
        public Vector2 NodePos { get; set; }
        public int Unlock { get; set; }
        public string NodeGraphics { get; set; }
        public string Soundtrack { get; set; }
    }

    public static class GameData
    {
        // ใช้ Dictionary เพื่อจัดการข้อมูลของแต่ละเลเวล
        public static Dictionary<int, LevelData> Levels = new Dictionary<int, LevelData>()
        {
            {
                0, new LevelData
                {
                    Terrain = "levels\\0\\0_terain.csv",
                    Coins = "levels\\0\\0_coins.csv",
                    Enemies = "levels\\0\\0_enemies.csv",
                    Decorations = "levels\\0\\0_decorations.csv",
                    Player = "levels\\0\\0_player.csv",
                    Constrains = "levels\\0\\0_constrains.csv",
                    Plants = "levels\\0\\0_plants.csv",
                    DecorationFishes = "levels\\0\\0_decoration_fishes.csv",
                    DecorationFishesConstrains = "levels\\0\\0_constrains_decoration_fishes.csv",
                    NodePos = new Vector2(800 / 7, 600 / 2),
                    Unlock = 1,
                    NodeGraphics = "graphics\\overworld\\0",
                    Soundtrack = "audio\\music\\The-Shallows.ogg"
                }
            },
            {
                1, new LevelData
                {
                    Terrain = "levels\\1\\1_terain.csv",
                    Coins = "levels\\1\\1_coins.csv",
                    Enemies = "levels\\1\\1_enemies.csv",
                    Decorations = "levels\\1\\1_decorations.csv",
                    Player = "levels\\1\\1_player.csv",
                    Constrains = "levels\\1\\1_constrains.csv",
                    Plants = "levels\\1\\1_plants.csv",
                    DecorationFishes = "levels\\1\\1_decoration_fishes.csv",
                    DecorationFishesConstrains = "levels\\1\\1_constrains_decoration_fishes.csv",
                    NodePos = new Vector2(800 / 4, 600 / 6),
                    Unlock = 2,
                    NodeGraphics = "graphics\\overworld\\1",
                    Soundtrack = "audio\\music\\Lost-Tides.ogg"
                }
            },
            {
                2, new LevelData
                {
                    Terrain = "levels\\2\\2_terain.csv",
                    Coins = "levels\\2\\2_coins.csv",
                    Enemies = "levels\\2\\2_enemies.csv",
                    Decorations = "levels\\2\\2_decorations.csv",
                    Player = "levels\\2\\2_player.csv",
                    Constrains = "levels\\2\\2_constrains.csv",
                    Plants = "levels\\2\\2_plants.csv",
                    DecorationFishes = "levels\\2\\2_decoration_fishes.csv",
                    DecorationFishesConstrains = "levels\\2\\2_constrains_decoration_fishes.csv",
                    NodePos = new Vector2(800 / 2.5f, 600 * 0.85f),
                    Unlock = 3,
                    NodeGraphics = "graphics\\overworld\\2",
                    Soundtrack = "audio\\music\\Drop-off.ogg"
                }
            },
            {
                3, new LevelData
                {
                    Terrain = "levels\\3\\3_terain.csv",
                    Coins = "levels\\3\\3_coins.csv",
                    Enemies = "levels\\3\\3_enemies.csv",
                    Decorations = "levels\\3\\3_decorations.csv",
                    Player = "levels\\3\\3_player.csv",
                    Constrains = "levels\\3\\3_constrains.csv",
                    Plants = "levels\\3\\3_plants.csv",
                    DecorationFishes = "levels\\3\\3_decoration_fishes.csv",
                    DecorationFishesConstrains = "levels\\3\\3_constrains_decoration_fishes.csv",
                    NodePos = new Vector2(800 / 2, 600 / 2.2f),
                    Unlock = 4,
                    NodeGraphics = "graphics\\overworld\\3",
                    Soundtrack = "audio\\music\\Ocean-Drifting.ogg"
                }
            },
            {
                4, new LevelData
                {
                    Terrain = "levels\\4\\4_terain.csv",
                    Coins = "levels\\4\\4_coins.csv",
                    Enemies = "levels\\4\\4_enemies.csv",
                    Decorations = "levels\\4\\4_decorations.csv",
                    Player = "levels\\4\\4_player.csv",
                    Constrains = "levels\\4\\4_constrains.csv",
                    Plants = "levels\\4\\4_plants.csv",
                    DecorationFishes = "levels\\4\\4_decoration_fishes.csv",
                    DecorationFishesConstrains = "levels\\4\\4_constrains_decoration_fishes.csv",
                    NodePos = new Vector2(800 / 1.3f, 600 / 5),
                    Unlock = 5,
                    NodeGraphics = "graphics\\overworld\\4",
                    Soundtrack = "audio\\music\\Thalassophobia.ogg"
                }
            },
            {
                5, new LevelData
                {
                    Terrain = "levels\\5\\5_terain.csv",
                    Coins = "levels\\5\\5_coins.csv",
                    Enemies = "levels\\5\\5_enemies.csv",
                    Decorations = "levels\\5\\5_decorations.csv",
                    Player = "levels\\5\\5_player.csv",
                    Constrains = "levels\\5\\5_constrains.csv",
                    Plants = "levels\\5\\5_plants.csv",
                    DecorationFishes = "levels\\5\\5_decoration_fishes.csv",
                    DecorationFishesConstrains = "levels\\5\\5_constrains_decoration_fishes.csv",
                    NodePos = new Vector2(800 * 0.85f, 600 / 1.4f),
                    Unlock = 5,
                    NodeGraphics = "graphics\\overworld\\5",
                    Soundtrack = "audio\\music\\Dark-Waters.ogg"
                }
            }
        };
    }
}
