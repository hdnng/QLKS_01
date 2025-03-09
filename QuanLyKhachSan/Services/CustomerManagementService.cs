using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace QuanLyKhachSan.Services
{
    public class CustomerManagementService
    {
        private readonly GenericFunction<Customer> _customerR;
        private readonly ILogger<CustomerManagementService> _logger;

        public CustomerManagementService(GenericFunction<Customer> customerR, ILogger<CustomerManagementService> logger)
        {
            _customerR = customerR ?? throw new ArgumentNullException(nameof(customerR));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddCustomerAsync(Customer model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                // Generate a new CustomerID for the customer
                var customerId = await GenerateCustomerIdAsync();

                var customer = new Customer
                {
                    CustomerID = customerId,
                    UserID = model.UserID,
                    FullName = model.FullName,
                    Birthday = model.Birthday,
                    CCCD = model.CCCD,
                    Gmail = model.Gmail,
                    PhoneNumber = model.PhoneNumber
                };

                // Add the customer to the database
                await _customerR.AddAsync(customer);
            }
            catch (Exception ex)
            {
                // Log the error if any
                _logger.LogError(ex, "Error adding customer");
                throw; // Re-throw the exception after logging it
            }
        }

        public async Task<string> GenerateCustomerIdAsync()
        {
            string prefix = "CS"; // Prefix for customer ID
            var customerCount = await _customerR.CountAsync(); // Count the number of customers in the database

            // Generate new customer ID based on the count
            var newNumber = customerCount + 1;
            string newCustomerId = prefix + newNumber.ToString("D3"); // Ensure the number always has 3 digits

            return newCustomerId;
        }
    }
}
