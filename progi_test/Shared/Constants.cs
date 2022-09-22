using progi_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace progi_test.Shared
{
     public static class FeeConstants
    {
        public const decimal STORAGE_AMOUNT = 100;
        public const decimal MINIMUM_BASE_AMOUNT = 10;
        public const decimal MAXIMUM_BASE_AMOUNT = 50;
        public const decimal BASE_PERCENTAGE = 10;
        public const decimal SUPPLIER_PERCENTAGE = 2;

        public static List<AssociationFeeItem> ASSOCIATION_FEE_LIST = new List<AssociationFeeItem>() {
            new AssociationFeeItem()
            {
                MinBidValue = decimal.MinValue,
                MaxBidValue = 0,
                FeeAmount = 0
            },
            new AssociationFeeItem()
            {
                MinBidValue = 0,
                MaxBidValue = 500,
                FeeAmount = 5
            },
            new AssociationFeeItem()
            {
                MinBidValue = 500,
                MaxBidValue = 1000,
                FeeAmount= 10
            },
            new AssociationFeeItem()
            {
                MinBidValue = 1000,
                MaxBidValue = 3000,
                FeeAmount = 15
            },
            new AssociationFeeItem()
            {
                MinBidValue = 3000,
                MaxBidValue = decimal.MaxValue,
                FeeAmount = 20
            },
        };
    }
}
