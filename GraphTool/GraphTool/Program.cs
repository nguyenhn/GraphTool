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
			Graph g = new Graph(1700);
			//g.AddEdge(0, 1, 1);
			//g.AddEdge(0, 1, 7);
			//g.AddEdge(1, 2, 2);
			//g.AddEdge(1, 2, 8);
			//g.AddEdge(2, 3, 3);
			//g.AddEdge(1, 3, 9);
			//g.AddEdge(3, 4, 4);
			//g.AddEdge(4, 5, 5);
			//g.AddEdge(5, 0, 6);
			string[] lines = System.IO.File.ReadAllLines(@"D:\test.txt");
			foreach (var line in lines)
			{
				var items = line.Split();
				g.AddEdge(int.Parse(items[0]), int.Parse(items[1]), int.Parse(items[2]), int.Parse(items[3]));
			}

			int s = 3, d = 6;
            g.AddEdge(s, 4, 1690, -1);
            g.AddEdge(4, s, 1691, -2);
            g.AddEdge(5, d, 1692, -3);
            g.AddEdge(d, 5, 1693, -4);
            //g.AddEdge(s, 85, 1690, -1);
            //g.AddEdge(85, s, 1691, -2);
            //g.AddEdge(102, d, 1692, -3);
            //g.AddEdge(d, 102, 1693, -4);
            g.PrintAllPaths(s, d);
			//var test = g.BFSAllPath;
			
			// Console.WriteLine("Lisstttt");
			// g.AllPathList.ForEach(x => Console.WriteLine(String.Join(" ",x)));
			// Console.WriteLine("Custom lissstttttt");			
			// foreach (var item in g.AllPathListWithDistance)
			// {
			// 	item.Item1.ForEach(i => Console.Write("{0}\t", i));
			// 	Console.WriteLine();
			// 	Console.WriteLine("Total cost = " + item.Item2);
			// 	Console.WriteLine("----------------------------------");
			// }
			Console.Read();




			// for (int i = 0; i < 599; i++)
			// {
			// 	Random rdm = new Random();
			// 	g.AddEdge(i, i + 1, rdm.Next(1, 100));
			// 	g.AddEdge(i, i + 1, rdm.Next(1, 100));
			// 	g.AddEdge(i, i + 1, rdm.Next(1, 100));
			// 	g.AddEdge(i + 1, i, rdm.Next(1, 100));
			// }
			
		}
	}
}
