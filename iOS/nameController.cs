using System;
using System.Collections.Generic;
using UIKit;
namespace MovieSearch.iOS
{
    public class nameController : UITableViewController
    {
        private List<string> _nameList;
        public nameController(List<string> namelist)
        {
            this._nameList = namelist;
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.Title = "Movie List";



            this.TableView.Source = new NameListDataSource(_nameList);
        }
    }

}
