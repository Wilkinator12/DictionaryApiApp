using System.Collections.Generic;

namespace DictionaryLibrary.DataAccess.Models
{
    public class DefinitionApiResponseModel
    {
        public string Definition { get; set; }
        public List<string> Synonyms { get; set; }
        public List<string> Antonyms { get; set; }
        public string Example { get; set; }
    }
}
