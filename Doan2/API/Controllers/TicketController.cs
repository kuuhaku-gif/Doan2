using System;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
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
                var res = _ticketBusiness.CreateTicket(model);
                return Ok(new { success = res, message = "Đặt vé thành công!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}