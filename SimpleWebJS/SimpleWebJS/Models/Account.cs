﻿namespace OnlineBankPortal.Models
{
	public class Account
	{
		public int AccountNumber { get; set; }
		public string AccountHolderName { get; set; }
		public decimal Balance { get; set; }
		public int UserId { get; set; }
	}

}
