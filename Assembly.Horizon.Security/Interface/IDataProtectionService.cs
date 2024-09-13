using static Assembly.Horizon.Security.Services.DataProtectionService;

namespace Assembly.Horizon.Security.Interface;

public interface IDataProtectionService
{
    DataProtectionKeys Protect(string password);
    byte[] GetComputedHash(string password, byte[] salt);
}
