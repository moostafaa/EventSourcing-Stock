using System.Threading.Tasks;

namespace StockMarket.Domain.Common
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
} 