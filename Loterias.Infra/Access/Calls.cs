using Loterias.Infra.Configuration;
using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable RCS1090, RCS1213

namespace Loterias.Infra.Access
{
    public class Calls : ServiceConfiguration
    {
        public Calls(Uri baseEndpoint) : base(baseEndpoint) { }

        /// <summary>  
        /// Common method for making GET calls  
        /// </summary>
        /// <typeparam name="T"></typeparam>  
        public async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>  
        /// Common method for making POST calls  
        /// </summary>
        /// <typeparam name="T"></typeparam>  
        public async Task<ServiceMessage<T>> PostAsync<T>(Uri requestUrl, T content)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceMessage<T>>(data);
        }

        public async Task<ServiceMessage<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceMessage<T1>>(data);
        }

        public async Task<ServiceMessage<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 content, IFormFile formFile)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content, formFile));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceMessage<T1>>(data);
        }

        public async Task<ServiceMessage<T>> PutAsync<T>(Uri requestUrl, T content)
        {
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceMessage<T>>(data);
        }

        public async Task<ServiceMessage<T1>> PutAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ServiceMessage<T1>>(data);
        }

        public async Task<T> DeleteAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.DeleteAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        public Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(Endpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint)
            {
                Query = queryString
            };
            return uriBuilder.Uri;
        }

        public HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public MultipartFormDataContent CreateHttpContent<T>(T content, IFormFile formFile)
        {
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            var fileContent = new StreamContent(formFile.OpenReadStream());
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "fileToUpload",
                FileName = formFile.FileName
            };
            formDataContent.Add(jsonContent);
            formDataContent.Add(fileContent, "fileToUpload");
            return formDataContent;
        }

        public static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        private void AddHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove("userIP");
            _httpClient.DefaultRequestHeaders.Add("userIP", Environment.UserDomainName);
        }
    }
}
