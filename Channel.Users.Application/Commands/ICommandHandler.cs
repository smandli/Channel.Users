using System.Threading.Tasks;

namespace Channel.Users.Application.Commands
{
    public interface ICommandHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }
}
