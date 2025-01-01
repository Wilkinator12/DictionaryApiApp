using System;
using System.Collections.Generic;
using System.Text;

namespace DictionaryLibrary.DataAccess.Models
{
    public class DictionaryApiResponseModel
    {
        public string Word { get; set; }
        public string Phonetic { get; set; }
        public List<PhoneticApiResponseModel> Phonetics { get; set; }
        public List<MeaningApiResponseModel> Meanings { get; set; }
        public LicenseApiResponseModel License { get; set; }
        public List<string> SourceUrls { get; set; }
    }
}
