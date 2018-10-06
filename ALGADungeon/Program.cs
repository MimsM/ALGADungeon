using System;

namespace ALGADungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = Init();

            //Game loop
            while (true)
            {
                Console.WriteLine("Console acties: quit, reset \n" +
                                  "Items: talisman");
                string playerInput = Console.ReadLine();

                switch (playerInput)
                {
                    case "quit":
                        Environment.Exit(0);
                        break;
                    case "reset":
                        Console.Clear();
                        graph = Init();
                        break;
                    case "talisman":
                        Console.Clear();
                        DrawLevel(graph);

                        Console.WriteLine("De talisman licht op en fluistert dat het eindpunt n stappen ver weg is \n");
                        break;
                }
            }
        }

        private static void DrawLevel(Graph graph)
        {
            //Create legend
            Console.WriteLine(
                "S = Room: Startpunt \n" +
                "E = Room: Eindpunt \n" +
                "X = Room: Hier ben je nu \n" +
                "* = Room: Bezocht \n" +
                "~ = Hallway: Ingestort \n" +
                "0 = Hallway: Level tegenstander(cost)\n"
            );

            //Print graph
            graph.Print(graph.current);
            Console.WriteLine("\n");
        }

        private static Graph Init()
        {
            //Init
            Graph graph = new Graph();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            int x = 0;
            int y = 0;

            //Await user x input
            while (x == 0)
            {
                Console.WriteLine("Please provide a value between 2 and 12 (x value)");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result >= 2 && result <= 12)
                        x = result;
                }
            }
            //Await user y input
            while (y == 0)
            {
                Console.WriteLine("Please provide a value between 2 and 5 (y value)");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result >= 2 && result <= 5)
                        y = result;
                }
            }

            //Clear console
            Console.Clear();

            //Generate graph with user input
            graph.GenerateRandomGraph(x, y);

            //Draw level
            DrawLevel(graph);

            return graph;
        }
    }
}
