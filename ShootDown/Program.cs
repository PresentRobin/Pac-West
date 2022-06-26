using System;

namespace ShootDown
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ShootOut())
                game.Run();
        }
    }
}
