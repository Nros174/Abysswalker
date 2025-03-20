using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;

namespace Abysswalker
{
    public class SoundManager
    {
        private SoundEffect jumpSound;
        private SoundEffect hitSound;
        private Song backgroundMusic;

        public void LoadContent(ContentManager content)
        {
            try
            {
                jumpSound = content.Load<SoundEffect>("jump");
                hitSound = content.Load<SoundEffect>("hit");
                backgroundMusic = content.Load<Song>("background_music");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading sound files: " + ex.Message);
            }
        }

        public void PlayJumpSound()
        {
            jumpSound?.Play();
        }

        public void PlayHitSound()
        {
            hitSound?.Play();
        }

        public void PlayBackgroundMusic()
        {
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
        }
    }
}