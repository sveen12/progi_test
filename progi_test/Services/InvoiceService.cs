using progi_test.Models;
using progi_test.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progi_test.Services
{
    public class InvoiceService : IInvoiceService
    {
        public (Invoice, string) GetInvoiceByBidAmount(decimal bidAmount, string user)
        {
            if (bidAmount < 1)
            {
                return (null, "The given bid should be greater than 1.");
            }

            Bid bid = new Bid(bidAmount, user);
            Invoice invoice = new Invoice(bid);

            return (invoice, null);
        }

        public (Invoice, string) GetInvoiceByTotalCost(decimal totalCost, string user)
        {
            if (totalCost < 1)
            {
                return (null, "The given total cost should be greater than or equal to 1.");
            }

            //Firs the association fee is calculated based on the total cost
            decimal associationFee = BidFeesUtility.GetAssociationFeeByTotalCost(totalCost);

            //With the found association fee it begins a trial and error process to determine the base fee, then the bid
            decimal calculatedBid = BidFeesUtility.GetBidByTotalCostAndAssociationFee(totalCost, associationFee);

            Bid bid = new Bid(calculatedBid, user);
            Invoice invoice = new Invoice(bid);

            if (invoice.TotalCost == 0)
            {
                return (null, "The given total cost doesn't generate a bid amount greater than or equal to 1.");
            }
            else if (invoice.AssociatedBid.Amount < 1)
            {
                return (null, "The given total cost doesn't generate a bid amount greater than or equal to 1.");
            }

            return (invoice, null);
        }
    }
}
