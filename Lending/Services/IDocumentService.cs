using Lending.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Services
{
    public interface IDocumentService
    {
        Task<Document> UploadAsync(Document document);
        Task<Document> UpdateAsync(Document document);
        Task<IEnumerable<Document>> GetByCustomerAsync(int customerId);
        Task<IEnumerable<Document>> GetPendingVerificationAsync();
        Task VerifyDocumentAsync(int documentId);
    }
}
