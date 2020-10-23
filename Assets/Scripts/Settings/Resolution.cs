using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public static class Resolution
    {

        private static float maxWidth = Display.main.systemWidth;
        private static float maxHeight = Display.main.systemHeight;

        private static Vector2[] screenSizes =
        {
            new Vector2(800, 600), new Vector2(1024, 768), new Vector2(1280, 720), new Vector2(1366, 768),
            new Vector2(1440, 900), new Vector2(1600, 900), new Vector2(1680, 1050), new Vector2(1920, 1080),
            new Vector2(2560, 1080), new Vector2(2560, 1440)
        };

        public static Vector2[] listScreenSizes
        {
            get
            {
                List<Vector2> listScreenSizes = new List<Vector2>();
                foreach (Vector2 vector2 in screenSizes) if (vector2.x <= maxWidth && vector2.y <= maxHeight) listScreenSizes.Add(vector2);
                return listScreenSizes.ToArray();
            }
        }

        private static Vector2[] resolutions = null;

        public static int DefaultResolution()
        {
            if (resolutions == null) resolutions = listScreenSizes;
            int indexResolution = 2;
            for (int i = 0; i < resolutions.Length; i++)
            {
                Vector2 resolution = resolutions[i];
                if (maxWidth == resolution.x && maxHeight == resolution.y)
                {
                    return i;
                }
            }
            return indexResolution;
        }

        public static Vector2 GetResolution(int index)
        {
            if (resolutions == null) resolutions = listScreenSizes;
            if (index >= 0 && index < resolutions.Length) return resolutions[index];
            else return resolutions[2];
        }

    }
}