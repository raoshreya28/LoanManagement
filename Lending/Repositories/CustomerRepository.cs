using Lending.Data;
using Lending.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lending.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                                 .Include(c => c.LoanApplications)
                                 .Include(c => c.Documents) // Added this line
                                 .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                                 .Include(c => c.LoanApplications)
                                 .Include(c => c.Documents) // Added this line
                                 .FirstOrDefaultAsync(c => c.UserId == id);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> EditAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == id);
            if (existing != null)
            {
                _context.Customers.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}