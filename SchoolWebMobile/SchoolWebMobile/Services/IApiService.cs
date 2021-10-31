using SchoolWebMobile.Models;
using System.Threading.Tasks;

namespace SchoolWebMobile.Services
{
    public interface IApiService
    {
        Task<Response> GetTokenAsync(string urlBase, string controller, TokenRequest request);

        Task<Response> GetSingleResultAsync<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken);

        Task<Response> GetMultipleResultsAsync<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken);
    }
}
