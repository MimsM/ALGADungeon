using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ALGADungeon
{
    class Vertex
    {
        // states: 0 = starting point, 1 = visiting, 2 = visited, 3 = not visited, 4 = end point
        public int state { get; set; }
        public string name { get; set; }
        public int number { get; set; }
        public Edge leftEdge { get; set; }
        public Edge rightEdge { get; set; }
        public Edge upEdge { get; set; }
        public Edge downEdge { get; set; }
        public bool visiting { get; set; }
        public HashSet<Vertex> adjacentVertices { get; set; }

        public Vertex(int state, string name)
        {
            adjacentVertices = new HashSet<Vertex>();
            this.state = state;
            this.name = name;
            visiting = false;
        }

        public void Print(List<Edge> MST, List<Vertex> dijkstra)
        {
            String printState = " ";
            Console.ResetColor();

            if (dijkstra.Contains(this))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                printState = "C";
            }
            else
            {
                Console.ResetColor();
            }
            
            switch (state)
            {
                case 0:
                    if (visiting)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        printState = "X";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        printState = "S";
                    }

                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    printState = "X";
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    printState = "*";
                    break;
                case 4:
                    if (visiting)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        printState = "X";
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        printState = "E";
                    }
                    break;
            }

            if (rightEdge != null)
            {
                Console.Write("    " + printState);
                Console.ResetColor();

                if (InSpanningTree(rightEdge, MST))
                    Console.ForegroundColor = ConsoleColor.Magenta;

                if (rightEdge.state == -1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("    ___" + "~" + "___");
                    Console.ResetColor();
                }
                else
                    Console.Write("    ___" + rightEdge.level + "___");

                Console.ResetColor();
                rightEdge.rightVertex.Print(MST, dijkstra);
            }
            else
            {
                Console.Write("    " + printState + "    ");
                Console.ResetColor();
            }
        }

        public void PrintEdge(List<Edge> MST)
        {
            if (rightEdge != null)
            {
                if(InSpanningTree(downEdge, MST))
                    Console.ForegroundColor = ConsoleColor.Magenta;

                if (downEdge.state == -1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("   |" + "~" + "|          ");
                    Console.ResetColor();
                }
                else
                    Console.Write("   |" + downEdge.level + "|          ");

                Console.ResetColor();
                rightEdge.rightVertex.PrintEdge(MST);
            }
            else
            {
                if (InSpanningTree(downEdge, MST))
                    Console.ForegroundColor = ConsoleColor.Magenta;

                if (downEdge.state == -1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("   |" + "~" + "|");
                    Console.ResetColor();
                }
                else
                    Console.Write("   |" + downEdge.level + "|");
                Console.ResetColor();
            }
        }

        public static bool InSpanningTree(Edge edge, List<Edge> MST)
        {
            foreach (Edge e in MST)
            {
                if (edge.leftVertex.number == e.leftVertex.number
                    && edge.rightVertex.number == e.rightVertex.number)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
