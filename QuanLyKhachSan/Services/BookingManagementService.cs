using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
namespace QuanLyKhachSan.Services
{
    public class BookingManagementService
    {
        private readonly GenericFunction<Booking> _bookingR;
        private readonly ILogger<BookingManagementService> _logger;

        public BookingManagementService(GenericFunction<Booking> bookingR, ILogger<BookingManagementService> logger)
        {
            _bookingR = bookingR ?? throw new ArgumentNullException(nameof(bookingR));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddBookingAsync(Booking model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                var bookingId = await GenerateBookingIdAsync();
                var booking = new Booking
                {
                    BookingID = bookingId,
                    EmployeeID = model.EmployeeID, // lấy mã từ giao diện bên nhân viên
                    CustomerID = model.CustomerID, // lấy mã từ giao diện bên khách hàng
                    NumGuests = model.NumGuests,
                    BookingTime = DateTime.Now,
                    Status = model.Status,
                    TotalPayment = model.TotalPayment,
                    CancelBookingTime = model.CancelBookingTime
                };

                // Add the customer to the database
                await _bookingR.AddAsync(booking);
            }
            catch (Exception ex)
            {
                // Log the error if any
                _logger.LogError(ex, "Error adding booking");
                throw; // Re-throw the exception after logging it
            }
        }

        public async Task<string> GenerateBookingIdAsync()
        {
            string prefix = "BO"; 
            var bookingCount = await _bookingR.CountAsync(); 

            // Generate new booking ID based on the count
            var newNumber = bookingCount + 1;
            string newBookingId = prefix + newNumber.ToString("D4"); // Ensure the number always has 3 digits

            return newBookingId;
        }
    }
}
