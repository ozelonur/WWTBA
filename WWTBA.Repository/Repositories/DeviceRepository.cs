using Microsoft.EntityFrameworkCore;
using WWTBA.Core.Models;
using WWTBA.Core.Repositories;

namespace WWTBA.Repository.Repositories
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        private readonly AppDbContext _context;
        public DeviceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Device>> GetDevicesByUserIdAsync(int userId)
        {
            return await _context.Devices
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }

        public async Task<Device> GetDeviceByUserIdAndIdentifierAsync(int userId, string deviceIdentifier)
        {
            return await _context.Devices
                .FirstOrDefaultAsync(d => d.UserId == userId && d.DeviceIdentifier == deviceIdentifier);
        }

        public async Task UpdateVerificationCodeAsync(int deviceId, string verificationCode, DateTime verificationCodeSentAt)
        {
            Device device = await _context.Devices.FindAsync(deviceId);
            if(device != null)
            {
                device.VerificationCode = verificationCode;
                device.VerificationCodeSentAt = verificationCodeSentAt;
                await _context.SaveChangesAsync();
            }
        }
    }
}