using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp1
{
    class Program
    {
        private static int x = 3;
        private static int y = 2;
        private static Vertex current;
        private static List<Vertex> visited;

        // pre-drawing variables
        private static String xEdges = "";
        private static String xRoomTop = "";
        private static String xRoomUp = "";
        private static String xRoomDown = "";
        private static String xRoomBottom2 = "";

        static void Main(string[] args)
        {
            //for (var i = 0; i < y; i++)
            //{
            //    for (var q = 0; q < x; q++)
            //    {

            //    }
            //}

            // pre-drawing
            for (var i = 0; i < x-1; i++)
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

            // legend
            Console.WriteLine(
                "S = Room: Startpunt \n" +
                "E = Room: Eindpunt \n" +
                "X = Room: Hier ben je nu \n" +
                "* = Room: Bezocht \n" +
                "~ = Hallway: Ingestort \n" +
                "0 = Hallway: Level tegenstander(cost)\n"
            );

            Print(n1);
            Console.WriteLine("\n");

            Console.ReadKey();

            n5.state = 30;
            n2.rightEdge.level = 88;

            Console.WriteLine("\n");
            Print(n1);
            Console.WriteLine("\n");

            Console.ReadKey();
        }

        public static void Print(Vertex v)
        {
            Console.WriteLine(xRoomTop + "\n" + xRoomUp + "\n" + v.Print() + "\n" + xRoomDown + "\n" + xRoomTop);

            if (v.downEdge != null)
            {
                Console.WriteLine(xEdges + "\n" + v.PrintEdge() + "\n" + xEdges);

                Print(v.downEdge.rightVertex);
            }
        }
    }
}
