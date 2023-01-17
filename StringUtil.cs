using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlonUnityUtils
{
    public static class StringUtil
    {
        public static string FormatSeconds(float seconds)
        {
            int minutes = (int)seconds / 60;
            int secondsLeft = (int)seconds % 60;
            return string.Format("{0:00}:{1:00}", minutes, secondsLeft);
        }
    }
}
