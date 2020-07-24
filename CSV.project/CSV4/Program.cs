using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Markup;

namespace CSV4
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {

                /* Reading file.strings */
                using (var reader = new StreamReader(@"C:\Log\file.strings.txt"))
                {
                    Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
                    List<string> keys = new List<string>();
                    var index = 0;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values.Length == 3)
                        {
                            if (index == 0)
                            {
                                for (int i = 0; i < values.Length; i++)
                                {
                                    keys.Add(values[i]);
                                    dict.Add(values[i], new List<string>());
                                }
                            }
                            else
                            {
                                for (int i = 0; i < values.Length; i++)
                                {
                                    dict[keys[i]].Add(values[i]);
                                }
                            }
                            index++;
                        }
                    }
                        /* Create a new file */

                    foreach (var kvp in dict.Skip(1))
                    {
                        string file = $@"C:\Log\file_{kvp.Key}.txt";
                        FileInfo files = new FileInfo(file);

                        /* Check if file already exists. If yes, delete it. */
                        if (files.Exists)
                        {
                            files.Delete();
                        }

                        using (StreamWriter sw = files.CreateText())
                        {

                            for (int i = 0; i < kvp.Value.Count; i++)
                            {
                                if (dict["key"][i] != "")
                                {

                                    if (kvp.Value != null) 
                                    {

                                        sw.WriteLine($"\"{dict["key"][i]}\" =" + $" \"{kvp.Value[i]}\";");
                                    }
                                    
                                }
                            }
                        }

                        /* Write file contents on console. */
                        using (StreamReader sr = File.OpenText(file))
                        {
                            string s = "";
                            while ((s = sr.ReadLine()) != null)
                            {
                                Console.WriteLine(s);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }
        }
    }
}

