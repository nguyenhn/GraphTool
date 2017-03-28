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

	// A directed graph using adjacency list representation
	class Graph
	{
		#region Public Attributes		
		public List<List<int>> AllPathList { get; set; }
		public List<Tuple<List<int>,double>> AllPathListWithDistance { get; set; }

		#endregion		
		int V; //Total Vertex of Graph
		List<int>[] Adjs; // Array of adjacency list
		List<Arc>[] CustomAdjs; /// Adjs[i] = All edges form node i
		Queue<int> mainQueue;
		Stack<int> mainStack;
		private void Init()
		{
			Adjs = new List<int>[V];
			CustomAdjs = new List<Arc>[V];
			//Initial all Adjs list for each vertex
			AllPathList = new List<List<int>>();
			AllPathListWithDistance = new List<Tuple<List<int>, double>>();
			mainQueue = new Queue<int>();
			mainStack = new Stack<int>();
		}
		#region public					
		public Graph (int v)
		{
			this.V = v;
			Init();
			for (int i = 0; i < Adjs.Length; i++)
			{
				Adjs[i] = new List<int>();
				CustomAdjs[i] = new List<Arc>();
			}
		}

		public void AddEdge(int u, int v)
		{
			Adjs[u].Add(v);			
		}

		public void AddEdge(int u, int v, double w)
		{
			Arc newEdge = new Arc(v, w);
			CustomAdjs[u].Add(newEdge);
		}

		//Print all Paths from source to destination
		public void PrintAllPaths(int s, int d)
		{
			//Mark all vertives not visited
			bool[] visited = new bool[V];
			//Path tracking
			int[] path = new int[V];
			int pathIndex = 0;
			//Call Graph Travesal algorithm
			DepthFirstSearch(s,d,visited,path,pathIndex);
		}

		#endregion
		#region private
		//Breath First Search		
		void BFS(int s,bool[] visited, int[] path, int pathIndex){

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
			for (int i = 0;i< pathIndex-1; i++)
			{
				int currentVertex = path[i];
				int nextVertex = path[i + 1];
				//Reminde CustomAdjs[currentVertex] list all adjacency of currentVertex
				sum += CustomAdjs[currentVertex].Find(x=>x.Des == nextVertex).Weight;
			}
			Tuple<List<int>, double> newTuple = new Tuple<List<int>, double>(path.Take(pathIndex).ToList(), sum);
			this.AllPathListWithDistance.Add(newTuple);
		}
		private void PrintPath(int[] path, int pathIndex)
		{
			for (int i = 0; i < pathIndex; i++)
			{
				Console.Write(path[i] + " ");
			}
			Console.WriteLine();
		}
		#endregion

	}
}
