using progi_test.Models;

namespace progi_test.Services
{
    public interface IInvoiceService
    {
        (Invoice,string) GetInvoiceByBidAmount(decimal bidAmount, string user);
        (Invoice, string) GetInvoiceByTotalCost(decimal totalCost, string user);

    }
}
