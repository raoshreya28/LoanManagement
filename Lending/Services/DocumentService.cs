using Lending.Data;
using Lending.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lending.Repositories;

namespace Lending.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<Document> UploadAsync(Document document)
        {
            return await _documentRepository.CreateAsync(document);
        }

        public async Task<Document> UpdateAsync(Document document)
        {
            return await _documentRepository.EditAsync(document);
        }

        public async Task<IEnumerable<Document>> GetByCustomerAsync(int customerId)
        {
            var allDocuments = await _documentRepository.GetAllAsync();
            return allDocuments.Where(d => d.CustomerId == customerId);
        }

        public async Task<IEnumerable<Document>> GetPendingVerificationAsync()
        {
            var allDocuments = await _documentRepository.GetAllAsync();
            return allDocuments.Where(d => !d.IsVerified);
        }

        public async Task VerifyDocumentAsync(int documentId)
        {
            var doc = await _documentRepository.GetByIdAsync(documentId);
            if (doc != null)
            {
                doc.IsVerified = true;
                doc.VerifiedAt = System.DateTime.UtcNow;
                await _documentRepository.EditAsync(doc);
            }
        }
    }
}