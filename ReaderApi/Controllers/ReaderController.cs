using BookApi.Services;
using CommonSpace.DatabaseEntity;
using CommonSpace.Models;
using Microsoft.AspNetCore.Mvc;
using ReaderApi.Services;

namespace ReaderApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReaderController : Controller
    {
        private readonly IReaderServices _readerServices;
        private readonly IBookServices _bookServices;

        public ReaderController(IReaderServices readerServices, IBookServices bookServices)
        {
            _readerServices = readerServices;
            _bookServices = bookServices;

        }

        [HttpGet("BasedonCriteria")]
        public ActionResult<List<Book>> SearchBookcriteria([FromBody] SearchBooks searchbooks)
        {
            try
            {
                var currentbook = _bookServices.SearchBookbyConditions(searchbooks);
                if (currentbook != null)
                {
                    return Ok(new { currentbook });
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("PurchaseBook")]
        public ActionResult<List<Payment>> MakePayment([FromBody] PaymentDetails payment)
        {
            try
            {
                var result = _readerServices.PurchaseBook(payment);
                if (result == null)
                {
                   return BadRequest();
                }
                else
                {
                    return Ok(new { result });
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        [HttpPost("PaymentbyEmail")]
        public ActionResult<string> GetallpaymentbyEmail([FromBody] PaymentDetailsbyEmail getPaymentDetails)
        {
            try
            {
                var result = _readerServices.GetAllPayments(getPaymentDetails);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
