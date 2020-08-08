using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace StrongestPathEngine
{
     public class Edge
    {
        public Node Target { get; private set; }
        public double Weight { get;   set; }

        public Edge(Node target, double weight)
        {
            Target = target;
            Weight = weight;
        }
    }
}
