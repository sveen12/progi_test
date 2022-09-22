using progi_test.Utils;

namespace progi_test.Models
{
    public class Invoice
    {
        public Invoice(Bid associatedBid)
        {
            AssociatedBid = associatedBid;

            BaseFee = BidFeesUtility.GetBaseFeeAmount(AssociatedBid.Amount);
            SupplierFee = BidFeesUtility.GetSupplierFee(AssociatedBid.Amount);
            AssociationFee = BidFeesUtility.GetAssociationFeeAmount(AssociatedBid.Amount);
            StorageFee = BidFeesUtility.GetStorageFee();
            TotalCost = InvoiceUtility.GetTotalCost(AssociatedBid.Amount, BaseFee, SupplierFee, AssociationFee, StorageFee);
        }

        public Bid AssociatedBid { get; private set; }

        public decimal BaseFee { get; private set; }

        public decimal SupplierFee {get; private set;  }

        public decimal AssociationFee { get; private set; }

        public decimal StorageFee { get; private set; }

        public decimal TotalCost { get; private set; }

    }
}
