using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlatformerTest.Base
{
    static class ProgramConfig
    {
        public static int windowWidth = 640;
        public static int windowHeight = 480;
        public static bool isFullScreen = false;

        public static int tileSize = 32;

        // temporary workaround for working with old Player Class
        public static int[,] LevelTiles = new int[0, 0];
    }
}
