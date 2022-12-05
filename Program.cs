// See https://aka.ms/new-console-template for more information

using AdventOfCode;
using AdventOfCode.Challenges;


var reader = new InputReader("https://adventofcode.com", ("ru", "53616c7465645f5fd1c78b84e715f66d0040c7fca0c25d873d0f80cc831cac1b"), ("session", "53616c7465645f5f6a4b3b9cfb28e698f80c58fbde9ae3bfd52429423a6dbb434523c9b145d7b3995b632cb1ecd22bb9cfbdf2e969c7b853d0e878284a2f1e65"));

var day01 = await Day01.Initialize(reader);
var firstPart = day01.GetPartOneAnswer();
var secondPart  = day01.GetPartTwoAnswer();

Console.WriteLine($"{firstPart}, {secondPart}");

var day02 = await Day02.Initialize(reader);
var points = day02.GetPointSumPart01();
var pointsCorrectede = day02.GetPointSumPart02();

Console.WriteLine($"{points}, {pointsCorrectede}");

var day03 = await Day03.Initialize(reader);
var answer = day03.TransformInput();

Console.WriteLine($"{answer}");