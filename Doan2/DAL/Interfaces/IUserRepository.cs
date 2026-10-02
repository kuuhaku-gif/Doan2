using Model;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        bool Register(UserModel model);
        bool SaveOtp(string email, string otpCode);
        bool VerifyOtpAndResetPassword(string email, string otpCode, string newPassword);
        UserModel Login(string email, string password);
        UserModel GetById(int userId);
    }
}