using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
namespace ProsjektExel;

public class Exel
{
    private string filePath = "test.csv";
    
    public List<ExpandoObject> ExelToObjects(string filePath)
    {
        
        var records = new List<ExpandoObject>();

        using (var reader = new StreamReader(filePath))
        {
            var headers = reader.ReadLine().Split(';');

            while (!reader.EndOfStream)
            {
                var fields = reader.ReadLine().Split(';');
                dynamic record = new ExpandoObject();        

                
                for (int i = 0; i < headers.Length; i++)
                {
                    string header = headers[i].Replace(" ", "");
                    string field = fields[i].Trim();
                    ((IDictionary<string, object>)record).Add(header, field);
                }
                records.Add(record);
            }
        } 
        
        if (records.Count > 0)
        {
            Console.WriteLine("Eksempel på dynamisk objekt:");
            var dict = (IDictionary<string, object>)records[0];

            foreach (var kvp in dict)
            {
                Console.WriteLine($"- {kvp.Key} = {kvp.Value}");
            }
        }
        
        return records;
    }
}