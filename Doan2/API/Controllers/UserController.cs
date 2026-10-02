using API.Helper;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserBusiness _userBusiness;
        public UserController(IUserBusiness userBusiness, IConfiguration config)
        {
            _userBusiness = userBusiness;
            _config = config; // Gán giá trị vào biến
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = _userBusiness.Login(request.Email, request.Password);
                if (user == null)
                    return Unauthorized(new { success = false, message = "Email hoặc mật khẩu không chính xác!" });

                // 4. Sử dụng _config để truyền vào hàm sinh Token
                var token = JwtHelper.GenerateToken(user, _config);

                return Ok(new
                {
                    success = true,
                    message = "Đăng nhập thành công!",
                    token = token,
                    data = user
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

     
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel model)
        {
            try
            {
                var res = _userBusiness.Register(model);
                return Ok(new { success = res, message = "Đăng ký tài khoản thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        
        [HttpGet("profile/{userId}")]
        public IActionResult GetProfile(int userId)
        {
            try
            {
                var user = _userBusiness.GetById(userId);
                if (user == null)
                    return NotFound(new { success = false, message = "Không tìm thấy người dùng!" });

                return Ok(new { success = true, data = user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

        }
        // 1. API: Gửi/Tạo mã OTP
        [HttpPost("send-otp")]
        public IActionResult SendOtp([FromBody] SendOtpRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                    return BadRequest(new { success = false, message = "Email không được để trống!" });

                var otpCode = _userBusiness.GenerateAndSendOtp(request.Email);

                if (!string.IsNullOrEmpty(otpCode))
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Mã OTP đã được tạo (hết hạn trong 5 phút)!",
                        otpCode = otpCode // Trả về mã này để test trên Swagger/Postman
                    });
                }

                return BadRequest(new { success = false, message = "Không thể tạo mã OTP!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // 2. API: Xác thực OTP và đặt mật khẩu mới
        [HttpPost("verify-otp-reset-password")]
        public IActionResult VerifyOtpResetPassword([FromBody] VerifyOtpRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.OtpCode) || string.IsNullOrEmpty(request.NewPassword))
                    return BadRequest(new { success = false, message = "Vui lòng nhập đầy đủ Email, mã OTP và mật khẩu mới!" });

                var res = _userBusiness.VerifyOtpAndResetPassword(request.Email, request.OtpCode, request.NewPassword);

                return Ok(new
                {
                    success = res,
                    message = "Xác thực OTP thành công! Mật khẩu mới đã được cập nhật."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}