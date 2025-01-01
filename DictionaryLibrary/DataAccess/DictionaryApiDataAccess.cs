using DictionaryLibrary.DataAccess.Models;
using DictionaryLibrary.DataAccess.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DictionaryLibrary.DataAccess
{
    public class DictionaryApiDataAccess : IDictionaryApiDataAccess
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl;


        public DictionaryApiDataAccess(ILogger<DictionaryApiDataAccess> logger, IHttpClientFactory httpClientFactory, IOptions<DictionaryApiOptions> options)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _apiUrl = options.Value.BaseUrl;
        }


        public async Task<List<DictionaryApiResponseModel>> LookupWord(string word)
        {
            List<DictionaryApiResponseModel> output;


            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiUrl}{word}");

            if (response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                try
                {
                    output = JsonSerializer.Deserialize<List<DictionaryApiResponseModel>>(responseText, options);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Could not deserialize Dictionary API response.\nWord: {word}\nException: {ex.Message}");
                    throw;
                }
            }
            else
            {
                string message = $"API response was not successful.\nWord: {word}\nReason: {response.ReasonPhrase}";
                _logger.LogError(message);
                throw new Exception(message);
            }


            return output;
        }
    }
}
