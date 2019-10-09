using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramCheckerLogic
{
    public interface IAnagramChecker
    {
        public bool CompareTwoWords(string s1, string s2);
        public Task<IEnumerable<string>> FindAnagramsAsync(string word);
        public IEnumerable<string> GetPermutations(string word);
    }
}
