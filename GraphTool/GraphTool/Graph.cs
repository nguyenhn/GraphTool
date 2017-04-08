using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTool
{
	//Custom Node for adjacency list: graph with weight or many different edge types
	/// <summary>
	/// Adjs[i] = All edges form nod i
	/// </summary>
	public class Arc
	{
		public int Des { get; set; } //Link to destination vertex
		public string Type { get; set; }
		public double Weight { get; set; }
		public int Label { get; set; }
		public Arc(int d, int label)
		{
			this.Des = d;
			this.Label = label;
		}
		public Arc(int d, double weight)
		{
			this.Des = d;
			this.Weight = weight;
		}
		public Arc(int d, double weight, string type)
		{
			this.Des = d;
			this.Weight = weight;
			this.Type = type;
		}
	}

	public class Node
	{
		public int Id { get; set; }
		public int Name { get; set; }
	}
	public class Edge
	{
		public int Id { get; set; }
		public int StartNode { get; set; }
		public int EndNode { get; set; }
		public int Cost { get; set; }
		public int Label { get; set; }
		public Edge(int startNode, int endNode, int id)
		{
			this.Id = id;
			this.StartNode = startNode;
			this.EndNode = endNode;
		}
		public Edge(int startNode, int endNode, int id, int label)
		{
			this.Id = id;
			this.StartNode = startNode;
			this.EndNode = endNode;
			this.Label = label;
		}
	}
	// A directed graph using adjacency list representation
	class Graph
	{
		#region Public Attributes		
		public List<List<int>> AllPathList { get; set; }
		public List<Tuple<List<int>, double>> AllPathListWithDistance { get; set; }

		#endregion
		int V; //Total Vertex of Graph
		List<int>[] Adjs; // Array of adjacency list
		List<Arc>[] CustomAdjs; /// Adjs[i] = All edges form node i
		List<Edge>[] EdgeAdjs; // by Id of edge
		List<Edge> EdgeList;
		Queue<int> mainQueue;
		Stack<int> mainStack;
		List<Arc>[] PreTrack;

		private void Init()
		{
			Adjs = new List<int>[V];
			CustomAdjs = new List<Arc>[V];
			EdgeList = new List<Edge>();
			EdgeAdjs = new List<Edge>[V];
			//Initial all Adjs list for each vertex
			AllPathList = new List<List<int>>();
			AllPathListWithDistance = new List<Tuple<List<int>, double>>();
			mainQueue = new Queue<int>();
			mainStack = new Stack<int>();
		}
		#region public					
		public Graph(int v)
		{
			this.V = v;
			Init();
			for (int i = 0; i < Adjs.Length; i++)
			{
				Adjs[i] = new List<int>();
				CustomAdjs[i] = new List<Arc>();
				EdgeAdjs[i] = new List<Edge>(); 
			}
		}
		
		public void AddEdge(int u, int v, int Id)
		{
			double idd = Id;
			Arc newEdge1 = new Arc(v, idd);
			CustomAdjs[u].Add(newEdge1);
			Edge newEdge = new Edge(u, v, Id);
			this.EdgeList.Add(newEdge);
			this.EdgeList.OrderBy(x => x.Id);
			if (EdgeAdjs[u]==null) EdgeAdjs[u] = new List<Edge>();
			this.EdgeAdjs[u].Add(newEdge);
		}
		public void AddEdge(int u, int v, int Id, int label)
		{
			double idd = Id;
			Arc newEdge1 = new Arc(v, idd);
			CustomAdjs[u].Add(newEdge1);
			Edge newEdge = new Edge(u, v, Id, label);
			this.EdgeList.Add(newEdge);
			this.EdgeList.OrderBy(x => x.Id);
			if (EdgeAdjs[u] == null) EdgeAdjs[u] = new List<Edge>();
			this.EdgeAdjs[u].Add(newEdge);
		}

		//public void AddEdge(int u, int v, double w)
		//{
		//	Arc newEdge = new Arc(v, w);
		//	CustomAdjs[u].Add(newEdge);
		//}

		//Print all Paths from source to destination
		public void PrintAllPaths(int s, int d)
		{
			//Mark all vertives not visited
				
			//Call Graph Travesal algorithm
			//DepthFirstSearch(s,d,visited,path,pathIndex);
			//BFSPath(s, d);
			//EdgesBFSPath(s, d);
			Console.WriteLine("-------------------------------------------------------------");
			BFS(s, d);
            RecursivePath(s,d,new bool[2000], new int[2000],0);
            //DFSAllPath(s, d);

        }

		#endregion
		#region private
		//Breath First Search		
		public List<int>[] Previous { get; set; }
		List<int[]> AllStackPath;
		bool pathFound;
		void EdgesBFSPath(int s, int d)
		{
			//Init new Graph on Path
			//bool[] visitedEdge = new bool[this.EdgeList.Max(x => x.Id) + 1];
			
			bool[] visitedEdge = new bool[1700];
			var startEdges = EdgeList.Where(x => x.StartNode == s);
			var endEdges = EdgeList.Where(x => x.EndNode == d);
			if ((startEdges == null)|| (endEdges == null)) return;
			foreach (var startEdge in startEdges)
			{
				Queue<List<Edge>> pathQueue = new Queue<List<Edge>>();
				List<Edge> tempPath = new List<Edge>();
				tempPath.Add(startEdge);
				pathQueue.Enqueue(tempPath);				
				Edge lastEdge;
				int pathLenght = 1;
				int currentLabel = startEdge.Label;
				List<Edge> newPath;
				while (pathQueue.Count > 0)
				{
					List<Edge> lastPath = pathQueue.Last();					
					tempPath = pathQueue.Dequeue();					
					//if (tempPath.Count < lastPath.Count) continue;
					lastEdge = tempPath.Last();
					visitedEdge[lastEdge.Id] = true;
					currentLabel = lastEdge.Label;
					if (currentLabel < 0)
					{
						visitedEdge = new bool[1700];
					}
					PrintTempPath(tempPath);
					if (lastEdge.EndNode == d)
					{
						Console.WriteLine("--------------------------------------------------");
						Console.WriteLine("good path ");
						PrintTempPath(tempPath);
						this.pathFound = true;
					}
					else
					{
						var nextEdgeChecks = EdgeAdjs[lastEdge.EndNode].Where(x => x.Label == currentLabel).ToList();
						if (nextEdgeChecks.Count>0)
						{
							foreach (var nextEdgeCheck in nextEdgeChecks)
							{
								if (visitedEdge[nextEdgeCheck.Id]) continue;
								if (tempPath.Contains(nextEdgeCheck)) continue;
								visitedEdge[nextEdgeCheck.Id] = true;
								newPath = new List<Edge>(tempPath);
								newPath.Add(nextEdgeCheck);
								pathLenght++;								
								pathQueue.Enqueue(newPath);
							}
							
						}
						else
						{
							foreach (var adjEdge in EdgeAdjs[lastEdge.EndNode])
							{
								if (tempPath.Contains(adjEdge)) continue;
								if (!visitedEdge[adjEdge.Id])
								{
									visitedEdge[adjEdge.Id] = true;
									newPath = new List<Edge>(tempPath);
									newPath.Add(adjEdge);
									pathLenght++;
									pathQueue.Enqueue(newPath);
								}
							}
						}						
					}
				}				
			}
			//if (!this.pathFound) EdgesBFSPath(s, 102);

		}

		void BFSPath(int s, int d)
		{
			//Init new Graph on Path
			Queue<List<int>> pathQueue = new Queue<List<int>>();
			Previous = new List<int>[V];
			List<int> tempPath = new List<int>();
			tempPath.Add(s);
			pathQueue.Enqueue(tempPath);
			int lastNode;
			while (pathQueue.Count > 0)
			{
				tempPath = pathQueue.Dequeue();
				lastNode = tempPath.Last();
				PrintTempPath(tempPath);
				if (lastNode == d)
				{
					Console.Write("--------------------------------------------------");
					Console.Write("good path ");
					PrintTempPath(tempPath);
				}
				else
				{
					foreach (var adjEdge in EdgeAdjs[lastNode])
					{
						int adjNode = adjEdge.EndNode;
						if (!tempPath.Contains(adjNode))
						{
							List<int> newPath = new List<int>(tempPath);
							newPath.Add(adjNode);
							pathQueue.Enqueue(newPath);
						}
					}
				}
			}

		}

		private void PrintTempPath(List<Edge> tempPath)
		{
			Console.Write(tempPath[0].StartNode + ":" + tempPath[0].Label);
			foreach (var item in tempPath)
			{
				Console.Write(" -> {0}:{1} ", item.EndNode, item.Label);
				//Console.Write("[{0},{1}]:{2} -> ", item.StartNode, item.EndNode, item.Id);
				//Console.Write(" -> " + item.EndNode);
			}
			Console.WriteLine();
		}

		private void PrintTempPath(List<int> tempPath)
		{
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
			foreach (var item in tempPath)
			{
				Console.Write(item + "-> ");
			}
			Console.WriteLine();
		}

		void BFS(int u, int v)
		{
			AllStackPath = new List<int[]>();
			Previous = new List<int>[V];
			//List<int>[] path = new List<int>[V];
			bool[] visited = new bool[V];
			visited[u] = true;
			mainQueue.Enqueue(u);
			int pathLenght = 0;
			while (mainQueue.Count > 0)
			{
				int current = mainQueue.Dequeue();
				pathLenght++;
				foreach (var adjEdge in CustomAdjs[current])
				{
					int adjNode = adjEdge.Des;
					if (Previous[adjNode] == null) Previous[adjNode] = new List<int>();
                    if(!Previous[adjNode].Contains(current)) Previous[adjNode].Add(current);
                    if (!visited[adjNode])
                    {
                        //mark visited this vertice
                        visited[adjNode] = true;                       
                        mainQueue.Enqueue(adjNode);
                    }
                }

			}
			if (!visited[v])
			{
				Console.WriteLine("There is no path to " + v);
				return;
			}

			foreach (var item in PathTo(u,v,visited,Previous))
			{
				Console.Write(item + ":"+ this.EdgeAdjs[item][0].Label + " -> ");
			}
		}

        private void RecursivePath(int s, int d, bool[] visited, int[] path, int pathIndex)
        {
            //Mark visited current vertex (u)
            visited[d] = true;
            path[pathIndex] = d;
            pathIndex++;
            // If current vertex is the destination, then print current path[]
            if (s == d)
            {
                PrintPath(path,pathIndex);
                //PrintPathToList(path, pathIndex);
                //PrintPathAndDistanceToList(path, pathIndex);
            }
            else //Current vertex is not the destination then recursive visite all adjacence vertices to this current vertex
            {
                foreach (var node in Previous[d])
                {
                    if (!visited[node])
                    {
                        RecursivePath(s, node, visited, path, pathIndex);
                    }
                }
            }
            //Remove current vertex from path and mark it as unvisited for another path
            pathIndex--;
            visited[d] = false;
        }

        private void DFSAllPath(int s, int d)
        {
            Stack<int> stack = new Stack<int>();
            List<int> path = new List<int>();
            bool[] visited = new bool[2000];
            
            stack.Push(s);
            while (stack.Count > 0)
            {
                int current = stack.Pop();
                visited[current] = true;
                path.Add(current);
                if (current == d)
                {
                    PrintTempPath(path);
                    foreach(var node in path)
                    {
                        visited[node] = false;
                    }
                }
                foreach (var parentOfcurrent in Previous[current])
                {
                    if (!visited[parentOfcurrent])
                    {
                        stack.Push(parentOfcurrent);
                        //visited[parentOfcurrent] = true;
                    }
                }
            }
        }

		private void tryPathDFS(int s,int d)
		{
            mainStack = new Stack<int>();
            Stack<int> path = new Stack<int>();
            bool[] visitedEdge = new bool[1700];
            var startEdges = EdgeList.Where(x => x.StartNode == s).ToList();
            var endEdges = EdgeList.Where(x => x.EndNode == d && Previous[d].Contains(x.StartNode)).ToList();
            if ((startEdges == null) || (endEdges == null)) return;
            foreach (var edge in startEdges)
            {
                mainStack.Push(edge.Id);                
            }           
            int current;
            while (mainStack.Count>0)
            {
                current = mainStack.Pop();
                visitedEdge[current] = true;
                Edge currentEdge = EdgeList.FirstOrDefault(x => x.Id == current);
                if (startEdges.Select(x=>x.Id).Contains(current))
                {
                    PrintTempPathStack(path);
                }
                else
                {
                    foreach (var vertice in Previous[currentEdge.EndNode])
                    {
                        var adjEdge = this.EdgeAdjs[currentEdge.StartNode].FirstOrDefault(x => x.StartNode == vertice);
                        if (adjEdge != null) mainStack.Push(adjEdge.Id);
                    }
                }
            }
		}

        private void PrintTempPathStack(Stack<int> path)
        {
            if (path.Count == 0) return;
            Console.WriteLine();
            while (path.Count > 0)
            {
                Console.Write(path.Pop() + " -> ");
            }
        }

        public bool HasPathTo(bool[] visited, int v)
		{
			return visited[v];
		}
		public IEnumerable<int> PathTo(int s, int v, bool[] visited, List<int>[] path)
		{
			if (!HasPathTo(visited, v)) return null;
			Stack<int> stackpath = new Stack<int>();
			for (int x = v; x != s; x = path[x][0])
				stackpath.Push(x);
			stackpath.Push(s);
			return stackpath;
		}


		//Depth First Search
	

		void DepthFirstSearch(int u, int d, bool[] visited, int[] path, int pathIndex)
		{
			//Mark visited current vertex (u)
			visited[u] = true;
			path[pathIndex] = u;
			pathIndex++;
			// If current vertex is the destination, then print current path[]
			if (u == d)
			{
				//PrintPath(path,pathIndex);
				PrintPathToList(path, pathIndex);
				PrintPathAndDistanceToList(path, pathIndex);
			}
			else //Current vertex is not the destination then recursive visite all adjacence vertices to this current vertex
			{
				foreach (var arc in CustomAdjs[u])
				{
					int adjVertex = arc.Des;
					if (!visited[adjVertex])
					{
						DepthFirstSearch(adjVertex, d, visited, path, pathIndex);
					}
				}
			}
			//Remove current vertex from path and mark it as unvisited for another path
			pathIndex--;
			visited[u] = false;
		}
		private void PrintPathToList(int[] path, int pathIndex)
		{
			this.AllPathList.Add(path.Take(pathIndex).ToList());
		}
		private void PrintPathAndDistanceToList(int[] path, int pathIndex)
		{
			double sum = 0;
			for (int i = 0; i < pathIndex - 1; i++)
			{
				int currentVertex = path[i];
				int nextVertex = path[i + 1];
				//Reminde CustomAdjs[currentVertex] list all adjacency of currentVertex
				sum += CustomAdjs[currentVertex].Find(x => x.Des == nextVertex).Weight;
			}
			Tuple<List<int>, double> newTuple = new Tuple<List<int>, double>(path.Take(pathIndex).ToList(), sum);
			this.AllPathListWithDistance.Add(newTuple);
		}
		private void PrintPath(int[] path, int pathIndex)
		{
            List<int> result = new List<int>();
            for (int i = pathIndex-1; i >= 0; i--)
            {
                result.Add(path[i]);
            }
            Console.WriteLine();
            Console.WriteLine("---------Rescusive-----------------------------------------------------");            
            foreach (var item in result)
            {
                Console.Write(item + " -> ");
            }			
			Console.WriteLine();
		}
		#endregion

	}
}

