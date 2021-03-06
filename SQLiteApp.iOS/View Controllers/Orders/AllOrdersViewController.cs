﻿
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SQLiteApp.iOS
{
	public partial class AllOrdersViewController : UIViewController
	{
		public string StoreID { get; set;}
		public string StoreName { get; set;}
		private Order _selectedOrder;

		public AllOrdersViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Database database = new Database ("Content/Stores.sqlite");

			Title = "Orders from " + StoreName;

			_orderSummaryTable.Source = new OrderSummarySource (database.GetOrdersForStore (StoreID), SelectOrder);
		}

		public void SelectOrder(Order order)
		{
			_selectedOrder = order;
			PerformSegue ("OrderDetailSegue", this);
		}

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
			if(segue.Identifier == "OrderDetailSegue")
			{
				var controller = segue.DestinationViewController as OrderDetailViewController;
				controller.Order = _selectedOrder;
				controller.StoreName = StoreName;
				return;
			}
		}
	}
}

