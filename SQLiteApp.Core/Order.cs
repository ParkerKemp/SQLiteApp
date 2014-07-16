using Mono.Data.Sqlite;
using System;
using System.Text.RegularExpressions;
namespace SQLiteApp.iOS
{
	public class Order
	{
		public string StoreID { get; set; }
		public string OrderID { get; set; }
		public string Date { get; set; }
		public int TotalItems { get; set; }
		public double TotalCost { get; set; }
		public string ContactName { get; set; }
		public bool RushOrder { get; set; }

		public Order(string storeID, string orderID, string date, int totalItems, double totalCost, string contactName, bool rushOrder)
		{
			StoreID = storeID;
			OrderID = orderID;
			Date = date;
			TotalItems = totalItems;
			TotalCost = totalCost;
			ContactName = contactName;
			RushOrder = rushOrder;
		}

		public static string RandomID(Database database)
		{
			string id;
			Random random = new Random();
			do
			{
				id = "";
				for (int i = 0; i < 32; i++)
					id += random.Next(0, 15).ToString("X");
			} while (!database.CheckUniqueOrderID(id));

			return id;
		}

		public static bool ValidCost(string input)
		{
			return Regex.IsMatch(input, @"^[0-9]+(\.[0-9]{1,2})?$");
		}

		public static bool ValidTotalItems(string input)
		{
			return Regex.IsMatch(input, @"^[0-9]+$");
		}

		public static bool ValidRushOrder(string input)
		{
			return Regex.IsMatch(input, @"^[YNyn]$");
		}

		public static bool ValidDate(string input)
		{
			return Regex.IsMatch(input, @"^.+$");
		}

		public static bool ValidContactName(string input)
		{
			return Regex.IsMatch(input, @"^.+$");
		}
	}
}