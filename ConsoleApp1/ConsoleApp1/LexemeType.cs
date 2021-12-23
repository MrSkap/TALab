using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatLab
{
	enum LexemeType
	{
		VarName, //имя (идентификатор переменной)
		IntValue, //целое число без знака
		IntType, //int
		FloatType,
		FloatValue,
		StringType,
		StringValue,
		CharType,
		CharValue,
		ArrayType,
		//if|else|while|int|string|char|float|read|write|abs|sqrt|sqr
		If,
		Else,
		While,
		Read,
		Write,
		Abs,
		Sqrt,
		Allocate, //new
		LeftBrace, //{
		RightBrace,
		LeftSquareBracket,
		RightSquareBracket,
		LeftRoundBracket,
		RightRoundBracket,
		Plus,
		Minus,
		Multiply,
		Divide,
		Semicolon,
		Comma,
		Less,
		Assign,
		More,
		Equal,
		LessOrEqual,
		MoreOrEqual,
		NotEqual,
		Finish,
		Error
	}
}
