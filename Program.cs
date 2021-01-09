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
                while (true)
                {
                    Console.Clear();
                    PrintLegend(sampleApp);
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        sampleApp.OnApplicationQuit();
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.A)
                    {
                        //sampleApp.ExecuteItem();
                    }
                    else if (keyInfo.Key == ConsoleKey.F)
                    {
                        //sampleApp.ExecuteItem();
                    }
                    else if (keyInfo.Key == ConsoleKey.H)
                    {
                        //sampleApp.ExecuteItem();
                    }
                    else if (keyInfo.Key == ConsoleKey.L)
                    {
                        //sampleApp.ExecuteItem();
                    }
                    else if (keyInfo.Key == ConsoleKey.R)
                    {
                        //sampleApp.ExecuteItem();
                    }
                    else if (keyInfo.Key == ConsoleKey.S)
                    {
                        //sampleApp.ExecuteItem();
                    }
                    Thread.Sleep(1);
                }

                ChromaAnimationAPI.StopAll();
                ChromaAnimationAPI.CloseAll();
                sampleApp.OnApplicationQuit();

            }

            Console.WriteLine("{0}", "[EXIT]");
        }
    }
}
