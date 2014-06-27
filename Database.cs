using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace SQLiteApp
{
	public class Database
	{
		private string _dbPath;

		public Database(string dbPath)
		{
			_dbPath = dbPath;
		}

		public Store[] GetAllStores()
		{
			SqliteCommand command = new SqliteCommand("SELECT S.*, Count(O.OrderID), Sum(O.TotalCost) FROM Stores AS S, Orders AS O WHERE S.StoreID = O.StoreID GROUP BY S.StoreID");
			return GetStoresFromCommand(command);
		}

		public Store[] SearchStores(string searchTerm)
		{
			SqliteCommand command = new SqliteCommand("SELECT S.*, Count(O.OrderID), Sum(O.TotalCost) FROM Stores AS S LEFT JOIN Orders AS O ON S.StoreID = O.StoreID WHERE S.StoreName LIKE @SearchTerm GROUP BY S.StoreID");
			command.Parameters.Add(new SqliteParameter("@SearchTerm", "%" + searchTerm + "%"));
			return GetStoresFromCommand(command);
		}

		private Store[] GetStoresFromCommand(SqliteCommand command)
		{
			List<Store> ret = new List<Store>();
			
			using (SqliteConnection connection = new SqliteConnection("Data Source=" + _dbPath + ";Version=3"))
			{
				connection.Open();
				command.Connection = connection;

				SqliteDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					Store store = new Store((string)reader["StoreID"], (string)reader["StoreName"], (string)reader["StoreNum"], (string)reader["SequenceNum"],
								(string)reader["ManagerName"], (string)reader["PhoneNum"], (string)reader["Address1"], (string)reader["Address2"],
								(string)reader["City"], (string)reader["State"], (int)reader["Zip"], (int)reader["TerritoryNum"]);
					
					
					try 
					{	        
						store.TotalExpenses = (double)reader["Sum(O.TotalCost)"];
					}
					catch (Exception)
					{
					}
					try 
					{	        
						store.TotalOrders = (long)reader["Count(O.OrderID)"];
					}
					catch (Exception)
					{
					}

					ret.Add(store);
				}
			}
			return ret.ToArray();
		}

		public void InsertStore(Store store)
		{
			string query = @"INSERT INTO Stores (TerritoryNum, SequenceNum, StoreName, StoreID, Address1, 
							Address2, City, State, Zip, StoreNum, ManagerName, PhoneNum) VALUES (@TerritoryNum,
							@SequenceNum, @StoreName, @StoreID, @Address1, @Address2, @City, @State, @Zip, @StoreNum,
							@ManagerName, @PhoneNum)";

			using (SqliteConnection connection = new SqliteConnection("Data Source=" + _dbPath + ";Version=3"))
			{
				connection.Open();
				SqliteCommand command = new SqliteCommand(query, connection);
				
				command.Parameters.Add(new SqliteParameter("@TerritoryNum", store.TerritoryNum));
				command.Parameters.Add(new SqliteParameter("@SequenceNum", store.SequenceNum));
				command.Parameters.Add(new SqliteParameter("@StoreName", store.StoreName));
				command.Parameters.Add(new SqliteParameter("@StoreID", store.StoreID));
				command.Parameters.Add(new SqliteParameter("@Address1", store.Address1));
				command.Parameters.Add(new SqliteParameter("@Address2", store.Address2));
				command.Parameters.Add(new SqliteParameter("@City", store.City));
				command.Parameters.Add(new SqliteParameter("@State", store.State));
				command.Parameters.Add(new SqliteParameter("@Zip", store.Zip));
				command.Parameters.Add(new SqliteParameter("@StoreNum", store.StoreNum));
				command.Parameters.Add(new SqliteParameter("@ManagerName", store.ManagerName));
				command.Parameters.Add(new SqliteParameter("@PhoneNum", store.PhoneNum));

				command.ExecuteNonQuery();
			}
		}

		public Order[] GetOrdersForStore(string storeID)
		{
			List<Order> ret = new List<Order>();
			string query = "SELECT * FROM Orders WHERE StoreID = @StoreID";

			using (SqliteConnection connection = new SqliteConnection("Data Source=" + _dbPath + ";Version=3"))
			{
				connection.Open();
				SqliteCommand command = new SqliteCommand(query, connection);
				command.Parameters.Add(new SqliteParameter("@StoreID", storeID));

				SqliteDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					try
					{
						ret.Add(new Order((string)reader["StoreID"], (string)reader["OrderID"], (string)reader["Date"], (int)reader["TotalItems"],
										(double)reader["TotalCost"], (string)reader["ContactName"], reader["RushOrder"].ToString().Equals("True")));
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
						throw;
					}
				}
			}
			return ret.ToArray();
		}
	}
}