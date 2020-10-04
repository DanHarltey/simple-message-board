using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageBoard.Data
{
    public interface IMessages
    {
        Task AddMessage(string messageText);

        Task<IEnumerable<string>> Get();
    }
}
