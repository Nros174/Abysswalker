using Microsoft.Xna.Framework;
using System;

namespace Abysswalker
{
    public static class Settings
    {
        // General settings
        public static int TileSize = 64;
        public static float MusicVolume = 1f;
        public static int ScreenWidth = GetScreenResolution().Item1;
        public static int ScreenHeight = GetScreenResolution().Item2;
        public static int FPS = 60;
        public static int PlayerSprintCooldownTime = 300;
        public static string PixelFont = "graphics\\ui\\font.ttf"; // Adjust path to load font from Content

        // Colors
        public static Color BLACK = new Color(0, 0, 0);
        public static Color WHITE = new Color(255, 255, 255);
        public static Color GRAY = new Color(100, 100, 100);
        public static Color DARK_GREEN = new Color(13, 26, 32);
        public static Color DARK_BLUE = new Color(2, 3, 28);
        public static Color RED = new Color(237, 2, 2);
        public static Color GOLD = new Color(209, 177, 0);
        public static Color GREEN = new Color(192, 240, 165);

        // Image paths
        // Buttons
        public static string PlayButtonNonActiveImage = "graphics\\main_menu\\button\\non_active\\start_btn.png";
        public static string ExitButtonNonActiveImage = "graphics\\main_menu\\button\\non_active\\exit_btn.png";
        public static string PlayButtonActiveImage = "graphics\\main_menu\\button\\active\\start_btn.png";
        public static string ExitButtonActiveImage = "graphics\\main_menu\\button\\active\\exit_btn.png";

        // Static objects
        public static string TerrainTileImage = "graphics\\terrain\\terain_tile.png";
        public static string Plant1Image = "graphics\\decorations\\static_decorations\\6.png";
        public static string Plant2Image = "graphics\\decorations\\static_decorations\\7.png";
        public static string DecorationsImage0 = "graphics\\decorations\\static_decorations\\1.png";
        public static string DecorationsImage1 = "graphics\\decorations\\static_decorations\\2.png";
        public static string DecorationsImage2 = "graphics\\decorations\\static_decorations\\3.png";
        public static string DecorationsImage3 = "graphics\\decorations\\static_decorations\\4.png";
        public static string DecorationsImage4 = "graphics\\decorations\\static_decorations\\5.png";
        public static string SubmarineImage = "graphics\\character\\submarine.png";

        // Animated objects
        public static string Plant3Images = "graphics\\decorations\\plant_3";
        public static string GoldCoinImages = "graphics\\coins\\gold";
        public static string SilverCoinImages = "graphics\\coins\\silver";
        public static string DartFishImages = "graphics\\decorations\\decoration_fishes\\dart_fish";
        public static string DefFishImages = "graphics\\decorations\\decoration_fishes\\def_fish";
        public static string JellyFishImages = "graphics\\decorations\\decoration_fishes\\jelly_fish";

        // Enemy
        public static string EnemyRunImages = "graphics\\enemies\\big_fish";
        public static string EnemyExplosionImages = "graphics\\enemies\\explosions\\enemy";

        // Player
        public static string PlayerImagesPath = "graphics\\character";

        // UI and main menu
        public static string UIHealthBarImage = "graphics\\ui\\health_bar.png";
        public static string UICoinImage = "graphics\\ui\\coin.png";
        public static string PauseImage = "graphics\\ui\\pause.png";
        public static string ScreenIconImage = "graphics\\ui\\screen_icon.png";
        public static string OverworldIconImage = "graphics\\overworld\\submarine.png";
        public static string LoadingImage = "graphics\\ui\\loading.png";

        // Background
        public static string BackgroundImage = "graphics\\background\\background.png";
        public static string MidgroundImage = "graphics\\background\\midground.png";

        // Audio
        // Music
        public static string MenuMusic = "audio\\music\\Watery-Cave.ogg";
        public static string OverworldMusic = "audio\\music\\23_loop.ogg";
        // Sound effects
        public static string CoinSound = "audio\\sounds\\coin.ogg";
        public static string DamageSound = "audio\\sounds\\damage.ogg";
        public static string DeathSound = "audio\\sounds\\death.ogg";
        public static string EnemyDeathSound = "audio\\sounds\\enemy_death.ogg";
        public static string SprintSound = "audio\\sounds\\sprint.ogg";
        public static string WinSound = "audio\\sounds\\win.ogg";

        // Helper function to get screen resolution
        private static Tuple<int, int> GetScreenResolution()
        {
            // You can replace this with a MonoGame-compatible method to fetch the screen resolution
            return new Tuple<int, int>(1920, 1080); // Replace with actual screen resolution if needed
        }
    }
}
