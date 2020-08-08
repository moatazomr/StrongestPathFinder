using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrongestPathEngine
{
   public  class DataManager
    {
        /// <summary>
        /// Default buffer size for read line operations from graph or souce target files
        /// </summary>
        const Int32 BufferSize = 1024;
        /// <summary>
        /// Load graph file which contains connections between graph nodes to graph object
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="filePath"></param>
        public  static void LoadGraphFile(Graph graph ,string filePath)
        {
            using (var fileStream = File.OpenRead(filePath))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                int lineCount = 0;
                while ((line = streamReader.ReadLine()) != null)
                // Process line
                {
                    var data = line.Split('\t');
                    if (data.Length != 3)
                        throw new ArgumentException("Incorrect data in line" + lineCount);
                    string node1 = data[0];
                    string node2 = data[1];
                    double weight = -1;
                    if( !double.TryParse(data[2] , out weight))
                        throw new ArgumentException("Incorrect weight in line" + lineCount);

                    Node soruce = new Node(node1);
                    Node target = new Node(node2);


                    if (!graph.Nodes.ContainsKey(soruce.ID))
                        graph.AddNode(soruce);
                    else
                        soruce = graph.Nodes[soruce.ID];

                    if (!graph.Nodes.ContainsKey(target.ID))
                        graph.AddNode(target);
                    else
                        target = graph.Nodes[target.ID];


                    soruce.AddEdge(target, weight);
                    lineCount++;
                }
            }
        }
        /// <summary>
        /// load sorce Target file to sourceNode and Target Node list
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="filePath"></param>
        /// <param name="TargetQueues"></param>
        /// <returns></returns>
        public static Node LoadSourceTargetFile(Graph graph, string filePath ,  List<Node> TargetQueues)
        {
            Node src;
            using (var fileStream = File.OpenRead(filePath ))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                String line;
                line = streamReader.ReadLine();
                if (string.IsNullOrEmpty(line.Trim()))
                    throw new ArgumentException("Empty line instead of source node");

                src = new Node(line);
                if (!graph.Nodes.ContainsKey(src.ID))
                    throw new ArgumentException("The sourceNode " + line + "is not exist in graph");
                else
                    src = graph.Nodes[src.ID];

                line = streamReader.ReadLine();

                if (!string.IsNullOrEmpty(line.Trim()))
                    throw new ArgumentException("No empty line between source and target nodes");

                while ((line = streamReader.ReadLine()) != null)
                // Process line
                {
                    {
                        if (string.IsNullOrEmpty(line.Trim()))
                            throw new ArgumentException("Empty line instead of target node");

                        else
                        {
                            Node target = new Node(line);
                            if (!graph.Nodes.ContainsKey(target.ID))
                                throw new ArgumentException("Target Node " + line + "is not exist in graph");
                            else
                                TargetQueues.Add(target);


                        }
                    }


                }
            }

            return src;
        }


        public static void SaveOutputFile(Graph graph, string filePath, Node src, IDictionary<int, double> TargetsPathWeight)
        {

            using (var fileStream = File.Open(filePath, FileMode.Create))
            using (var streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
            {


                foreach (var item in TargetsPathWeight)
                {
                    var n = graph.Nodes[item.Key];
                    string l = src.Name + " " + n.Name + " " + n.PathWeight + ": ";
                    l += n.StrongestPath; 
                    streamWriter.WriteLine(l);

                }

            }
        }
    }
}
