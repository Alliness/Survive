using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.Assertions;

namespace Game.Scripts.Utils
{
    public class CSVReader
    {
        public static List<string[]> ReadFile(String filePath)
        {
            var content = new List<string[]>();
            var file = new StreamReader(filePath, Encoding.UTF8);
            
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                Assert.IsNotNull(line);
                string[] s = line.Split(new[] {","}, StringSplitOptions.None);
                content.Add(s);
            }
            return content;
        }
    }
}