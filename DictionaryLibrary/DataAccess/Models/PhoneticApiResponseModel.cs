namespace DictionaryLibrary.DataAccess.Models
{
    public class PhoneticApiResponseModel
    {
        public string Text { get; set; }
        public string Audio { get; set; }
        public string SourceUrl { get; set; }
        public LicenseApiResponseModel License { get; set; }
    }
}
