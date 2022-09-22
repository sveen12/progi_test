using progi_test.Models;
using progi_test.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace progi_test.Utils
{
    public static class InvoiceUtility
    {
        public static decimal GetTotalCost(decimal bidAmount)
        {
            return CalculateTotalCost(
                bidAmount, 
                BidFeesUtility.GetBaseFeeAmount(bidAmount),
                 BidFeesUtility.GetSupplierFee(bidAmount),
                 BidFeesUtility.GetAssociationFeeAmount(bidAmount),
                 BidFeesUtility.GetStorageFee()
                );
        }

        public static decimal GetTotalCost(decimal bidAmount, decimal baseFee, decimal supplierFee, decimal associationFee, decimal storageFee)
        {
            return CalculateTotalCost(bidAmount, baseFee, supplierFee, associationFee, storageFee);
        }

        private static decimal CalculateTotalCost(decimal bidAmount, decimal baseFee, decimal supplierFee, decimal associationFee, decimal storageFee)
        {
            decimal total = 0;

            if (bidAmount != 0)
            {
                total = bidAmount + baseFee + supplierFee + associationFee + storageFee;
            }

            return total;
        }
    }
}
