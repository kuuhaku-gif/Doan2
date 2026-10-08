using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketBusiness _ticketBusiness;

        public TicketController(ITicketBusiness ticketBusiness)
        {
            _ticketBusiness = ticketBusiness;
        }

        [HttpGet("history/{userId}")]
        public IActionResult GetHistory(int userId)
        {
            try
            {
                var res = _ticketBusiness.GetTicketsByUser(userId);
                return Ok(new { success = true, data = res });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("book-ticket")]
        public IActionResult BookTicket([FromBody] TicketModel model)
        {
            try
            {
                // Lấy UserId từ Claims của Token đang đăng nhập nếu client không truyền
                // var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                // if (int.TryParse(userIdClaim, out int uid)) model.UserId = uid;

                var result = _ticketBusiness.CreateTicket(model);

                if (result)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Đặt vé thành công!",
                        ticketCode = model.TicketCode // Trả lại mã vừa sinh
                    });
                }

                return BadRequest(new { success = false, message = "Đặt vé thất bại!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("checkin/{ticketCode}")]
        public IActionResult CheckInTicket(string ticketCode)
        {
            try
            {
                // Gọi BLL -> DAL thực thi sp_ticket_checkin
                var result = _ticketBusiness.CheckInTicket(ticketCode);
                return Ok(new { success = true, message = "Soát vé thành công! Mời khách vào phòng chiếu." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet("booked-seats")]
        public IActionResult GetBookedSeats([FromQuery] int movieId, [FromQuery] string cinemaRoom, [FromQuery] DateTime showtime)
        {
            try
            {
                // Gọi BLL -> DAL để lấy danh sách chuỗi ghế đã đặt
                var bookedSeats = _ticketBusiness.GetBookedSeats(movieId, cinemaRoom, showtime);
                return Ok(new { success = true, data = bookedSeats });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}