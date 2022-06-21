using System;

namespace BullRidingGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new bullRiding())
                game.Run();
        }
    }
}
