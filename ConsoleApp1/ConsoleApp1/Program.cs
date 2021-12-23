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
			string testProgInString = new StreamReader(testProg).ReadToEnd();
			try
			{
				Analyzer textAnalyzer = new Analyzer(testProgInString);
				List<Lexeme> buff = textAnalyzer.GetData();

				Console.WriteLine(testProg.Length);
			}
			catch (Exception ex)
			{
				TextWriter errorWriter = Console.Error;
				errorWriter.WriteLine(ex.Message);
			}
		}
	}
}
