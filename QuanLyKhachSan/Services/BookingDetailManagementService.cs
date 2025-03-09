using Microsoft.AspNetCore.Mvc;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace QuanLyKhachSan.Services
{
    public class BookingDetailManagementService
    {
        private readonly GenericFunction<BookingDetail> _bookingdR;
        private readonly ILogger<BookingDetailManagementService> _logger;

        public BookingDetailManagementService(GenericFunction<BookingDetail> bookingdR, ILogger<BookingDetailManagementService> logger)
        {
            _bookingdR = bookingdR ?? throw new ArgumentNullException(nameof(bookingdR));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task AddBookingDetailAsync(BookingDetail model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                var bookingd = new BookingDetail
                {
                    BookingID = model.BookingID,
                    RoomID = model.RoomID, //Lấy từ giao diện hoặc từ giao diện nhập admin
                    CheckInDate = model.CheckInDate, //nhập vào
                    CheckOutDate = model.CheckOutDate,
                    TotalPrice = model.TotalPrice
                };

                // Add the customer to the database
                await _bookingdR.AddAsync(bookingd);
            }
            catch (Exception ex)
            {
                // Log the error if any
                _logger.LogError(ex, "Error adding booking detail");
                throw; // Re-throw the exception after logging it
            }
        }

        public decimal CalculateTotalPrice(decimal pricePerDay, int days)
        {
            return pricePerDay * days; // Nhân giá mỗi ngày với số đêm ở
        }
    }
}
