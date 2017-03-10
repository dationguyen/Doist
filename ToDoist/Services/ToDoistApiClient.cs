using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ToDoist.Models;

namespace ToDoist.Services
{
    public class ToDoistApiClient : RestApiClient, IToDoistApiClient
    {
        #region Constant        
        private const string BaseUri = "https://todoist.com/API/v7/sync";        
        private const string TokenValue = "f0c87d8d5372ee7833690dce8228b7ffa2a3131f";
        #endregion

        #region Constructor
        public ToDoistApiClient()
        {
            //Initialize  HttpClient
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUri),
                Timeout = TimeSpan.FromSeconds(10)
            };            
        }
        #endregion

        #region public methods
        /// <summary>
        /// Get item list from the todoist api
        /// </summary>
        /// <returns></returns>
        public async Task<List<Item>> GetItemsAzync()
        {
            //hard code the parametter for quick delivery
            var parameters = new Dictionary<string, string>
            {
                { "token", TokenValue },
                { "sync_token", "*" },
                { "resource_types", "[\"items\"]" }
            };
            //call Post request
            var response = await ApiPost<ResponseMessage>("", parameters);
            return response?.items;
        }
        #endregion

    }
}
