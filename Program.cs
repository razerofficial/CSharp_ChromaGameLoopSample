using ChromaSDK;
using System;
using System.Threading;

namespace CSharp_ChromaGameLoopSample
{
    class Program
    {
        static void PrintLegend(SampleApp app, int startIndex, int selectedIndex, int maxItems, bool supportsStreaming, byte platform)
        {
            Console.WriteLine("C# GAME LOOP CHROMA SAMPLE APP");

            Console.WriteLine();

            if (supportsStreaming)
            {
                Console.Write("Use `P` to switch streaming platforms. ");
            }

            Console.WriteLine("Press `ESC` to Quit.");
            Console.WriteLine("Press `H` to toggle hotkeys [{0}]", app.GetHotkeys() ? "ON" : "OFF");
            Console.WriteLine("Press `E` to toggle extended keyboard [{0}]", app.GetExtended() ? "ON" : "OFF");
            Console.WriteLine("Press `A` for ammo/health. [{0}]", app.GetAmmo() ? "ON" : "OFF");
            Console.WriteLine("Press `1` for gradient 1 [{0}]", app.GetStateIndexGradient1() ? "ON" : "OFF");
            Console.WriteLine("Press `2` for gradient 2 [{0}]", app.GetStateIndexGradient2() ? "ON" : "OFF");
            Console.WriteLine("Press `3` for gradient 3 [{0}]", app.GetStateIndexGradient3() ? "ON" : "OFF");
            Console.WriteLine("Press `4` for gradient 4 [{0}]", app.GetStateIndexGradient4() ? "ON" : "OFF");
            Console.WriteLine("Press `C` to change base color.");

            if (supportsStreaming)
            {
                Console.WriteLine();

                Console.WriteLine("Streaming Info (SUPPORTED):");
                ChromaSDK.Stream.StreamStatusType status = ChromaAnimationAPI.CoreStreamGetStatus();
                Console.WriteLine(string.Format("Status: {0}", ChromaAnimationAPI.CoreStreamGetStatusString(status)));
                Console.WriteLine(string.Format("Shortcode: {0}", app.GetShortcode()));
                Console.WriteLine(string.Format("Stream Id: {0}", app.GetStreamId()));
                Console.WriteLine(string.Format("Stream Key: {0}", app.GetStreamKey()));
                Console.WriteLine(string.Format("Stream Focus: {0}", app.GetStreamFocus()));
                Console.WriteLine();

                for (int index = startIndex; index <= maxItems; ++index)
                {
                    if (index == selectedIndex)
                    {
                        Console.Write("[*] ");
                    }
                    else
                    {
                        Console.Write("[ ] ");
                    }
                    Console.Write("{0, 8}", app.GetEffectName(index, platform));

                    if (index > 0)
                    {
                        if ((index % 4) == 0)
                        {
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Write("\t\t");
                        }
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Press ENTER to execute selection.");
            }
        }

        static void Main(string[] args)
        {
            SampleApp sampleApp = new SampleApp();
            sampleApp.Start();

            if (sampleApp.GetInitResult() == RazerErrors.RZRESULT_SUCCESS)
            {
                bool supportsStreaming = ChromaAnimationAPI.CoreStreamSupportsStreaming();

                const int START_INDEX = -9;
                const int MAX_ITEMS = 0;

                int selectedIndex = START_INDEX;

                byte platform = 0;

                DateTime inputTimer = DateTime.MinValue;

                ThreadStart ts = new ThreadStart(sampleApp.GameLoop);
                Thread thread = new Thread(ts);
                thread.Start();
                while (true)
                {
                    if (inputTimer < DateTime.Now)
                    {
                        Console.Clear();
                        PrintLegend(sampleApp, START_INDEX, selectedIndex, MAX_ITEMS, supportsStreaming, platform);
                        inputTimer = DateTime.Now + TimeSpan.FromMilliseconds(100);
                    }
                    ConsoleKeyInfo keyInfo = Console.ReadKey();

                    sampleApp.HandleInput(keyInfo);

                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (selectedIndex > START_INDEX)
                        {
                            --selectedIndex;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (selectedIndex < MAX_ITEMS)
                        {
                            ++selectedIndex;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                    else if (keyInfo.Key == ConsoleKey.P)
                    {
                        platform = (byte)((platform + 1) % 4); //PC, AMAZON LUNA, MS GAME PASS, NVIDIA GFN
                    }
                    else if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        sampleApp.ExecuteItem(selectedIndex, supportsStreaming, platform);
                    }
                    Thread.Sleep(1);
                }
                thread.Join();

            }

            Console.WriteLine("{0}", "[EXIT]");
        }
    }
}
