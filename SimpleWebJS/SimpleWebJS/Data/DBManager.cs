using OnlineBankPortal.Models;
using System.Data.SQLite;


namespace OnlineBankPortal.Data
{
	public class DBManager
	{
		private static readonly string connectionString = "Data Source=mydatabase.db;Version=3;";

		// Existing methods for CreateTable, Insert, Update, Delete, and other functionalities...

		public static void SeedData()
		{
			// Seed random user profiles
			SeedUserProfiles();

			// Seed random accounts
			SeedAccounts();

			// Seed random transactions
			SeedTransactions();
		}

		private static void SeedUserProfiles()
		{
			Random rand = new Random();
			string[] firstNames = { "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Hannah" };
			string[] lastNames = { "Smith", "Johnson", "Brown", "Lee", "Taylor", "Clark", "Lewis" };
			int accountNumberCounter = 1000;

			foreach (var firstName in firstNames)
			{
				foreach (var lastName in lastNames)
				{
					UserProfile userProfile = new UserProfile
					{
						Username = $"{firstName.ToLower()}.{lastName.ToLower()}",
						Email = $"{firstName.ToLower()}.{lastName.ToLower()}@example.com",
						FullName = $"{firstName} {lastName}",
						Address = "123 Main St",
						Phone = $"(123) {rand.Next(100, 999)}-{rand.Next(1000, 9999)}",
						ProfilePictureUrl = "images/person.png",
						PasswordHash = "hashed_password_here",// Replace with actual password hashing
						AccountNumber = accountNumberCounter++
					};

					UserProfileManager.CreateUserProfile(userProfile);
				}
			}
			UserProfile adminProfile = new UserProfile
			{
				Username = "admin",
				Email = "admin@example.com",
				FullName = "admin",
				Address = "123 Main St",
				Phone = "(123)-332-433",
				ProfilePictureUrl = "images/person.png",
				PasswordHash = "admin_password_here",// Replace with actual password hashing
				AccountNumber = 0			};

			UserProfileManager.CreateUserProfile(adminProfile);
		}

		private static string GetRandomProfileImagePath()
		{
			string[] imagePaths = new string[]
			{
				"ss.png",
				"ss1.png",
				"ss2.png"
			};

			Random rand = new Random();
			int randomIndex = rand.Next(0, imagePaths.Length);
			return imagePaths[randomIndex];
		}

		private static void SeedAccounts()
		{
			Random rand = new Random();
			List<UserProfile> userProfiles = UserProfileManager.GetAllUserProfiles();

			foreach (var userProfile in userProfiles)
			{
				//for (int i = 0; i < rand.Next(1, 4); i++)
				//{
				Account account = new Account
				{
					AccountNumber = userProfile.AccountNumber,
					AccountHolderName = userProfile.FullName,
					Balance = (decimal)(rand.Next(1000, 10000) + rand.NextDouble()),
					UserId = userProfile.UserId
				};

				AccountManager.CreateAccount(account);
				//}
			}
		}

		private static void SeedTransactions()
		{
			int transactionCounter = 1000;
			Random rand = new Random();
			List<Account> accounts = AccountManager.GetAllAccounts();

			foreach (var account in accounts)
			{
				//for (int i = 0; i < rand.Next(1, 6); i++)
				//{
				Transaction transaction = new Transaction
				{
					//TransactionId = transactionCounter++,
					AccountNumber = account.AccountNumber,
					TransactionType = rand.Next(2) == 0 ? "Deposit" : "Withdrawal",
					Amount = (decimal)(rand.Next(10, 5000) + rand.NextDouble()),
					Description = "default description"
					//TransactionDate = DateTime.Now.AddMinutes(-rand.Next(1, 10000))
				};

				//TransactionManager.CreateTransaction(transaction);

				// Update account balance
				if (transaction.TransactionType == "Deposit")
				{
					//decimal Amount = (decimal)(rand.Next(10, 5000) + rand.NextDouble());
					
					TransactionManager.Deposit(account.AccountNumber, transaction.Amount, ($"Debitted to account {account.AccountNumber}"));
					//account.Balance += transaction.Amount;
				}
				else
				{
					TransactionManager.Withdraw(account.AccountNumber, transaction.Amount, ($"Credited from account {account.AccountNumber}"));
				}

				AccountManager.UpdateAccount(account);
				//
			}
		}

		public static void CreateTables()
		{
			UserProfileManager.CreateTable();
			AccountManager.CreateTable();
			TransactionManager.CreateTable();
			SeedData();
		}

		public static void DropTables()
		{
			using (SQLiteConnection connection = new SQLiteConnection(connectionString))
			{
				connection.Open();
				using (SQLiteCommand command = connection.CreateCommand())
				{
					// Drop the tables if they exist
					command.CommandText = "DROP TABLE IF EXISTS Accounts";
					command.ExecuteNonQuery();

					command.CommandText = "DROP TABLE IF EXISTS Transactions";
					command.ExecuteNonQuery();

					command.CommandText = "DROP TABLE IF EXISTS UserProfiles";
					command.ExecuteNonQuery();
				}
				connection.Close();
			}
		}

	}

}
