
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SQLiteApp.iOS
{
	public partial class EditStoreViewController : StoreEntryViewController
	{
		public Store Store{get; set;}

		public EditStoreViewController (IntPtr handle) : base (handle)
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
			
			Title = "Edit store";

			_storeName.ShouldReturn = TextFieldShouldReturn;
			_storeID.ShouldReturn = TextFieldShouldReturn;
			_storeNum.ShouldReturn = TextFieldShouldReturn;
			_territoryNum.ShouldReturn = TextFieldShouldReturn;
			_sequenceNum.ShouldReturn = TextFieldShouldReturn;
			_managerName.ShouldReturn = TextFieldShouldReturn;
			_address1.ShouldReturn = TextFieldShouldReturn;
			_address2.ShouldReturn = TextFieldShouldReturn;
			_city.ShouldReturn = TextFieldShouldReturn;
			_state.ShouldReturn = TextFieldShouldReturn;
			_zip.ShouldReturn = TextFieldShouldReturn;
			_phoneNum.ShouldReturn = TextFieldShouldReturn;

			_updateStoreButton.TouchUpInside += (sender, e) => 
			{
				
			};
		}

		private bool TextFieldShouldReturn(UITextField textField)
		{
			textField.ResignFirstResponder();
			return true;
		}

		private Store AddStore()
		{
			if(!ValidateInputs())
				return null;
			
			int zip = int.Parse(_zip.Text);
			int territoryNum = int.Parse(_territoryNum.Text);
			
			string storeName = _storeName.Text;
			string storeID = _storeID.Text;
			string storeNum = _storeNum.Text;
			string sequenceNum = _sequenceNum.Text;
			string managerName = _managerName.Text;
			string address1 = _address1.Text;
			string address2 = _address2.Text;
			string city = _city.Text;
			string state = _state.Text;
			string phoneNum = _phoneNum.Text;

			Store store = new Store(storeID, storeName, storeNum, sequenceNum, managerName, 
				phoneNum, address1, address2, city, state, zip, territoryNum);

			_database.InsertStore(store);
			
			return store;
		}

		private bool ValidateInputs()
		{
			bool ret = true;

			ret &= Validate(_storeName, Store.ValidStoreName);
			ret &= Validate(_storeID, Store.ValidStoreID);
			ret &= Validate(_storeNum, Store.ValidStoreNum);
			ret &= Validate(_territoryNum, Store.ValidTerritoryNum);
			ret &= Validate(_sequenceNum, Store.ValidSequenceNum);
			ret &= Validate(_address1, Store.ValidAddress1);
			ret &= Validate(_address2, Store.ValidAddress2);
			ret &= Validate(_city, Store.ValidCity);
			ret &= Validate(_state, Store.ValidState);
			ret &= Validate(_zip, Store.ValidZip);
			ret &= Validate(_managerName, Store.ValidManagerName);
			ret &= Validate(_phoneNum, Store.ValidPhoneNum);

			return ret;
		}
	}
}

