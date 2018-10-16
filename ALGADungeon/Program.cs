using System;
using System.Collections.Generic;

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
                Console.WriteLine("Console acties: quit, reset, nuke \n" +
                                  "Game acties: right, left, down, up \n" +
                                  "Items: talisman, handgranaat, kompas");
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
                    case "nuke":
                        Console.Clear();
                        graph.Nuke();
                        DrawLevel(graph);
                        break;
                    case "right":
                        Console.Clear();
                        if (graph.current.rightEdge != null && graph.current.rightEdge.state != -1)
                        {
                            graph.GoDirection(2);

                            //Redraw level
                            DrawLevel(graph);

                            if (graph.current.state == 4)
                            {
                                WonGame();

                                string playerInput2 = Console.ReadLine();

                                switch (playerInput2)
                                {
                                    case "yes":
                                        Console.Clear();
                                        graph = Init();
                                        break;

                                    case "no":
                                        Environment.Exit(0);
                                        break;
                                }
                            }
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
                        if (graph.current.leftEdge != null && graph.current.leftEdge.state != -1)
                        {
                            graph.GoDirection(4);

                            //Redraw level
                            DrawLevel(graph);

                            if (graph.current.state == 4)
                            {
                                WonGame();

                                string playerInput2 = Console.ReadLine();

                                switch (playerInput2)
                                {
                                    case "yes":
                                        Console.Clear();
                                        graph = Init();
                                        break;

                                    case "no":
                                        Environment.Exit(0);
                                        break;
                                }
                            }
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
                        if (graph.current.upEdge != null && graph.current.upEdge.state != -1)
                        {
                            graph.GoDirection(1);

                            //Redraw level
                            DrawLevel(graph);

                            if (graph.current.state == 4)
                            {
                                WonGame();

                                string playerInput2 = Console.ReadLine();

                                switch (playerInput2)
                                {
                                    case "yes":
                                        Console.Clear();
                                        graph = Init();
                                        break;

                                    case "no":
                                        Environment.Exit(0);
                                        break;
                                }
                            }
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
                        if (graph.current.downEdge != null && graph.current.downEdge.state != -1)
                        {
                            graph.GoDirection(3);

                            //Redraw level
                            DrawLevel(graph);

                            if (graph.current.state == 4)
                            {
                                WonGame();

                                string playerInput2 = Console.ReadLine();

                                switch (playerInput2)
                                {
                                    case "yes":
                                        Console.Clear();
                                        graph = Init();
                                        break;

                                    case "no":
                                        Environment.Exit(0);
                                        break;
                                }
                            }
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
                    case "handgranaat":
                        Console.Clear();

                        graph.Grenade();
                        DrawLevel(graph);

                        Console.WriteLine("De kerker schudt op zijn grondvesten, de tegenstander in een aangrenzende hallway is vermorzeld! \n" +
                            "Een donderend geluid maakt duidelijk dat gedeeltes van de kerker zijn ingestort... \n");
                        break;
                    case "kompas":
                        Console.Clear();
                        DrawLevel(graph);

                        Console.WriteLine(graph.Compass());
                        break;
                }
            }
        }

        private static void DrawLevel(Graph graph)
        {
            //Create legend
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("S = Room: Startpunt");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("E = Room: Eindpunt");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("X = Room: Hier ben je nu");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("* = Room: Bezocht");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("~ = Hallway: Ingestort");
            Console.ResetColor();
            Console.WriteLine("0 = Hallway: Level tegenstander(cost)\n");

            //Print graph
            graph.Print(graph.root);
            Console.WriteLine("\n");
        }

        private static void WonGame()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations! You have reached the endpoint!\nDo you want to play again? (yes/no) ");
            Console.ResetColor();
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
            graph.MinimumSpanningTree();
            DrawLevel(graph);

            return graph;
        }
    }
}
