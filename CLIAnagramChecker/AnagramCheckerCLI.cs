using AnagramCheckerLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CLIAnagramChecker
{

    public class AnagramCheckerCLI
    {
        static AnagramChecker checker = new AnagramChecker();


        static void Main(string[] args)
        {
            if (args.Length == 0 || (!args[0].Equals("check") && !args[0].Equals("getKnown") && !args[0].Equals("getPermutations")))
            {
                Console.WriteLine("Unknown mode! Possible: \"check\" or \"getKnown\" or \"getPermutations\"");
                return;
            }

            if (args[0].Equals("check"))
            {
                Check(args);
            }
            else if (args[0].Equals("getKnown"))
            {
                GetKnown(args);
            }
            else if (args[0].Equals("getPermutations"))
            {
                GetPermutations(args);
            }
        }

        static void Check(string[] args)
        {
            if (args.Length != 3)
            {
                return;
            }

            if (checker.CompareTwoWords(args[1], args[2]))
            {
                Console.WriteLine("\"" + args[1] + "\" and \"" + args[2] + "\" are anagrams");
            }
            else
            {
                Console.WriteLine("\"" + args[1] + "\" and \"" + args[2] + "\" are no anagrams");
            }
        }

        static async void GetKnown(string[] args)
        {
            if (args.Length != 2)
            {
                return;
            }
            IEnumerable<string> anagrams = await checker.FindAnagramsAsync(args[1]);

            if (anagrams.Count() == 0)
            {
                Console.WriteLine("No anagrams found!");
            }
            else
            {
                foreach (var curAnagram in anagrams)
                {
                    Console.WriteLine(curAnagram);
                }
            }
        }

        static void GetPermutations(string[] args)
        {
            if (args.Length != 2)
            {
                return;
            }
            IEnumerable<string> permutations = checker.GetPermutations(args[1]);

            if (permutations.Count() == 0)
            {
                Console.WriteLine("No permutations found!");
            }
            else
            {
                foreach (var curPermutation in permutations)
                {
                    Console.WriteLine(curPermutation);
                }
            }
        }
    }
}
