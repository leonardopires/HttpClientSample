using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample.Core
{
    public interface IRepository<T>
    {
        Task<string> Create(T product);

        Task<T> Get(string id);

        Task<T> Update(T product);

        Task<int> Delete(string id);
    }
}
