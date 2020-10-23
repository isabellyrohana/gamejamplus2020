using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public static class Fontsize
    {
        public enum FontsizeEnum
        {
            SMALL,
            NORMAL,
            BIG
        }

        public static int DefaultFontsize => (int) FontsizeEnum.NORMAL;

        public static int Length => Enum.GetNames(typeof(FontsizeEnum)).Length;

        public static string GetFontsize(FontsizeEnum fontsizeEnum)
        {
            switch (fontsizeEnum)
            {
                case FontsizeEnum.SMALL: return "Small";
                case FontsizeEnum.BIG: return "Big";
                default: return "Normal";
            }
        }
    }
}