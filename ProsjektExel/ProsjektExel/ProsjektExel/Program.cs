using System.Dynamic;
using ProsjektExel;

var ExelToObjects = new Exel();

var records = ExelToObjects.ExelToObjects(@"test.csv");

foreach (dynamic record in records)
{
    Console.WriteLine($"{record.Dato}");
}