using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Utility
{
    public class Audio
    {
        public static void PlayOnce(Uri resourceUri)
        {
            CreateSoundPlayer(resourceUri).Play();
        }

        public static void PlayOnce(string filePath)
        {
            CreateSoundPlayer(filePath).Play();
        }

        #region "private"

        private static SoundPlayer CreateSoundPlayer(string filePath)
        {
            return new SoundPlayer(filePath);
        }

        private static SoundPlayer CreateSoundPlayer(Uri resourceUri)
        {
            return new SoundPlayer(GetStream(resourceUri));
        }

        private static UnmanagedMemoryStream GetStream(Uri resourceUri)
        {
            return GetResourceManager().GetStream(resourceUri.ToString());
        }

        private static ResourceManager GetResourceManager()
        {
            return Properties.Resources.ResourceManager;
        }

        #endregion

    }
}
