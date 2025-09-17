using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;

namespace Lending.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;
        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {
            return await _context.Documents
                                 .Include(d => d.Customer)
                                 .Include(d => d.LoanApplication)
                                 .ToListAsync();
        }

        public async Task<Document?> GetByIdAsync(int id)
        {
            return await _context.Documents
                                 .Include(d => d.Customer)
                                 .Include(d => d.LoanApplication)
                                 .FirstOrDefaultAsync(d => d.DocumentId == id);
        }

        public async Task<Document> CreateAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<Document> EditAsync(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Documents.FirstOrDefaultAsync(d => d.DocumentId == id);
            if (existing != null)
            {
                _context.Documents.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
