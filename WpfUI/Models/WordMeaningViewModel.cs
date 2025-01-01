namespace WpfUI.Models
{
    public class WordMeaningViewModel
    {
        public string PartOfSpeech { get; set; } = null!;
        public List<MeaningDefinitionViewModel> Definitions { get; set; } = new List<MeaningDefinitionViewModel>();
    }
}
