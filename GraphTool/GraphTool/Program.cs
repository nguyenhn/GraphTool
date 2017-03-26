using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTool
{
	class Program
	{
		public static int A;
		public static void Test(int a)
		{
			a++;
		}		
		static void Main(string[] args)
		{
			A = 10;
			Console.WriteLine(A);
			Console.ReadKey();
		}
	}
}
