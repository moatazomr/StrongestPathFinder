using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace StrongestPathEngine
{
    public class Graph
    {
        /// <summary>
        /// Dictionary for nodes in graph with key is node ID as integer to speedup quering nodes operations .
        /// ConcurrentDictionary is used to support multithreading accessing within graph traversal algorithm
        /// </summary>
        public ConcurrentDictionary<int, Node> Nodes { get; private set; }

        public Graph()
        {
            Nodes = new ConcurrentDictionary<int, Node>();
        }

        public void AddNode(string name)
        {
            var node = new Node(name)  ;
            
            Nodes.AddOrUpdate(node.ID, node,(i,oldNode)=> node);
        }
        public void AddNode(Node node)
        {
             
            Nodes.AddOrUpdate(node.ID, node, (i, oldNode) => node);
        }

        public void AddEdge(int fromNode, int toNode, double  weight)
        {
            Nodes[fromNode].AddEdge(Nodes[toNode], weight);
        }
    }
}
