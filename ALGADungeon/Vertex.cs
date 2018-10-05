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
        public Edge leftEdge { get; set; }
        public Edge rightEdge { get; set; }
        public Edge upEdge { get; set; }
        public Edge downEdge { get; set; }
        public bool visiting { get; set; }

        public Vertex(int state)
        {
            this.state = state;

            if (state == 1)
            {
                visiting = true;
            }
            else
            {
                visiting = false;
            }
        }

        public string Print()
        {
            String printState = " ";

            if (state == 0)
            {
                printState = visiting ? "X" : "S";
            }
            else if (state == 1)
            {
                printState = "X";
            }
            else if (state == 4)
            {
                printState = "E";
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
