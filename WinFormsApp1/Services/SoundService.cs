// Services/SoundService.cs
using System.Media;

namespace FoodOrderingSystem.Services
{
    public static class SoundService
    {
        private static bool soundEnabled = true;

        public static bool SoundEnabled
        {
            get => soundEnabled;
            set => soundEnabled = value;
        }

        public static void PlayNewOrderSound()
        {
            if (!soundEnabled) return;

            try
            {
                // Play system sound for new order
                SystemSounds.Exclamation.Play();
            }
            catch
            {
                // Ignore sound errors
            }
        }

        public static void PlayOrderReadySound()
        {
            if (!soundEnabled) return;

            try
            {
                // Play system sound for order ready
                SystemSounds.Asterisk.Play();
            }
            catch
            {
                // Ignore sound errors
            }
        }

        public static void PlayOrderServedSound()
        {
            if (!soundEnabled) return;

            try
            {
                // Play system sound for order served
                SystemSounds.Hand.Play();
            }
            catch
            {
                // Ignore sound errors
            }
        }

        public static void PlayErrorSound()
        {
            if (!soundEnabled) return;

            try
            {
                // Play system sound for errors
                SystemSounds.Hand.Play();
            }
            catch
            {
                // Ignore sound errors
            }
        }

        public static void PlaySuccessSound()
        {
            if (!soundEnabled) return;

            try
            {
                // Play system sound for success
                SystemSounds.Beep.Play();
            }
            catch
            {
                // Ignore sound errors
            }
        }
    }
}