using System.Threading;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IService<IServiceRequest, IServiceResponse>
    {
        Task<IServiceResponse> Execute(IServiceRequest request, CancellationToken cancellationToken);
    }
}
