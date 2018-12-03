using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace dotnet
{
    public class Journal
    {
        private readonly static List<string> entries = new List<string>();
        private static int count;

        public int AddEntry(string entry){
            entries.Add($"{++count}: {entry}");
            return count;
        }

        public void RemoveEntry(int index){
            entries.RemoveAt(index);
        }

        public override string ToString(){
            return string.Join(Environment.NewLine, entries);
        }

    }

    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false){
            if( overwrite || !File.Exists(filename)){
                File.WriteAllText(filename,journal.ToString());
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Journal j = new Journal();
            j.AddEntry("Went to the doctor!!");
            j.AddEntry("Have a kitkat break");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = @"C:\Users\KeshavChaurasia\Desktop\codes\dotnet\journal.txt";
            p.SaveToFile(j, filename);
        }
    }
}
