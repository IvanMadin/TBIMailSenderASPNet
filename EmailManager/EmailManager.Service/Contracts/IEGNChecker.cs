using System.Threading.Tasks;

namespace EmailManager.Service.Contracts
{
    public interface IEGNChecker
    {
        Task<bool> IsRealAsync(string egn);
    }
}