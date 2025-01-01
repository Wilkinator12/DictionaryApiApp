using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUI.Models;

namespace WpfUI.Formatting
{
    public class DefinitionStringFormatter
    {
        public static string GetFormattedDefinition(DictionaryWordViewModel dictionaryWord)
        {
            string output = string.Empty;


            // Add the header
            output += $"{dictionaryWord.Word} - ({dictionaryWord.Phonetic})";


            // Add meanings
            foreach (var meaning in dictionaryWord.Meanings)
            {
                output += "\n\n\n";
                output += GetFormattedMeaning(meaning);
            }


            return output;
        }

        private static string GetFormattedMeaning(WordMeaningViewModel wordMeaning)
        {
            string output = string.Empty;


            // Add part of speech
            output += $"** {wordMeaning.PartOfSpeech} **";


            for (int i = 0; i < wordMeaning.Definitions.Count; i++)
            {
                output += "\n\n";
                output += GetFormattedDefinition(wordMeaning.Definitions[i], i + 1);
            }


            return output;
        }

        private static string GetFormattedDefinition(MeaningDefinitionViewModel meaningDefinition, int definitionNumber)
        {
            var output = string.Empty;


            output += $"{definitionNumber}: {meaningDefinition.Definition}";

            if (string.IsNullOrWhiteSpace(meaningDefinition.Example) == false)
            {
                output += $"\nEx: \"{meaningDefinition.Example}\""; 
            }


            return output;
        }
    }
}
