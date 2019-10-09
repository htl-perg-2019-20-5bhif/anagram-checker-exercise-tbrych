using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramCheckerLogic
{
    public class AnagramChecker : IAnagramChecker
    {
        public bool CompareTwoWords(string s1, string s2)
        {
            //Order the words alphabetically
            string wordAlphabet1 = String.Concat(s1.ToLower().OrderBy(c => c));
            string wordAlphabet2 = String.Concat(s2.ToLower().OrderBy(c => c));

            return wordAlphabet1.Equals(wordAlphabet2);
        }

        public async Task<IEnumerable<string>> FindAnagramsAsync(string word)
        {
            IEnumerable<string> words = await ParseDictionary();

            List<string> anagrams = new List<string>();
            foreach (var curWord in words)
            {
                if (CompareTwoWords(word, curWord))
                {
                    if (!word.Equals(curWord) && !anagrams.Contains(curWord))
                    {
                        anagrams.Add(curWord);
                    }
                }
            }
            return anagrams;
        }

        public IEnumerable<string> GetPermutations(string word)
        {
            List<string> results = new List<string>();
            heapPermutation(word.ToCharArray(), word.Length, word.Length, results);

            return results;
        }

        //Generating permutation using Heap Algorithm 
        private bool heapPermutation(char[] a, int size, int n, List<string> results)
        {
            // if size becomes 1 then prints the obtained 
            // permutation 
            if (size == 1)
                results.Add(new string(a));

            for (int i = 0; i < size; i++)
            {
                heapPermutation(a, size - 1, n, results);

                // if size is odd, swap first and last 
                // element 
                if (size % 2 == 1)
                {
                    char temp = a[0];
                    a[0] = a[size - 1];
                    a[size - 1] = temp;
                }

                // If size is even, swap ith and last 
                // element 
                else
                {
                    char temp = a[i];
                    a[i] = a[size - 1];
                    a[size - 1] = temp;
                }
            }

            return true;
        }

        private async Task<IEnumerable<string>> ParseDictionary()
        {
            DictionaryFileReader dict = new DictionaryFileReader();
            string dictionaryContent = await dict.ReadDictionaryAsync();
            List<string> words = new List<string>();
            
            var dictionaryWords = dictionaryContent.Replace("\r", string.Empty).Split('\n');
            foreach (var curLine in dictionaryWords)
            {
                var curWords = curLine.Split("=");
                if (curWords.Length == 2)
                {
                    words.AddRange(curWords);
                }
            }

            return words;
        }
    }
}
