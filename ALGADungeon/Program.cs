using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using ALGADungeon;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.GenerateRandomGraph(5, 5);

            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            
            // legend
            Console.WriteLine(
                "S = Room: Startpunt \n" +
                "E = Room: Eindpunt \n" +
                "X = Room: Hier ben je nu \n" +
                "* = Room: Bezocht \n" +
                "~ = Hallway: Ingestort \n" +
                "0 = Hallway: Level tegenstander(cost)\n"
            );

            graph.Print(graph.current);
            Console.WriteLine("\n");

            Console.ReadKey();
        }
    }
}
