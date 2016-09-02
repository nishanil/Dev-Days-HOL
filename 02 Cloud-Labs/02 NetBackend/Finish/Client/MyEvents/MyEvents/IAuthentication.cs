using System.Threading.Tasks;

namespace MyEvents
{
    public interface IAuthentication
    {
        Task<bool> Authenticate();
    }
}
