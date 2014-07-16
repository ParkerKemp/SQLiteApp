
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SQLiteApp.iOS
{
	public partial class StoreDetailsViewController : UIViewController
	{
		public Store Store{get; set;}

		public StoreDetailsViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Database database = new Database("Content/Stores.sqlite");

			database.GetOrderOverview(Store);

			Title = Store.StoreName;

			_storeID.Text = Store.StoreID;
			_territoryNum.Text = Store.TerritoryNum.ToString();
			_sequenceNum.Text = Store.SequenceNum;
			_address.Text = Store.Address1 + "\n" + Store.Address2 + "\n" + Store.City + ", " + Store.State + " " + Store.Zip;
			_storeNum.Text = Store.StoreNum;
			_managerName.Text = Store.ManagerName;
			_phoneNum.Text = Store.PhoneNum;
			_orderDetailsButton.SetTitle(Store.TotalOrders.ToString (), UIControlState.Normal);

			_orderDetailsButton.TouchUpInside += (sender, e) => {
				PerformSegue("AllOrdersSegue", this);
			};
//			_totalOrders.Text = Store.TotalOrders.ToString();
			_totalExpenses.Text = "$" + string.Format("{0:0.00}", Store.TotalExpenses);

			//UINavigationBar navBar = NavigationController.NavigationBar;
			//UIToolbar toolbar = new UIToolbar();
			//toolbar.

			UIBarButtonItem[] buttons = new UIBarButtonItem[1];
			UIBarButtonItem button = new UIBarButtonItem (UIImage.FromFile ("Edit.png").Scale(new SizeF(40, 40)), UIBarButtonItemStyle.Plain, (sender, e) => {Console.WriteLine("Pressed.");});// UIControlState.Normal, UIBarMetrics.Default);
			buttons[0] = new UIBarButtonItem ("Edit", UIBarButtonItemStyle.Plain, (sender, e) => 
			{
				PerformSegue("EditStoreSegue", this);
			});
			buttons[0] = new UIBarButtonItem ("Delete", UIBarButtonItemStyle.Plain, (sender, e) => {Console.WriteLine("Pressed.");});// UIControlState.Normal, UIBarMetrics.Default);
			NavigationItem.SetRightBarButtonItems(buttons, true);

			var deleteButton = new UIBarButtonItem(UIBarButtonSystemItem.Trash, (sender, e) => 
			{

			});

			var editButton = new UIBarButtonItem(UIBarButtonSystemItem.Edit, (sender, e) => 
			{

			});

			NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { deleteButton, editButton };
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			if (segue.Identifier == "AllOrdersSegue") {
				var controller = segue.DestinationViewController as AllOrdersViewController;
				controller.StoreID = Store.StoreID;
				controller.StoreName = Store.StoreName;
				return;
			}
			if(segue.Identifier == "EditStoreSegue"){
				var controller = segue.DestinationViewController as EditStoreViewController;
				controller.Store = Store;
				return;
			}
		}
	}
}

