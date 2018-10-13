﻿using System;
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

        public string Print()
        {
            String printState = " ";

            switch (state)
            {
                case 0:
                    printState = visiting ? "X" : "S";
                    break;
                case 1:
                    printState = "X";
                    break;
                case 2:
                    printState = "*";
                    break;
                case 4:
                    printState = visiting ? "X" : "E";
                    break;
            }

            if (this.rightEdge != null)
            {
                return "    " + printState + "    ___" + rightEdge.level + "___" + rightEdge.rightVertex.Print();
            }

            return "    " + printState + "    ";
        }

        public string PrintEdge()
        {
            if (this.rightEdge != null)
            {
                return "   |" + downEdge.level + "|          " + rightEdge.rightVertex.PrintEdge();
            }

            return "   |" + downEdge.level + "|";
        }
    }
}
