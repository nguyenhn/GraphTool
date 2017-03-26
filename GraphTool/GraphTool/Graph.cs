using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTool
{
	// A directed graph using adjacency list representation
	class Graph
	{
		#region Public Attributes
		public List<List<int>> AllPathList { get; set; }
		#endregion
		int V; //Total Vertex of Graph
		List<int>[] Adjs; // Array of adjacency list
		#region public		
		public Graph (int v)
		{
			this.V = v;
			Adjs = new List<int>[V];
			//Initial all Adjs list for each vertex
			AllPathList = new List<List<int>>();

			for (int i = 0; i < Adjs.Length; i++)
			{
				Adjs[i] = new List<int>();
			}
		}

		public void AddEdge(int u, int v)
		{
			Adjs[u].Add(v);
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
			DepthFirstSearch(s,d,visited,path,ref pathIndex);

		}

		//Depth First Search
		void DepthFirstSearch(int u,int d, bool[] visited, int[] path,ref int pathIndex)
		{
			//Mark visited current vertex (u)
			visited[u] = true;
			path[pathIndex] = u;
			pathIndex++;
			// If current vertex is the destination, then print current path[]
			if (u == d)
			{
				PrintPath(path,pathIndex);
				PrintPathToList(path,pathIndex);
			}
			else //Current vertex is not the destination then recursive visite all adjacence vertices to this current vertex
			{
				foreach (int i in Adjs[u])
				{
					if (!visited[i])
					{
						DepthFirstSearch(i, d, visited, path, ref pathIndex);
					}
				}
			}
			//Remove current vertex from path and mark it as unvisited for another path
			pathIndex--;
			visited[u] = false;			
		}
		#endregion
		#region private
		private void PrintPathToList(int[] path, int pathIndex)
		{
			this.AllPathList.Add(path.Take(pathIndex).ToList());
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
