// See https://aka.ms/new-console-template for more information

using CodeConverter;

var file = "Input: 100001111 (binært)" +
           "Output: 10F (hex)" +
           "" +
           "Input: 11000010110001001100011 (binært)" +
           "Output: abc (ascii)" +
           "" +
           "Input: 1A (hex)" +
           "Output: 00011010 (binært)" +
           "" +
           "Input: 61 62 63 (hex)" +
           "Output: abc (ascii)" +
           "" +
           "Input: abc (ascii)" +
           "Output: 01100001 01100010 01100011 (binært)" +
           "" +
           "Input: abc (ascii)" +
           "Output: 61 62 63 (hex)";

var converter = new Converter();

Console.WriteLine(converter.Run("binary","hex", "100001111"));

Console.WriteLine(converter.Run("binary","ascii", "11000010110001001100011"));

Console.WriteLine(converter.Run("hex","binary", "1A"));

Console.WriteLine(converter.Run("hex","ascii", "61 62 63"));

Console.WriteLine(converter.Run("ascii","binary", "abc"));

Console.WriteLine(converter.Run("ascii","hex", "abc"));
    
    