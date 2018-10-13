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
                                  "Game acties: right, left, down, up \n" +
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
                    case "right":
                        Console.Clear();
                        if (graph.current.rightEdge != null)
                        {
                            graph.GoDirection(2);

                            //Redraw level
                            DrawLevel(graph);
                        }
                        else
                        {
                            //Redraw level
                            DrawLevel(graph);

                            Console.WriteLine("You can't go right!\n");
                        }
                        break;
                    case "left":
                        Console.Clear();
                        if (graph.current.leftEdge != null)
                        {
                            graph.GoDirection(4);

                            //Redraw level
                            DrawLevel(graph);
                        }
                        else
                        {
                            //Redraw level
                            DrawLevel(graph);

                            Console.WriteLine("You can't go left!\n");
                        }
                        break;
                    case "up":
                        Console.Clear();
                        if (graph.current.upEdge != null)
                        {
                            graph.GoDirection(1);

                            //Redraw level
                            DrawLevel(graph);
                        }
                        else
                        {
                            //Redraw level
                            DrawLevel(graph);

                            Console.WriteLine("You can't go up!\n");
                        }
                        break;
                    case "down":
                        Console.Clear();
                        if (graph.current.downEdge != null)
                        {
                            graph.GoDirection(3);

                            //Redraw level
                            DrawLevel(graph);
                        }
                        else
                        {
                            //Redraw level
                            DrawLevel(graph);

                            Console.WriteLine("You can't go down!\n");
                        }
                        break;
                    case "talisman":
                        Console.Clear();
                        DrawLevel(graph);

                        int n = graph.TalismanBFS();

                        Console.WriteLine("De talisman licht op en fluistert dat het eindpunt " + n + " stappen ver weg is \n");
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
            graph.Print(graph.root);
            Console.WriteLine("\n");
        }

        private static Graph Init()
        {
            //Init
            Graph graph = new Graph();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            bool startStopInitialised = false;
            int x, y, startX, startY, endX, endY;
            x = y = startX = startY = endX = endY = 0;

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

            //Await user start/endpoint input
            while (!startStopInitialised)
            {
                Console.WriteLine("Do you want a default, random or custom start/endpoint? (type: default, random or custom)");
                string playerInput = Console.ReadLine();

                switch (playerInput)
                {
                    //Generate graph with user input
                    case "default":
                        graph.GenerateRandomGraph(x, y, 0, 0, x - 1, y - 1);
                        startStopInitialised = true;
                        break;
                    case "":
                        graph.GenerateRandomGraph(x, y, 0, 0, x - 1, y - 1);
                        startStopInitialised = true;
                        break;
                    case "random":
                        graph.GenerateRandomGraph(x, y, graph.RandomNumber(0, x - 1), graph.RandomNumber(0, y - 1),
                            graph.RandomNumber(0, x - 1), graph.RandomNumber(0, y - 1));
                        startStopInitialised = true;
                        break;
                    case "custom":
                        //Await user start x input
                        while (startX == 0)
                        {
                            Console.WriteLine("Please provide a value between 1 and " + x + " (starting point x value)");

                            if (int.TryParse(Console.ReadLine(), out int result))
                            {
                                if (result >= 1 && result <= x)
                                    startX = result;
                            }
                        }
                        //Await user start y input
                        while (startY == 0)
                        {
                            Console.WriteLine("Please provide a value between 1 and " + y + " (starting point y value)");

                            if (int.TryParse(Console.ReadLine(), out int result))
                            {
                                if (result >= 1 && result <= y)
                                    startY = result;
                            }
                        }

                        //Await user end x input
                        while (endX == 0)
                        {
                            Console.WriteLine("Please provide a value between 1 and " + x + " (end point x value)");

                            if (int.TryParse(Console.ReadLine(), out int result))
                            {
                                if (result >= 1 && result <= x)
                                    endX = result;
                            }
                        }
                        //Await user end y input
                        while (endY == 0)
                        {
                            Console.WriteLine("Please provide a value between 1 and " + y + " (end point y value)");

                            if (int.TryParse(Console.ReadLine(), out int result))
                            {
                                if (result >= 1 && result <= y)
                                {
                                    if (startX == endX && startY == result)
                                    {
                                        Console.WriteLine("Starting point and end point can't be the same");
                                    }
                                    else
                                    {
                                        endY = result;
                                    }
                                }
                            }
                        }

                        graph.GenerateRandomGraph(x, y, startX - 1, startY - 1, endX - 1, endY - 1);
                        startStopInitialised = true;
                        break;
                }
            }

            //Clear console and draw level
            Console.Clear();
            DrawLevel(graph);

            return graph;
        }
    }
}
