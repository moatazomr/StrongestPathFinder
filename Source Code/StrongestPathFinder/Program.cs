using StrongestPathEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrongestPathFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

           
            if (args.Length < 3)
                throw new ArgumentException("Missing input paramters to run. example: StrongestPathFinder.exe graph.txt source target.txt output.txt");

            Graph graph = new StrongestPathEngine.Graph();
            //Load graph file to graph object be creating nodes and edges between nodes
            DataManager.LoadGraphFile(graph, args[0].ToString());


            Node sourceNode = null;
            List<Node> TargetQueues = new List<Node>();
            //load sorce Target file to sourceNode and Target Node list
            sourceNode = DataManager.LoadSourceTargetFile(graph, args[1].ToString(), TargetQueues);

            PathCalculator calc = new PathCalculator();

            
            DateTime startTime =  DateTime.Now;//Stopwatch to calculate time elapsed in calculating graph strongest paths
            //Calcaulate Stronegst path for between source node to each targett node
            var TargetsPathWeight = calc.CalculateStrongestPath(graph, sourceNode.ID, TargetQueues.Select(r => r.ID).ToList());

            Console.WriteLine("Finish calculating Strongest Path for Targets in " + DateTime.Now.Subtract(startTime).TotalMilliseconds + " ms.");

            //Write output file with target nodes strongest path details
            DataManager.SaveOutputFile(graph, args[2].ToString(), sourceNode, TargetsPathWeight);
            Console.WriteLine("Finish Writing Output file");
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error while processing data : " + ex.Message);
            }
        }

    }

    }

