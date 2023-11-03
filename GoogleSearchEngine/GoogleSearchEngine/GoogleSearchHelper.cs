using GoogleSearchEngine.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GoogleSearchEngine
{
    internal class GoogleSearchHelper
    {
        private string _apiKey = string.Empty;
        private string _searchEngineId = string.Empty;

        public GoogleSearchHelper()
        {
            SetApiKey("Your API Key");
            SetEngineId("Your Engine ID");
        }

        public void SetApiKey(string apiKey)
        {
            _apiKey = apiKey;
        }

        public void SetEngineId(string engineId)
        {
            _searchEngineId = engineId;
        }

        public async Task<SearchBundle> SearchByQuery(string query)
        {
            if (string.IsNullOrEmpty(_apiKey) || string.IsNullOrEmpty(_searchEngineId))
            {
                throw new InvalidOperationException("API key and search engine ID must be set before performing a search.");
            }

            string apiUrl = $"https://www.googleapis.com/customsearch/v1?key={_apiKey}&cx={_searchEngineId}&q={Uri.EscapeDataString(query)}&lr=lang_zh-TW";

            using HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();
            dynamic? data = JsonConvert.DeserializeObject(jsonResponse);

            SearchBundle searchResults = new SearchBundle(query);

            if (data != null)
            {
                foreach (var item in data.items)
                {
                    string title = item.title;
                    string url = item.link;

                    SearchResult result = new SearchResult { Title = title, Url = url };
                    searchResults.Entries.Add(result);
                }
            }

            return searchResults;
        }
    }
}
