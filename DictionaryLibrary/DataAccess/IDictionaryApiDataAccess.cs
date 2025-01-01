using DictionaryLibrary.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DictionaryLibrary.DataAccess
{
    public interface IDictionaryApiDataAccess
    {
        Task<List<DictionaryApiResponseModel>> LookupWordAsync(string word);
    }
}