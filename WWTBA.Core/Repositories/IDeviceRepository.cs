using WWTBA.Core.Models;

namespace WWTBA.Core.Repositories
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Task<IEnumerable<Device>> GetDevicesByUserIdAsync(int userId);
        Task<Device> GetDeviceByUserIdAndIdentifierAsync(int userId, string deviceIdentifier);
        Task UpdateVerificationCodeAsync(int deviceId, string verificationCode, DateTime verificationCodeSentAt);
    }
}

