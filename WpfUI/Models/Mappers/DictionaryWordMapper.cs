using DictionaryLibrary.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.Models.Mappers
{
    public static class DictionaryWordMapper
    {
        public static DictionaryWordViewModel ToViewModel(this DictionaryApiResponseModel model)
        {
            DictionaryWordViewModel viewModel = new DictionaryWordViewModel()
            {
                Word = model.Word,
                Phonetic = model.Phonetic
            };

            foreach (var meaningModel in model.Meanings)
            {
                var meaningViewModel = new WordMeaningViewModel()
                {
                    PartOfSpeech = meaningModel.PartOfSpeech
                };

                foreach (var definitionModel in meaningModel.Definitions)
                {
                    var definitionViewModel = new MeaningDefinitionViewModel()
                    {
                        Definition = definitionModel.Definition,
                        Example = definitionModel.Example
                    };

                    meaningViewModel.Definitions.Add(definitionViewModel);
                }


                viewModel.Meanings.Add(meaningViewModel);
            }


            return viewModel;
        }
    }
}
