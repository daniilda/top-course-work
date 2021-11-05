using System;

namespace TopCourseWorkBl.DataLayer.Dto
{
    public class Transaction
    {
        public long CustomerId { get; set; }
        public DateTime DateTime { get; set; }
        public int TransactionMccCode { get; set; }
        public int TransactionType { get; set; }
        public int Amount { get; set; }
        public long TerminalId { get; set; }
    }
}