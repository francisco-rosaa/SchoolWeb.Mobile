using SchoolWebMobile.Models;
using System.Threading.Tasks;

namespace SchoolWebMobile.Services
{
    public interface IApiService
    {
        Task<Response> GetTokenAsync(string urlBase, string controller, TokenRequest request);
    }
}
