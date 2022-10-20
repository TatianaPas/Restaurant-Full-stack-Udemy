using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Utilities
{
    public static class SD
    {
        public const string ManagerRole = "Manager";
        public const string FrontDeskRole = "Front";
        public const string KitcheRole = "Kitchen";
        public const string CustomerRole = "Customer";

        public const string StatusPending = "Pending_Payment";
        public const string StatusSubmitted = "Submitted_PaymentApproved";
        public const string StatusRejected = "Rejected_Payment";
        public const string StatusInProcess = "Being Prepared";
        public const string StatusReady = "Ready For Pick Up";
        public const string StatusCompleted = "Completed";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        public const string SessisonCart = "SessisonCart";
    }
}
