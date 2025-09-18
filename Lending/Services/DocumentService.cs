using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lending.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _context;

        public DocumentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Document> UploadAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<Document> UpdateAsync(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<IEnumerable<Document>> GetByCustomerAsync(int customerId)
        {
            return await _context.Documents
                                 .Where(d => d.CustomerId == customerId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetPendingVerificationAsync()
        {
            return await _context.Documents
                                 .Where(d => !d.IsVerified)
                                 .Include(d => d.Customer)
                                 .Include(d => d.LoanApplication)
                                 .ToListAsync();
        }

        public async Task VerifyDocumentAsync(int documentId)
        {
            var doc = await _context.Documents.FindAsync(documentId);
            if (doc != null)
            {
                doc.IsVerified = true;
                doc.VerifiedAt = System.DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}
