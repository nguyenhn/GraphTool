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
			Graph g = new Graph(601);
			g.AddEdge(0, 1, 3);
			g.AddEdge(0, 1, 3);
			g.AddEdge(0, 1, 3);
			g.AddEdge(0, 2, 2);
			g.AddEdge(0, 3, 2);
			g.AddEdge(2, 0, 5);
			g.AddEdge(2, 0, 5);
			g.AddEdge(2, 0, 5);
			g.AddEdge(2, 0, 5);
			g.AddEdge(2, 1, 8);
			g.AddEdge(1, 3, 5);
			g.AddEdge(1, 3, 5);
			g.AddEdge(1, 3, 5);
			g.AddEdge(1, 3, 5);
			for (int i = 0; i < 599; i++)
			{
				Random rdm = new Random();
				g.AddEdge(i, i + 1, rdm.Next(1, 100));
				g.AddEdge(i, i + 1, rdm.Next(1, 100));
				g.AddEdge(i, i + 1, rdm.Next(1, 100));
				g.AddEdge(i + 1, i, rdm.Next(1, 100));
			}
			//string[] lines = System.IO.File.ReadAllLines(@"D:\WriteLines.txt");
			//foreach(var line in lines)
			//{
			//	var items = line.Split();
			//	g.AddEdge(int.Parse(items[0]), int.Parse(items[1]), double.Parse(items[2]));
			//}

			int s = 2, d = 500;
			g.PrintAllPaths(s, d);
			Console.WriteLine("Lisstttt");
			g.AllPathList.ForEach(x => Console.WriteLine(String.Join(" ",x)));
			Console.WriteLine("Custom lissstttttt");			
			foreach (var item in g.AllPathListWithDistance)
			{
				item.Item1.ForEach(i => Console.Write("{0}\t", i));
				Console.WriteLine();
				Console.WriteLine("Total cost = " + item.Item2);
				Console.WriteLine("----------------------------------");
			}
			Console.Read();
		}
	}
}
