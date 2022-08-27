using CommonSpace.ApplicationVariables;
using CommonSpace.DatabaseEntity;
using CommonSpace.Models;

namespace ReaderApi.Services
{
    public class ReaderServices : IReaderServices
    {
        public LibraryContext libDbcontext { get; set; }

        public ReaderServices(LibraryContext dbcontext)
        {
            libDbcontext = dbcontext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="payment"></param>
        /// <returns>Adding the payments</returns>
        /// <exception cref="Exception"></exception>

        public string PurchaseBook(PaymentDetails paymentdetails)
        {
            try
            {
                var details = libDbcontext.Payments.Where(p => p.BuyerEmail == paymentdetails.BuyerEmail && p.BookId == paymentdetails.BookId).Any(p=>p.BuyerEmail==paymentdetails.BuyerEmail && p.BookId==paymentdetails.BookId);
                if (details == true)
                {
                    return AppVariables.bookAlreadyBought;
                }
                else
                {
                    var payment = new Payment();
                    payment.BuyerName = paymentdetails.BuyerName;
                    payment.BuyerEmail = paymentdetails.BuyerEmail;
                    payment.BookId = paymentdetails.BookId;
                    payment.Price = paymentdetails.Price;
                    payment.Logo = paymentdetails.Logo;
                    payment.Title = paymentdetails.Title;
                    payment.PaymentDate = DateTime.UtcNow;
                    libDbcontext.Payments.Add(payment);
                    libDbcontext.SaveChanges();
                    return AppVariables.paymentSuccessful;
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="getPayment"></param>
        /// <returns>to get\see all the payments by emailid</returns>
        /// <exception cref="Exception"></exception>

        public List<Payment> GetAllPayments(PaymentDetailsbyEmail getPayment)
        {
            try
            {

                var result = libDbcontext.Payments.Where(p => p.BuyerEmail == getPayment.BuyerEmail).ToList();
                return result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
