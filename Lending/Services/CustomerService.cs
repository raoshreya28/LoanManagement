using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lending.Data;
using Lending.Models;
using Lending.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lending.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILoanApplicationRepository _loanApplicationRepository;

        public CustomerService(ICustomerRepository customerRepository, ILoanApplicationRepository loanApplicationRepository)
        {
            _customerRepository = customerRepository;
            _loanApplicationRepository = loanApplicationRepository;
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            return await _customerRepository.CreateAsync(customer);
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            return await _customerRepository.EditAsync(customer);
        }

        public async Task DeleteAsync(int customerId)
        {
            await _customerRepository.DeleteAsync(customerId);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer?> GetByIdAsync(int customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);
        }

        public async Task<IEnumerable<LoanApplication>> GetLoanApplicationsAsync(int customerId)
        {
            var allApplications = await _loanApplicationRepository.GetAllAsync();
            return allApplications.Where(app => app.CustomerId == customerId);
        }

    }
}