using System.Collections.Generic;

namespace DictionaryLibrary.DataAccess.Models
{
    public class MeaningApiResponseModel
    {
        public string PartOfSpeech { get; set; }
        public List<DefinitionApiResponseModel> Definitions { get; set; }
        public List<string> Synonyms { get; set; }
        public List<string> Antonyms { get; set; }
    }
}
