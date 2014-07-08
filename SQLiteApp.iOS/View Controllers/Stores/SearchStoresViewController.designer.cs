// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace SQLiteApp.iOS
{
	[Register ("SearchStoresViewController")]
	partial class SearchStoresViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITableView _searchStoresTable { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UISearchDisplayController searchDisplayController { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (_searchStoresTable != null) {
				_searchStoresTable.Dispose ();
				_searchStoresTable = null;
			}
			if (searchDisplayController != null) {
				searchDisplayController.Dispose ();
				searchDisplayController = null;
			}
		}
	}
}
