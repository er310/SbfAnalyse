using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SbfAnalyse
{
    class Program
    {
        static void Main()
        {
            int round = 1;
            string directory = string.Empty;
            string go = "YES";
            while (go == "YES")
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("Round = {0}", round);
                if (round == 1)
                {
                    Console.WriteLine("File Directory = ");
                    directory = Console.ReadLine();

                    // Default directory
                    if (string.IsNullOrEmpty(directory))
                        directory = "C:\\Users\\Ehasan\\Documents\\Office Work\\Git\\ntwine\\sbf outputs";
                }

                Console.WriteLine("Order Ids = ");
                string orderIds = Console.ReadLine();

                List<string> contains = new List<string>();
                List<string> existContains = new List<string>();
                List<string> nonExistcontains = new List<string>();
                if (!string.IsNullOrEmpty(orderIds))
                {
                    var idList = orderIds.Split(',');
                    foreach (var id in idList)
                    {
                        contains.Add(string.Format("<order_id>{0}</order_id>", id));
                    }
                }
                else
                {
                    Console.WriteLine("Line Contain = ");
                    string genericContains = Console.ReadLine();
                    if (!string.IsNullOrEmpty(genericContains))
                    {
                        var genConList = genericContains.Split(',');
                        foreach (var genCon in genConList)
                        {
                            contains.Add(genCon);
                        }
                    }
                }

                foreach (string searchKey in contains)
                {
                    try
                    {
                        Console.WriteLine("#### Start ####");
                        Console.WriteLine("Looking for value = {0}", searchKey);
                        Console.WriteLine(string.Empty);

                        var key = searchKey;
                        var keyFound = false;
                        var files = from file in Directory.EnumerateFiles(directory, "*.out", SearchOption.AllDirectories)
                                    from line in File.ReadLines(file)
                                    where line.Contains(key)
                                    select new
                                    {
                                        File = file,
                                        Line = line
                                    };
                        files = files.ToList();

                        int i = 0;
                        foreach (var f in files)
                        {
                            Console.WriteLine("#{0}: ", i);
                            Console.WriteLine("Filename: {0}", f.File);
                            Console.WriteLine("\t{0}", f.Line);
                            Console.WriteLine(string.Empty);
                            i++;
                            keyFound = true;
                        }

                        Console.WriteLine(string.Empty);
                        Console.WriteLine("#### Mini Summary ####");

                        if (keyFound)
                        {
                            Console.WriteLine("Found Record: YES");
                            existContains.Add(key);
                        }
                        else
                        {
                            Console.WriteLine("Found Record: NO");
                            nonExistcontains.Add(key);
                        }

                        Console.WriteLine("{0} files found.", files.Count());
                        Console.WriteLine("#### End ####");
                    }
                    catch (UnauthorizedAccessException uaEx)
                    {
                        Console.WriteLine(uaEx.Message);
                    }
                    catch (PathTooLongException pathEx)
                    {
                        Console.WriteLine(pathEx.Message);
                    }
                }

                Console.WriteLine(string.Empty);
                Console.WriteLine("#### Final Summary ####");

                Console.WriteLine(string.Empty);
                Console.WriteLine("Exists Contains");
                foreach (var existContain in existContains)
                {
                    Console.WriteLine(existContain);
                }

                Console.WriteLine(string.Empty);
                Console.WriteLine("Non-Exists Contains");
                foreach (var nonExistContain in nonExistcontains)
                {
                    Console.WriteLine(nonExistContain);
                }
                Console.WriteLine("#### End ####");

                Console.WriteLine("Continue? YES/No = ");
                go = Console.ReadLine();
                round++;
            }
        }
    }
}
