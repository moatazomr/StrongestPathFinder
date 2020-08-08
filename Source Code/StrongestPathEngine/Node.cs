using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace StrongestPathEngine
{
     public class Node
    {
        IList<Edge> _connections;
      
        public string Name { get; private set; }

        /// <summary>
        /// Strongest path between source node to Tagert as :
        /// source node target node 1 path weight: source node w1 node1 w2  node2...wk target node
        /// </summary>
        public string  StrongestPath { get; set; }

        public IEnumerable<Edge> Connections
        {
            get { return _connections; }
        }

        /// <summary>
        /// List of nodes between source and current node on strongest path
        /// This list is updated on changes in strongest path calculation
        /// </summary>
        public List<int> StrongestPathNodes
        {
            get; set;
        }

        /// <summary>
        /// ID is key for node genererated form hash code for node name to be used as key to identiy node in graph 

        /// </summary>
        public int ID { get;   set; }

        public double PathWeight { get;  set; }
        public bool IsChecked { get; internal set; }
        public bool IsConnected { get; internal set; }

        public Node(string name)
        {
            Name = name;
            //Generate ID for node by hasing node name
            ID = (name).GetHashCode();
            _connections = new List<Edge>();
        }


        /// <summary>
        /// Generate ID as hash for node name Based on hasshCode implementation it will not duplicated for nodes with different names
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int GetID(string name)
        {
          return (name).GetHashCode();
            
        }

         public void AddEdge(Node node2, double weight , bool twoWay = true  )
        {
            if (node2 == null) throw new ArgumentNullException("targetNode can't be null");
            if (node2 == this) throw new ArgumentException("Node can't be connected to itself.");
            if (weight <= 0) throw new ArgumentException("Weight must be positive.");

            //if (_connections.Any(t => t.Target.ID == node2.ID))
            //{
            //    var n = _connections.First(t => t.Target.ID == node2.ID);
            //    if (n.Weight < weight)
            //        n.Weight = weight;
            //    return;
            //}
            //else
            //By default add edge as 2 way direction
              _connections.Add(new Edge(node2, weight));
            //Add reversed connection between node2 to node1
            if (twoWay)
                node2.AddEdge(this, weight , false);
        }
    }
}
