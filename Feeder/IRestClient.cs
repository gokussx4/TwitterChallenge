using System.Threading.Tasks;

namespace Feeder
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string relativeUrl);
    }
}