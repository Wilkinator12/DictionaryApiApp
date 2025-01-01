using DictionaryLibrary.DataAccess;
using DictionaryLibrary.DataAccess.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfLibrary.TextBoxes;
using WpfUI.Formatting;
using WpfUI.Models.Mappers;


namespace WpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDictionaryApiDataAccess _db;
        private readonly TextBoxIdler _textBoxIdler;


        public MainWindow(IDictionaryApiDataAccess db)
        {
            InitializeComponent(); 
            _db = db;

            _textBoxIdler = new TextBoxIdler(wordTextBox, "Enter word here...");
        }

        private async void LookupButton_Click(object sender, RoutedEventArgs e)
        {
            string wordToLookUp = wordTextBox.Text;

            if (string.IsNullOrWhiteSpace(wordToLookUp)) return;


            DictionaryApiResponseModel? dictionaryDefinition = null;
            string definitionText = string.Empty;


            try
            {
                dictionaryDefinition = (await _db.LookupWordAsync(wordToLookUp)).First();
            }
            catch
            {
                definitionText = $"No definition found for {wordToLookUp}";
            }


            if (dictionaryDefinition != null)
            {
                definitionText = DefinitionStringFormatter.GetFormattedDefinition(dictionaryDefinition.ToViewModel());
                _textBoxIdler.EnterIdleMode();
            }
            else
            {
                wordTextBox.Clear();
                wordTextBox.Focus();
            }

            definitionsTextBlock.Text = definitionText;

            definitionsScrollViewer.ScrollToTop();
        }

        private void LookupButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Button)sender).FontWeight = FontWeights.Bold;
        }

        private void LookupButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Button)sender).FontWeight = FontWeights.Regular;
        }
    }
}