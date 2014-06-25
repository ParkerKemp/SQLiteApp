namespace SQLiteApp
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
	}
}