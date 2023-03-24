using SovosWebProject.Models;
using System.Threading.Tasks;

namespace SovosWebProject.Services
{
    public interface IApiService
    {
        Task<List<Category>> ListAll();
        Task<Category> GetById(int id);

        Task<bool> Create(Category objeto);

        Task<bool> Update(Category objeto);

        Task<bool> Delete(int id);
    }
}
