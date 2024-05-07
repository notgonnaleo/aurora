using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Enums.Payments
{
    public enum PaymentsEnum
    {
        Pending = 1, 
        Declined = 2,
        Confirmed = 3, 
        Failed = 4,
        Refunded = 5,
    }

    public enum PaymentTypesEnum
    {
        Credit = 1,
        Debit = 2,
        BankSlip = 3,
        PIX = 4,
    }
}
