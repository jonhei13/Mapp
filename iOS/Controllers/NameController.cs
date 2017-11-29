using System;
using System.Collections.Generic;
using UIKit;
namespace MovieSearch.iOS.Controllers
{
    public class nameController : UITableViewController
    {
        private List<MovieDetails> _nameList;
        private Action<int> _onSelected;

        public nameController(List<MovieDetails> namelist /*Action<int> onSelectedPerson*/)
        {
            this._nameList = namelist;
            //this._onSelected = onSelectedPerson; 
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Movie List";



            this.TableView.Source = new NameListDataSource(_nameList);
        }
        public void _onSelect(int row)
        {
           // var okAlertController = UIAlertController.Create("Selected", this._nameList[row], UIAlertControllerStyle.Alert);
           // okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
           // this.PresentViewController(okAlertController, true, null);

        }
    }

}
