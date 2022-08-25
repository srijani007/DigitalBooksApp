using CommonSpace.DatabaseEntity;
using CommonSpace.Models;

namespace ReaderApi.Services
{
    public interface IReaderServices
    {
        LibraryContext libDbcontext { get; set; }

        List<Payment> GetAllPayments(PaymentDetailsbyEmail getPayment);
        string PurchaseBook(PaymentDetails paymentdetails);
    }
}