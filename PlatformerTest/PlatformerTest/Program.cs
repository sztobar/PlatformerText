using System;
using Microsoft.Xna.Framework;

namespace PlatformerTest
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //using (Game game = new Game1())
            using (Game game = new CameraGame.CameraTestGame())
            {
                game.Run();
            }
        }
    }
#endif
}

