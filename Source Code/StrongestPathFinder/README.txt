This program used to calculate strongest path between source and target nodes in a graph

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