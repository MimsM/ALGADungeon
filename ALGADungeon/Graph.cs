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
        
        // pre-drawing variables
        public String xEdges = "";
        public String xRoomTop = "";
        public String xRoomUp = "";
        public String xRoomDown = "";
        public String xRoomBottom2 = "";

        public Graph()
        {
        }

        //generate graph with no of x and y elements
        public void GenerateRandomGraph(int x, int y)
        {
            if (x > 1 && x <= 12 && y > 1 && y <= 5)
            {
                Vertex[,] grid = new Vertex[x, y];

                for (int row = 0; row < y; row++)
                {
                    for (int column = 0; column < x; column++)
                    {
                        //Create vertices

                        //starting point
                        if (row == 0 && column == 0)
                        {
                            grid[column, row] = new Vertex(1);
                        }
                        //end point
                        else if (row == y - 1 && column == x - 1)
                        {
                            grid[column, row] = new Vertex(4);
                        }
                        else
                        {
                            grid[column, row] = new Vertex(3);
                        }

                        //Create edges

                        //First row all horizontal edges
                        if (row == 0 && column >= 1)
                        {
                            //First create right edge of previous vertex in same row
                            grid[column - 1, row].rightEdge =
                                new Edge(RandomNumber(), grid[column - 1, row], grid[column, row]);
                            //Then set leftEdge of current vertex to previous vertex's rightEdge
                            grid[column, row].leftEdge = grid[column - 1, row].rightEdge;
                        }
                        //Other rows and edges
                        else if (row >= 1)
                        {
                            //First create down edge of vertex in same column but previous row
                            grid[column, row - 1].downEdge = new Edge(RandomNumber(), grid[column, row - 1], grid[column, row]);
                            //Then set upEdge of current vertex to previous vertex's downEdge
                            grid[column, row].upEdge = grid[column, row - 1].downEdge;

                            if (column >= 1)
                            {
                                //Create horizontal edges like first row
                                grid[column - 1, row].rightEdge =
                                    new Edge(RandomNumber(), grid[column - 1, row], grid[column, row]);
                                grid[column, row].leftEdge = grid[column - 1, row].rightEdge;
                            }
                        }

                        vertices.Add(grid[column, row]);
                    }
                }

                root = start = current = grid[0, 0];
                end = grid[x - 1, y - 1];

                DrawMap(x);
            }
            else
            {
                Console.WriteLine(
                    "Out of bounds: \n" +
                    "X must be between 2 and 12 \n" +
                    "Y must be between 2 and 5 \n"
                );
            }
        }

        private int RandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1, 10);
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

        public void DrawMap(int x)
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
