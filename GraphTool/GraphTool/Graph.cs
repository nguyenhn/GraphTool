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
		int V; //Total Vertex of Graph
		List<int>[] Adjs; // Array of adjacency list
		#region public		
		public Graph (int v)
		{
			this.V = v;
			Adjs = new List<int>[V];
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
		void DepthFirstSearch(int u,int v, bool[] visited, int[] path,ref int pathIndex)
		{
			//Mark visited current vertex (u)
			visited[u] = true;
			path[pathIndex] = u;
			pathIndex++;
			// If current vertex is same as destination, then print current path[]
			
		}
		#endregion
		#region private

		#endregion

	}
}
