using System;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace CinemaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowtimeController : ControllerBase
    {
        private readonly IShowtimeBusiness _showtimeBusiness;

        public ShowtimeController(IShowtimeBusiness showtimeBusiness)
        {
            _showtimeBusiness = showtimeBusiness;
        }

        // Khách hàng xem danh sách suất chiếu của phim
        [HttpGet("by-movie/{movieId}")]
        public IActionResult GetByMovie(int movieId)
        {
            try
            {
                var data = _showtimeBusiness.GetByMovieId(movieId);
                return Ok(new { success = true, data });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Xem thông tin 1 suất chiếu cụ thể
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var item = _showtimeBusiness.GetById(id);
                if (item == null)
                    return NotFound(new { success = false, message = "Không tìm thấy suất chiếu!" });

                return Ok(new { success = true, data = item });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // Quản trị viên thêm suất chiếu mới
        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] ShowtimeModel model)
        {
            try
            {
                var result = _showtimeBusiness.Create(model);
                if (result)
                {
                    return Ok(new { success = true, message = "Tạo suất chiếu thành công!" });
                }
                return BadRequest(new { success = false, message = "Tạo suất chiếu thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}