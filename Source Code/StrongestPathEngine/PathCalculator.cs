using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrongestPathEngine
{
    public class PathCalculator
    {
        Graph graph;

        /// <summary>
        /// Calculate Strongest path for each target node in graph
        /// </summary>
        /// <returns>Dictionary for PAth weight for each target node</returns>
        public IDictionary<int, double> CalculateStrongestPath(Graph graphOj, int sourceNodeID, List<int> targets)
        {
            graph = graphOj;

            if (!graph.Nodes.Any(n => n.Key == sourceNodeID))
                throw new ArgumentException("Source node must be in graph.");

            targets.ForEach(t =>
            {
                if (!graph.Nodes.Any(n => n.Key == t))
                    throw new ArgumentException("Target nodes must be in graph.");
            });

            //Init graph nodes 
            InitialiseGraph(sourceNodeID);
            ProcessConnectedGraphToSource(sourceNodeID );
            ProcessGraph(sourceNodeID, targets);
            return ExtractDistances(graph, targets);
        }

        private void InitialiseGraph(int sourceNodeID)
        {
            foreach (Node node in graph.Nodes.Values)
            {
                //Set all node's path weight to be -NaN 
                node.PathWeight = double.NegativeInfinity;

                node.StrongestPath = "";
                node.StrongestPathNodes = new List<int>();
            }
            //Set Path weight for source node to 1 and path contains source node itself only
            graph.Nodes[sourceNodeID].PathWeight = 1;
            graph.Nodes[sourceNodeID].StrongestPath = graph.Nodes[sourceNodeID].Name;
        }


/// <summary>
/// Process all graph nodes to mark all nodes connected to source node to decress earsh space for stronegest path algorithm
/// </summary>
/// <param name="sourceNodeID"></param> 
        private void ProcessConnectedGraphToSource(int sourceNodeID )
        {


            bool finished = false;
            //Queue for nodes must be visited to calculate path weight
            graph.Nodes[sourceNodeID].IsConnected = true;

            var queue = graph.Nodes.Values.ToList();
 
            Console.WriteLine("Search for connected nodes to source node " + graph.Nodes[sourceNodeID].Name + " No. of nodes in gragh :"+ queue.Count);

            //Loop on all nodes until all nodes visited and path weight calculated
            while (true)
            {
                // Algorithm  start from source node which has isconnected = true then process neighbors nodes  
                Node nextNode = queue.FirstOrDefault(n => !n.IsChecked && n.IsConnected);
                if (nextNode == null)
                { 
                    var c2 = queue.Count(n => n.IsConnected);
                     Console.WriteLine("Finish Searching connected nodes to source. Find " + c2 + " connected nodes.");
                     break;
                }
                ProcessConnectNode(nextNode.ID, 0);
                nextNode.IsChecked = true;
                

            }
        }

        /// <summary>
        /// Process graph is graph traversal method which update path weight for all graph nodes starting from source node all 
        /// </summary>
        /// <param name="sourceNodeID"></param>
        /// <param name="targets"></param>
        // Traversal algorithm use modified breadth-first search to calculate path weight for each node
        /*  procedure SP(G, v):
         * create a queue Q
         * enqueue v onto Q
         * mark v
         * while Q is not empty:
         *     t ← MaxPathWeighNode(Q)
         *     for all edges e in G.adjacentEdges(t) do
         *         if e.Target.PathWeight < t.Weight * e.Weight
         *         e.Target.PathWeight = t.Weight * e.Weight
         *     dequeue t from Q
         */
        private void ProcessGraph(int sourceNodeID, List<int> targets)
        {
           

            bool finished = false;
            //Queue for nodes must be visited to calculate path weight
            
            var queue = graph.Nodes.Values.Where(n => n.IsConnected).ToList();
            int queueCount = 0 ;
            Console.WriteLine("Start calculating strongest path for " + queue.Count() + " nodes in graph");

            //Loop on all nodes until all nodes visited and path weight calculated
            while (queue.Any())
            {
                // Algorithm  start from source node which has max initial path weight in graph =1 then process node and calculate path weight for all source node neighbors 
                //Use parallel query to utilize multi threading in calculating max path weith in queue
                var maxWeight = queue.AsParallel().Max(u =>  u.PathWeight);
                Node nextNode = queue.FirstOrDefault(n => n.PathWeight == maxWeight);
                ProcessNode(nextNode.ID  );
                queue.Remove(nextNode);
                 

            }
        }

     
        /// <summary>
        /// Update path weight for each neighbors for current node
        /// </summary>
        /// <param name="nodeID"></param>
        private void ProcessNode(int nodeID)
        {
            Node node = graph.Nodes[nodeID];

             foreach (var connection in node.Connections)
            {

            

                double pathWeight = node.PathWeight * connection.Weight;
                Node nextNode = graph.Nodes[connection.Target.ID];

                //Update path weight for next node if calculated weight > current path weight &
                //this path isn't contain next node so no cycles will be in stongest path like A-> F : A->B->F->E->B->F
                if (pathWeight > nextNode.PathWeight && !node.StrongestPathNodes.Contains(nextNode.ID))
                {
                    nextNode.PathWeight = pathWeight;
                    //Strongest path for next node is strongest path for node + node
                    nextNode.StrongestPath = node.StrongestPath + " " + connection.Weight + " " + nextNode.Name;

                    var t = new List<int>();
                   t.AddRange(node.StrongestPathNodes);
                   t.Add(node.ID);
                    nextNode.StrongestPathNodes = t;

                }

            }
             

        }
        /// <summary>
        /// Mark all connnections to node connected if it connected to source then go to process all connection's nodes
        /// </summary>
        /// <param name="nodeID"></param>
        /// <param name="depth"></param>
        private void ProcessConnectNode(int nodeID , int depth)
        {
            Node node = graph.Nodes[nodeID];

            //limit max recursive calls to 10 to prevent stack overflow exception
            if (node.IsChecked || depth>10)
            { 
                return;
            }

            depth++;
            node.IsChecked = true;
             foreach (var item in node.Connections )
            {
                item.Target.IsConnected = true;
                if(!item.Target.IsChecked)
                    ProcessConnectNode(item.Target.ID , depth);
            }
              
        }

        /// <summary>
        /// Return Dictionary for all target nodes with it's path weoght to source
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="targets"></param>
        /// <returns></returns>
        private IDictionary<int, double> ExtractDistances(Graph graph, List<int> targets)
        {
            return graph.Nodes.Where(n => n.Value.StrongestPathNodes.Count > 0 && targets.Contains(n.Value.ID))
                .ToDictionary(n => n.Key, n => n.Value.PathWeight);
        }
    }
}
