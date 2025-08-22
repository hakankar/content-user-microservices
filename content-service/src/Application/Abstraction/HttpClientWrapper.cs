using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Abstraction
{

    public class HttpClientWrapper : IHttpClientWrapper
    {

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
        private readonly string mediaType = "application/json";
        public HttpClientWrapper()
        {
        }
        public async Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default)
        {
            T? result = default(T)!;
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.SendAsync(requestMessage, cancellationToken).Result;
                await response.Content.ReadAsStringAsync(cancellationToken).ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    result = JsonSerializer.Deserialize<T>(x.Result, options);
                });
            }
            return result;
        }
        public async Task<T> GetAsync<T>(string url, TimeSpan timeSpan, CancellationToken cancellationToken = default)
        {
            T? result = default(T)!;
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(requestMessage, cancellationToken);
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                result = JsonSerializer.Deserialize<T>(content, options)!;
            }
            return result;
        }
        public async Task PostAsync(string url, object postObject, CancellationToken cancellationToken = default)
        {
            var payload = JsonSerializer.Serialize(postObject);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(payload, Encoding.UTF8, mediaType);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(requestMessage, cancellationToken);
            }
        }
        public async Task<T> PostAsync<T>(string url, object postObject, CancellationToken cancellationToken = default)
        {
            T? result = default(T)!;
            var payload = JsonSerializer.Serialize(postObject);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(payload, Encoding.UTF8, mediaType);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(requestMessage, cancellationToken);
                await response.Content.ReadAsStringAsync(cancellationToken).ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    result = JsonSerializer.Deserialize<T>(x.Result, options);
                });
            }
            return result;
        }
        public async Task<T> PutAsync<T>(string url, object postObject, CancellationToken cancellationToken = default)
        {
            T? result = default(T)!;
            var payload = JsonSerializer.Serialize(postObject);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
            requestMessage.Content = new StringContent(payload, Encoding.UTF8, mediaType);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(requestMessage, cancellationToken);
                await response.Content.ReadAsStringAsync(cancellationToken).ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    result = JsonSerializer.Deserialize<T>(x.Result, options);
                });
            }
            return result;
        }
        public async Task<T> DeleteAsync<T>(string url, object postObject, CancellationToken cancellationToken = default)
        {
            T? result = default(T)!;
            var payload = JsonSerializer.Serialize(postObject);
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, url);
            requestMessage.Content = new StringContent(payload, Encoding.UTF8, mediaType);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(requestMessage, cancellationToken);
                await response.Content.ReadAsStringAsync(cancellationToken).ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    result = JsonSerializer.Deserialize<T>(x.Result, options);
                });
            }
            return result;
        }
        public async Task<byte[]> GetFileAsync(string url, CancellationToken cancellationToken = default)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(requestMessage, cancellationToken);
                var fileBytes = await response.Content.ReadAsByteArrayAsync(cancellationToken);
                return fileBytes;
            }
        }
        public async Task<Stream> GetFileStreamAsync(string url, CancellationToken cancellationToken = default)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
                return stream;
            }
        }
    }

}
