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
        
        public static void PlayOnce(string filePath)
        {
            CreateSoundPlayer(filePath).Play();
        }

        #region "private"

        private static SoundPlayer CreateSoundPlayer(string filePath)
        {
            return new SoundPlayer(filePath);
        }
       
        #endregion

    }
}
