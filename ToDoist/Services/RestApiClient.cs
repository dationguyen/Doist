using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToDoist
{
    public class RestApiClient
    {
        #region field
        protected HttpClient _httpClient;
        #endregion

        #region Method

        /// <summary>
        /// call simple Get request to an Uri and return specific type
        /// </summary>
        /// <typeparam name="T">Type of output</typeparam>
        /// <param name="requestUri">path uri</param>
        /// <returns></returns>
        public async Task<T> ApiGet<T>(string requestUri)
        {
            T result;
            try
            {
                var response = await _httpClient.GetStringAsync(requestUri);
                result = JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                result = default(T);
            }

            return result;
        }

        /// <summary>
        /// call Get request with parameter to an Uri and return specific type
        /// </summary>
        /// <typeparam name="T">Type of output</typeparam>
        /// <param name="requestUri">path uri</param>
        /// <param name="param">parameter</param>
        /// <returns></returns>
        public async Task<T> ApiGet<T>(string requestUri, Dictionary<string, string> param)
        {
            var newUri = AddQueryString(requestUri, param);
            return await ApiGet<T>(newUri);
        }

        /// <summary>
        /// call Post request with parameter to an Uri and return specific type
        /// </summary>
        /// <typeparam name="T">Type of output</typeparam>
        /// <param name="requestUri">path uri</param>
        /// <param name="param">parameter</param>
        /// <returns></returns>
        public async Task<T> ApiPost<T>(string requestUri, Dictionary<string, string> param)
        {
            using (var encodedContent = new FormUrlEncodedContent(param))
            {
                T result;
                try
                {
                    var response = await _httpClient.PostAsync(requestUri, encodedContent);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {                        
                        result = JsonConvert.DeserializeObject<T>(responseContent);
                    }
                    else
                    {
                        throw new Exception(responseContent);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message.ToString());
                    result = default(T);
                }

                return result;
            }
        }

        /// <summary>
        /// add query string to the Uri
        /// </summary>
        /// <param name="requestUri">base path uri</param>
        /// <param name="param">parameter</param>
        /// <returns></returns>
        private string AddQueryString(string requestUri, Dictionary<string, string> param)
        {
            string result = requestUri;
            result += "?";
            result += string.Join("&", param.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)));
            return result;
        }
        #endregion
    }
}
