using progi_test.Models;
using progi_test.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace progi_test.Utils
{
    public static class BidFeesUtility
    {
        public static decimal GetAssociationFeeAmount(decimal bidAmount)
        {
            decimal feeAmount = 0;

            foreach (AssociationFeeItem current in FeeConstants.ASSOCIATION_FEE_LIST.OrderBy(x => x.FeeAmount))
            {
                if (bidAmount > current.MinBidValue && bidAmount <= current.MaxBidValue)
                {
                    feeAmount = current.FeeAmount;
                    break;
                }
            }

            return feeAmount;
        }

        public static decimal GetBaseFeeAmount(decimal bidAmount)
        {
            decimal feeResult = bidAmount * (FeeConstants.BASE_PERCENTAGE / 100);

            if (feeResult < FeeConstants.MINIMUM_BASE_AMOUNT)
            {
                feeResult = FeeConstants.MINIMUM_BASE_AMOUNT;
            }
            else if (feeResult > FeeConstants.MAXIMUM_BASE_AMOUNT)
            {
                feeResult = FeeConstants.MAXIMUM_BASE_AMOUNT;
            }

            return feeResult;
        }

        public static decimal GetSupplierFee(decimal bidAmount)
        {
            decimal result = bidAmount * (FeeConstants.SUPPLIER_PERCENTAGE / 100);
            return result;
        }

        public static decimal GetStorageFee()
        {
            return FeeConstants.STORAGE_AMOUNT;
        }

        /// <summary>
        /// This method is used to find the bid amount based on the total cost and the previous found association fee.
        /// This is done by "trial and error". There are three possible scenarios: when the base fee is the minimum, when
        /// the base fee is the maximum, and when the base fee is a percentage of the bid. Then the total cost for each bid case is calculated.
        /// After that we should compare the hypothetical total cost for each of the 3 cases. We should compare those 3 cases with
        /// the initial total cost. We return the bid amount related to the total cost which is the same as the given one.
        /// </summary>
        /// <param name="totalCost"></param>
        /// <param name="associationFee"></param>
        /// <returns></returns>
        public static decimal GetBidByTotalCostAndAssociationFee(decimal totalCost, decimal associationFee)
        {
            decimal resultBid;

            decimal minimumBid = (totalCost - associationFee - FeeConstants.STORAGE_AMOUNT - FeeConstants.MINIMUM_BASE_AMOUNT) / (1 + (FeeConstants.SUPPLIER_PERCENTAGE / 100));
            decimal middleBid = (totalCost - associationFee - FeeConstants.STORAGE_AMOUNT) / (1 + (FeeConstants.BASE_PERCENTAGE / 100) + (FeeConstants.SUPPLIER_PERCENTAGE / 100));
            decimal maximumBid = (totalCost - associationFee - FeeConstants.STORAGE_AMOUNT - FeeConstants.MAXIMUM_BASE_AMOUNT) / (1 + (FeeConstants.SUPPLIER_PERCENTAGE / 100));

            decimal minimumTotalCost = InvoiceUtility.GetTotalCost(minimumBid);
            decimal middleTotalCost = InvoiceUtility.GetTotalCost(middleBid);
            decimal maximumTotalCost = InvoiceUtility.GetTotalCost(maximumBid);

            resultBid = minimumTotalCost == totalCost ? minimumBid : middleTotalCost == totalCost ? middleBid : maximumTotalCost == totalCost ? maximumBid : 0;

            return resultBid;
        }

        /// <summary>
        /// This method generates a dictionary which key is the association fee and the value is a tuple 
        /// with total costs based on the bid edge cases (min, max) related to the association fee.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<decimal, Tuple<decimal, decimal>> GetTotalCostRangesByAssociationFee()
        {
            Dictionary<decimal, Tuple<decimal, decimal>> rangesByAssociationFee = new Dictionary<decimal, Tuple<decimal, decimal>>();

            foreach (AssociationFeeItem aux in FeeConstants.ASSOCIATION_FEE_LIST.OrderBy(x => x.FeeAmount))
            {
                decimal totalCostWithMinBid = aux.MinBidValue == decimal.MinValue ? decimal.MinValue : InvoiceUtility.GetTotalCost(aux.MinBidValue);
                decimal totalCostWithMaxBid = aux.MaxBidValue == decimal.MaxValue ? decimal.MaxValue : InvoiceUtility.GetTotalCost(aux.MaxBidValue);

                rangesByAssociationFee.Add(aux.FeeAmount, new Tuple<decimal, decimal>(totalCostWithMinBid, totalCostWithMaxBid));
            }

            return rangesByAssociationFee;
        }

        public static decimal GetAssociationFeeByTotalCost(decimal totalCost)
        {
            decimal associationFee = 0;

            foreach (var rangeByAssociationFee in GetTotalCostRangesByAssociationFee())
            {
                if (totalCost > rangeByAssociationFee.Value.Item1 && totalCost <= rangeByAssociationFee.Value.Item2)
                {
                    associationFee = rangeByAssociationFee.Key;
                    break;
                }
            }

            return associationFee;
        }
    }
}
