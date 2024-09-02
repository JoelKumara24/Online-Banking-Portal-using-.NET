using System.Data.SQLite;
using OnlineBankPortal.Models;

namespace OnlineBankPortal.Data
{
	public class TransactionManager
	{
		private static string connectionString = "Data Source=mydatabase.db;Version=3;";

		public static bool CreateTable()
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = @"
                        CREATE TABLE Transactions (
                            TransactionId INTEGER PRIMARY KEY,
                            AccountNumber INT NOT NULL,
                            TransactionType VARCHAR(10) NOT NULL,  -- 'Deposit' or 'Withdrawal'
                            Amount DECIMAL(10, 2) NOT NULL,
                            Description TEXT,
                            TransactionDate DATETIME NOT NULL,
                            FOREIGN KEY (AccountNumber) REFERENCES Accounts(AccountNumber)
                        )";
						command.ExecuteNonQuery();
					}

					connection.Close();
				}

				Console.WriteLine("Transactions table created successfully.");
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
				return false;
			}
		}

		public static bool Deposit(int accountNumber, decimal amount, string description)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						// Update the account balance by deposit amount
						command.CommandText = @"
                UPDATE Accounts
                SET Balance = Balance + @Amount
                WHERE AccountNumber = @AccountNumber";
						command.Parameters.AddWithValue("@AccountNumber", accountNumber);
						command.Parameters.AddWithValue("@Amount", amount);
						int rowsUpdated = command.ExecuteNonQuery();

						// Generate a description for the deposit
					//	string description = $"Deposit to account {accountNumber}";

						// Insert a transaction record for the deposit with the description
						command.CommandText = @"
                INSERT INTO Transactions (AccountNumber, TransactionType, Amount, Description, TransactionDate)
                VALUES (@AccountNumber, 'Deposit', @Amount, @Description, @TransactionDate)";
						command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
						command.Parameters.AddWithValue("@Description", description);
						int rowsInserted = command.ExecuteNonQuery();

						connection.Close();

						// Check if both the account balance update and transaction insertion were successful
						if (rowsUpdated > 0 && rowsInserted > 0)
						{
							return true;
						}
					}

					connection.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}

			return false;
		}


		public static bool Withdraw(int accountNumber, decimal amount, string description)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						// Update the account balance by withdrawal amount
						command.CommandText = @"
                UPDATE Accounts
                SET Balance = Balance - @Amount
                WHERE AccountNumber = @AccountNumber";
						command.Parameters.AddWithValue("@AccountNumber", accountNumber);
						command.Parameters.AddWithValue("@Amount", amount);
						int rowsUpdated = command.ExecuteNonQuery();

						// Generate a description for the withdrawal
						//string description = $"Withdrawal from account {accountNumber}";

						// Insert a transaction record for the withdrawal with the description
						command.CommandText = @"
                INSERT INTO Transactions (AccountNumber, TransactionType, Amount, Description, TransactionDate)
                VALUES (@AccountNumber, 'Withdrawal', @Amount, @Description, @TransactionDate)";
						command.Parameters.AddWithValue("@TransactionDate", DateTime.Now);
						command.Parameters.AddWithValue("@Description", description);
						int rowsInserted = command.ExecuteNonQuery();

						connection.Close();

						// Check if both the account balance update and transaction insertion were successful
						if (rowsUpdated > 0 && rowsInserted > 0)
						{
							return true;
						}
					}

					connection.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}

			return false;
		}


		public static List<Transaction> GetTransactionsByAccountNumber(int accountNumber)
		{
			List<Transaction> transactions = new List<Transaction>();

			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = "SELECT * FROM Transactions WHERE AccountNumber = @AccountNumber";
						command.Parameters.AddWithValue("@AccountNumber", accountNumber);

						using (SQLiteDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								transactions.Add(new Transaction
								{
									TransactionId = Convert.ToInt32(reader["TransactionId"]),
									AccountNumber = Convert.ToInt32(reader["AccountNumber"]),
									TransactionType = reader["TransactionType"].ToString(),
									Amount = Convert.ToDecimal(reader["Amount"]),
									TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
									Description = reader["Description"].ToString()
								});
							}
						}
					}

					connection.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}

			return transactions;
		}

		public static bool CreateTransaction(Transaction transaction)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = @"
                    INSERT INTO Transactions (AccountNumber, TransactionType, Amount, TransactionDate, Description)
                    VALUES (@AccountNumber, @TransactionType, @Amount, @TransactionDate, @Description)";

						command.Parameters.AddWithValue("@AccountNumber", transaction.AccountNumber);
						command.Parameters.AddWithValue("@TransactionType", transaction.TransactionType);
						command.Parameters.AddWithValue("@Amount", transaction.Amount);
						command.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
						command.Parameters.AddWithValue("@Description", transaction.Description);

						int rowsInserted = command.ExecuteNonQuery();

						if (rowsInserted > 0)
						{
							// Update the account balance here
							// You need to retrieve the current balance of the account, apply the transaction,
							// and update the account's balance accordingly

							connection.Close();
							return true;
						}
					}

					connection.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}

			return false;
		}


		public static List<Transaction> GetAllTransactions()
		{
			List<Transaction> transactions = new List<Transaction>();

			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = "SELECT * FROM Transactions";

						using (SQLiteDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								Transaction transaction = new Transaction
								{
									TransactionId = Convert.ToInt32(reader["TransactionId"]),
									AccountNumber = reader["AccountNumber"] == DBNull.Value ? 0 : Convert.ToInt32(reader["AccountNumber"]),
									TransactionType = reader["TransactionType"].ToString(),
									Amount = reader["Amount"] == DBNull.Value ? 0.0m : Convert.ToDecimal(reader["Amount"]),
									TransactionDate = reader["TransactionDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["TransactionDate"]),
									Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : ""
								};

								transactions.Add(transaction);
							}
						}
					}

					connection.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}

			return transactions;
		}

	}

}
