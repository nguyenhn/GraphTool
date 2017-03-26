using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTool
{
	class Program
	{
		static void Main(string[] args)
		{
			Graph g = new Graph(4);
			g.AddEdge(0, 1);
			g.AddEdge(0, 2);
			g.AddEdge(0, 3);
			g.AddEdge(2, 0);
			g.AddEdge(2, 1);
			g.AddEdge(1, 3);
			int s = 2, d = 3;
			g.PrintAllPaths(s, d);		

			Console.WriteLine("Lisstttt");
			g.AllPathList.ForEach(x => Console.WriteLine(String.Join(" ",x)));
			Console.Read();
		}
	}
}
