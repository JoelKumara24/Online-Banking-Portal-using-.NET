namespace OnlineBankPortal.Data
{
	using System;
	using System.Collections.Generic;
	using System.Data.SQLite;
    using OnlineBankPortal.Models;
	using System.Linq;

	public class UserProfileManager
	{
		private static readonly string connectionString = "Data Source=mydatabase.db;Version=3;";

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
                        CREATE TABLE IF NOT EXISTS UserProfiles (
                            UserId INTEGER PRIMARY KEY,
                            Username TEXT NOT NULL UNIQUE,
                            Email TEXT NOT NULL UNIQUE,
                            FullName TEXT NOT NULL,
                            Address TEXT,
                            Phone TEXT,
                            ProfilePictureUrl TEXT,
                            PasswordHash TEXT NOT NULL,
                            AccountNumber INTEGER NOT NULL UNIQUE
                        )";
						command.ExecuteNonQuery();
					}

					connection.Close();
				}

				Console.WriteLine("UserProfiles table created successfully.");
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
				return false;
			}
		}

		public static bool CreateUserProfile(UserProfile userProfile)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = @"
                        INSERT INTO UserProfiles (Username, Email, FullName, Address, Phone, ProfilePictureUrl, PasswordHash, AccountNumber)
                        VALUES (@Username, @Email, @FullName, @Address, @Phone, @ProfilePictureUrl, @PasswordHash, @AccountNumber)";

						command.Parameters.AddWithValue("@Username", userProfile.Username);
						command.Parameters.AddWithValue("@Email", userProfile.Email);
						command.Parameters.AddWithValue("@FullName", userProfile.FullName);
						command.Parameters.AddWithValue("@Address", userProfile.Address ?? "");
						command.Parameters.AddWithValue("@Phone", userProfile.Phone ?? "");
						command.Parameters.AddWithValue("@ProfilePictureUrl", userProfile.ProfilePictureUrl ?? "");
						command.Parameters.AddWithValue("@PasswordHash", userProfile.PasswordHash);
						command.Parameters.AddWithValue("@AccountNumber", userProfile.AccountNumber);

						command.ExecuteNonQuery();
					}

					connection.Close();
				}

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
				return false;
			}
		}

		public static UserProfile GetUserProfileByUsername(string username)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = "SELECT * FROM UserProfiles WHERE Username = @Username";
						command.Parameters.AddWithValue("@Username", username);

						using (SQLiteDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								return MapUserProfile(reader);
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

		public static UserProfile GetUserProfileByEmail(string email)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = "SELECT * FROM UserProfiles WHERE Email = @Email";
						command.Parameters.AddWithValue("@Email", email);

						using (SQLiteDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								return MapUserProfile(reader);
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

		public static bool UpdateUserProfile(UserProfile userProfile)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = @"
                        UPDATE UserProfiles
                        SET Username = @Username, Email = @Email, FullName = @FullName,
                            Address = @Address, Phone = @Phone,
                            ProfilePictureUrl = @ProfilePictureUrl, PasswordHash = @PasswordHash, AccountNumber = @AccountNumber
                        WHERE UserId = @UserId";

						command.Parameters.AddWithValue("@Username", userProfile.Username);
						command.Parameters.AddWithValue("@Email", userProfile.Email);
						command.Parameters.AddWithValue("@FullName", userProfile.FullName);
						command.Parameters.AddWithValue("@Address", userProfile.Address ?? "");
						command.Parameters.AddWithValue("@Phone", userProfile.Phone ?? "");
						command.Parameters.AddWithValue("@ProfilePictureUrl", userProfile.ProfilePictureUrl ?? "");
						command.Parameters.AddWithValue("@PasswordHash", userProfile.PasswordHash);
						command.Parameters.AddWithValue("@UserId", userProfile.UserId);
						command.Parameters.AddWithValue("@AccountNumber", userProfile.AccountNumber);


						int rowsUpdated = command.ExecuteNonQuery();

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

		public static bool DeleteUserProfile(int userId)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = "DELETE FROM UserProfiles WHERE UserId = @UserId";
						command.Parameters.AddWithValue("@UserId", userId);

						int rowsDeleted = command.ExecuteNonQuery();

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

		private static UserProfile MapUserProfile(SQLiteDataReader reader)
		{
			return new UserProfile
			{
				UserId = Convert.ToInt32(reader["UserId"]),
				Username = reader["Username"].ToString(),
				Email = reader["Email"].ToString(),
				FullName = reader["FullName"].ToString(),
				Address = reader["Address"].ToString(),
				Phone = reader["Phone"].ToString(),
				ProfilePictureUrl = reader["ProfilePictureUrl"].ToString(),
				PasswordHash = reader["PasswordHash"].ToString(),
				AccountNumber = Convert.ToInt32(reader["AccountNumber"])
			};
		}

		public static List<UserProfile> GetAllUserProfiles()
		{
			List<UserProfile> userProfiles = new List<UserProfile>();

			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = "SELECT * FROM UserProfiles";

						using (SQLiteDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								UserProfile userProfile = new UserProfile
								{
									UserId = Convert.ToInt32(reader["UserId"]),
									Username = reader["Username"].ToString(),
									Email = reader["Email"].ToString(),
									FullName = reader["FullName"].ToString(),
									Address = reader["Address"].ToString(),
									Phone = reader["Phone"].ToString(),
									ProfilePictureUrl = reader["ProfilePictureUrl"].ToString(),
									PasswordHash = reader["PasswordHash"].ToString(),
									AccountNumber = Convert.ToInt32(reader["AccountNumber"])
								};

								userProfiles.Add(userProfile);
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

			return userProfiles;
		}

		public static UserProfile GetUserProfileById(int userId)
		{
			try
			{
				using (SQLiteConnection connection = new SQLiteConnection(connectionString))
				{
					connection.Open();

					using (SQLiteCommand command = connection.CreateCommand())
					{
						command.CommandText = "SELECT * FROM UserProfiles WHERE UserId = @UserId";
						command.Parameters.AddWithValue("@UserId", userId);

						using (SQLiteDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								return MapUserProfile(reader);
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

	}

}


