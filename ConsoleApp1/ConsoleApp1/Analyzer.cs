using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatLab
{
	class Analyzer
	{
		string program_text;
		int current_index;
		DebugInfo current_info;
		List<Lexeme> data;
		public Analyzer(string program_Text)
		{
			program_text = program_Text;
		}
		public void Run()
		{
			Lexeme cur_lexeme = new Lexeme();

			while (current_index < program_text.Length)
			{
				while (current_index < program_text.Length && Char.IsWhiteSpace(program_text[current_index]))
				{
					switch (program_text[current_index++])
					{
						case ' ':
							++current_info.pos;
							break;
						case '\n':
							++current_info.line; current_info.pos = 1;
							break;
					}
				}

				if (current_index >= program_text.Length)
				{
					break;
				}

				cur_lexeme = NextLexeme();
				cur_lexeme.debugInfo = current_info;

				if (cur_lexeme.lexemeType == LexemeType.Error)
				{
					string msg = "Analyzer error; Unknown char with value = '" + cur_lexeme.value + "';";
					throw new Exception(msg + '\n' + cur_lexeme.debugInfo);
				}

				current_info.pos += cur_lexeme.value.Length;
				data.Add(cur_lexeme);
			}

			cur_lexeme.lexemeType = LexemeType.Finish;
			cur_lexeme.debugInfo = current_info;
			data.Add(cur_lexeme);
		}
		public List<Lexeme>GetData()
		{
			return data;
		}
		Lexeme NextLexeme()
		{
			char cur_ch = program_text[current_index];
			current_index++;

			Lexeme result = new Lexeme();
			result.lexemeType = LexemeType.Error;
			result.value = cur_ch.ToString();

			if (IsLetterOrNotMeaningSybols(cur_ch)) // читаем ключевое слово
			{
				result.lexemeType = LexemeType.VarName;
				cur_ch = program_text[current_index];
				while (current_index < program_text.Length && IsLetterOrNotMeaningSybols(cur_ch) || IsLetterOrNotMeaningSybols(cur_ch))
				{
					NextSybol(ref result, ref cur_ch);
				}
				FindKeyWord(ref result);
			}
			else if (Char.IsNumber(cur_ch))//читаем значение int или float
			{
				result.lexemeType = LexemeType.IntValue;
				cur_ch = program_text[current_index];

				while (current_index < program_text.Length && Char.IsNumber(cur_ch))
				{
					NextSybol(ref result, ref cur_ch);
				}
				if(current_index< program_text.Length && cur_ch == '.')
				{
					result.lexemeType = LexemeType.FloatValue;
					NextSybol(ref result, ref cur_ch);
					while (current_index < program_text.Length && Char.IsNumber(cur_ch))
					{
						NextSybol(ref result, ref cur_ch);
					}
				}
				if (current_index < program_text.Length && IsLetterOrNotMeaningSybols(cur_ch))
				{
					while (current_index < program_text.Length && IsLetterOrNotMeaningSybols(cur_ch))
					{
						NextSybol(ref result, ref cur_ch);
					}
					result.lexemeType = LexemeType.Error;
				}
			}
			else FindMeaningSymbolOrString(ref result, cur_ch);
			
			return result;
		}

		private void FindMeaningSymbolOrString(ref Lexeme result, char cur_ch)
		{
			if (cur_ch == '{')
			{
				result.lexemeType = LexemeType.LeftBrace;
			}
			else if (cur_ch == '}')
			{
				result.lexemeType = LexemeType.RightBrace;
			}
			else if (cur_ch == '[')
			{
				result.lexemeType = LexemeType.LeftSquareBracket;
			}
			else if (cur_ch == ']')
			{
				result.lexemeType = LexemeType.RightSquareBracket;
			}
			else if (cur_ch == '(')
			{
				result.lexemeType = LexemeType.LeftRoundBracket;
			}
			else if (cur_ch == ')')
			{
				result.lexemeType = LexemeType.RightRoundBracket;
			}
			else if (cur_ch == '+')
			{
				result.lexemeType = LexemeType.Plus;
			}
			else if (cur_ch == '-')
			{
				result.lexemeType = LexemeType.Minus;
			}
			else if (cur_ch == '*')
			{
				result.lexemeType = LexemeType.Multiply;
			}
			else if (cur_ch == '/')
			{
				result.lexemeType = LexemeType.Divide;
			}
			else if (cur_ch == ';')
			{
				result.lexemeType = LexemeType.Semicolon;
			}
			else if (cur_ch == ',')
			{
				result.lexemeType = LexemeType.Comma;
			}
			else if (cur_ch == '<')
			{
				result.lexemeType = LexemeType.Less;
				if (current_index < program_text.Length && program_text[current_index] == '=')
				{
					current_index++;
					result.lexemeType = LexemeType.LessOrEqual;
					result.value = "<=";
				}
			}
			else if (cur_ch == '=')
			{
				result.lexemeType = LexemeType.Assign;
				if (current_index < program_text.Length && program_text[current_index] == '=')
				{
					current_index++;
					result.lexemeType = LexemeType.Equal;
					result.value = "==";
				}
			}
			else if (cur_ch == '>')
			{
				result.lexemeType = LexemeType.More;
				if (current_index < program_text.Length && program_text[current_index] == '=')
				{
					current_index++;
					result.lexemeType = LexemeType.MoreOrEqual;
					result.value = ">=";
				}
			}
			else if (cur_ch == '!' && current_index < program_text.Length && program_text[current_index] == '=')
			{
				current_index++;
				result.lexemeType = LexemeType.NotEqual;
				result.value = "!=";
			}
			else if(cur_ch == '"' && current_index < program_text.Length)
			{
				current_index++;
				result.lexemeType = LexemeType.StringType;
				cur_ch = program_text[current_index];
				while(current_index < program_text.Length && program_text[current_index] != '"')
				{
					NextSybol(ref result, ref cur_ch);
				}
				if(current_index >= program_text.Length)
				{
					result.lexemeType = LexemeType.Error;
				}
			}

		}

		private void FindKeyWord(ref Lexeme result)
		{
			if (result.value == "int")
			{
				result.lexemeType = LexemeType.IntType;
			}
			else if (result.value == "float")
			{
				result.lexemeType = LexemeType.FloatType;
			}
			else if (result.value == "string")
			{
				result.lexemeType = LexemeType.StringType;
			}
			else if (result.value == "arrayString")
			{
				result.lexemeType = LexemeType.ArrayStringType;
			}
			else if (result.value == "arrayFloat")
			{
				result.lexemeType = LexemeType.ArrayFloatType;
			}
			else if (result.value == "arrayInt")
			{
				result.lexemeType = LexemeType.ArrayIntType;
			}
			else if (result.value == "if")
			{
				result.lexemeType = LexemeType.If;
			}
			else if (result.value == "else")
			{
				result.lexemeType = LexemeType.Else;
			}
			else if (result.value == "while")
			{
				result.lexemeType = LexemeType.While;
			}
			else if (result.value == "read")
			{
				result.lexemeType = LexemeType.Read;
			}
			else if (result.value == "write")
			{
				result.lexemeType = LexemeType.Write;
			}
			else if (result.value == "new")
			{
				result.lexemeType = LexemeType.Allocate;
			}
		}

		private void NextSybol(ref Lexeme lex, ref char ch)
		{
			lex.value += ch;
			current_index++;
			ch = program_text[current_index];
		}

		private bool IsLetterOrNotMeaningSybols(char ch)
		{
			return ch == '_' || ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z');
		}
	}
}
