using Model;

namespace BLL.Interfaces
{
    public interface IUserBusiness
    {
        bool Register(UserModel model);       
        string GenerateAndSendOtp(string email);
        bool VerifyOtpAndResetPassword(string email, string otpCode, string newPassword);
        UserModel Login(string email, string password);
        UserModel GetById(int userId);
    }
}