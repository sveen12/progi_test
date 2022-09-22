using NUnit.Framework;
using progi_test.Models;
using progi_test.Services;
using progi_test.Shared;
using System.Linq;

namespace progi_test.tests
{
    public class Tests
    {
        private InvoiceService _invoiceService;
        private readonly string USER = "test_user";

        [SetUp]
        public void SetUp()
        {
            _invoiceService = new InvoiceService();
        }
        
        //Excersice mandatory tests
        [Test]
        public void GetInvoiceByBidAmount_BaseCaseWhileBidIsGiven()
        {
            decimal bidAmount = 1000;

            (Invoice result, _) = _invoiceService.GetInvoiceByBidAmount(bidAmount, USER);

            Assert.AreEqual(1180, result.TotalCost, "With 1000 as bid the algorithm shall return 1180 as total cost.");
            Assert.AreEqual(bidAmount, result.AssociatedBid.Amount, "Bid amount after process should be the same.");

        }

        [Test]
        public void GetInvoiceByBidAmount_BaseCaseWhileTotalCostIsGiven()
        {
            decimal totalCost = 1180;

            (Invoice result, _) = _invoiceService.GetInvoiceByTotalCost(totalCost, USER);

            Assert.AreEqual(1000, result.AssociatedBid.Amount, "With 1180 as total cost the algorithm shall return 1000 as bid amount.");
            Assert.AreEqual(totalCost, result.TotalCost, "Total costafter process should be the same.");
        }

        [Test]
        public void GetInvoiceByTotalCost_ComparedWithGetInvoiceByBidAmount()
        {
            var bidAmount = 1000;
            var totalCost = 1180;

            (Invoice result1, _) = _invoiceService.GetInvoiceByTotalCost(totalCost, USER);
            (Invoice result2, _) = _invoiceService.GetInvoiceByBidAmount(bidAmount, USER);

            Assert.AreEqual(result1.AssociatedBid.Amount, bidAmount, "Bid amount should be the same as the var.");
            Assert.AreEqual(result1.TotalCost, totalCost, "Total cost should be the same as the var.");

            Assert.AreEqual(result1.AssociatedBid.Amount, result2.AssociatedBid.Amount, "Bid amount should be the same at both results.");
            Assert.AreEqual(result1.TotalCost, result2.TotalCost, "Total cost should be the same at both results.");
        }


        //Additional tests in order to verify the algorithm
        [Test]
        public void GetInvoiceByBidAmount_BidEqualsZero()
        {
            (Invoice result, string message) = _invoiceService.GetInvoiceByBidAmount(0, USER);

            Assert.IsNull(result);
            Assert.AreEqual("The given bid should be greater than 1.", message);
        }

        [Test]
        public void GetInvoiceByTotalCost_TotalCostEqualsZero()
        {
            (Invoice result, string message) = _invoiceService.GetInvoiceByTotalCost(0, USER);

            Assert.IsNull(result);
            Assert.AreEqual("The given total cost should be greater than or equal to 1.", message);
        }


        [Test]
        public void GetInvoiceByBidAmount_ComparedWithGetInvoiceByTotalCost()
        {
            var maxBidPerAssociationFee = FeeConstants.ASSOCIATION_FEE_LIST.OrderBy(x => x.FeeAmount).Last().MinBidValue + 500;

            for (decimal bidAmount = 1; bidAmount <= maxBidPerAssociationFee; bidAmount += 5)
            {

                (Invoice result1, _) = _invoiceService.GetInvoiceByBidAmount(bidAmount, USER);
                (Invoice result2, _) = _invoiceService.GetInvoiceByTotalCost(result1.TotalCost, USER);


                Assert.AreEqual(bidAmount, result1.AssociatedBid.Amount, "Bid amount of result 1 should be the same as var.");
                Assert.AreEqual(bidAmount, result2.AssociatedBid.Amount, "Bid amount of result 2 should be the same as var.");

                Assert.AreEqual(result1.TotalCost, result2.TotalCost, "Total cost should be the same at both results.");
            }
        }
    }
}