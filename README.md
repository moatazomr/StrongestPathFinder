# StrongestPathFinder
This program used to solve 1-to-N Strongest Path Problem by calculating strongest path between source and target nodes in a graph

Problem Description

Let G(V;E) be an undirected graph where V is a set of nodes and E a set of
edges. Each edge e 2 E is associated with a weight w, 0 < w < 1. We define
the weight of a path (n1; n2; ..... ; nk) as w1 * w2 *  .... *  wk-1, where wi is the
weight of the edge between ni and ni+1.
Given a node n (called source node) and a set of nodes fn1........ nkg (called
target nodes), 

 program will find the strongest path by using modified algorithm of Dijkstra's Shortest Path
from the source node to each of the target nodes.


 Inputs and output
program uses three parameters:

Graph File. A text file that contains the data for the graph G. Each line of
the file corresponds to an edge, with the format
node1 node2 weight
node1 and node2 are the names (of type string) of the nodes. For ex-
ample, one line may be
Alice Bob 0.667

Source and Target File. A text file that lists names of the source node and
the target nodes. The format of the file is as follows:
source node
target node1
target node2
...
target nodek
That is, the first line is the name of source node, the second line is empty,
and each of the remaining lines is the name of a target node. Here, we
1
assume that the number of target nodes is much smaller than the total
number of nodes in the graph.

Output File. A text file that contains the result of your program. Each line
corresponds to the strongest path from the source node to a target node,
with the following format:
source node target node 1 path weight: source node w1 node1 w2
node2 ...wk target node
Here path weight is the weight of the strongest path from the source node
to the target node. w1 is the edge weight between the source node and
node1, w2 is the edge weight between node1 and node2, etc. If there are
multiple strongest paths between the source and a target, just output one
of them.

In summary, to run your program, it would be like:
your program graph.txt source target.txt output.txt


System requirements:
- Windows OS
-.Net framwork 4.0 or higher

Running application:
 Run StrongestPathFinder.exe in bin folder with 3 paramters for praph file path , source_target file path and output file path 
 ex: 
 StrongestPathFinder.exe graph.txt source_target.txt output.txt


Build from source:
Need Visual Studio 2015 and run  StrongestPathFinder.sln file then build solution
There are 2 sub projects:
1- StrongestPathEngine : It contains graph data structure in calsses and calculator class which calculate strongest path on graph
2- StrongestPathFinder : It executable program to run strongestpath engine on files