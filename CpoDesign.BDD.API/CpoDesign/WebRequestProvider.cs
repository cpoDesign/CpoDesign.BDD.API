using System;
using System.Net.Http;

namespace CpoDesign.BDD.API.CpoDesign
{
    public class WebRequestProvider
    {
        public static WebRequestResult MakeRequest(string requestMethod, string url, Header[] header)
        {
            switch (requestMethod)
            {
                case "GET":
                    return ExecuteGetRequest(url, header);

                default:
                    return new WebRequestResult { Executed = false };
            }
        }

        private static WebRequestResult ExecuteGetRequest(string url, Header[] header)
        {
            var result = new WebRequestResult();
            using (var client = new HttpClient())
            {
                try
                {
                    var requestResult = client.GetAsync(url, completionOption: HttpCompletionOption.ResponseContentRead).GetAwaiter().GetResult();

                    result.Executed = true;
                    result.Response = requestResult.Headers;
                    result.Content = requestResult.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    result.Exception = ex;
                    result.Executed = false;
                }
            }

            return result;
        }

        //public async Task<T> GetData<T>(string url, List<KeyValuePair<string, string>> headers)
        //{
        //    var _httpClientFactory = new HttpClientFactory();

        //    try
        //    {
        //        var _client = _httpClientFactory.CreateClient();
        //        await PopulateHeaders(headers, _client);

        //        var response = await _client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
        //        PopulateCorrelation(response);

        //        _logger.LogDebug($"Request to: {url} resulted with status: {response.StatusCode} and reason phrase: {response.ReasonPhrase}");
        //        response.EnsureSuccessStatusCode();
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            _logger.LogError($"failed to retrieve result");
        //        }
        //        if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
        //        {
        //            return default(T);
        //        }

        //        var jsonString = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<T>(jsonString);
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        _logger.LogError($"An error occurred accessing function {url} {ex.Message}", ex);
        //        throw;
        //    }
        //}

        //public async Task<T> PostData<T>(string url, string userId, List<KeyValuePair<string, string>> headers, string content)
        //{
        //    _logger.LogDebug($"HttpClient: Using {url}");

        //    try
        //    {
        //        var _client = _httpClientFactory.CreateClient();
        //        await PopulateHeaders(headers, _client);

        //        var httpContent = new StringContent(content, Encoding.UTF8, MediaTypeApplicationJson);

        //        var response = await _client.PostAsync(url, httpContent);
        //        PopulateCorrelation(response);

        //        _logger.LogDebug($"Request to: {url} resulted with status: {response.StatusCode} and reason phrase: {response.ReasonPhrase}");
        //        response.EnsureSuccessStatusCode();
        //        if (!response.IsSuccessStatusCode)
        //        {
        //            _logger.LogError($"failed to retrieve result");
        //        }

        //        _logger.LogDebug("Attempting to parse response to provided object");

        //        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        _logger.LogError($"An error occurred accessing function {url} {ex.Message}", ex);
        //        throw;
        //    }
        //}

    }
}
