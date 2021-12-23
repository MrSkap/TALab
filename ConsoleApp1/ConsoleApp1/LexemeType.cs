using System;
using System.Collections.Generic;
using System.Text;

namespace AutomatLab
{
	public enum LexemeType
	{
		VarName, //имя (идентификатор переменной)
		IntValue, //целое число без знака
		IntType, //int
		FloatType,
		FloatValue,
		StringType,
		StringValue,
		ArrayStringType,
		ArrayIntType,
		ArrayFloatType,
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
