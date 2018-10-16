using System;
using System.Collections.Generic;
using System.Text;

namespace ALGADungeon
{
    class Edge
    {
        // level 1-9 is enemy level
        public int level { get; set; }
        // 1 = alive, 0 = defeated, -1 = edge destroyed (no access)
        public int state { get; set; }
        public Vertex leftVertex { get; }
        public Vertex rightVertex { get; }

        public Edge(int level, Vertex leftVertex, Vertex rightVertex)
        {
            this.level = level;
            this.leftVertex = leftVertex;
            this.rightVertex = rightVertex;
            this.state = 1;
        }
    }
}
