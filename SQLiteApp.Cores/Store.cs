using Newtonsoft.Json;
using System.Text.RegularExpressions;
namespace SQLiteApp.iOS
{
	public class Store
	{
		public string StoreID { get; set; }
		public string StoreName { get; set; }
		public string StoreNum { get; set; }
		public string SequenceNum { get; set; }
		public string ManagerName { get; set; }
		public string PhoneNum { get; set; }
		public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public int Zip { get; set; }
		public int TerritoryNum { get; set; }
		public long TotalOrders { get; set; }
		public double TotalExpenses { get; set; }

		[JsonConstructor]
		public Store(string storeID, string storeName, string storeNum, string sequenceNum, string managerName, string phoneNum,
			string address1, string address2, string city, string state, int zip, int territoryNum, long totalOrders, double totalExpenses)
		{
			StoreID = storeID;
			StoreName = storeName;
			StoreNum = storeNum;
			SequenceNum = sequenceNum;
			ManagerName = managerName;
			PhoneNum = phoneNum;
			Address1 = address1;
			Address2 = address2;
			City = city;
			State = state;
			Zip = zip;
			TerritoryNum = territoryNum;
			TotalOrders = totalOrders;
			TotalExpenses = totalExpenses;
		}

		public Store(string storeID, string storeName, string storeNum, string sequenceNum, string managerName, string phoneNum,
			string address1, string address2, string city, string state, int zip, int territoryNum)
		{
			StoreID = storeID;
			StoreName = storeName;
			StoreNum = storeNum;
			SequenceNum = sequenceNum;
			ManagerName = managerName;
			PhoneNum = phoneNum;
			Address1 = address1;
			Address2 = address2;
			City = city;
			State = state;
			Zip = zip;
			TerritoryNum = territoryNum;
		}

		public static bool ValidStoreID(string input)
		{
			return Regex.IsMatch(input, "^[a-zA-Z0-9]{1,10}$");
		}

		public static bool ValidStoreName(string input)
		{
			return Regex.IsMatch(input, "^.{1,50}$");
		}

		public static bool ValidTerritoryNum(string input)
		{
			return Regex.IsMatch(input, "^[0-9]{1,5}$");
		}

		public static bool ValidSequenceNum(string input)
		{
			return Regex.IsMatch(input, "^[0-9]{0,4}$");
		}

		public static bool ValidStoreNum(string input)
		{
			return Regex.IsMatch(input, "^.{0,5}$");
		}

		public static bool ValidAddress1(string input)
		{
			return Regex.IsMatch(input, "^.{1,40}$");
		}

		public static bool ValidAddress2(string input)
		{
			return Regex.IsMatch(input, "^.{0,40}$");
		}

		public static bool ValidCity(string input)
		{
			return Regex.IsMatch(input, "^.{1,20}$");
		}

		public static bool ValidState(string input)
		{
			return Regex.IsMatch(input, "^[A-Za-z]{2,2}$");
		}

		public static bool ValidZip(string input)
		{
			return Regex.IsMatch(input, "^[0-9]{5,9}$");
		}

		public static bool ValidManagerName(string input)
		{
			return Regex.IsMatch(input, "^.{1,50}$");
		}

		public static bool ValidPhoneNum(string input)
		{
			return Regex.IsMatch(input, "^[0-9 -]{10,14}$");
		}
	}
}