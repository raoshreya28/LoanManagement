using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public interface IDocumentRepository
    {
        Task<Document> CreateAsync(Document document);
        Task<Document> EditAsync(Document document);
        Task DeleteAsync(int id);
        Task<IEnumerable<Document>> GetAllAsync();
        Task<Document?> GetByIdAsync(int id);
    }
}
