using System;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = _userBusiness.Login(request.Email, request.Password);
                if (user == null)
                    return Unauthorized(new { success = false, message = "Email hoặc mật khẩu không chính xác!" });

                return Ok(new { success = true, message = "Đăng nhập thành công!", data = user });
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
    }
}