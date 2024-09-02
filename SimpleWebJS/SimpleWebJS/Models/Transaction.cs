﻿namespace OnlineBankPortal.Models
{

	public class Transaction
	{
		public int TransactionId { get; set; }
		public int AccountNumber { get; set; }
		public string TransactionType { get; set; }
		public decimal Amount { get; set; }

		public string Description { get; set; }
		public DateTime TransactionDate { get; set; }
	}
}
