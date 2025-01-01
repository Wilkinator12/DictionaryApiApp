using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.Models
{
    public class DictionaryWordViewModel
    {
        public string Word { get; set; } = null!;
        public string Phonetic { get; set; } = null!;
        public List<WordMeaningViewModel> Meanings { get; set; } = new List<WordMeaningViewModel>();
    }
}
