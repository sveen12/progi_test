using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using progi_test.Models;
using progi_test.Services;
using System;

namespace progi_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private string USER = "user";
        private readonly IInvoiceService _invoiceService;

        public InvoiceController( IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("GetBidValue/{totalCost}")]
        public IActionResult GetBidValue(decimal totalCost)
        {
            (Invoice result, string message) = _invoiceService.GetInvoiceByTotalCost(totalCost, USER);

            if (result != null)
            {
                return Ok(new { invoice = result });
            }
            else
            {
                return BadRequest(new { error = message });
            }
        }

        [HttpGet("GetTotalcost/{bidValue}")]
        public IActionResult  GetTotalcost(decimal bidValue)
        {
            (Invoice result, string message) = _invoiceService.GetInvoiceByBidAmount(bidValue, USER);

            if(result != null)
            {
                return Ok(new { invoice = result });
            }
            else
            {
                return BadRequest(new { error = message });
            }
        }
    }
}
