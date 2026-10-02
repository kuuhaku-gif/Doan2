using BLL.Interfaces;
using DAL.Interfaces;
using Model;

namespace BLL
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _res;

        public UserBusiness(IUserRepository res)
        {
            _res = res;
        }

        public bool Register(UserModel model)
        {
            return _res.Register(model);
        }

        public UserModel Login(string email, string password)
        {
            return _res.Login(email, password);
        }

        public UserModel GetById(int userId)
        {
            return _res.GetById(userId);
        }
        public string GenerateAndSendOtp(string email)
        {
            // Sinh ngẫu nhiên mã số từ 100000 đến 999999
            string otp = new Random().Next(100000, 999999).ToString();

            bool isSaved = _res.SaveOtp(email, otp);
            if (isSaved)
            {
                // Ghi chú: Nếu tích hợp dịch vụ gửi Email (như MailKit/SMTP), bạn gọi gửi mail ở đây
                return otp;
            }

            return null;
        }

        public bool VerifyOtpAndResetPassword(string email, string otpCode, string newPassword)
        {
            return _res.VerifyOtpAndResetPassword(email, otpCode, newPassword);
        }
    }
}