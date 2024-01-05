using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_IV
{
    public class Morse
    {
        private const string Taah = "===";
        private const string Ti = "=";
        private const string Point = ".";
        private const string PointLetter = "...";
        private const string PointWord = ".....";

        private readonly Dictionary<string, char> _alphabet;

        public Morse()
        {
            _alphabet = new Dictionary<string, char>()
            {
                {$"{Ti}.{Taah}", 'A'},
                {$"{Taah}.{Ti}.{Ti}.{Ti}", 'B'},
                {$"{Taah}.{Ti}.{Taah}.{Ti}", 'C'},
                {$"{Taah}.{Ti}.{Ti}", 'D'},
                {$"{Ti}", 'E'},
                {$"{Ti}.{Ti}.{Taah}.{Ti}", 'F'},
                {$"{Taah}.{Taah}.{Ti}", 'G'},
                {$"{Ti}.{Ti}.{Ti}.{Ti}", 'H'},
                {$"{Ti}.{Ti}", 'I'},
                {$"{Ti}.{Taah}.{Taah}.{Taah}", 'J'},
                {$"{Taah}.{Ti}.{Taah}", 'K'},
                {$"{Ti}.{Taah}.{Ti}.{Ti}", 'L'},
                {$"{Taah}.{Taah}", 'M'},
                {$"{Taah}.{Ti}", 'N'},
                {$"{Taah}.{Taah}.{Taah}", 'O'},
                {$"{Ti}.{Taah}.{Taah}.{Ti}", 'P'},
                {$"{Taah}.{Taah}.{Ti}.{Taah}", 'Q'},
                {$"{Ti}.{Taah}.{Ti}", 'R'},
                {$"{Ti}.{Ti}.{Ti}", 'S'},
                {$"{Taah}", 'T'},
                {$"{Ti}.{Ti}.{Taah}", 'U'},
                {$"{Ti}.{Ti}.{Ti}.{Taah}", 'V'},
                {$"{Ti}.{Taah}.{Taah}", 'W'},
                {$"{Taah}.{Ti}.{Ti}.{Taah}", 'X'},
                {$"{Taah}.{Ti}.{Taah}.{Taah}", 'Y'},
                {$"{Taah}.{Taah}.{Ti}.{Ti}", 'Z'},
            };
        }

        public int LettersCount(string code)
        {
            string[] letters = code.Split(new[] { PointLetter }, StringSplitOptions.None);
            Console.WriteLine("letters count : " + letters.Length);
            return letters.Length;
        }

        public int WordsCount(string code)
        {
            // Count the number of words in Morse code
            string[] words = code.Split(new[] { PointWord }, StringSplitOptions.None);
            foreach (string word in words)
            {
                LettersCount(word);
            }
            Console.WriteLine("words count : " + words.Length);
            return words.Length;
        }

        public string MorseTranslation(string code)
        {
            string[] words = code.Split(new[] { PointWord }, StringSplitOptions.None);
            List<string> translatedWords = new List<string>();

            foreach (string word in words)
            {
                string[] letters = word.Split(new[] { PointLetter }, StringSplitOptions.None);
                StringBuilder translation = new StringBuilder();

                foreach (string letter in letters)
                {
                    if (_alphabet.TryGetValue(letter, out char translatedLetter))
                    {
                        translation.Append(translatedLetter);
                    }
                    else
                    {
                        // Handle unknown Morse code
                        translation.Append('?');
                    }
                }
                translatedWords.Add(translation.ToString());
            }

            string result = string.Join(" ", translatedWords);
            return result;
        }

        public string EfficientMorseTranslation(string code)
        {
            //TODO
            return string.Empty;
        }

        public string MorseEncryption(string sentence)
        {
            string space = " ";
            string[] words = sentence.ToUpper().Split(new[] { space }, StringSplitOptions.None);
            List<string> encryptedWords = new List<string>();

            foreach (string word in words)
            {
                char[] letters = word.ToCharArray();
                List<string> encryptedLetters = new List<string>();

                foreach (char letter in letters)
                {
                    if (_alphabet.ContainsValue(letter))
                    {
                        string morseCode = _alphabet.First(pair => pair.Value == letter).Key;
                        encryptedLetters.Add(morseCode);
                    }
                    else
                    {
                        // Handle unknown characters
                        encryptedLetters.Add("?");
                    }
                }

                string encryptedWord = string.Join(PointLetter, encryptedLetters);
                encryptedWords.Add(encryptedWord);
            }

            string encryptedSentence = string.Join(PointWord, encryptedWords);
            return encryptedSentence;
        }
    }
}
