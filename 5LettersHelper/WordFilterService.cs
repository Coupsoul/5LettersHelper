using System.IO;


namespace _5LettersHelper
{
    class WordFilterService
    {
        private readonly List<string>? _allWords;

        public WordFilterService()
        {
            try
            {
                _allWords = File.ReadAllLines("FiveLettersWords.txt").ToList();
            }
            catch (Exception ex)
            {
                _allWords = new List<string>();
                System.Windows.MessageBox.Show($"Ошибка загрузки словаря: {ex.Message}");
            }
        }

        public List<string> GetFilteredWords(string mask, string excludedLetters, string includedLetters)
        {
            var filteredByMask = FilterByMask(_allWords, mask);
            var filteredByExcluded = FilterByExcludedLetters(filteredByMask, excludedLetters);
            var finalWords = FilterByIncludedLetters(filteredByExcluded, includedLetters);

            return finalWords;
        }

        private List<string> FilterByMask(List<string> words, string mask)
        {
            if (string.IsNullOrWhiteSpace(mask))
                return words;

            var result = new List<string>();
            foreach (string word in words)
            {
                bool isMatch = true;
                for (int i = 0; i < mask.Length; i++)
                {
                    if (mask[i] != '*' && mask[i] != word[i])
                    {
                        isMatch = false;
                        break;
                    }
                }
                if (isMatch)
                    result.Add(word);
            }
            return result;
        }

        private List<string> FilterByExcludedLetters(List<string> words, string excludedLetters)
        {
            if (string.IsNullOrWhiteSpace(excludedLetters))
                return words;

            var excludedChars = excludedLetters.ToCharArray();
            return words.Where(word => !excludedChars.Any(excludedChar => word.Contains(excludedChar))).ToList();
        }

        private List<string> FilterByIncludedLetters(List<string> words, string includedLetters)
        {
            if (string.IsNullOrWhiteSpace(includedLetters))
                return words;

            var includedChars = includedLetters.ToCharArray();
            return words.Where(word => includedChars.All(includedChar =>  word.Contains(includedChar))).ToList();
        }
    }
}