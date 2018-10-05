using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using ConsoleApp1;

namespace ALGADungeon
{
    class Graph
    {
        public Vertex root { get; set; }
        public List<Vertex> vertices = new List<Vertex>();
        public Vertex start { get; set; }
        public Vertex end { get; set; }
        public Vertex current { get; set; }
        public List<Vertex> visited { get; set; }

        private int x;
        private int y;
        
        // pre-drawing variables
        public String xEdges = "";
        public String xRoomTop = "";
        public String xRoomUp = "";
        public String xRoomDown = "";
        public String xRoomBottom2 = "";

        public Graph()
        {
            x = 3;
            y = 2;
            DrawMap();

            //generate predefined graph
            Vertex n1 = new Vertex(0);
            Vertex n2 = new Vertex(1);
            Vertex n3 = new Vertex(3);
            Vertex n4 = new Vertex(3);
            Vertex n5 = new Vertex(3);
            Vertex n6 = new Vertex(3);
            Vertex n7 = new Vertex(3);
            Vertex n8 = new Vertex(3);
            Vertex n9 = new Vertex(4);

            n1.downEdge = new Edge(2, n1, n4);
            n1.rightEdge = new Edge(5, n1, n2);

            n2.leftEdge = n1.rightEdge;
            n2.downEdge = new Edge(4, n2, n5);
            n2.rightEdge = new Edge(4, n2, n3);

            n3.leftEdge = n2.rightEdge;
            n3.downEdge = new Edge(6, n3, n6);

            n4.upEdge = n1.downEdge;
            n4.rightEdge = new Edge(3, n4, n5);
            n4.downEdge = new Edge(2, n4, n7);

            n5.leftEdge = n4.rightEdge;
            n5.upEdge = n2.downEdge;
            n5.rightEdge = new Edge(8, n5, n6);
            n5.downEdge = new Edge(3, n5, n8);

            n6.leftEdge = n5.rightEdge;
            n6.upEdge = n3.downEdge;
            n6.downEdge = new Edge(4, n6, n9);

            n7.upEdge = n4.downEdge;
            n7.rightEdge = new Edge(8, n7, n8);

            n8.leftEdge = n7.rightEdge;
            n8.upEdge = n5.downEdge;
            n8.rightEdge = new Edge(2, n8, n9);

            n9.leftEdge = n8.rightEdge;
            n9.upEdge = n6.downEdge;

            root = n1;
            start = n1;
            end = n9;
            current = n1;

            vertices.Add(n1);
            vertices.Add(n2);
            vertices.Add(n3);
            vertices.Add(n4);
            vertices.Add(n5);
            vertices.Add(n6);
            vertices.Add(n7);
            vertices.Add(n8);
            vertices.Add(n9);
        }

        public Graph(Vertex start, Vertex end)
        {
            DrawMap();
            this.start = start;
            this.end = end;
            root = start;
            x = 3;
            x = y;
        }

        public Graph(int x, int y)
        {
            DrawMap();
            this.x = x;
            this.y = y;
            //generate graph with no of x and y elements
        }

        public void Print(Vertex v)
        {
            Console.WriteLine(xRoomTop + "\n" + xRoomUp + "\n" + v.Print() + "\n" + xRoomDown + "\n" + xRoomTop);

            if (v.downEdge != null)
            {
                Console.WriteLine(xEdges + "\n" + v.PrintEdge() + "\n" + xEdges);

                Print(v.downEdge.rightVertex);
            }
        }

        public void DrawMap()
        {
            // pre-drawing
            for (var i = 0; i < x - 1; i++)
            {
                xEdges += "   | |          ";
                xRoomTop += "---   ---       ";
                xRoomUp += "|       |_______";
                xRoomDown += "|       |       ";
            }

            xEdges += "   | |";
            xRoomTop += "---   ---";
            xRoomUp += "|       |";
            xRoomDown += "|       |";
            // end of pre-drawing
        }
    }
}
