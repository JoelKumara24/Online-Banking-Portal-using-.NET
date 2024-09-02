using OnlineBankPortal.Models;
using System.Data.SQLite;

namespace OnlineBankPortal.Data
{
	public class AccountManager
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
                        CREATE TABLE IF NOT EXISTS Accounts (
                            AccountNumber INT PRIMARY KEY,
                            AccountHolderName VARCHAR(255) NOT NULL,
                            Balance DECIMAL(10, 2) NOT NULL,
                            UserId INT,
                            FOREIGN KEY (AccountNumber) REFERENCES UserProfiles(AccountNumber),
                            FOREIGN KEY (UserId) REFERENCES UserProfiles(UserId)
                        )";
						command.ExecuteNonQuery();
					}

					connection.Close();
				}

				Console.WriteLine("Accounts table created successfully.");
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
				return false;
			}
		}

		public static bool CreateAccount(Account account)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = @"
                        INSERT INTO Accounts (AccountNumber, AccountHolderName, Balance, UserId)
                        VALUES (@AccountNumber, @AccountHolderName, @Balance, @UserId)";

						command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber);
						command.Parameters.AddWithValue("@AccountHolderName", account.AccountHolderName);
						command.Parameters.AddWithValue("@Balance", account.Balance);
						command.Parameters.AddWithValue("@UserId", account.UserId);

						int rowsInserted = command.ExecuteNonQuery();

						connection.Close();

						if (rowsInserted > 0)
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

		public static Account GetAccountByAccountNumber(int accountNumber)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = "SELECT * FROM Accounts WHERE AccountNumber = @AccountNumber";
						command.Parameters.AddWithValue("@AccountNumber", accountNumber);

						using (SQLiteDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								return new Account
								{
									AccountNumber = Convert.ToInt32(reader["AccountNumber"]),
									AccountHolderName = reader["AccountHolderName"].ToString(),
									Balance = Convert.ToDecimal(reader["Balance"]),
									UserId = Convert.ToInt32(reader["UserId"])
								};
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

			return null;
		}

		public static bool UpdateAccount(Account updatedAccount)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = @"
                        UPDATE Accounts
                        SET AccountHolderName = @AccountHolderName, Balance = @Balance, UserId = @UserId
                        WHERE AccountNumber = @AccountNumber";

						command.Parameters.AddWithValue("@AccountNumber", updatedAccount.AccountNumber);
						command.Parameters.AddWithValue("@AccountHolderName", updatedAccount.AccountHolderName);
						command.Parameters.AddWithValue("@Balance", updatedAccount.Balance);
						command.Parameters.AddWithValue("@UserId", updatedAccount.UserId);

						int rowsUpdated = command.ExecuteNonQuery();

						connection.Close();

						if (rowsUpdated > 0)
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

		public static bool DeleteAccount(int accountNumber)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = "DELETE FROM Accounts WHERE AccountNumber = @AccountNumber";
						command.Parameters.AddWithValue("@AccountNumber", accountNumber);

						int rowsDeleted = command.ExecuteNonQuery();

						connection.Close();

						if (rowsDeleted > 0)
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

		public static List<Account> GetAllAccounts()
		{
			List<Account> accounts = new List<Account>();

			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = "SELECT * FROM Accounts";

						using (SQLiteDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								Account account = new Account
								{
									AccountNumber = Convert.ToInt32(reader["AccountNumber"]),
									AccountHolderName = reader["AccountHolderName"].ToString(),
									Balance = Convert.ToDecimal(reader["Balance"]),
									UserId = Convert.ToInt32(reader["UserId"])
								};

								accounts.Add(account);
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

			return accounts;
		}
	}

}

