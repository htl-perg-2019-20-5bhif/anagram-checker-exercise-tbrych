using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AnagramCheckerLogic
{
    class DictionaryFileReader
    {
        public async Task<string> ReadDictionaryAsync()
        {
            string dictionaryContent;
            try
            {
                var dictFileName = "dictionary.txt";
                dictionaryContent = await File.ReadAllTextAsync(dictFileName);
            }
            catch (FileNotFoundException ex)
            {
                System.Diagnostics.Debug.WriteLine("Error reading the dictionar! Error: " + ex.GetType().ToString());
                throw;
            }

            return dictionaryContent;
        }
    }
}
