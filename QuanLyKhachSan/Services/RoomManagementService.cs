using QuanLyKhachSan.Models;
using QuanLyKhachSan.Repositories;

namespace QuanLyKhachSan.Services
{
    public class RoomManagementService
    {
        private readonly GenericFunction<Room> _roomRepository;
        private readonly ILogger<RoomManagementService> _logger;

        public RoomManagementService(GenericFunction<Room> roomRepository, ILogger<RoomManagementService> logger)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Thêm một phòng mới
        public async Task AddRoomAsync(Room model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                // Generate a new RoomID for the room
                var roomId = await GenerateRoomIdAsync();

                var room = new Room
                {
                    RoomID = roomId,
                    RoomTypeID = model.RoomTypeID,
                    Description = model.Description,
                    Capacity = model.Capacity,
                    IMGUrl = model.IMGUrl,
                    Status = model.Status,
                    PricePerDay = model.PricePerDay
                };

                // Thêm phòng vào cơ sở dữ liệu
                await _roomRepository.AddAsync(room);
            }
            catch (Exception ex)
            {
                // Log the error if any
                _logger.LogError(ex, "Error adding room");
                throw; // Re-throw the exception after logging it
            }
        }

        // Tạo RoomID mới dựa trên số lượng phòng hiện tại
        public async Task<string> GenerateRoomIdAsync()
        {
            string prefix = "RM";
            var roomCount = await _roomRepository.CountAsync(); // Đếm số phòng hiện có trong cơ sở dữ liệu

            var newNumber = roomCount + 1;
            string newRoomId = prefix + newNumber.ToString("D3"); // Đảm bảo rằng số luôn có 3 chữ số

            return newRoomId;
        }

    }
}
