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

		public List<Store> GetAllStores()
		{
			SqliteCommand command = new SqliteCommand("SELECT * FROM Stores");
			return GetStoresFromCommand(command);
		}

		public List<Store> SearchStores(string searchTerm)
		{
			SqliteCommand command = new SqliteCommand("SELECT * FROM Stores WHERE StoreName LIKE @SearchTerm");
			command.Parameters.Add(new SqliteParameter("@SearchTerm", "%" + searchTerm + "%"));
			return GetStoresFromCommand(command);
		}
		
		private List<Store> GetStoresFromCommand(SqliteCommand command)
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
					
					ret.Add(store);
				}
			}
			return ret;
		}

		public bool InsertStore(Store store)
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

				try {
					command.ExecuteNonQuery ();
				} catch (Exception) {
					return false;
				}
			}
			return true;
		}

		public void GetOrderOverview(Store store)
		{
			string query = @"SELECT Count(OrderID), Sum(TotalCost) FROM Orders WHERE StoreID = @StoreID";

			using (SqliteConnection connection = new SqliteConnection("Data Source=" + _dbPath + ";Version=3"))
			{
				connection.Open();
				SqliteCommand command = new SqliteCommand(query, connection);

				command.Parameters.Add(new SqliteParameter("@StoreID", store.StoreID));

				SqliteDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					try
					{
						store.TotalExpenses = (double)reader["Sum(TotalCost)"];
					}
					catch (Exception)
					{
					}
					
					store.TotalOrders = (long)reader["Count(OrderID)"];
				}
			}
		}

		public void UpdateStore(Store store)
		{
			string query = @"UPDATE Stores SET TerritoryNum = @TerritoryNum, SequenceNum = @SequenceNum, StoreName = @StoreName, 
							Address1 = @Address1, Address2 = @Address2, City = @City, State = @State, Zip = @Zip,
							StoreNum = @StoreNum, ManagerName = @ManagerName, PhoneNum = @Phonenum WHERE StoreID = @StoreID";

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

		public void DeleteStore(string storeID)
		{
			string query = @"DELETE FROM Stores WHERE StoreID = @StoreID";
			using (SqliteConnection connection = new SqliteConnection("Data Source=" + _dbPath + ";Version=3"))
			{
				connection.Open();
				SqliteCommand command = new SqliteCommand(query, connection);

				command.Parameters.Add(new SqliteParameter("@StoreID", storeID));

				command.ExecuteNonQuery();
			}
		}

		public void InsertOrder(Order order)
		{
			string query = @"INSERT INTO Orders (OrderID, StoreID, TotalItems, TotalCost, ContactName, RushOrder, 
							Date) VALUES (@OrderID, @StoreID, @TotalItems, @TotalCost, @ContactName, @RushOrder, @Date)";

			using (SqliteConnection connection = new SqliteConnection("Data Source=" + _dbPath + ";Version=3"))
			{
				connection.Open();
				SqliteCommand command = new SqliteCommand(query, connection);

				command.Parameters.Add(new SqliteParameter("@OrderID", order.OrderID));
				command.Parameters.Add(new SqliteParameter("@StoreID", order.StoreID));
				command.Parameters.Add(new SqliteParameter("@TotalItems", order.TotalItems));
				command.Parameters.Add(new SqliteParameter("@TotalCost", order.TotalCost));
				command.Parameters.Add(new SqliteParameter("@ContactName", order.ContactName));
				command.Parameters.Add(new SqliteParameter("@RushOrder", order.RushOrder));
				command.Parameters.Add(new SqliteParameter("@Date", order.Date));

				command.ExecuteNonQuery();
			}
		}

		public void UpdateOrder(Order order)
		{
			string query = @"UPDATE Orders SET TotalItems = @TotalItems, TotalCost = @TotalCost, 
							ContactName = @ContactName, RushOrder = @RushOrder, Date = @Date WHERE OrderID = @OrderID";
			using (SqliteConnection connection = new SqliteConnection("Data Source=" + _dbPath + ";Version=3"))
			{
				connection.Open();
				SqliteCommand command = new SqliteCommand(query, connection);

				command.Parameters.Add(new SqliteParameter("@TotalItems", order.TotalItems));
				command.Parameters.Add(new SqliteParameter("@TotalCost", order.TotalCost));
				command.Parameters.Add(new SqliteParameter("@ContactName", order.ContactName));
				command.Parameters.Add(new SqliteParameter("@RushOrder", order.RushOrder));
				command.Parameters.Add(new SqliteParameter("@Date", order.Date));
				command.Parameters.Add(new SqliteParameter("@OrderID", order.OrderID));

				command.ExecuteNonQuery();
			}
		}

		public void DeleteOrder(string orderID)
		{
			string query = @"DELETE FROM Orders WHERE OrderID = @OrderID";
			using (SqliteConnection connection = new SqliteConnection("Data Source=" + _dbPath + ";Version=3"))
			{
				connection.Open();
				SqliteCommand command = new SqliteCommand(query, connection);

				command.Parameters.Add(new SqliteParameter("@OrderID", orderID));

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

		public bool CheckUniqueOrderID(string orderID)
		{
			string query = "SELECT EXISTS(SELECT * FROM Orders WHERE OrderID = @OrderID)";
			
			using (SqliteConnection connection = new SqliteConnection("Data Source=" + _dbPath + ";Version=3"))
			{
				connection.Open();
				SqliteCommand command = new SqliteCommand(query, connection);
				command.Parameters.Add(new SqliteParameter("@OrderID", orderID));

				SqliteDataReader reader = command.ExecuteReader();
				reader.Read();
				try
				{
					return reader.GetInt16(0) == 0;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					throw;
				}
			}
		}
	}
}