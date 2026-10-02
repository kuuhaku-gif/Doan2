using System;

namespace Model
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } 
        public string Membership { get; set; }
        public int Points { get; set; }
        public DateTime JoinedDate { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    // Yêu cầu gửi mã OTP
    public class SendOtpRequest
    {
        public string Email { get; set; }
    }

    // Yêu cầu xác thực OTP và đổi mật khẩu
    public class VerifyOtpRequest
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
        public string NewPassword { get; set; }
    }
}