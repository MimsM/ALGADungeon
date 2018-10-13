using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        //public List<Vertex> visited { get; set; }
        
        // pre-drawing variables
        public String xEdges = "";
        public String xRoomTop = "";
        public String xRoomUp = "";
        public String xRoomDown = "";
        public String xRoomBottom2 = "";

        public Graph()
        {
        }

        //generate graph with no of x and y elements + start/endpoint
        public void GenerateRandomGraph(int x, int y, int startX, int startY, int endX, int endY)
        {
            if (x > 1 && x <= 12 && y > 1 && y <= 5)
            {
                Vertex[,] grid = new Vertex[x, y];

                for (int row = 0; row < y; row++)
                {
                    for (int column = 0; column < x; column++)
                    {
                        //Create vertices with a state and name
                        grid[column, row] = new Vertex(3, "y" + (row + 1) + " x" + (column + 1));

                        //Create edges

                        //First row all horizontal edges
                        if (row == 0 && column >= 1)
                        {
                            //First create right edge of previous vertex in same row
                            grid[column - 1, row].rightEdge =
                                new Edge(RandomNumber(1,9), grid[column - 1, row], grid[column, row]);
                            
                            //Then set leftEdge of current vertex to previous vertex's rightEdge
                            grid[column, row].leftEdge = grid[column - 1, row].rightEdge;

                            //Add to adjacency list
                            grid[column - 1, row].adjacentVertices.Add(grid[column, row]);
                            grid[column, row].adjacentVertices.Add(grid[column - 1, row]);
                        }
                        //Other rows and edges
                        else if (row >= 1)
                        {
                            //First create down edge of vertex in same column but previous row
                            grid[column, row - 1].downEdge = new Edge(RandomNumber(1,9), grid[column, row - 1], grid[column, row]);

                            //Then set upEdge of current vertex to previous vertex's downEdge
                            grid[column, row].upEdge = grid[column, row - 1].downEdge;

                            //Add to adjacency list
                            grid[column, row - 1].adjacentVertices.Add(grid[column, row]);
                            grid[column, row].adjacentVertices.Add(grid[column, row - 1]);

                            if (column >= 1)
                            {
                                //Create horizontal edges like first row
                                grid[column - 1, row].rightEdge =
                                    new Edge(RandomNumber(1,9), grid[column - 1, row], grid[column, row]);
                                grid[column, row].leftEdge = grid[column - 1, row].rightEdge;

                                //Add to adjacency list
                                grid[column - 1, row].adjacentVertices.Add(grid[column, row]);
                                grid[column, row].adjacentVertices.Add(grid[column - 1, row]);
                            }
                        }

                        vertices.Add(grid[column, row]);
                    }
                }
                //Create start
                start = current = grid[startX, startY];
                start.state = 0;
                start.visiting = true;

                //Create end
                end = grid[endX, endY];

                //If start == end -> find another end
                while (end.name.Equals(start.name))
                {
                    end = grid[RandomNumber(0, x - 1), RandomNumber(0, y - 1)];
                }
                end.state = 4;

                root = grid[0, 0];

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

        public int RandomNumber(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max + 1);
        }

        public void GoDirection(int direction)
        {
            //Set old vertex to visited 
            current.visiting = false;
            if (current.state != 0 && current.state != 4)
            {
                current.state = 2;
            }

            //Change vertex
            switch (direction)
            {
                case 1:
                    current = current.upEdge.leftVertex;
                    break;
                case 2:
                    current = current.rightEdge.rightVertex;
                    break;
                case 3:
                    current = current.downEdge.rightVertex;
                    break;
                case 4:
                    current = current.leftEdge.leftVertex;
                    break;
            }

            //Set new vertex to visiting
            if (current.state != 0 && current.state != 4)
            {
                current.state = 1;
            }
            current.visiting = true;
        }

        public int TalismanBFS()
        {
            int depth = 0;
            int elementsToDepthIncrease = 1;
            int nextElementsToDepthIncrease = 0;
            Queue<Vertex> queue = new Queue<Vertex>();
            HashSet<Vertex> visited = new HashSet<Vertex>();
            
            //Add current vertex to queue
            queue.Enqueue(current);

            while (queue.Count != 0)
            {
                //Remove vertex from queue and add it to visited list
                Vertex vertex = queue.Dequeue();
                visited.Add(vertex);

                //If visited vertex is endpoint -> return
                if (vertex.name.Equals(end.name))
                {
                    return depth;
                }

                Debug.WriteLine("Visiting vertex " + vertex.name);

                //Loop trough adjacentVertices
                foreach (Vertex adjacentVertex in vertex.adjacentVertices)
                {
                    if (!visited.Contains(adjacentVertex) && !queue.Contains(adjacentVertex))
                    {
                        queue.Enqueue(adjacentVertex);
                        nextElementsToDepthIncrease++;
                    }
                }

                //Calculate depth
                elementsToDepthIncrease--;
                if (elementsToDepthIncrease == 0)
                {
                    depth++;
                    elementsToDepthIncrease = nextElementsToDepthIncrease;
                    nextElementsToDepthIncrease = 0;
                }
            }

            return depth;
        }

        public void Print(Vertex v)
        {
            Console.WriteLine(xRoomTop + "\n" + xRoomUp);
            v.Print();
            Console.WriteLine("\n" + xRoomDown + "\n" + xRoomTop);

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
