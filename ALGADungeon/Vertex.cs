using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Vertex
    {
        // states: 0 = starting point, 1 = visiting, 2 = visited, 3 = not visited, 4 = end point
        public int state { get; set; }
        public string name { get; set; }
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

        public void Print()
        {
            String printState = " ";

            Console.ResetColor();
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
                Console.Write("    ___" + rightEdge.level + "___");
                rightEdge.rightVertex.Print();
            }
            else
            {
                Console.Write("    " + printState + "    ");
                Console.ResetColor();
            }
        }

        public string PrintEdge()
        {
            if (rightEdge != null)
            {
                return "   |" + downEdge.level + "|          " + rightEdge.rightVertex.PrintEdge();
            }

            return "   |" + downEdge.level + "|";
        }
    }
}
