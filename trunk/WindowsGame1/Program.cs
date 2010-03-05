using System;

namespace UltimateErasme
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (UltimateErasme game = new UltimateErasme())
            {
                game.Run();
            }
        }
    }
}

