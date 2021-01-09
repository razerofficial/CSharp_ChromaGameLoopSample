using ChromaSDK;
using System;
using System.Threading;

namespace CSharp_ChromaGameLoopSample
{
    class Program
    {
        static void PrintLegend(SampleApp sampleApp)
        {
            Console.WriteLine(@"C# GAME LOOP CHROMA SAMPLE APP

Press `ESC` to Quit.
Press `A` for ammo/health.
Press `F` for fire.
Press `H` to toggle hotkeys.
Press `L` for landscape.
Press `R` for rainbow.
Press `S` for spiral.");
        }

        static void Main(string[] args)
        {
            SampleApp sampleApp = new SampleApp();
            sampleApp.Start();

            if (sampleApp.GetInitResult() == RazerErrors.RZRESULT_SUCCESS)
            {
                ThreadStart ts = new ThreadStart(sampleApp.GameLoop);
                Thread thread = new Thread(ts);
                thread.Start();
                while (true)
                {
                    Console.Clear();
                    PrintLegend(sampleApp);
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    sampleApp.HandleInput(keyInfo);

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    Thread.Sleep(1);
                }
                thread.Join();

            }

            Console.WriteLine("{0}", "[EXIT]");
        }
    }
}
