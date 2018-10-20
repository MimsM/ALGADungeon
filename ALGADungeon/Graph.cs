using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;

namespace ALGADungeon
{
    class Graph
    {
        public Vertex root { get; set; }
        public List<Vertex> vertices { get; set; }
        public List<Vertex> dijkstra { get; set; }
        public List<Edge> edges { get; set; }
        public List<Edge> MST { get; set; }
        public Vertex start { get; set; }
        public Vertex end { get; set; }
        public Vertex current { get; set; }
        //public List<Vertex> visited { get; set; }
        
        // pre-drawing variables
        public String xEdges = "";
        public String xRoomTop = "";
        public String xRoomUp = "";
        public String xRoomDown = "";

        public Graph()
        {
            vertices = new List<Vertex>();
            edges = new List<Edge>();
            MST = new List<Edge>();
        }

        //generate graph with no of x and y elements + start/endpoint
        public void GenerateRandomGraph(int x, int y, int startX, int startY, int endX, int endY)
        {
            int number = 1;
            if (x > 1 && x <= 12 && y > 1 && y <= 5)
            {
                Vertex[,] grid = new Vertex[x, y];

                for (int row = 0; row < y; row++)
                {
                    for (int column = 0; column < x; column++)
                    {
                        //Create vertices with a state and name
                        grid[column, row] = new Vertex(3, "y" + (row + 1) + " x" + (column + 1)) {number = number};
                        number++;

                        //Create edges

                        //First row all horizontal edges
                        if (row == 0 && column >= 1)
                        {
                            //First create right edge of previous vertex in same row
                            grid[column - 1, row].rightEdge =
                                new Edge(RandomNumber(1,9), grid[column - 1, row], grid[column, row]);

                            //Add to edge list
                            edges.Add(grid[column - 1, row].rightEdge);
                            
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

                            //Add to edge list
                            edges.Add(grid[column, row - 1].downEdge);

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

                                //Add to edge list
                                edges.Add(grid[column - 1, row].rightEdge);

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

        public void MinimumSpanningTree()
        {
            //Kruskal minimum spanning tree

            // Inital sort
            edges = edges.OrderBy(x => x.level).ToList();

            // Set parents table
            int[] parent = Enumerable.Range(0, vertices.Count + 1).ToArray();

            // Spanning tree list
            List<Edge> spanningTree = new List<Edge>();
            foreach (var edge in edges)
            {
                var startNodeRoot = FindRoot(edge.leftVertex.number, parent);
                var endNodeRoot = FindRoot(edge.rightVertex.number, parent);

                if (startNodeRoot != endNodeRoot)
                {
                    // Add edge to the spanning tree
                    spanningTree.Add(edge);

                    // Mark one root as parent of the other
                    parent[endNodeRoot] = startNodeRoot;
                }
            }

            //Set MST
            MST = spanningTree;
        }

        private int FindRoot(int node, int[] parent)
        {
            int kRoot = node;
            while (kRoot != parent[kRoot])
            {
                kRoot = parent[kRoot];
            }

            while (node != kRoot)
            {
                var oldParent = parent[node];
                parent[node] = kRoot;
                node = oldParent;
            }

            return kRoot;
        }

        public void Dijkstra()
        {
            Dictionary<Vertex, Dictionary<Vertex, int>> dijkstraVertices = new Dictionary<Vertex, Dictionary<Vertex, int>>();

            var previous = new Dictionary<Vertex, Vertex>();
            var distances = new Dictionary<Vertex, int>();
            var nodes = new List<Vertex>();

            //create list of all vertices with their neighbours and level of enemies
            foreach (var vertex in vertices)
            {
                var neighbours = new Dictionary<Vertex, int>();

                if (vertex.leftEdge != null && vertex.leftEdge.state != -1)
                {
                    neighbours.Add(vertex.leftEdge.leftVertex, vertex.leftEdge.level);
                }

                if (vertex.upEdge != null && vertex.upEdge.state != -1)
                {
                    neighbours.Add(vertex.upEdge.leftVertex, vertex.upEdge.level);
                }

                if (vertex.rightEdge != null && vertex.rightEdge.state != -1)
                {
                    neighbours.Add(vertex.rightEdge.rightVertex, vertex.rightEdge.level);
                }

                if (vertex.downEdge != null && vertex.downEdge.state != -1)
                {
                    neighbours.Add(vertex.downEdge.rightVertex, vertex.downEdge.level);
                }

                dijkstraVertices.Add(vertex, neighbours);
            }

            List<Vertex> path = null;

            //add 0 distance to current vertex, add other max distances and vertexes to lists
            foreach (var vertex in dijkstraVertices)
            {
                if (vertex.Key == current)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }

                nodes.Add(vertex.Key);
            }
            
            //check distances to neigbours and create shortest route
            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == end)
                {
                    path = new List<Vertex>();
                    path.Add(current);
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }

                foreach (var neighbour in dijkstraVertices[smallest])
                {
                    var alt = distances[smallest] + neighbour.Value;
                    if (alt < distances[neighbour.Key])
                    {
                        distances[neighbour.Key] = alt;
                        previous[neighbour.Key] = smallest;
                    }
                }
            }

            dijkstra = path;
        }

        public string Compass()
        {
            //Create path in correct order
            List<Vertex> temp = dijkstra;
            Vertex[] path = new Vertex[temp.Count];
            for (int i = temp.Count - 1; i >= 1; i--)
            {
                path[temp.Count - i] = temp[i];
            }
            path[0] = temp[0];

            string returnString = "Je haalt het kompas uit je zak. Het trilt in je hand en " +
                                  "projecteert in lichtgevende letters op de muur:\n";

            var enemies = new List<int>();

            for (int i = 0; i < path.Length; i++)
            {
                if (i > 0)
                {
                    Vertex previous = path[i - 1];
                    Vertex vertexI = path[i];

                    if (previous.rightEdge != null &&  previous.rightEdge.state != -1 && previous.rightEdge.rightVertex.number == vertexI.number)
                    {
                        returnString += "Oost - ";
                        enemies.Add(previous.rightEdge.level);
                    }
                    else if (previous.leftEdge != null && previous.leftEdge.state != -1 && previous.leftEdge.leftVertex.number == vertexI.number)
                    {
                        returnString += "West - ";
                        enemies.Add(previous.leftEdge.level);
                    }
                    else if (previous.upEdge != null && previous.upEdge.state != -1 && previous.upEdge.leftVertex.number == vertexI.number)
                    {
                        returnString += "Noord - ";
                        enemies.Add(previous.upEdge.level);
                    }
                    else
                    {
                        returnString += "Zuid - ";
                        enemies.Add(previous.downEdge.level);
                    }
                }
            }
            returnString = returnString.Substring(0, returnString.Length - 3);
            returnString += "\n";

            if (enemies.Count > 1)
            {
                returnString += enemies.Count + " tegenstanders (";
            }
            else
            {
                returnString += enemies.Count + " tegenstander (";
            }

            foreach (var enemy in enemies)
            {
                returnString += "level " + enemy + ", ";
            }

            returnString = returnString.Substring(0, returnString.Length - 2);
            returnString += ")\n";

            return returnString;
        }

        public void Grenade()
        {
            List<Edge> nearbyEdgesInMst = new List<Edge>();

            //Check all edges
            if (current.rightEdge != null)
            {
                //If in MST add to list
                if (Vertex.InSpanningTree(current.rightEdge, MST))
                    nearbyEdgesInMst.Add(current.rightEdge);
                //Else demolish edge
                else
                    current.rightEdge.state = -1;
            }
            if (current.leftEdge != null)
            {
                if (Vertex.InSpanningTree(current.leftEdge, MST))
                    nearbyEdgesInMst.Add(current.leftEdge);
                else
                    current.leftEdge.state = -1;
            }
            if (current.upEdge != null)
            {
                if (Vertex.InSpanningTree(current.upEdge, MST))
                    nearbyEdgesInMst.Add(current.upEdge);
                else
                    current.upEdge.state = -1;
            }
            if (current.downEdge != null)
            {
                if (Vertex.InSpanningTree(current.downEdge, MST))
                    nearbyEdgesInMst.Add(current.downEdge);
                else
                    current.downEdge.state = -1;
            }

            //Random edge in mst to zero
            Edge edgeToZero = nearbyEdgesInMst[RandomNumber(0, nearbyEdgesInMst.Count - 1)];
            edgeToZero.level = 0;
        }

        public void Nuke()
        {
            foreach (Edge edge in edges)
            {
                if (!Vertex.InSpanningTree(edge, MST))
                    edge.state = -1;
            }
        }

        public void Print(Vertex v)
        {
            Console.WriteLine(xRoomTop + "\n" + xRoomUp);
            v.Print(MST, dijkstra);
            Console.WriteLine("\n" + xRoomDown + "\n" + xRoomTop);

            if (v.downEdge != null)
            {
                Console.WriteLine(xEdges);
                v.PrintEdge(MST);
                Console.WriteLine("\n" + xEdges);
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
