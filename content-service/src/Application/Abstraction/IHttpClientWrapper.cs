using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction
{
    public interface IHttpClientWrapper
    {
        public Task<T> GetAsync<T>(string url, CancellationToken cancellationToken = default);
        public Task PostAsync(string url, object postObject, CancellationToken cancellationToken = default);
        public Task<T> PostAsync<T>(string url, object postObject, CancellationToken cancellationToken = default);
        public Task<T> PutAsync<T>(string url, object postObject, CancellationToken cancellationToken = default);
        public Task<T> DeleteAsync<T>(string url, object postObject, CancellationToken cancellationToken = default);
    }
}
