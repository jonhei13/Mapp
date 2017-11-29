using System;
using System.Collections.Generic;
using UIKit;
namespace MovieSearch.iOS.Controllers
{
    public class nameController : UITableViewController
    {
        private List<MovieDetails> _nameList;

        public nameController(List<MovieDetails> namelist)
        {
            this._nameList = namelist;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Movie List";



            this.TableView.Source = new NameListDataSource(_nameList, _onSelected);
        }
        public void _onSelected(MovieDetails mov)
        {
            this.NavigationController.PushViewController(new MovieDetailsController(mov), true);
            
           // var okAlertController = UIAlertController.Create("Selected", this._nameList[row], UIAlertControllerStyle.Alert);
           // okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
           // this.PresentViewController(okAlertController, true, null);

        }
    }

}
