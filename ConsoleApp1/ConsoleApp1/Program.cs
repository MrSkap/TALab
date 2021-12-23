using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AutomatLab
{
	
	class Program
	{
		static string path = Directory.GetCurrentDirectory() + @"\testProg.txt";
		
		static void Main(string[] args)
		{
			FileStream testProg;
			if (File.Exists(path)) testProg = File.OpenRead(path);
			else testProg = File.Create(path);
			try
			{
				Analyzer textAnalyzer = new Analyzer();
				List<Lexeme> buff = textAnalyzer.GetData();

				Console.WriteLine(testProg.Length);
			}
			catch
			{

			}
		}
	}
}
